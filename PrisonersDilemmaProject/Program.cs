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

            GameSetup GamePrep = new GameSetup(Players);
            GamePlay Game = new GamePlay();

            Game.RunGame(8, GamePrep.PlayerList);

            foreach (Player player in GamePrep.PlayerList)
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
