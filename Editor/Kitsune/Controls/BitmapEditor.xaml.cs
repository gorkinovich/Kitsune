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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            ARGB, RGB, Indexed
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

        public int SurfaceWidth { get; set; }

        public int SurfaceHeight { get; set; }

        public Format SurfaceFormat { get; set; }

        public Color GridColor { get; set; }

        public int GridSeparation { get; set; }

        public int GridOffsetX { get; set; }

        public int GridOffsetY { get; set; }

        public int GridCellSizeX { get; set; }

        public int GridCellSizeY { get; set; }

        //------------------------------------------------------------------------
        // Methods
        //------------------------------------------------------------------------

        /// <summary>
        /// Makes a new object of the class.
        /// </summary>
        public BitmapEditor () {
            InitializeComponent();
        }

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

        public void UpdateBitmaps () {
            //...
        }

        public void ShowGrid (bool value) {
            if (value) {
                BitmapGrid.Visibility = Visibility.Visible;
                //...
            } else {
                BitmapGrid.Visibility = Visibility.Hidden;
                //...
            }
        }

        //------------------------------------------------------------------------
        // Callbacks
        //------------------------------------------------------------------------

        /// <summary>
        /// Callback for the mouse down event for the <c>Grid</c>.
        /// </summary>
        /// <param name="sender">The object where the event handler is attached.</param>
        /// <param name="e">The event data.</param>
        private void Grid_MouseDown (object sender, MouseButtonEventArgs e) {
            if (Editable) {
                //...
            }
        }

        /// <summary>
        /// Callback for the mouse move event for the <c>Grid</c>.
        /// </summary>
        /// <param name="sender">The object where the event handler is attached.</param>
        /// <param name="e">The event data.</param>
        private void Grid_MouseMove (object sender, MouseEventArgs e) {
            if (Editable) {
                //...
            }
        }

        /// <summary>
        /// Callback for the mouse wheel event for the <c>Grid</c>.
        /// </summary>
        /// <param name="sender">The object where the event handler is attached.</param>
        /// <param name="e">The event data.</param>
        private void Grid_MouseWheel (object sender, MouseWheelEventArgs e) {
            if (Editable) {
                //...
            }
        }
    }
}
