//================================================================================
// Copyright(c) 2023 Gorka Suárez García
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
//================================================================================

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Kitsune.Controls {
    /// <summary>
    /// Interaction logic for BitmapEditor.xaml
    /// </summary>
    public partial class BitmapEditor : UserControl {
        //------------------------------------------------------------------------
        // Internal Types
        //------------------------------------------------------------------------

        /// <summary>
        /// This enumeration represents the supported pixel formats
        /// of the bitmap editor.
        /// </summary>
        public enum Format {
            ARGB, RGB, Indexed, Unknown
        }

        //------------------------------------------------------------------------
        // Fields & Properties
        //------------------------------------------------------------------------

        /// <summary>
        /// The main source bitmap of the editor.
        /// </summary>
        private WriteableBitmap databmp;

        /// <summary>
        /// The grid source bitmap of the editor.
        /// </summary>
        private WriteableBitmap gridbmp;

        /// <summary>
        /// The foreground pixel color.
        /// </summary>
        private byte[] foregroundColor;

        /// <summary>
        /// The background pixel color.
        /// </summary>
        private byte[] backgroundColor;

        /// <summary>
        /// Gets or sets if the bitmap is editable by the user or not.
        /// </summary>
        public bool Editable { get; set; }

        /// <summary>
        /// Gets or sets the foreground pixel color.
        /// </summary>
        public Color ForegroundColor {
            get => ImageTools.BytesToColor(foregroundColor);
            set => foregroundColor = ImageTools.ColorToBytes(value);
        }

        /// <summary>
        /// Gets or sets the background pixel color.
        /// </summary>
        public Color BackgroundColor {
            get => ImageTools.BytesToColor(backgroundColor);
            set => backgroundColor = ImageTools.ColorToBytes(value);
        }

        /// <summary>
        /// Gets or sets the foreground index color.
        /// </summary>
        public byte ForegroundIndex {
            get => foregroundColor?[0] ?? 0;
            set => foregroundColor = new byte[] { value };
        }

        /// <summary>
        /// Gets or sets the background index color.
        /// </summary>
        public byte BackgroundIndex {
            get => backgroundColor?[0] ?? 0;
            set => backgroundColor = new byte[] { value };
        }

        /// <summary>
        /// Gets or sets the visibility of the bitmap grid.
        /// </summary>
        public bool EnableGrid {
            get => BitmapGrid.IsVisible;
            set => ShowGrid(value);
        }

        //------------------------------------------------------------------------
        // Fields & Properties (Configuration)
        //------------------------------------------------------------------------

        /// <summary>
        /// The main source bitmap template of the editor.
        /// </summary>
        private BitmapTemplate surfaceTemplate = new BitmapTemplate();

        /// <summary>
        /// The grid source bitmap template of the editor.
        /// </summary>
        private BitmapGridTemplate gridTemplate = new BitmapGridTemplate();

        /// <summary>
        /// Gets or sets the width of the main source bitmap.
        /// </summary>
        public int SurfaceWidth {
            get => surfaceTemplate.Width;
            set => surfaceTemplate.Width = value;
        }

        /// <summary>
        /// Gets or sets the height of the main source bitmap.
        /// </summary>
        public int SurfaceHeight {
            get => surfaceTemplate.Height;
            set => surfaceTemplate.Height = value;
        }

        /// <summary>
        /// Gets or sets the format of the main source bitmap.
        /// </summary>
        public Format SurfaceFormat {
            get => surfaceTemplate.Format.BitsPerPixel switch {
                32 => Format.ARGB,
                24 => Format.RGB,
                8 => Format.Indexed,
                _ => Format.Unknown
            };
            set => surfaceTemplate.Format = value switch {
                Format.Indexed => PixelFormats.Indexed8,
                Format.RGB => PixelFormats.Bgr24,
                _ => PixelFormats.Bgra32
            };
        }

        /// <summary>
        /// Gets or sets the palette of the main source bitmap.
        /// </summary>
        public BitmapPalette SurfacePalette {
            get => surfaceTemplate.Palette;
            set => surfaceTemplate.Palette = value;
        }

        /// <summary>
        /// Gets or sets the color of the grid source bitmap.
        /// </summary>
        public Color GridColor {
            get => gridTemplate.Color;
            set => gridTemplate.Color = value;
        }

        /// <summary>
        /// Gets or sets the separation of the grid source bitmap.
        /// </summary>
        public int GridSeparation {
            get => gridTemplate.Separation;
            set => gridTemplate.Separation = value;
        }

        /// <summary>
        /// Gets or sets the x-offset of the grid source bitmap.
        /// </summary>
        public int GridOffsetX {
            get => gridTemplate.OffsetX;
            set => gridTemplate.OffsetX = value;
        }

        /// <summary>
        /// Gets or sets the y-offset of the grid source bitmap.
        /// </summary>
        public int GridOffsetY {
            get => gridTemplate.OffsetY;
            set => gridTemplate.OffsetY = value;
        }

        /// <summary>
        /// Gets or sets the x-cell size of the grid source bitmap.
        /// </summary>
        public int GridCellSizeX {
            get => gridTemplate.CellSizeX;
            set => gridTemplate.CellSizeX = value;
        }

        /// <summary>
        /// Gets or sets the y-cell size of the grid source bitmap.
        /// </summary>
        public int GridCellSizeY {
            get => gridTemplate.CellSizeY;
            set => gridTemplate.CellSizeY = value;
        }

        //------------------------------------------------------------------------
        // Constructors
        //------------------------------------------------------------------------

        /// <summary>
        /// Makes a new object of the class.
        /// </summary>
        public BitmapEditor () {
            InitializeComponent();
        }

        //------------------------------------------------------------------------
        // Methods
        //------------------------------------------------------------------------

        /// <summary>
        /// Sets the bitmap scaling mode of the bitmap data.
        /// </summary>
        /// <param name="mode">The scaling mode value.</param>
        public void SetBitmapScalingMode (BitmapScalingMode mode) {
            RenderOptions.SetBitmapScalingMode(BitmapData, mode);
        }

        /// <summary>
        /// Sets the bitmap scaling mode of the bitmap grid.
        /// </summary>
        /// <param name="mode">The scaling mode value.</param>
        public void SetGridBitmapScalingMode (BitmapScalingMode mode) {
            RenderOptions.SetBitmapScalingMode(BitmapGrid, mode);
        }

        /// <summary>
        /// Generates the source bitmaps of the editor.
        /// </summary>
        public void GenerateBitmaps () {
            GenerateSurfaceBitmap();
            GenerateGridBitmap();
        }

        /// <summary>
        /// Generates the main source bitmap of the editor.
        /// </summary>
        public void GenerateSurfaceBitmap () {
            if (surfaceTemplate != null) {
                databmp = ImageFactory.CreateFrom(surfaceTemplate);
                databmp.ClearPixels();
                BitmapData.Source = databmp;
            }
        }

        /// <summary>
        /// Generates the grid source bitmap of the editor.
        /// </summary>
        public void GenerateGridBitmap () {
            if (gridTemplate != null) {
                gridTemplate.Source = surfaceTemplate;
                gridbmp = ImageFactory.CreateFrom(gridTemplate);
                BitmapGrid.Source = gridbmp;
            }
        }

        /// <summary>
        /// Shows or hides the grid source bitmap of the editor.
        /// </summary>
        /// <param name="value">The visibility flag.</param>
        public void ShowGrid (bool value) {
            if (value) {
                GenerateGridBitmap();
                BitmapGrid.Visibility = Visibility.Visible;
            } else {
                BitmapGrid.Visibility = Visibility.Hidden;
                gridbmp = null;
                BitmapGrid.Source = gridbmp;
            }
        }

        //------------------------------------------------------------------------
        // Callbacks
        //------------------------------------------------------------------------

        /// <summary>
        /// Writes a pixel in the main source bitmap of the editor.
        /// </summary>
        /// <param name="e">The mouse event data.</param>
        private void WritePixelWithMouse (MouseEventArgs e) {
            if (Editable && databmp != null) {
                var position = BitmapData.GetSourceCoordinates(e);
                if (e.LeftButton == MouseButtonState.Pressed) {
                    databmp.WritePixel(position, foregroundColor);
                } else if (e.RightButton == MouseButtonState.Pressed) {
                    databmp.WritePixel(position, backgroundColor);
                }
            }
        }

        /// <summary>
        /// Callback for the mouse down event for the <c>Grid</c>.
        /// </summary>
        /// <param name="sender">The object where the event handler is attached.</param>
        /// <param name="e">The event data.</param>
        private void Grid_MouseDown (object sender, MouseButtonEventArgs e) {
            WritePixelWithMouse(e);
        }

        /// <summary>
        /// Callback for the mouse move event for the <c>Grid</c>.
        /// </summary>
        /// <param name="sender">The object where the event handler is attached.</param>
        /// <param name="e">The event data.</param>
        private void Grid_MouseMove (object sender, MouseEventArgs e) {
            WritePixelWithMouse(e);
        }

        /// <summary>
        /// Callback for the mouse wheel event for the <c>Grid</c>.
        /// </summary>
        /// <param name="sender">The object where the event handler is attached.</param>
        /// <param name="e">The event data.</param>
        private void Grid_MouseWheel (object sender, MouseWheelEventArgs e) {
        }
    }
}
