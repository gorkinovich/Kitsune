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
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Kitsune.Controls {
    //============================================================================
    // ImageFactory
    //============================================================================

    /// <summary>
    /// This static type represents a factory for bitmap images.
    /// </summary>
    public static class ImageFactory {
        //------------------------------------------------------------------------
        // Constants, Fields & Properties
        //------------------------------------------------------------------------

        /// <summary>
        /// The default DPI for a source image.
        /// </summary>
        public const int DefaultDPI = 96;

        //------------------------------------------------------------------------
        // Methods (General Bitmaps)
        //------------------------------------------------------------------------

        /// <summary>
        /// Creates a RGBA (32 bits) writeable bitmap object to work with it.
        /// </summary>
        /// <param name="width">The width of the image.</param>
        /// <param name="height">The height of the image.</param>
        /// <param name="dpi">The dots per inch of the image.</param>
        /// <returns>The new instance created of the image.</returns>
        public static WriteableBitmap CreateARGB (int width, int height, int dpi = DefaultDPI) {
            return new WriteableBitmap(width, height, dpi, dpi, PixelFormats.Bgra32, null);
        }

        /// <summary>
        /// Creates a RGB (24 bits) writeable bitmap object to work with it.
        /// </summary>
        /// <param name="width">The width of the image.</param>
        /// <param name="height">The height of the image.</param>
        /// <param name="dpi">The dots per inch of the image.</param>
        /// <returns>The new instance created of the image.</returns>
        public static WriteableBitmap CreateRGB (int width, int height, int dpi = DefaultDPI) {
            return new WriteableBitmap(width, height, dpi, dpi, PixelFormats.Bgr24, null);
        }

        /// <summary>
        /// Creates 256 colors (8 bits) a writeable bitmap object to work with it.
        /// </summary>
        /// <param name="width">The width of the image.</param>
        /// <param name="height">The height of the image.</param>
        /// <param name="palette">The palette of the image.</param>
        /// <param name="dpi">The dots per inch of the image.</param>
        /// <returns>The new instance created of the image.</returns>
        public static WriteableBitmap CreateIndexed8 (int width, int height,
            BitmapPalette palette, int dpi = DefaultDPI) {
            return new WriteableBitmap(width, height, dpi, dpi, PixelFormats.Indexed8, palette);
        }

        /// <summary>
        /// Creates 16 colors (4 bits) a writeable bitmap object to work with it.
        /// </summary>
        /// <param name="width">The width of the image.</param>
        /// <param name="height">The height of the image.</param>
        /// <param name="palette">The palette of the image.</param>
        /// <param name="dpi">The dots per inch of the image.</param>
        /// <returns>The new instance created of the image.</returns>
        public static WriteableBitmap CreateIndexed4 (int width, int height,
            BitmapPalette palette, int dpi = DefaultDPI) {
            return new WriteableBitmap(width, height, dpi, dpi, PixelFormats.Indexed4, palette);
        }

        //------------------------------------------------------------------------
        // Methods (Grid Bitmaps)
        //------------------------------------------------------------------------

        /// <summary>
        /// Creates a writeable bitmap with a grid drawn in it.
        /// </summary>
        /// <param name="source">The source bitmap that will be used with the grid.</param>
        /// <param name="color">The color of the lines of the grid.</param>
        /// <param name="separation">The separation between lines.</param>
        /// <param name="offset">The separation between dots in the line.</param>
        /// <param name="cellSize">The size in pixels of the cell.</param>
        /// <param name="dpi">The dots per inch of the grid.</param>
        /// <returns>The new instance created of the grid.</returns>
        public static WriteableBitmap CreateGrid (BitmapSource source, Color color,
            int separation, int offset = 1, int cellSize = 1, int dpi = DefaultDPI) {
            return CreateGrid(source.PixelWidth, source.PixelHeight, color,
                separation, offset, offset, cellSize, cellSize, dpi);
        }

        /// <summary>
        /// Creates a writeable bitmap with a grid drawn in it.
        /// </summary>
        /// <param name="source">The source bitmap that will be used with the grid.</param>
        /// <param name="color">The color of the lines of the grid.</param>
        /// <param name="separation">The separation between lines.</param>
        /// <param name="offsetX">The x-separation between dots in the line.</param>
        /// <param name="offsetY">The y-separation between dots in the line.</param>
        /// <param name="cellSizeX">The x-size in pixels of the cell.</param>
        /// <param name="cellSizeY">The y-size in pixels of the cell.</param>
        /// <param name="dpi">The dots per inch of the grid.</param>
        /// <returns>The new instance created of the grid.</returns>
        public static WriteableBitmap CreateGrid (BitmapSource source, Color color,
            int separation, int offsetX, int offsetY, int cellSizeX, int cellSizeY,
            int dpi = DefaultDPI) {
            return CreateGrid(source.PixelWidth, source.PixelHeight, color,
                separation, offsetX, offsetY, cellSizeX, cellSizeY, dpi);
        }

        /// <summary>
        /// Creates a writeable bitmap with a grid drawn in it.
        /// </summary>
        /// <param name="width">The width of the grid.</param>
        /// <param name="height">The height of the grid.</param>
        /// <param name="color">The color of the lines of the grid.</param>
        /// <param name="separation">The separation between lines.</param>
        /// <param name="offset">The separation between dots in the line.</param>
        /// <param name="cellSize">The size in pixels of the cell.</param>
        /// <param name="dpi">The dots per inch of the grid.</param>
        /// <returns>The new instance created of the grid.</returns>
        public static WriteableBitmap CreateGrid (int width, int height, Color color,
            int separation, int offset = 1, int cellSize = 1, int dpi = DefaultDPI) {
            return CreateGrid(width, height, color, separation, offset, offset,
                cellSize, cellSize, dpi);
        }

        /// <summary>
        /// Creates a writeable bitmap with a grid drawn in it.
        /// </summary>
        /// <param name="width">The width of the grid.</param>
        /// <param name="height">The height of the grid.</param>
        /// <param name="color">The color of the lines of the grid.</param>
        /// <param name="separation">The separation between lines.</param>
        /// <param name="offsetX">The x-separation between dots in the line.</param>
        /// <param name="offsetY">The y-separation between dots in the line.</param>
        /// <param name="cellSizeX">The x-size in pixels of the cell.</param>
        /// <param name="cellSizeY">The y-size in pixels of the cell.</param>
        /// <param name="dpi">The dots per inch of the grid.</param>
        /// <returns>The new instance created of the grid.</returns>
        public static WriteableBitmap CreateGrid (int width, int height, Color color,
            int separation, int offsetX, int offsetY, int cellSizeX, int cellSizeY,
            int dpi = DefaultDPI) {
            // First, we will create the bitmap object:
            width = width * separation + 1;
            height = height * separation + 1;
            var bitmap = CreateARGB(width, height, dpi);
            // Second, we will configure a transform function for coordinates:
            var stride = bitmap.GetStride();
            var depth = bitmap.GetDepth();
            Func<int, int, int> transform = (x, y) => y * stride + x * depth;
            // Then, we will paint the lines of the grid and return the object:
            bitmap.WritePixels(pixels => {
                // Drawing horizontal lines of the grid:
                for (int y = 0; y < height; y += separation * cellSizeY) {
                    for (int x = 0; x < width; x += offsetX) {
                        ImageTools.SetARGB(pixels, transform(x, y), color);
                    }
                }
                // Drawing vertical lines of the grid:
                for (int x = 0; x < width; x += separation * cellSizeX) {
                    for (int y = 0; y < height; y += offsetY) {
                        ImageTools.SetARGB(pixels, transform(x, y), color);
                    }
                }
            });
            return bitmap;
        }

        //------------------------------------------------------------------------
        // Methods (Templates)
        //------------------------------------------------------------------------

        /// <summary>
        /// Creates a writeable bitmap object to work with it.
        /// </summary>
        /// <param name="victim">The template of the image.</param>
        /// <returns>The new instance created of the image.</returns>
        public static WriteableBitmap CreateFrom (BitmapTemplate victim) {
            return new WriteableBitmap(victim.Width, victim.Height, victim.DotsPerInch,
                victim.DotsPerInch, victim.Format, victim.Palette);
        }

        /// <summary>
        /// Creates a writeable bitmap object to work with it.
        /// </summary>
        /// <param name="victim">The template of the image.</param>
        /// <returns>The new instance created of the image.</returns>
        public static WriteableBitmap CreateFrom (BitmapGridTemplate victim) {
            return CreateGrid(victim.Source.Width, victim.Source.Height, victim.Color,
                victim.Separation, victim.OffsetX, victim.OffsetY, victim.CellSizeX,
                victim.CellSizeY, victim.Source.DotsPerInch);
        }
    }

    //============================================================================
    // BitmapTemplate
    //============================================================================

    /// <summary>
    /// This type represents bitmap template descriptor.
    /// </summary>
    public class BitmapTemplate {
        /// <summary>
        /// The width of the image.
        /// </summary>
        public int Width = 0;

        /// <summary>
        /// The width of the image.
        /// </summary>
        public int Height = 0;

        /// <summary>
        /// The dots per inch of the image.
        /// </summary>
        public int DotsPerInch = ImageFactory.DefaultDPI;

        /// <summary>
        /// The format of the image.
        /// </summary>
        public PixelFormat Format = PixelFormats.Bgra32;

        /// <summary>
        /// The palette of the image.
        /// </summary>
        public BitmapPalette Palette = null;
    }

    //============================================================================
    // BitmapGridTemplate
    //============================================================================

    /// <summary>
    /// This type represents bitmap grid template descriptor.
    /// </summary>
    public class BitmapGridTemplate {
        /// <summary>
        /// The source builder of the grid image.
        /// </summary>
        public BitmapTemplate Source = null;

        /// <summary>
        /// The color of the lines of the grid.
        /// </summary>
        public Color Color = new Color { A = 0xFF, R = 0x00, G = 0x00, B = 0x00 };

        /// <summary>
        /// The separation between lines in the grid.
        /// </summary>
        public int Separation = 16;

        /// <summary>
        /// The x-separation between dots in the line.
        /// </summary>
        public int OffsetX = 1;

        /// <summary>
        /// The y-separation between dots in the line.
        /// </summary>
        public int OffsetY = 1;

        /// <summary>
        /// The x-size in pixels of the cell.
        /// </summary>
        public int CellSizeX = 1;

        /// <summary>
        /// The y-size in pixels of the cell.
        /// </summary>
        public int CellSizeY = 1;

        /// <summary>
        /// The separation between dots in the line.
        /// </summary>
        public int Offset {
            set {
                OffsetX = value;
                OffsetY = value;
            }
        }

        /// <summary>
        /// Sets the size in pixels of the cell.
        /// </summary>
        public int CellSize {
            set {
                CellSizeX = value;
                CellSizeY = value;
            }
        }
    }
}
