using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisonersDilemmaProject
{
    public class Game
    {
        public static int[,,] PayoutMatrix { get; set; }
        
        // Stores all player choices made during a game. size = [#turns][#players][#players]
        // calling: TurnPlayerChoices[turn #][active player choice][opponent]
        public static int[,,] TurnPlayerChoices { get; set; } 

        // Stored all player winnings earned during a game. size = [#turns][#players][#players]
        // calling: TurnPlayerPayoffs[turn #][amount active player won][opponent]
        public static int[,,] TurnPlayerPayoffs { get; set; }

        public int NumPlayers { get; set; }
        public int Turns { get; set; }
        public List<Player> PlayerList { get; set; }
       
        //payoff numbers
        private int S = 0;
        private int T = 5;
        private int R = 3;
        private int P = 1;


       

        public Game(int turns, int players)
        {
            this.PlayerList = new List<Player>();
            this.NumPlayers = players;
            this.Turns = turns;
            setTestPlayers();
            setRandomPlayers();

            PayoutMatrix = new int[,,] { { { R, R }, { S, T } },
                                         { { T, S }, { P, P } } };
            TurnPlayerChoices = new int[turns, players, players];
            TurnPlayerPayoffs = new int[turns, players, players];

        }

        private void setTestPlayers()
        {
            PlayerList.Add(new Player(0, this.NumPlayers));
            PlayerList.Add(new Player(1, this.NumPlayers));
            PlayerList.Add(new Player(2, this.NumPlayers));
            PlayerList.Add(new Player(3, this.NumPlayers));
            PlayerList.Add(new Player(4, this.NumPlayers));
            PlayerList.Add(new Player(5, this.NumPlayers));
        }

        private void setRandomPlayers()
        {
            return;
            //while (this.PlayerList.Count() < this.NumPlayers)
            //{
            //    Console.WriteLine("stuff");
            //}
        }
    }
}
