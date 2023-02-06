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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Kitsune.Controls {
    /// <summary>
    /// This static type represents a collections of tools for bitmap images.
    /// </summary>
    public static class ImageTools {
        //------------------------------------------------------------------------
        // Constants, Fields & Properties
        //------------------------------------------------------------------------

        /// <summary>
        /// How many bits per byte there are.
        /// </summary>
        public const int BitsPerByte = 8;

        //------------------------------------------------------------------------
        // Methods (Pixels & Colors)
        //------------------------------------------------------------------------

        /// <summary>
        /// Sets the color of a pixel inside an array of bytes.
        /// </summary>
        /// <param name="pixels">The array of bytes to change.</param>
        /// <param name="index">The index of the pixel.</param>
        /// <param name="color">The color of the pixel.</param>
        public static void SetARGB (byte[] pixels, int index, Color color) {
            pixels[index] = color.B;
            pixels[index + 1] = color.G;
            pixels[index + 2] = color.R;
            pixels[index + 3] = color.A;
        }

        /// <summary>
        /// Sets the color of a pixel inside an array of bytes.
        /// </summary>
        /// <param name="pixels">The array of bytes to change.</param>
        /// <param name="index">The index of the pixel.</param>
        /// <param name="color">The color of the pixel.</param>
        public static void SetRGB (byte[] pixels, int index, Color color) {
            pixels[index] = color.B;
            pixels[index + 1] = color.G;
            pixels[index + 2] = color.R;
        }

        /// <summary>
        /// Transforms an array of bytes into a color object.
        /// </summary>
        /// <param name="color">The value to transform.</param>
        /// <returns>The transformed value.</returns>
        public static Color BytesToColor (byte[] color) {
            return new Color() {
                B = color?[0] ?? 0,
                G = color?[1] ?? 0,
                R = color?[2] ?? 0,
                A = color?[3] ?? 0
            };
        }

        /// <summary>
        /// Transforms a color object into an array of bytes.
        /// </summary>
        /// <param name="color">The value to transform.</param>
        /// <returns>The transformed value.</returns>
        public static byte[] ColorToBytes (Color color) {
            return new byte[] {
                color.B,
                color.G,
                color.R,
                color.A
            };
        }

        //------------------------------------------------------------------------
        // Image: Extension Methods
        //------------------------------------------------------------------------

        /// <summary>
        /// Gets the source coordinates for a mouse event arguments.
        /// </summary>
        /// <param name="value">The image instance.</param>
        /// <param name="e">The mouse event arguments.</param>
        /// <returns>The coordinates inside the source image.</returns>
        public static Point GetSourceCoordinates (this Image value, MouseEventArgs e) {
            if (value.Source != null) {
                var position = e.GetPosition(value);
                var widthFactor = value.Source.Width / value.ActualWidth;
                var heightFactor = value.Source.Height / value.ActualHeight;
                return new Point(position.X * widthFactor, position.Y * heightFactor);
            } else {
                return new Point(double.NaN, double.NaN);
            }
        }

        //------------------------------------------------------------------------
        // Point: Extension Methods
        //------------------------------------------------------------------------

        /// <summary>
        /// Determines whether the specified point is finite.
        /// </summary>
        /// <param name="value">The point instance.</param>
        /// <returns><c>true</c> if the value is finite (zero, subnormal or
        /// normal); <c>false</c> otherwise.</returns>
        public static bool IsFinite (this Point value) {
            return double.IsFinite(value.X) && double.IsFinite(value.Y);
        }

        //------------------------------------------------------------------------
        // WriteableBitmap: Extension Methods
        //------------------------------------------------------------------------

        /// <summary>
        /// Gets the depth in bytes of the bitmap.
        /// </summary>
        /// <param name="value">The bitmap instance.</param>
        /// <returns>The number of bytes of the depth.</returns>
        public static int GetDepth (this WriteableBitmap value) {
            return value.Format.BitsPerPixel / BitsPerByte;
        }

        /// <summary>
        /// Gets the depth in bytes of the bitmap.
        /// </summary>
        /// <param name="value">The bitmap instance.</param>
        /// <returns>The number of bytes of the depth.</returns>
        public static double GetRealDepth (this WriteableBitmap value) {
            return ((double) value.Format.BitsPerPixel) / BitsPerByte;
        }

        /// <summary>
        /// Gets the stride of the bitmap.
        /// </summary>
        /// <param name="value">The bitmap instance.</param>
        /// <returns>The stride value of the bitmap.</returns>
        public static int GetStride (this WriteableBitmap value) {
            return value.PixelWidth * value.Format.BitsPerPixel / BitsPerByte;
        }

        /// <summary>
        /// Gets the stride for a given width.
        /// </summary>
        /// <param name="value">The bitmap instance.</param>
        /// <param name="width">The with of the stride.</param>
        /// <returns>The stride value of the bitmap.</returns>
        public static int GetStride (this WriteableBitmap value, int width) {
            return width * value.Format.BitsPerPixel / BitsPerByte;
        }

        /// <summary>
        /// Gets the bitmap pixel data into an array of bytes.
        /// </summary>
        /// <param name="value">The bitmap instance.</param>
        /// <returns>An array with the bytes of the bitmap.</returns>
        public static byte[] GetPixels (this WriteableBitmap value) {
            byte[] pixels = new byte[value.PixelHeight * value.GetStride()];
            value.CopyPixels(pixels, value.GetStride(), 0);
            return pixels;
        }

        /// <summary>
        /// Gets the bitmap pixel data into an array of bytes.
        /// </summary>
        /// <param name="value">The bitmap instance.</param>
        /// <param name="source">The source rectangle to get.</param>
        /// <returns>An array with the bytes of the bitmap.</returns>
        public static byte[] GetPixels (this WriteableBitmap value, Int32Rect source) {
            byte[] pixels = new byte[source.Height * value.GetStride(source.Width)];
            value.CopyPixels(source, pixels, value.GetStride(), 0);
            return pixels;
        }

        /// <summary>
        /// Checks if a position is inside the bitmap.
        /// </summary>
        /// <param name="value">The bitmap instance.</param>
        /// <param name="position">The position to check.</param>
        /// <returns><c>true</c> if the position is inside the bitmap;
        /// otherwise <c>false</c>.</returns>
        public static bool IsInside (this WriteableBitmap value, Point position) {
            return 0 <= position.X && position.X < value.Width
                && 0 <= position.Y && position.Y < value.Height;
        }

        /// <summary>
        /// Checks if a position is inside the bitmap.
        /// </summary>
        /// <param name="value">The bitmap instance.</param>
        /// <param name="x">The x position of the pixel.</param>
        /// <param name="y">The y position of the pixel.</param>
        /// <returns><c>true</c> if the position is inside the bitmap;
        /// otherwise <c>false</c>.</returns>
        public static bool IsInside (this WriteableBitmap value, int x, int y) {
            return 0 <= x && x < value.PixelWidth && 0 <= y && y < value.PixelHeight;
        }

        /// <summary>
        /// Writes a pixel inside a bitmap.
        /// </summary>
        /// <param name="value">The bitmap instance.</param>
        /// <param name="position">The position of the pixel.</param>
        /// <param name="color">The color of the pixel.</param>
        public static void WritePixel (this WriteableBitmap value, Point position, byte color) {
            value.WritePixel(position, new byte[] { color });
        }

        /// <summary>
        /// Writes a pixel inside a bitmap.
        /// </summary>
        /// <param name="value">The bitmap instance.</param>
        /// <param name="position">The position of the pixel.</param>
        /// <param name="color">The color of the pixel.</param>
        public static void WritePixel (this WriteableBitmap value, Point position, byte[] color) {
            if (position.IsFinite()) {
                value.WritePixel((int) position.X, (int) position.Y, color);
            }
        }

        /// <summary>
        /// Writes a pixel inside a bitmap.
        /// </summary>
        /// <param name="value">The bitmap instance.</param>
        /// <param name="x">The x position of the pixel.</param>
        /// <param name="y">The y position of the pixel.</param>
        /// <param name="color">The color of the pixel.</param>
        public static void WritePixel (this WriteableBitmap value, int x, int y, byte color) {
            value.WritePixel(x, y, new byte[] { color });
        }

        /// <summary>
        /// Writes a pixel inside a bitmap.
        /// </summary>
        /// <param name="value">The bitmap instance.</param>
        /// <param name="x">The x position of the pixel.</param>
        /// <param name="y">The y position of the pixel.</param>
        /// <param name="color">The color of the pixel.</param>
        public static void WritePixel (this WriteableBitmap value, int x, int y, byte[] color) {
            if (value.IsInside(x, y)) {
                const int OnePixelWidth = 1;
                const int OnePixelHeight = 1;
                value.WritePixels(new Int32Rect(x, y, OnePixelWidth, OnePixelHeight),
                    color, value.GetStride(OnePixelWidth), 0);
            }
        }

        /// <summary>
        /// Writes some random pixels inside a bitmap.
        /// </summary>
        /// <param name="value">The bitmap instance.</param>
        /// <param name="render">The on render event function.</param>
        public static void WritePixels (this WriteableBitmap value, Action<byte[]> render) {
            value.WritePixels(new Int32Rect(0, 0, value.PixelWidth, value.PixelHeight), render);
        }

        /// <summary>
        /// Writes some random pixels inside a bitmap.
        /// </summary>
        /// <param name="value">The bitmap instance.</param>
        /// <param name="source">The rectangle to write.</param>
        /// <param name="render">The on render event function.</param>
        public static void WritePixels (this WriteableBitmap value, Int32Rect source,
            Action<byte[]> render) {
            // Create the buffer and render some pixels on it:
            var stride = source.Width * value.Format.BitsPerPixel / BitsPerByte;
            byte[] pixels = new byte[source.Height * stride];
            if (render != null) {
                render(pixels);
            }
            // Then, write the buffer on the bitmap object:
            value.WritePixels(source, pixels, stride, 0);
        }

        /// <summary>
        /// Writes some random pixels inside a bitmap.
        /// </summary>
        /// <param name="value">The bitmap instance.</param>
        public static void WriteRandomPixels (this WriteableBitmap value) {
            value.WritePixels(pixels => {
                Random random = new Random();
                random.NextBytes(pixels);
            });
        }

        /// <summary>
        /// Clears all the pixels inside a bitmap.
        /// </summary>
        /// <param name="value">The bitmap instance.</param>
        public static void ClearPixels (this WriteableBitmap value) {
            value.WritePixels(null);
        }
    }
}
