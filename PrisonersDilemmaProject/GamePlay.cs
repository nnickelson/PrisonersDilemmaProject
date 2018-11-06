using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisonersDilemmaProject
{
    public class GamePlay
    {
        public void RunGame(int turns, List<Player> players)
        {
            for (int i = 0; i < turns; i++)
            {
                SimulateRound(players);
            }
        }
        public void SimulateRound(List<Player> players)
        {
            foreach (Player p in players)
            {
                p.StoredPlayerChoices.Add(new int[players.Count]);
                p.StoredOpponentChoices.Add(new int[players.Count]);
                p.StoredPayoffResults.Add(new int[players.Count]);
            }
            int turn = players[0].StoredPlayerChoices.Count - 1;
            Console.WriteLine("Turn = " + (turn + 1));
            for (int i = 0; i < players.Count(); i++)
            {
                
                players[i].StoredPayoffResults[turn][i] = 0;
                players[i].StoredPlayerChoices[turn][i] = 0;
                for (int j = i+1; j<players.Count(); j++)
                {
                    players[i].StoredPlayerChoices[turn][j] = Strategies.ReturnStrategy(players, players[i].StrategyType, i, j);
                    players[j].StoredPlayerChoices[turn][i] = Strategies.ReturnStrategy(players, players[j].StrategyType, j, i);

                    int cPlayer = players[i].StoredPlayerChoices[turn][j];
                    int oPlayer = players[j].StoredPlayerChoices[turn][i];

                    //Console.WriteLine("current player = " + i + " " + cPlayer);
                    //Console.WriteLine("opposing player = " + oPlayer);

                    players[i].StoredOpponentChoices[turn][j] = oPlayer;
                    players[j].StoredOpponentChoices[turn][i] = cPlayer;
                    
                    players[i].StoredPayoffResults[turn][j] = 
                        GameSetup.PayoutMatrix[cPlayer, oPlayer, 0];
                   
                    players[j].StoredPayoffResults[turn][i] = 
                        GameSetup.PayoutMatrix[cPlayer, oPlayer, 1];

                    players[i].OpponentStrategy[j] = players[j].StrategyType;
                    players[j].OpponentStrategy[i] = players[i].StrategyType;
                    
                }
            }
            int m = 0;
            foreach (Player player in players)
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
          
            for (int x = 0; x < players.Count; x++)
            {
                for (int y = 0; y <  players[x].StoredPlayerChoices.Count; y++)
                {
                    //Console.WriteLine("length = " + players[x].StoredPlayerChoices[y].Length);
                    for (int z = 0; z < players[x].StoredPlayerChoices[y].Length; z++)
                    {
                        //Console.Write("   y = " + y + "    ");
                        //Console.Write("   z = " + z + "    ");
                        Console.Write(players[x].StoredPlayerChoices[y][z]);
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
        }
    }
}
