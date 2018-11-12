using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisonersDilemmaProject
{
    public class Player
    {
        public int StrategyType { get; set; }
        public static string[] StratName = {"Only Defect", "Only Cooperate", "Tit For Tat 1", "Tit For Tat 2", "Tit For Tat 3", 
            "Cheater 1", "Cheater 2", "Cheater 3", "P Retaliation", "Random"};
        public string StrategyName { get; set; }
        public static Random r = new Random();
        public int[] playerCheck1;
        public int[] playerCheck2;


        public Player(int strat)
        {
            this.StrategyType = strat;
            this.StrategyName = StratName[strat];
            this.playerCheck1 = new int[Game.NumPlayers];
            this.playerCheck2 = new int[Game.NumPlayers];
        }
    }
}
