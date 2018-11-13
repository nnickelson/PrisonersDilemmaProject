using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisonersDilemmaProject
{
    class Program
    {
        static void Main(string[] args)
        {
            int games = 20;
            int Players = 100;
            int Turns = 100;

            for (int i = 0; i < games; i++)
            {
                Console.WriteLine("GAME: " + (i + 1));
                GamePlay PlayGame = new GamePlay(new Game(Turns, Players));
                //Console.WriteLine(Game.PayoutMatrix[])

                PlayGame.RunGame();
            }
            Console.ReadKey();
        }
    }
}
