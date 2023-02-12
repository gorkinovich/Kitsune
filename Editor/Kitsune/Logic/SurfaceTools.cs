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

namespace Kitsune.Logic {
    /// <summary>
    /// This static type represents a collections of tools for C64 bitmaps.
    /// </summary>
    public static class SurfaceTools {
        //------------------------------------------------------------------------
        // Constants
        //------------------------------------------------------------------------

        public const int PixelsPerByte = 8;

        public const int MultiColorPixelsPerByte = 4;

        public const int BitsPerPixel = 1;

        public const int BitsPerMultiColorPixel = 2;

        public const int LowerOffset = 0;

        public const int UpperOffset = 7;

        public const int UpperMultiColorOffset = 3;

        //------------------------------------------------------------------------
        // Methods (Check)
        //------------------------------------------------------------------------

        public static bool IsInside (int x, int y, int width, int height) {
            return 0 <= x && x < width && 0 <= x && x < height;
        }

        public static bool IsInside (int x, int y, int destinationX, int destinationY,
            int destinationWidth, int destinationHeight) {
            return destinationX <= x && x < destinationX + destinationWidth
                && destinationY <= x && x < destinationY + destinationHeight;
        }

        //------------------------------------------------------------------------
        // Methods (Pixels)
        //------------------------------------------------------------------------

        public static byte GetPixel (int x, int y, byte[] pixels, int width,
            int height, bool multicolor = false) {
            var realWidth = multicolor ? width / BitsPerMultiColorPixel : width;
            var pixelsPerByte = multicolor ? MultiColorPixelsPerByte : PixelsPerByte;

            if (IsInside(x, y, realWidth, height)) {
                var index = (y * realWidth + x) / pixelsPerByte;
                var offset = (y * realWidth + x) % pixelsPerByte;
                return GetPixel(offset, pixels[index], multicolor);
            }
            return 0;
        }


        public static void SetPixel (int x, int y, byte color, byte[] pixels,
            int width, int height, bool multicolor = false) {
            //TODO: Check this code...
        }

        public static byte GetPixel (int offset, byte pixel, bool multicolor = false) {
            if (multicolor) {
                if (LowerOffset <= offset && offset <= UpperMultiColorOffset) {
                    switch (offset) {
                        case 0:
                            return (byte) ((pixel & 0xC0) >> 6);
                        case 1:
                            return (byte) ((pixel & 0x30) >> 4);
                        case 2:
                            return (byte) ((pixel & 0x0C) >> 2);
                        case 3:
                            return (byte) (pixel & 0x03);
                    }
                }
            } else {
                if (LowerOffset <= offset && offset <= UpperOffset) {
                    switch (offset) {
                        case 0:
                            return (byte) ((pixel & 0x80) >> 7);
                        case 1:
                            return (byte) ((pixel & 0x40) >> 6);
                        case 2:
                            return (byte) ((pixel & 0x20) >> 5);
                        case 3:
                            return (byte) ((pixel & 0x10) >> 4);
                        case 4:
                            return (byte) ((pixel & 0x08) >> 3);
                        case 5:
                            return (byte) ((pixel & 0x04) >> 2);
                        case 6:
                            return (byte) ((pixel & 0x02) >> 1);
                        case 7:
                            return (byte) (pixel & 0x01);
                    }
                }
            }
            return 0;
        }
    }
}
