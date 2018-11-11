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
            int Players = 5;
            int Turns = 5;

            GamePlay PlayGame = new GamePlay(new Game(Turns, Players));
            //Console.WriteLine(Game.PayoutMatrix[])

            PlayGame.RunGame();

            Console.ReadKey();
        }
    }
}
