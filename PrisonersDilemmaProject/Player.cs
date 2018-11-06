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
        public int CurrentPlayerChoice { get; set; }
        public int[] CurrentRoundChoices { get; set; }
        public int[] PlayerChoice { get; set; }
        public int[] PayoffPerPlayer { get; set; }
        public List<int[]> StoredOpponentChoices { get; set; }
        public List<int[]> StoredPlayerChoices { get; set; }
        public int[] OpponentStrategy { get; set; }
        public List<int> TotalRoundPayoff { get; set; }
        public List<int[]> StoredPayoffResults { get; set; }
        public static string[] StratName = {"Always Defect", "Always Cooperate", "Tit For Tat 1", "Tit For Tat 2", "Tit For Tat 3", 
            "Cheater 1", "Cheater 2", "Cheater 3", "Permanent Retaliation"};
        public string StrategyName { get; set; }

        
        public Player(int strat, int players)
        {
            this.StoredOpponentChoices = new List<int[]>();
            this.StoredPlayerChoices = new List<int[]>();
            this.OpponentStrategy = new int[players];
            this.TotalRoundPayoff = new List<int>();
            this.StoredPayoffResults = new List<int[]>();
            this.StrategyType = strat;
            this.StrategyName = StratName[strat];
            this.CurrentRoundChoices = new int[players];
            this.PlayerChoice = new int[players];
            this.PayoffPerPlayer = new int[players];
        }
    }
}
