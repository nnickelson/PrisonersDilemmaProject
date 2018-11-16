using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisonersDilemmaProject
{
    public class Game
    {
        //Stores a payoff matrix based on the private ints from the payoff numbers section
        public static int[,,] PayoutMatrix { get; set; }
        
        // Stores all player choices made during a game. size = [#turns][#players][#players]
        // calling: TurnPlayerChoices[turn #][active player choice][opponent]
        public static int[,,] TurnPlayerChoices { get; set; } 

        // Stored all player winnings earned during a game. size = [#turns][#players][#players]
        // calling: TurnPlayerPayoffs[turn #][amount active player won][opponent]
        public static int[,,] TurnPlayerPayoffs { get; set; }


        public static int Turn = 0;

        public static int NumPlayers { get; set; }
        public static int NumTurns { get; set; }
        public List<Player> PlayerList { get; set; }
        public Random rnd = new Random();
       
        //payoff numbers
        private int S = 0;
        private int T = 5;
        private int R = 3;
        private int P = 1;

        public Game(int turns, int players)
        {
            this.PlayerList = new List<Player>();
            NumPlayers = players;
            NumTurns = turns;
            setTestPlayers();
            setRandomPlayers(players);

            PayoutMatrix = new int[,,] { { { R, R }, { S, T } },
                                         { { T, S }, { P, P } } };
            TurnPlayerChoices = new int[turns, players, players];
            TurnPlayerPayoffs = new int[turns, players, players + 1];
            for (int i = 0; i < turns; i++)
                for (int j = 0; j < players; j++) 
                    TurnPlayerPayoffs[i, j, players] = 0;

        }

        //sets the list of players participating in all tournaments
        private void setTestPlayers()
        {
            PlayerList.Add(new Player(0));
            PlayerList.Add(new Player(1));
            PlayerList.Add(new Player(2));
            PlayerList.Add(new Player(3));
            PlayerList.Add(new Player(4));
            PlayerList.Add(new Player(5));
            PlayerList.Add(new Player(6));
            PlayerList.Add(new Player(7));
            PlayerList.Add(new Player(8));
            PlayerList.Add(new Player(9));
            PlayerList.Add(new Player(10));
        }

        //Fills the rest of the player list with randomly selected strategies
        private void setRandomPlayers(int players)
        {
            int rndPlayers = players - PlayerList.Count();
            for (int i = 0; i < rndPlayers; i++) 
            {
                PlayerList.Add(new Player(this.rnd.Next(2, 8)));
            }
            return;
        }
    }
}
