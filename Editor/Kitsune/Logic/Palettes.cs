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

namespace Kitsune.Logic {
    /// <summary>
    /// This static type contains some definitions of the VIC-II palette.
    /// </summary>
    public static class Palettes {
        //------------------------------------------------------------------------
        // General palettes
        //------------------------------------------------------------------------

        /// <summary>
        /// Makes a new C64 palette with the CCS64 colors.
        /// </summary>
        /// <returns>The new C64 palette.</returns>
        public static BitmapPalette CCS64 () {
            var colors = new uint[] {
                0xFF191D19, 0xFFFCF9FC, 0xFF933A4C, 0xFFB6FAFA,
                0xFFD27DED, 0xFF6ACF6F, 0xFF4F44D8, 0xFFFBFB8B,
                0xFFD89C5B, 0xFF7F5307, 0xFFEF839F, 0xFF575753,
                0xFFA3A7A7, 0xFFB7FBBF, 0xFFA397FF, 0xFFEFE9E7
            };
            return new BitmapPalette(FromUInt32(colors));
        }

        /// <summary>
        /// Makes a new C64 palette with the VICE colors.
        /// </summary>
        /// <returns>The new C64 palette.</returns>
        public static BitmapPalette VICE () {
            var colors = new uint[] {
                0xFF000000, 0xFFFDFEFC, 0xFFBE1A24, 0xFF30E6C6,
                0xFFB41AE2, 0xFF1FD21E, 0xFF211BAE, 0xFFDFF60A,
                0xFFB84104, 0xFF6A3304, 0xFFFE4A57, 0xFF424540,
                0xFF70746F, 0xFF59FE59, 0xFF5F53FE, 0xFFA4A7A2
            };
            return new BitmapPalette(FromUInt32(colors));
        }

        /// <summary>
        /// Makes a new C64 palette with the Wikipedia colors.
        /// </summary>
        /// <returns>The new C64 palette.</returns>
        public static BitmapPalette Wikipedia () {
            var colors = new uint[] {
                0xFF000000, 0xFFFFFFFF, 0xFF9F4E44, 0xFF6ABFC6,
                0xFFA057A3, 0xFF5CAB5E, 0xFF50459B, 0xFFC9D487,
                0xFFA1683C, 0xFF6D5412, 0xFFCB7E75, 0xFF626262,
                0xFF898989, 0xFF9AE29B, 0xFF887ECB, 0xFFADADAD
            };
            return new BitmapPalette(FromUInt32(colors));
        }

        //------------------------------------------------------------------------
        // Alternative palettes
        //------------------------------------------------------------------------

        /// <summary>
        /// Makes a new C64 palette with the AseSprite colors.
        /// </summary>
        /// <returns>The new C64 palette.</returns>
        public static BitmapPalette AseSprite () {
            var colors = new uint[] {
                0xFF000000, 0xFFFFFFFF, 0xFF883932, 0xFF67B6BD,
                0xFF8B3F96, 0xFF55A049, 0xFF40318D, 0xFFBFCE72,
                0xFF8B5429, 0xFF574200, 0xFFB86962, 0xFF505050,
                0xFF787878, 0xFF94E089, 0xFF7869C4, 0xFF9F9F9F
            };
            return new BitmapPalette(FromUInt32(colors));
        }

        /// <summary>
        /// Makes a new C64 palette with the CCS64 colors.
        /// </summary>
        /// <returns>The new C64 palette.</returns>
        public static BitmapPalette CCS64v2 () {
            var colors = new uint[] {
                0xFF101010, 0xFFFFFFFF, 0xFFE04040, 0xFF60FFFF,
                0xFFE060E0, 0xFF40E040, 0xFF4040E0, 0xFFFFFF40,
                0xFFE0A040, 0xFF9C7448, 0xFFFFA0A0, 0xFF545454,
                0xFF888888, 0xFFA0FFA0, 0xFFA0A0FF, 0xFFC0C0C0
            };
            return new BitmapPalette(FromUInt32(colors));
        }

        /// <summary>
        /// Makes a new C64 palette with the Colodore colors.
        /// </summary>
        /// <returns>The new C64 palette.</returns>
        public static BitmapPalette Colodore () {
            var colors = new uint[] {
                0xFF000000, 0xFFFFFFFF, 0xFF96282E, 0xFF5BD6CE,
                0xFF9F2DAD, 0xFF41B936, 0xFF2724C4, 0xFFEFF347,
                0xFF9F4815, 0xFF5E3500, 0xFFDA5F66, 0xFF474747,
                0xFF787878, 0xFF91FF84, 0xFF6864FF, 0xFFAEAEAE
            };
            return new BitmapPalette(FromUInt32(colors));
        }

        /// <summary>
        /// Makes a new C64 palette with the MultiPaint colors.
        /// </summary>
        /// <returns>The new C64 palette.</returns>
        public static BitmapPalette MultiPaint () {
            var colors = new uint[] {
                0xFF000000, 0xFFFFFFFF, 0xFF813338, 0xFF75CEC8,
                0xFF8E3C97, 0xFF56AC4D, 0xFF2E2C9B, 0xFFEDF171,
                0xFF8E5029, 0xFF553800, 0xFFC46C71, 0xFF4A4A4A,
                0xFF7B7B7B, 0xFFA9FF9F, 0xFF706DEB, 0xFFB2B2B2
            };
            return new BitmapPalette(FromUInt32(colors));
        }

        /// <summary>
        /// Makes a new C64 palette with the Colodore colors.
        /// </summary>
        /// <returns>The new C64 palette.</returns>
        public static BitmapPalette PeptoNTSC () {
            var colors = new uint[] {
                0xFF000000, 0xFFFFFFFF, 0xFF67372B, 0xFF70A3B1,
                0xFF6F3D86, 0xFF588C42, 0xFF342879, 0xFFB7C66E,
                0xFF6F4E25, 0xFF423800, 0xFF996659, 0xFF434343,
                0xFF6B6B6B, 0xFF9AD183, 0xFF6B5EB5, 0xFF959595
            };
            return new BitmapPalette(FromUInt32(colors));
        }

        /// <summary>
        /// Makes a new C64 palette with the Colodore colors.
        /// </summary>
        /// <returns>The new C64 palette.</returns>
        public static BitmapPalette PeptoPAL () {
            var colors = new uint[] {
                0xFF000000, 0xFFFFFFFF, 0xFF68372B, 0xFF70A4B2,
                0xFF6F3D86, 0xFF588D43, 0xFF352879, 0xFFB8C76F,
                0xFF6F4F25, 0xFF433900, 0xFF9A6759, 0xFF444444,
                0xFF6C6C6C, 0xFF9AD284, 0xFF6C5EB5, 0xFF959595
            };
            return new BitmapPalette(FromUInt32(colors));
        }

        /// <summary>
        /// Makes a new C64 palette with the VIC-II OLD colors.
        /// </summary>
        /// <returns>The new C64 palette.</returns>
        public static BitmapPalette VIC2OLD () {
            var colors = new uint[] {
                0xFF000000, 0xFFFFFFFF, 0xFF58291D, 0xFF91C6D5,
                0xFF915CA8, 0xFF588D43, 0xFF352879, 0xFFB8C76F,
                0xFF916F43, 0xFF433900, 0xFF9A6759, 0xFF353535,
                0xFF747474, 0xFF9AD284, 0xFF7466BE, 0xFFB8B8B8
            };
            return new BitmapPalette(FromUInt32(colors));
        }

        /// <summary>
        /// Makes a new C64 palette with the VIC-II PAL colors.
        /// </summary>
        /// <returns>The new C64 palette.</returns>
        public static BitmapPalette VIC2PAL () {
            var colors = new uint[] {
                0xFF000000, 0xFFFFFFFF, 0xFF68372B, 0xFF70A4B2,
                0xFF6F3D86, 0xFF588D43, 0xFF352879, 0xFFB8C76F,
                0xFF6F4F25, 0xFF433900, 0xFF9A6759, 0xFF444444,
                0xFF6C6C6C, 0xFF9AD284, 0xFF6C5EB5, 0xFF959595
            };
            return new BitmapPalette(FromUInt32(colors));
        }

        //------------------------------------------------------------------------
        // Conversion functions
        //------------------------------------------------------------------------

        /// <summary>
        /// Transforms an array of UInt32 with ARGB colors into an array of Color objects.
        /// </summary>
        /// <param name="values">The array with the colors to transform.</param>
        /// <returns>The new array of Color objects.</returns>
        public static Color[] FromUInt32 (uint[] values) {
            var colors = new Color[values.Length * 2];
            for (int i = 0; i < values.Length; i++) {
                colors[i] = FromUInt32(values[i]);
                colors[i + values.Length] = colors[i];
                colors[i + values.Length].A = 0;
            }
            return colors;
        }

        /// <summary>
        /// Transforms a UInt32 with an ARGB color into a Color object.
        /// </summary>
        /// <param name="values">The color to transform.</param>
        /// <returns>The new Color objects.</returns>
        public static Color FromUInt32 (uint value) {
            byte a = (byte) ((value & 0xFF000000) >> 24);
            byte r = (byte) ((value & 0x00FF0000) >> 16);
            byte g = (byte) ((value & 0x0000FF00) >> 8);
            byte b = (byte) (value & 0x000000FF);
            return new Color { A = a, R = r, G = g, B = b };
        }
    }
}
