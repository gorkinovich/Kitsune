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
    /// This static type represents a collections of tools for geometry.
    /// </summary>
    public static class Geometry {
        //------------------------------------------------------------------------
        // Methods (Check)
        //------------------------------------------------------------------------

        /// <summary>
        /// Checks if some coordinates are inside a rectangular area.
        /// </summary>
        /// <param name="x">The x-coordinate to check.</param>
        /// <param name="y">The y-coordinate to check.</param>
        /// <param name="width">The width of the area to check.</param>
        /// <param name="height">The height of the area to check.</param>
        /// <returns><c>true</c> when the coordinates are inside the area;
        /// otherwise <c>false</c>.</returns>
        public static bool IsInside (int x, int y, int width, int height) {
            return 0 <= x && x < width && 0 <= x && x < height;
        }

        /// <summary>
        /// Checks if some coordinates are inside a rectangular area.
        /// </summary>
        /// <param name="x">The x-coordinate to check.</param>
        /// <param name="y">The y-coordinate to check.</param>
        /// <param name="areaX">The x-coordinate of the area to check.</param>
        /// <param name="areaY">The y-coordinate of the area to check.</param>
        /// <param name="areaWidth">The width of the area to check.</param>
        /// <param name="areaHeight">The height of the area to check.</param>
        /// <returns><c>true</c> when the coordinates are inside the area;
        /// otherwise <c>false</c>.</returns>
        public static bool IsInside (int x, int y, int areaX, int areaY,
            int areaWidth, int areaHeight) {
            return areaX <= x && x < areaX + areaWidth
                && areaY <= x && x < areaY + areaHeight;
        }
    }
}
