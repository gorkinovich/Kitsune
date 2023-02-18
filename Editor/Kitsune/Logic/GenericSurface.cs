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

namespace Kitsune.Logic {
    /// <summary>
    /// This abstract class represents a Commodore 64 generic bitmap.
    /// </summary>
    /// <remarks>The width and height given to this class, must be the high
    /// resolution mode sizes. The reasson of this, is that the class will
    /// work always with high resolution coordinates.</remarks>
    public abstract class GenericSurface {
        //------------------------------------------------------------------------
        // Constants
        //------------------------------------------------------------------------

        /// <summary>
        /// The background color value in a high resolution or multi-color bitmap.
        /// </summary>
        public const int BackgroundColor = 0;

        /// <summary>
        /// The foreground color value in a high resolution or multi-color bitmap.
        /// </summary>
        public const int ForegroundColor = 1;

        /// <summary>
        /// The first extra color value in a multi-color bitmap.
        /// </summary>
        public const int FirstMultiColor = 2;

        /// <summary>
        /// The second extra color value in a multi-color bitmap.
        /// </summary>
        public const int SecondMultiColor = 3;

        /// <summary>
        /// The number of pixels per byte in a high resolution bitmap.
        /// </summary>
        private const int pixelsPerByte = 8;

        /// <summary>
        /// The lower integer value for an offset in a byte with pixels.
        /// </summary>
        private const int lowerOffset = 0;

        /// <summary>
        /// The upper integer value for an offset in a byte with pixels
        /// in the high resolution mode.
        /// </summary>
        private const int upperOffset = 7;

        //------------------------------------------------------------------------
        // Fields & Properties
        //------------------------------------------------------------------------

        /// <summary>
        /// The width of the bitmap in high resolution mode.
        /// </summary>
        private int width;

        /// <summary>
        /// The height of the bitmap in high resolution mode.
        /// </summary>
        private int height;

        /// <summary>
        /// The pixels of the bitmap.
        /// </summary>
        private byte[] pixels;

        /// <summary>
        /// Gets the width of the bitmap.
        /// </summary>
        public int Width => width;

        /// <summary>
        /// Gets the height of the bitmap.
        /// </summary>
        public int Height => height;

        /// <summary>
        /// Gets the size in bytes of the bitmap.
        /// </summary>
        public int Size => pixels.Length;

        /// <summary>
        /// Gets or sets the multi-color mode of the bitmap.
        /// </summary>
        public bool MultiColor { get; set; }

        /// <summary>
        /// Gets or sets a pixel of the bitmap.
        /// </summary>
        /// <param name="x">The x-coordinate in the bitmap.</param>
        /// <param name="y">The y-coordinate in the bitmap.</param>
        /// <returns>The pixel in the coordinates.</returns>
        public int this[int x, int y] {
            get => GetPixel(x, y);
            set => SetPixel(x, y, value);
        }

        // TODO: Add properties to define the palette of the surface...

        //------------------------------------------------------------------------
        // Events
        //------------------------------------------------------------------------

        /// <summary>
        /// Occurs when the bitmap pixels are modified.
        /// </summary>
        public event EventHandler<SurfaceChangedEventArgs> Changed;

        //------------------------------------------------------------------------
        // Constructors
        //------------------------------------------------------------------------

        /// <summary>
        /// Makes a new object of the class.
        /// </summary>
        /// <param name="width">The width of the bitmap.</param>
        /// <param name="height">The height of the bitmap.</param>
        /// <param name="multiColor">The multi-color mode of the bitmap.</param>
        public GenericSurface (int width, int height, bool multiColor = false) {
            this.pixels = new byte[width * height / pixelsPerByte];
            this.width = width;
            this.height = height;
            this.MultiColor = multiColor;
        }

        //------------------------------------------------------------------------
        // Methods
        //------------------------------------------------------------------------

        /// <summary>
        /// Gets a pixel inside a bitmap.
        /// </summary>
        /// <param name="x">The x-coordinate in the bitmap.</param>
        /// <param name="y">The y-coordinate in the bitmap.</param>
        /// <returns>The value of the pixel (0-1 in high resolution
        /// mode; 0-3 in multi-color mode).</returns>
        public int GetPixel (int x, int y) {
            if (Geometry.IsInside(x, y, width, height)) {
                var (index, offset) = GetIndexAndOffset(x, y);
                return GetPixel(pixels[index], offset, MultiColor);
            }
            return default;
        }

        /// <summary>
        /// Sets a pixel inside a bitmap.
        /// </summary>
        /// <param name="x">The x-coordinate in the bitmap.</param>
        /// <param name="y">The y-coordinate in the bitmap.</param>
        /// <param name="color">The color to set (0-1 in high
        /// resolution mode; 0-3 in multi-color mode).</param>
        public void SetPixel (int x, int y, int color) {
            if (Geometry.IsInside(x, y, width, height)) {
                color = ClampColor(color);
                var (index, offset) = GetIndexAndOffset(x, y);
                pixels[index] = SetPixel(pixels[index], offset, color, MultiColor);
                Changed(this, new SurfaceChangedEventArgs(x, y, this));
            }
        }

        /// <summary>
        /// Clamps a color to the inclusive range of colors of the bitmap.
        /// </summary>
        /// <param name="color">The color to be clamped.</param>
        /// <returns>The color value inside the range.</returns>
        public int ClampColor (int color) {
            return Math.Clamp(color, BackgroundColor, MultiColor ? SecondMultiColor : ForegroundColor);
        }

        /// <summary>
        /// Gets the index and offset inside the pixels buffer of the bitmap.
        /// </summary>
        /// <param name="x">The x-coordinate in the bitmap.</param>
        /// <param name="y">The y-coordinate in the bitmap.</param>
        /// <returns>A tuple where the first component is the index of the final
        /// byte, and the second component is the offset inside the byte.</returns>
        protected (int, int) GetIndexAndOffset (int x, int y) {
            var position = (y * width + x);
            return (position / pixelsPerByte, position % pixelsPerByte);
        }

        //------------------------------------------------------------------------
        // Static Methods
        //------------------------------------------------------------------------

        /// <summary>
        /// Gets a pixel inside a byte.
        /// </summary>
        /// <param name="pixel">The byte with the pixel to get.</param>
        /// <param name="offset">The x-offset to get.</param>
        /// <param name="multicolor">The multi-color mode flag of the bitmap;
        /// when <c>false</c> the bitmap is in high resolution mode.</param>
        /// <returns>The value of the pixel, 0-1 in high resolution mode,
        /// 0-3 in multi-color mode.</returns>
        protected static int GetPixel (byte pixel, int offset, bool multicolor) {
            offset = Math.Clamp(offset, lowerOffset, upperOffset);
            if (multicolor) {
                return offset switch {
                    < 2 => (pixel & 0xC0) >> 6,
                    < 4 => (pixel & 0x30) >> 4,
                    < 6 => (pixel & 0x0C) >> 2,
                    _ => pixel & 0x03
                };
            } else {
                return offset switch {
                    0 => (pixel & 0x80) >> 7,
                    1 => (pixel & 0x40) >> 6,
                    2 => (pixel & 0x20) >> 5,
                    3 => (pixel & 0x10) >> 4,
                    4 => (pixel & 0x08) >> 3,
                    5 => (pixel & 0x04) >> 2,
                    6 => (pixel & 0x02) >> 1,
                    _ => pixel & 0x01
                };
            }
        }

        /// <summary>
        /// Sets a pixel inside a byte.
        /// </summary>
        /// <param name="pixel">The byte with the pixel to modify.</param>
        /// <param name="offset">The x-offset to set.</param>
        /// <param name="color">The color to set.</param>
        /// <param name="multicolor">The multi-color mode flag of the bitmap;
        /// when <c>false</c> the bitmap is in high resolution mode.</param>
        /// <returns>A new byte with the pixel modified.</returns>
        protected static byte SetPixel (byte pixel, int offset, int color, bool multicolor) {
            offset = Math.Clamp(offset, lowerOffset, upperOffset);
            if (multicolor) {
                color &= 0x03;
                return offset switch {
                    < 2 => (byte) ((pixel & ~0xC0) | (color << 6)),
                    < 4 => (byte) ((pixel & ~0x30) | (color << 4)),
                    < 6 => (byte) ((pixel & ~0x0C) | (color << 2)),
                    _ => (byte) ((pixel & ~0x03) | color)
                };
            } else {
                color &= 0x01;
                return offset switch {
                    0 => (byte) ((pixel & ~0x80) | (color << 7)),
                    1 => (byte) ((pixel & ~0x40) | (color << 6)),
                    2 => (byte) ((pixel & ~0x20) | (color << 5)),
                    3 => (byte) ((pixel & ~0x10) | (color << 4)),
                    4 => (byte) ((pixel & ~0x08) | (color << 3)),
                    5 => (byte) ((pixel & ~0x04) | (color << 2)),
                    6 => (byte) ((pixel & ~0x02) | (color << 1)),
                    _ => (byte) ((pixel & ~0x01) | color)
                };
            }
        }
    }
}
