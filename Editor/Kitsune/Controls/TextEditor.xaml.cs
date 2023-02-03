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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kitsune.Controls {
    /// <summary>
    /// Interaction logic for TextEditor.xaml
    /// </summary>
    public partial class TextEditor : UserControl {
        public TextEditor () {
            InitializeComponent();
        }

        private void InnerTextBox_KeyDown (object sender, KeyEventArgs e) {
            string spaces = new string(' ', 4);
            if (e.Key == Key.Tab && Keyboard.Modifiers == ModifierKeys.None) {
                e.Handled = true;
                if (string.IsNullOrEmpty(InnerTextBox.SelectedText)) {
                    InnerTextBox.SelectedText = spaces;
                    InnerTextBox.Select(InnerTextBox.SelectionStart + spaces.Length, 0);
                } else {
                    SelectLineBegin();
                    var lines = InnerTextBox.SelectedText.Split('\n');
                    lines = lines.Select(x => spaces + x).ToArray();
                    InnerTextBox.SelectedText = string.Join("\n", lines);
                }
            } else if (e.Key == Key.Tab && Keyboard.Modifiers == ModifierKeys.Shift) {
                e.Handled = true;
                bool unselect = InnerTextBox.SelectionLength == 0;
                SelectLineBegin();
                var lines = InnerTextBox.SelectedText.Split('\n');
                lines = lines.Select(x => {
                    if (x.StartsWith(spaces)) {
                        return x.Substring(spaces.Length);
                    } else {
                        return x.TrimStart();
                    }
                }).ToArray();
                InnerTextBox.SelectedText = string.Join("\n", lines);
                if (unselect) {
                    InnerTextBox.Select(InnerTextBox.SelectionStart + InnerTextBox.SelectionLength, 0);
                }
            }
        }

        private void SelectLineBegin () {
            var start = InnerTextBox.SelectionStart;
            var length = InnerTextBox.SelectionLength;
            var index = start;
            while (index > 0 && InnerTextBox.Text[index] != '\n') {
                index--;
            }
            if (index > 0) {
                index++;
            }
            InnerTextBox.Select(index, start + length - index);
        }
    }
}
