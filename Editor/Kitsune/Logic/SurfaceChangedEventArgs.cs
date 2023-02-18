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

namespace Kitsune.Logic {
    /// <summary>
    /// This class represents an event that occurs when a bitmap is modified.
    /// </summary>
    public class SurfaceChangedEventArgs : EventArgs {
        //------------------------------------------------------------------------
        // Fields & Properties
        //------------------------------------------------------------------------

        /// <summary>
        /// Gets the area changed in the bitmap.
        /// </summary>
        public Int32Rect Area { get; init; }

        /// <summary>
        /// Gets the changed bitmap of the event.
        /// </summary>
        public GenericSurface Surface { get; init; }

        //------------------------------------------------------------------------
        // Constructors
        //------------------------------------------------------------------------

        /// <summary>
        /// Makes a new object of the class.
        /// </summary>
        public SurfaceChangedEventArgs () {
        }

        /// <summary>
        /// Makes a new object of the class.
        /// </summary>
        /// <param name="x">The x-coordinate changed in the bitmap.</param>
        /// <param name="y">The y-coordinate changed in the bitmap.</param>
        /// <param name="surface">The changed bitmap of the event.</param>
        public SurfaceChangedEventArgs (int x, int y, GenericSurface surface) {
            Area = new Int32Rect(x, y, 1, 1);
            Surface = surface;
        }
    }
}
