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
            int Players = 6;
            int Turns = 5;

            GamePlay PlayGame = new GamePlay(new Game(Turns, Players));

            PlayGame.RunGame();

            foreach (Player player in PlayGame.RPDGame.PlayerList)
            {
                //Console.WriteLine("player: " + GamePrep.PlayerList.IndexOf(player) + "------" + player.StrategyType);
                foreach(var item in player.StoredPayoffResults)
                {
                    //Console.WriteLine("Round of play");
                    foreach (var item1 in item)
                    {
                        //Console.Write(item1 + " ");
                    }
                    //Console.WriteLine();
                    Console.WriteLine();
                }
            }

            Console.ReadKey();
        }
    }
}
