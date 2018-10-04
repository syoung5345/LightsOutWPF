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

namespace LightsOutWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private LightsOutGame game;
        public MainWindow()
        {
            game = new LightsOutGame();

            InitializeComponent();

            CreateGrid();
            DrawGrid();
        }
        private void CreateGrid()
        {
            int rectSize = (int)boardCanvas.Width / game.GridSize;
            // Create rectangles for grid
            for (int r = 0; r < game.GridSize; r++)
            {
                for (int c = 0; c < game.GridSize; c++)
                {
                    Rectangle rect = new Rectangle();
                    rect.Fill = Brushes.White;
                    rect.Width = rectSize + 1;
                    rect.Height = rect.Width + 1;
                    rect.Stroke = Brushes.Black;
                    // Store each row and col as a Point
                    rect.Tag = new Point(r, c);
                    // Register event handler
                    rect.MouseLeftButtonDown += Rect_MouseLeftButtonDown;
                    // Put the rectangle at the proper location within the canvas
                    Canvas.SetTop(rect, r * rectSize);
                    Canvas.SetLeft(rect, c * rectSize);
                    // Add the new rectangle to the canvas' children
                    boardCanvas.Children.Add(rect);
                }
            }
        }

        private void Rect_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Rectangle rect = sender as Rectangle;
            var rowCol = (Point)rect.Tag;
            int row = (int)rowCol.X;
            int col = (int)rowCol.Y;
            game.Move(row, col);
            DrawGrid();
            if (game.IsGameOver())
            {
                MessageBox.Show("You Won!");
            }
        }

        private void DrawGrid()
        {
            int index = 0;

            // Set the colors of the rectangles
            for (int r = 0; r < game.GridSize; r++)
            {
                for (int c = 0; c < game.GridSize; c++)
                {
                    Rectangle rect = boardCanvas.Children[index] as Rectangle;
                    index++;
                    if (game.GetGridValue(r, c))
                    {
                        // On
                        rect.Fill = Brushes.White;
                        rect.Stroke = Brushes.Black;
                    }
                    else
                    {
                        // Off
                        rect.Fill = Brushes.Black;
                        rect.Stroke = Brushes.White;
                    }
                }
            }
        }

        private void MenuExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            game.NewGame();
            DrawGrid();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            About aboutForm = new About();
            aboutForm.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            game.NewGame();
            DrawGrid();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void CloseCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        private void NewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            game.NewGame();
            DrawGrid();
        }

        private void OpenCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            About aboutForm = new About();
            aboutForm.ShowDialog();
        }

        private void CloseCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (game.IsGameOver())
            {
                e.CanExecute = true;
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}
