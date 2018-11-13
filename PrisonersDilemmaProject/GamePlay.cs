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
            Console.WriteLine("Turns = " + Game.NumTurns + "   players = " + Game.NumPlayers);
        }

        public void RunGame()
        {
            for (int i = 0; i < Game.NumTurns; i++)
            {
                SimulateRound(i);
            }
            RoundResults();
        }
        public void SimulateRound(int turn)
        {
            Game.Turn = turn;
            for (int i = 0; i < RPDGame.PlayerList.Count(); i++)
            {

                Game.TurnPlayerPayoffs[turn, i, i] = 0;
                Game.TurnPlayerChoices[turn, i, i] = 0;
                for (int j = i+1; j< Game.NumPlayers; j++)
                {
                    Game.TurnPlayerChoices[turn,i,j] = 
                        Strategies.ReturnStrategy(RPDGame.PlayerList[i].StrategyType, i, j, RPDGame.PlayerList);
                    Game.TurnPlayerChoices[turn,j,i] = 
                        Strategies.ReturnStrategy(RPDGame.PlayerList[j].StrategyType, j, i, RPDGame.PlayerList);
                    int cPlayer = Game.TurnPlayerChoices[turn, i, j];
                    int oPlayer = Game.TurnPlayerChoices[turn, j, i];


                    Game.TurnPlayerPayoffs[turn, i, j] = Game.PayoutMatrix[cPlayer, oPlayer, 0];
                    Game.TurnPlayerPayoffs[turn, i, Game.NumPlayers] =
                        Game.TurnPlayerPayoffs[turn, i, Game.NumPlayers] + Game.TurnPlayerPayoffs[turn, i, j];
                    Game.TurnPlayerPayoffs[turn, j, i] = Game.PayoutMatrix[cPlayer, oPlayer, 1];
                    Game.TurnPlayerPayoffs[turn, j, Game.NumPlayers] =
                        Game.TurnPlayerPayoffs[turn, j, Game.NumPlayers] + Game.TurnPlayerPayoffs[turn, j, i];

                }
            }
            for (int n = 0; n < Game.NumPlayers; n++)
            {
                RPDGame.PlayerList[n].totalPayoff =
                    RPDGame.PlayerList[n].totalPayoff + Game.TurnPlayerPayoffs[turn, n, Game.NumPlayers];
            }  
        }

        public void RoundResults()
        {   /*
            for (int i = 0; i < Game.NumTurns; i++)
            {
                Console.WriteLine("Turn: " + i);
                for (int j = 0; j < Game.NumPlayers; j++)
                {
                    Console.Write(String.Format("{0,15}", RPDGame.PlayerList[j].StrategyName));
                    for (int k = 0; k < Game.NumPlayers; k++)
                    {
                        if (k != j)
                        {
                            Console.Write(String.Format("{0,15} {1,2}  ", (RPDGame.PlayerList[k].StrategyName), (Game.TurnPlayerPayoffs[i, j, k])));
                        }
                        else
                        {
                            Console.Write(String.Format("{0,20}", "XXXXXXXX   "));
                        }
                    }
                    Console.Write(String.Format("Total = {0,3}", Game.TurnPlayerPayoffs[i, j, Game.NumPlayers]));
                    Console.WriteLine();
                }
            }*/
            //Console.WriteLine();
            //Console.WriteLine();
            int []maxNum = new int[6];
            int []minNum = new int[6];
            int []maxPlayer = new int[6];
            int []minPlayer = new int[6];
            double[] Strats = new double[11];
            
            for (int k = 0; k < Game.NumPlayers; k++)
            {
                if (k < 5)
                {
                    maxNum[k] = RPDGame.PlayerList[k].totalPayoff;
                    maxPlayer[k] = k;
                    minNum[k] = RPDGame.PlayerList[k].totalPayoff;
                    minPlayer[k] = k;
                }
                else
                {
                    maxNum[5] = RPDGame.PlayerList[k].totalPayoff;
                    maxPlayer[5] = k;
                    minNum[5] = RPDGame.PlayerList[k].totalPayoff;
                    minPlayer[5] = k;
                }
                for (int n = 0; n < 5; n++)
                {
                    for (int m = n+1; m < 6; m++)
                    {
                        if (maxNum[n] < maxNum[m])
                        {
                            int t = maxNum[n];
                            maxNum[n] = maxNum[m];
                            maxNum[m] = t;
                            t = maxPlayer[n];
                            maxPlayer[n] = maxPlayer[m];
                            maxPlayer[m] = t;
                        }
                    }
                }
                Strats[RPDGame.PlayerList[k].StrategyType] = Strats[RPDGame.PlayerList[k].StrategyType] + 1;
            }
            
            for (int i = 0; i < 5; i++)
            {
                //Console.WriteLine("i = " + i);
                Console.WriteLine("Place " + (i + 1) + " Player: " + maxPlayer[i] + 
                    " " + RPDGame.PlayerList[maxPlayer[i]].StrategyName + "   Total: " + maxNum[i]);
            }
            for (int x = 0; x < 11; x++)
            {
                //Console.WriteLine(Strats[x]);
                Strats[x] = Strats[x] / Game.NumPlayers;
            }
            Console.WriteLine("Strats proportions: ");
            for (int y = 0; y < 11; y++)
            {
                Console.WriteLine(Player.StratName[y] + ": " + Strats[y] + "  ");
            }
            Console.WriteLine();Console.WriteLine();
        }
    }
}
