using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    internal class Game
    {
        string[,] _grid;
        private int _size = 0;
        private int _cellsFilled = 0;
        private string _currentCharacter = "X";
        private bool _gameOver = false;
        private bool _winner = false;

        public string CurrentCharacter { get { return _currentCharacter; } }
        public bool GameOver { get { return _gameOver; } }
        public bool Winner { get { return _winner; } }

        public void Start()
        {
            _currentCharacter = "X";
            _gameOver = false;
            _winner = false;
            _grid = null;
            _size = 0;
            _cellsFilled = 0;

            bool valid = false;

            while (!valid)
            {
                Console.Write("Enter number of grid rows/columns: ");

                if (int.TryParse(Console.ReadLine(), out _size))
                {
                    if ((_size >= 3) && ((_size % 2) == 1))
                    {
                        _grid = new string[_size, _size];
                        valid = true;
                    }
                    else
                    {
                        Console.WriteLine("Value must be an odd number greater than or equal to 3.");
                    }
                }
            }
        }

        public void Play()
        {
            Console.WriteLine("Play {0}: ", _currentCharacter);

            int row;
            Console.Write("Row: ");
            if (ValidateCoordinate(Console.ReadLine(), out row))
            {
                int column;
                Console.Write("Column: ");

                if (ValidateCoordinate(Console.ReadLine(), out column))
                {
                    Play(row - 1, column - 1);
                }
            }
        }

        private void Play(int row, int column)
        {
            if (!IsCellEmpty(row, column))
            {
                Console.WriteLine("That location is already played.");
                return;
            }

            SetCell(row, column);

            if (CheckRow(row) || CheckColumn(column) || CheckDiagonal(row, column))
            {
                _winner = true;
                _gameOver = true;

                Console.WriteLine("\n{0} wins!", _currentCharacter);
            }
            else if (GridFull())
            {
                _gameOver = true;
                Console.WriteLine("\nGame over.");
            }

            if (!_gameOver)
            {
                SetCurrentCharacter();
            }
        }

        public void DrawGrid()
        {
            Console.WriteLine();

            for (int r = 0; r < _size; r++)
            {
                string s = "";

                if (r > 0)
                {
                    for (int c = 0; c < _size; c++)
                    {
                        s += "----";
                        //s += "____";
                    }

                    Console.WriteLine(s);
                }

                s = "";
                for (int c = 0; c < _size; c++)
                {
                    s += string.Format(" {0} ", !string.IsNullOrEmpty(_grid[r, c]) ? _grid[r, c] : " ");
                    if (c < _size - 1)
                    {
                        s += "|";
                    }
                }

                Console.WriteLine(s);

                
            }

            Console.WriteLine();
        }

        private bool IsCellEmpty(int r, int c)
        {
            return string.IsNullOrEmpty(_grid[r, c]);
        }

        private void SetCell(int r, int c)
        {
            _grid[r, c] = _currentCharacter;
            _cellsFilled++;
        }

        private bool GridFull()
        {
            return _cellsFilled == (_size*_size);
        }

        private void SetCurrentCharacter()
        {
            _currentCharacter = _currentCharacter == "X" ? "Y" : "X";
        }

        private bool CellContainsCurrentCharacter(int r, int c)
        {
            return _grid[r, c] == _currentCharacter;
        }

        private bool CheckRow(int r)
        {
            for (int c = 0; c < _size; c++)
            {
                if (!CellContainsCurrentCharacter(r, c))
                {
                    return false;
                }
            }

            return true;
        }

        private bool CheckColumn(int c)
        {
            for (int r = 0; r < _size; r++)
            {
                if (!CellContainsCurrentCharacter(r, c))
                {
                    return false;
                }
            }

            return true;
        }

        private bool CheckDiagonal(int r, int c)
        {
            bool result = false;

            if (r == c)
            {
                result = (r == (_size / 2) + 1) ? (CheckDiagonal1() || CheckDiagonal2()) : CheckDiagonal1();
            }
            else if ((r + c) == (_size - 1))
            {
                result = CheckDiagonal2();
            }

            return result;
        }

        private bool CheckDiagonal1()
        {
            for (int r = 0, c = 0; r < _size; r++, c++)
            {
                if (!CellContainsCurrentCharacter(r, c))
                {
                    return false;
                }
            }

            return true;
        }

        private bool CheckDiagonal2()
        {
            for (int r = 0, c = (_size - 1); r < _size; r++, c--)
            {
                if (!CellContainsCurrentCharacter(r, c))
                {
                    return false;
                }
            }

            return true;
        }

        private bool ValidateCoordinate(string input, out int dimension)
        {
            if (int.TryParse(input, out dimension))
            {
                if (dimension <= 0 || dimension > _size)
                {
                    Console.WriteLine("Value out of range.");
                    return false;
                }

                return true;
            }

            Console.WriteLine("Invalid value.");
            return false;
        }
    }
}
