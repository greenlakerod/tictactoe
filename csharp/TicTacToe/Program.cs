using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();

            bool play = true;

            while (play)
            {
                game.Start();

                while (!game.GameOver)
                {
                    game.Play();
                    game.DrawGrid();
                }

                Console.WriteLine();
                Console.Write("Play again? (y/n): ");

                string response = Console.ReadLine();
                play = !string.IsNullOrWhiteSpace(response) ? response.ToLower() == "y" : false;
                Console.WriteLine();
            }
        }
    }
}
