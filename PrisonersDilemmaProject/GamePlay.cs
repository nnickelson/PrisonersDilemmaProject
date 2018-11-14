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
        {   
            int []maxNum = new int[6];
            int []minNum = new int[6];
            int []maxPlayer = new int[6];
            int []minPlayer = new int[6];
            double[] Strats = new double[11];
            int[] allScores = new int[Game.NumPlayers];
            int[] allPlayers = new int[Game.NumPlayers];
            
            for (int k = 0; k < Game.NumPlayers; k++)
            {
                allScores[k] = RPDGame.PlayerList[k].totalPayoff;
                allPlayers[k] = k;
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
            double[] numStrats = new double[11];
            for (int x = 0; x < 11; x++)
            {
                numStrats[x] = Strats[x];
                Strats[x] = Strats[x] / Game.NumPlayers;
            }
            Console.WriteLine("Strats proportions: ");
            for (int y = 0; y < 11; y++)
            {
                Console.WriteLine(Player.StratName[y] + ": " + Strats[y] + "  ");
            }
            Console.WriteLine();Console.WriteLine();
            sortAllPlayers(allPlayers, allScores, numStrats);
            Console.WriteLine();
        }

        public void sortAllPlayers(int[]player, int[] score, double[] strats)
        {
            int[] places = new int[11];
            for (int i = 0; i < Game.NumPlayers-1; i++)
            {
                for (int j = i + 1; j < Game.NumPlayers; j++)
                {
                    if (score[j] < score[i])
                    {
                        int t = player[j];
                        player[j] = player[i];
                        player[i] = t;
                        t = score[j];
                        score[j] = score[i];
                        score[i] = t;
                    }
                } 
            }
            
            foreach (int w in player)
            {
                Console.WriteLine("Player " + w + " " + RPDGame.PlayerList[w].StrategyName + " score = " 
                    + RPDGame.PlayerList[w].totalPayoff);
            }
            Console.WriteLine("########################################");
            for (int a = 0; a < Game.NumPlayers; a++)
            {
                //Console.WriteLine("stratType =" + RPDGame.PlayerList[a].StrategyType);
                //Console.WriteLine("a = " + a);
                places[RPDGame.PlayerList[player[a]].StrategyType]
                    = places[RPDGame.PlayerList[player[a]].StrategyType] + a;
            }
            for (int b = 0; b < 11; b++)
            {
                //Console.WriteLine("Places = " + places[b] + "  No strats = " + strats[b]);
                Console.WriteLine(Player.StratName[b] + " average placement: " + places[b]/strats[b]);
            }
            Console.WriteLine("-------------------------");
        }
    }
}
