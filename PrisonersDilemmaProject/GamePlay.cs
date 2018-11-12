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
                    

                    Game.TurnPlayerPayoffs[turn,i,j] = Game.PayoutMatrix[cPlayer, oPlayer, 0];
                    Game.TurnPlayerPayoffs[turn,j,i] = Game.PayoutMatrix[cPlayer, oPlayer, 1];
                    
                }
            }  
        }

        public void RoundResults()
        {
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
                    Console.WriteLine();
                }
            }
            Console.WriteLine();
        }
    }
}
