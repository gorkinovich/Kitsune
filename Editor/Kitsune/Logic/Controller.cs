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
using System.Windows.Media.Imaging;

namespace Kitsune.Logic {
    /// <summary>
    /// The Kitsune editor controller.
    /// </summary>
    public class Controller {
        //------------------------------------------------------------------------
        // Fields
        //------------------------------------------------------------------------

        /// <summary>
        /// The main instance of the controller.
        /// </summary>
        private static Controller instance = new Controller();

        /// <summary>
        /// The current data of the
        /// </summary>
        private Model model = new Model();

        //------------------------------------------------------------------------
        // Properties
        //------------------------------------------------------------------------

        /// <summary>
        /// Gets the main instance of the controller.
        /// </summary>
        public static Controller Instance => instance;

        /// <summary>
        /// Gets if the current data has been modified.
        /// </summary>
        public bool Modified { get; private set; }

        /// <summary>
        /// Gets the main palette of the C64.
        /// </summary>
        public BitmapPalette Palette => model.Palette;

        //------------------------------------------------------------------------
        // Methods
        //------------------------------------------------------------------------

        /// <summary>
        /// Makes a new object of the class.
        /// </summary>
        private Controller () {
        }
    }
}
