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
    /// This class represents a Commodore 64 sprite bitmap.
    /// </summary>
    public class SpriteSurface {
        //------------------------------------------------------------------------
        // Constants
        //------------------------------------------------------------------------

        /// <summary>
        /// The width in pixels of a sprite bitmap.
        /// </summary>
        public const int Width = 24;

        /// <summary>
        /// The height in pixels of a sprite bitmap.
        /// </summary>
        public const int Height = 21;

        /// <summary>
        /// The size in bytes of the pixels of a sprite bitmap.
        /// </summary>
        public const int Size = 63;

        /// <summary>
        /// The transparent color value of a sprite bitmap.
        /// </summary>
        public const int TransparentColor = 0;

        /// <summary>
        /// The sprite color value of a sprite bitmap.
        /// </summary>
        public const int SpriteColor = 1;

        /// <summary>
        /// The first multi-color value of a sprite bitmap.
        /// </summary>
        public const int FirstMultiColor = 2;

        /// <summary>
        /// The second multi-color value of a sprite bitmap.
        /// </summary>
        public const int SecondMultiColor = 3;

        //------------------------------------------------------------------------
        // Fields & Properties
        //------------------------------------------------------------------------

        /// <summary>
        /// The pixels of the sprite surface.
        /// </summary>
        private byte[] pixels = new byte[Size];

        /// <summary>
        /// The main color of the sprite surface.
        /// </summary>
        private byte color = 0;

        /// <summary>
        /// Gets or sets the main color of the sprite surface.
        /// </summary>
        public byte Color {
            get => color;
            set => color = (byte) (value < Palette.NumberOfColors ? value : 0);
        }

        /// <summary>
        /// The multi-color flag of the sprite surface.
        /// </summary>
        public bool IsMultiColor { get; set; } = false;

        /// <summary>
        /// Gets or sets a pixel of the sprite surface.
        /// </summary>
        /// <param name="x">The x-coordinate in the sprite.</param>
        /// <param name="y">The y-coordinate in the sprite.</param>
        /// <returns>The pixel in the coordinates.</returns>
        public byte this[int x, int y] {
            get => SurfaceTools.GetPixel(x, y, pixels, Width, Height, IsMultiColor);
            set => SurfaceTools.SetPixel(x, y, value, pixels, Width, Height, IsMultiColor);
        }

        //------------------------------------------------------------------------
        // Methods
        //------------------------------------------------------------------------

        /// <summary>
        /// Makes a new object of the class.
        /// </summary>
        public SpriteSurface () {
        }
    }
}
