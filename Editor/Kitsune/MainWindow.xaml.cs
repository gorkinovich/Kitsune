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
using System.ComponentModel;
using Kitsune.Logic;
using MainResources = Kitsune.Content.Resources;

namespace Kitsune {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow () {
            InitializeComponent();
        }

        private void FileNew_Command (object sender, RoutedEventArgs e) {
            MessageBox.Show("New clicked!");
        }

        private void FileOpen_Command (object sender, RoutedEventArgs e) {
            MessageBox.Show("Open clicked!");

        }

        private void FileSave_Command (object sender, RoutedEventArgs e) {
            MessageBox.Show("Save clicked!");
        }

        private void FileExit_Command (object sender, RoutedEventArgs e) {
            Application.Current.Shutdown();
        }

        private void Window_Closing (object sender, CancelEventArgs e) {
            if (Controller.Instance.Modified) {
                var answer = MessageBox.Show(MainResources.MB_ExitMsg, MainResources.MB_ExitTitle,
                    MessageBoxButton.OKCancel, MessageBoxImage.Exclamation, MessageBoxResult.OK);
                if (answer != MessageBoxResult.OK) {
                    e.Cancel = true;
                }
            }
        }

        private WriteableBitmap currentBitmap = null;
        private byte currentColor = 1;
        private bool currentEditable = false;

        private void Test1_Command (object sender, RoutedEventArgs e) {
            TilesTabItem.Focus();
            currentEditable = false;

            var format = PixelFormats.Bgr24;
            int depth = format.BitsPerPixel / 8;
            int width = 640;
            int height = 480;

            currentBitmap = new WriteableBitmap(width, height, 96, 96, format, null);
            byte[] pixels = new byte[width * height * depth];
            Random random = new Random();
            random.NextBytes(pixels);
            currentBitmap.WritePixels(new Int32Rect(0, 0, width, height), pixels, width * depth, 0);
            CurrentTiles.Source = currentBitmap;
        }

        private void Test2_Command (object sender, RoutedEventArgs e) {
            TilesTabItem.Focus();
            currentEditable = false;

            var format = PixelFormats.Indexed4;
            int width = 320;
            int height = 200;

            currentBitmap = new WriteableBitmap(width, height, 96, 96, format, Controller.Instance.Palette);
            byte[] pixels = new byte[width * height / 2];
            Random random = new Random();
            random.NextBytes(pixels);
            currentBitmap.WritePixels(new Int32Rect(0, 0, width, height), pixels, width / 2, 0);
            CurrentTiles.Source = currentBitmap;
        }

        private void Test3_Command (object sender, RoutedEventArgs e) {
            TilesTabItem.Focus();
            currentEditable = false;

            var format = PixelFormats.Indexed8;
            int width = 320;
            int height = 200;

            currentBitmap = new WriteableBitmap(width, height, 96, 96, format, Controller.Instance.Palette);
            byte[] pixels = new byte[width * height];
            currentBitmap.WritePixels(new Int32Rect(0, 0, width, height), pixels, width, 0);
            CurrentTiles.Source = currentBitmap;

            currentEditable = true;
        }

        private void DrawPixel (int x, int y, byte color) {
            if (currentBitmap != null && currentEditable) {
                int width = currentBitmap.PixelWidth;
                int height = currentBitmap.PixelHeight;
                if (0 <= x && x < width && 0 <= y && y < height) {
                    var pixels = new byte[] { color };
                    currentBitmap.WritePixels(new Int32Rect(x, y, 1, 1), pixels, 1, 0);
                }
            }
        }

        private void CurrentTilesChange (Point position, MouseButtonState left, MouseButtonState right) {
            if (left == MouseButtonState.Pressed) {
                DrawPixel((int) position.X, (int) position.Y, currentColor);
            } else if (right == MouseButtonState.Pressed) {
                DrawPixel((int) position.X, (int) position.Y, 0);
            }
        }

        private Point GetCoordinates(MouseEventArgs e) {
            var position = e.GetPosition(CurrentTiles);
            var widthFactor = currentBitmap.Width / CurrentTiles.ActualWidth;
            var heightFactor = currentBitmap.Height / CurrentTiles.ActualHeight;
            return new Point(position.X * widthFactor, position.Y * heightFactor);
        }

        private void CurrentTiles_MouseDown (object sender, MouseButtonEventArgs e) {
            CurrentTilesChange(GetCoordinates(e), e.LeftButton, e.RightButton);
        }

        private void CurrentTiles_MouseMove (object sender, MouseEventArgs e) {
            var position = GetCoordinates(e);
            CurrentTilesChange(position, e.LeftButton, e.RightButton);
            StatusBarText.Text = $"Drawing in ({position.X}, {position.Y}) of "
                + $" ({CurrentTiles.Source.Width}, {CurrentTiles.Source.Height})";
        }

        private void CurrentTiles_MouseWheel (object sender, MouseWheelEventArgs e) {
            var next = e.Delta > 0 ? currentColor + 1 : currentColor - 1;
            if (next > 15) {
                currentColor = 1;
            } else if (next < 1) {
                currentColor = 15;
            } else {
                currentColor = (byte) next;
            }
        }
    }
}
