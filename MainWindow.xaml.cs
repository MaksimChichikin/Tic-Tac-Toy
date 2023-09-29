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

namespace Tic_Tac_Toe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Button[,] board;
        private int currentPlayer;
        private bool gameOver;
        public MainWindow()
        {
            InitializeComponent();
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            board = new Button[3, 3]
             {
                { button00, button01, button02 },
                { button10, button11, button12 },
                { button20, button21, button22 }
             };

            currentPlayer = 0;
            gameOver = false;

            foreach (var button in board)
            {
                button.Content = string.Empty;
                button.IsEnabled = true;
            }

            PlayerTurn.Text = "Ход игрока 1 (X)";
        
    }

        private void button00_Click(object sender, RoutedEventArgs e)
        {
            if (gameOver)
                return;

            var button = (Button)sender;
            int row = Grid.GetRow(button);
            int col = Grid.GetColumn(button);

            if (board[row, col].Content == string.Empty)
            {
                board[row, col].Content = currentPlayer % 2 == 0 ? "X" : "O";
                currentPlayer++;

                if (CheckForWinner(row, col))
                {
                    MessageBox.Show($"Игрок {(currentPlayer - 1) % 2 + 1} победил!");
                    gameOver = true;
                }
                else if (currentPlayer == 9)
                {
                    MessageBox.Show("Ничья!");
                    gameOver = true;
                }

                PlayerTurn.Text = $"Ход игрока {currentPlayer % 2 + 1} ({(currentPlayer % 2 == 0 ? "X" : "O")})";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            InitializeBoard();
        }
        private bool CheckForWinner(int row, int col)
        {
            // Проверка по горизонтали
            if (board[row, 0].Content != string.Empty &&
                board[row, 0].Content == board[row, 1].Content &&
                board[row, 1].Content == board[row, 2].Content)
            {
                return true;
            }

            // Проверка по вертикали
            if (board[0, col].Content != string.Empty &&
                board[0, col].Content == board[1, col].Content &&
                board[1, col].Content == board[2, col].Content)
            {
                return true;
            }

            // Проверка по главной диагонали
            if (row == col)
            {
                if (board[0, 0].Content != string.Empty &&
                    board[0, 0].Content == board[1, 1].Content &&
                    board[1, 1].Content == board[2, 2].Content)
                {
                    return true;
                }
            }

            // Проверка по побочной диагонали
            if (row + col == 2)
            {
                if (board[0, 2].Content != string.Empty &&
                    board[0, 2].Content == board[1, 1].Content &&
                    board[1, 1].Content == board[2, 0].Content)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
