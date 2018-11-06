using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisonersDilemmaProject
{
    public class GameSetup
    {
        public static int[,,] PayoutMatrix { get; set; }

        private int NumPlayers { get; set; }
        private int S = 0;
        private int T = 5;
        private int R = 3;
        private int P = 1;


        public List<Player> PlayerList { get; set; }
        public int Turns { get; set; }


        public GameSetup(int p)
        {
            this.PlayerList = new List<Player>();
            this.NumPlayers = p;
            setTestPlayers();
            setRandomPlayers();

            PayoutMatrix = new int[,,] { { { R, R }, { S, T } },
                                         { { T, S }, { P, P } } };

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
