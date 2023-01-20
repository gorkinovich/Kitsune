//================================================================================
// Copyright(c) 2023 Gorka Suárez
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

using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Kitsune {
    /// <summary>
    /// This static type contains some definitions of the VIC-II palette.
    /// </summary>
    public static class Palettes {
        /// <summary>
        /// Makes a new C64 palette with the CCS64 colors.
        /// </summary>
        /// <returns>The new C64 palette.</returns>
        public static BitmapPalette CCS64 () {
            var colors = new uint[] {
                0xFF000000, 0xFFFFFFFF, 0xFF933A4C, 0xFFB6FAFA,
                0xFFD27DED, 0xFF6ACF6F, 0xFF4F44D8, 0xFFFBFB8B,
                0xFFD89C5B, 0xFF7F5307, 0xFFEF839F, 0xFF575753,
                0xFFA3A7A7, 0xFFB7FBBF, 0xFFA397FF, 0xFFEFE9E7
            };
            return new BitmapPalette(FromUInt32(colors));
        }

        /// <summary>
        /// Transforms an array of UInt32 with ARGB colors into an array of Color objects.
        /// </summary>
        /// <param name="values">The array with the colors to transform.</param>
        /// <returns>The new array of Color objects.</returns>
        public static Color[] FromUInt32 (uint[] values) {
            var colors = new Color[values.Length];
            for (int i = 0; i < values.Length; i++) {
                colors[i] = FromUInt32(values[i]);
            }
            return colors;
        }

        /// <summary>
        /// Transforms a UInt32 with an ARGB color into a Color object.
        /// </summary>
        /// <param name="values">The color to transform.</param>
        /// <returns>The new Color objects.</returns>
        public static Color FromUInt32(uint value) {
            byte a = (byte) ((value & 0xFF000000) >> 24);
            byte r = (byte) ((value & 0x00FF0000) >> 16);
            byte g = (byte) ((value & 0x0000FF00) >> 8);
            byte b = (byte) (value & 0x000000FF);
            return new Color { A = a, R = r, G = g, B = b };
        }
    }
}
