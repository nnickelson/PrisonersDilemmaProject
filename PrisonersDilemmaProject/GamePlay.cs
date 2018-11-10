using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisonersDilemmaProject
{
    public class GamePlay
    {
        public Game RPDGame;

        public GamePlay(Game game)
        {
            this.RPDGame = game;
            Console.WriteLine("Turns = " + RPDGame.Turns + "   players = " + RPDGame.NumPlayers);
        }

        public void RunGame()
        {
            for (int i = 0; i < RPDGame.Turns; i++)
            {
                SimulateRound();
            }
        }
        public void SimulateRound()
        {
            foreach (Player p in RPDGame.PlayerList)
            {
                p.StoredPlayerChoices.Add(new int[RPDGame.PlayerList.Count]);
                p.StoredOpponentChoices.Add(new int[RPDGame.PlayerList.Count]);
                p.StoredPayoffResults.Add(new int[RPDGame.PlayerList.Count]);
            }
            int turn = RPDGame.PlayerList[0].StoredPlayerChoices.Count - 1;
            Console.WriteLine("Turn = " + (turn + 1));
            for (int i = 0; i < RPDGame.PlayerList.Count(); i++)
            {

                RPDGame.PlayerList[i].StoredPayoffResults[turn][i] = 0;
                RPDGame.PlayerList[i].StoredPlayerChoices[turn][i] = 0;
                for (int j = i+1; j< RPDGame.PlayerList.Count(); j++)
                {
                    RPDGame.PlayerList[i].StoredPlayerChoices[turn][j] = Strategies.ReturnStrategy(RPDGame.PlayerList, RPDGame.PlayerList[i].StrategyType, i, j);
                    RPDGame.PlayerList[j].StoredPlayerChoices[turn][i] = Strategies.ReturnStrategy(RPDGame.PlayerList, RPDGame.PlayerList[j].StrategyType, j, i);

                    int cPlayer = RPDGame.PlayerList[i].StoredPlayerChoices[turn][j];
                    int oPlayer = RPDGame.PlayerList[j].StoredPlayerChoices[turn][i];

                    //Console.WriteLine("current player = " + i + " " + cPlayer);
                    //Console.WriteLine("opposing player = " + oPlayer);

                    RPDGame.PlayerList[i].StoredOpponentChoices[turn][j] = oPlayer;
                    RPDGame.PlayerList[j].StoredOpponentChoices[turn][i] = cPlayer;

                    RPDGame.PlayerList[i].StoredPayoffResults[turn][j] = 
                        Game.PayoutMatrix[cPlayer, oPlayer, 0];

                    RPDGame.PlayerList[j].StoredPayoffResults[turn][i] = 
                        Game.PayoutMatrix[cPlayer, oPlayer, 1];

                    RPDGame.PlayerList[i].OpponentStrategy[j] = RPDGame.PlayerList[j].StrategyType;
                    RPDGame.PlayerList[j].OpponentStrategy[i] = RPDGame.PlayerList[i].StrategyType;
                    
                }
            }
            int m = 0;
            foreach (Player player in RPDGame.PlayerList)
            {
                Console.Write("{0,-20}", player.StrategyName );
                int n = 0;
                foreach (int num in player.StoredPayoffResults[turn])
                {
                    //Console.WriteLine("m,n = "+ m + "  " + n);
                    if (m != n)
                    {
                        Console.Write("{0,-17} {1,2:N1}  ", Player.StratName[player.OpponentStrategy[n]], num);
                    }
                    else
                    {
                        Console.Write("{0,-23}", ("XXXXXXXXXXXXXXXXXXXXXX"));
                    }
                    n++;
                }
                Console.WriteLine();
                
                player.TotalRoundPayoff.Add(player.PayoffPerPlayer.Sum());
                m++;
            }
            Console.WriteLine();
          
            for (int x = 0; x < RPDGame.PlayerList.Count; x++)
            {
                for (int y = 0; y < RPDGame.PlayerList[x].StoredPlayerChoices.Count; y++)
                {
                    //Console.WriteLine("length = " + players[x].StoredPlayerChoices[y].Length);
                    for (int z = 0; z < RPDGame.PlayerList[x].StoredPlayerChoices[y].Length; z++)
                    {
                        //Console.Write("   y = " + y + "    ");
                        //Console.Write("   z = " + z + "    ");
                        Console.Write(RPDGame.PlayerList[x].StoredPlayerChoices[y][z]);
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
        }
    }
}
