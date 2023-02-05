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
using Kitsune.Controls;
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
        private Color gridColor = new Color() { A = 0xFF, R = 0xFF, G = 0xFF, B = 0xFF };

        private void Test1_Command (object sender, RoutedEventArgs e) {
            TilesTabItem.Focus();
            currentEditable = false;

            currentBitmap = ImageTools.CreateRGB(320, 200);
            currentBitmap.WriteRandomPixels();
            CurrentTiles.Source = currentBitmap;
        }

        private void Test2_Command (object sender, RoutedEventArgs e) {
            TilesTabItem.Focus();
            currentEditable = false;

            currentBitmap = ImageTools.CreateIndexed4(320, 200, Controller.Instance.Palette);
            currentBitmap.WriteRandomPixels();
            CurrentTiles.Source = currentBitmap;
        }

        private void Test3_Command (object sender, RoutedEventArgs e) {
            TilesTabItem.Focus();
            currentEditable = false;

            currentBitmap = ImageTools.CreateIndexed8(320, 200, Controller.Instance.Palette);
            currentBitmap.ClearPixels();
            CurrentTiles.Source = currentBitmap;
            TilesGrid.Source = ImageTools.CreateGrid(currentBitmap, gridColor, 16, 1);
            currentEditable = true;
        }

        private void CurrentTilesChange (Point position, MouseButtonState left, MouseButtonState right) {
            if (currentBitmap != null && currentEditable) {
                if (left == MouseButtonState.Pressed) {
                    currentBitmap.WritePixel(position, currentColor);
                } else if (right == MouseButtonState.Pressed) {
                    currentBitmap.WritePixel(position, 0);
                }
            }
        }

        private void CurrentTiles_MouseDown (object sender, MouseButtonEventArgs e) {
            CurrentTilesChange(CurrentTiles.GetSourceCoordinates(e), e.LeftButton, e.RightButton);
        }

        private void CurrentTiles_MouseMove (object sender, MouseEventArgs e) {
            var position = CurrentTiles.GetSourceCoordinates(e);
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

        private int test_separation = 16, test_offset = 1;

        private void Window_KeyDown (object sender, KeyEventArgs e) {
            if (TilesTabItem.IsFocused) {
                bool updateGrid = false;

                switch (e.Key) {
                    case Key.F5:
                        RenderOptions.SetBitmapScalingMode(TilesGrid, BitmapScalingMode.Fant);
                        e.Handled = true;
                        break;

                    case Key.F6:
                        RenderOptions.SetBitmapScalingMode(TilesGrid, BitmapScalingMode.HighQuality);
                        e.Handled = true;
                        break;

                    case Key.F7:
                        RenderOptions.SetBitmapScalingMode(TilesGrid, BitmapScalingMode.Linear);
                        e.Handled = true;
                        break;

                    case Key.F8:
                        RenderOptions.SetBitmapScalingMode(TilesGrid, BitmapScalingMode.NearestNeighbor);
                        e.Handled = true;
                        break;

                    case Key.D1:
                        test_separation = 2;
                        updateGrid = true;
                        e.Handled = true;
                        break;

                    case Key.D2:
                        test_separation = 4;
                        updateGrid = true;
                        e.Handled = true;
                        break;

                    case Key.D3:
                        test_separation = 8;
                        updateGrid = true;
                        e.Handled = true;
                        break;

                    case Key.D4:
                        test_separation = 16;
                        updateGrid = true;
                        e.Handled = true;
                        break;

                    case Key.D5:
                        test_separation = 24;
                        updateGrid = true;
                        e.Handled = true;
                        break;

                    case Key.D6:
                        test_separation = 32;
                        updateGrid = true;
                        e.Handled = true;
                        break;

                    case Key.Q:
                        test_offset = 1;
                        updateGrid = true;
                        e.Handled = true;
                        break;

                    case Key.W:
                        test_offset = 2;
                        updateGrid = true;
                        e.Handled = true;
                        break;

                    case Key.E:
                        test_offset = 3;
                        updateGrid = true;
                        e.Handled = true;
                        break;

                    case Key.R:
                        test_offset = 4;
                        updateGrid = true;
                        e.Handled = true;
                        break;
                }

                if (updateGrid) {
                    TilesGrid.Source = ImageTools.CreateGrid(currentBitmap,
                        gridColor, test_separation, test_offset);
                }
            }
        }
    }
}
