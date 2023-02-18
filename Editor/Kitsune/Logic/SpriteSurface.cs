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

namespace Kitsune.Logic {
    /// <summary>
    /// This class represents a Commodore 64 sprite bitmap.
    /// </summary>
    public class SpriteSurface : GenericSurface {
        //------------------------------------------------------------------------
        // Constants
        //------------------------------------------------------------------------

        /// <summary>
        /// The width in pixels of a sprite bitmap.
        /// </summary>
        public const int DefaultWidth = 24;

        /// <summary>
        /// The height in pixels of a sprite bitmap.
        /// </summary>
        public const int DefaultHeight = 21;

        //------------------------------------------------------------------------
        // Constructors
        //------------------------------------------------------------------------

        /// <summary>
        /// Makes a new object of the class.
        /// </summary>
        /// <param name="multiColor">The multi-color mode of the bitmap.</param>
        public SpriteSurface (bool multiColor = false)
            : base(DefaultWidth, DefaultHeight, multiColor) {}
    }
}
