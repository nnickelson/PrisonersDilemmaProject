using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



/// <summary>
/// This Class file evaluates the strategy of the player and returns a choice made by that player
/// </summary>
namespace PrisonersDilemmaProject
{
    static class Strategies
    {
        public static int ReturnStrategy(int strategy, int player, int opponent)
        {
            int strat = 0;
            switch (strategy)
            {
                case 0:
                    strat = AlwaysDefect();
                    break;
                case 1:
                    strat = AlwaysCooperate();
                    break;
                case 2:
                    strat = TitForTat1(player, opponent);
                    break;
                case 3:
                    strat = TitForTat2(player, opponent);
                    break;
                case 4:
                    strat = TitForTat3(player, opponent);
                    break;
                case 5:
                    strat = Cheater1(player, opponent);
                    break;
                case 6:
                    strat = Cheater2(player, opponent);
                    break;
                case 7:
                    strat = Cheater3(player, opponent);
                    break;
                case 8:
                    strat = PermanentRetaliation(player, opponent);
                    break;
                case 9:
                    strat = Random(player, opponent);
                    break;
            }
            return strat;
        }

        //Strategy 0 - Always Defect
        private static int AlwaysDefect()
        {
            return 1;
        }

        //Strategy 2 - Always Cooperate
        private static int AlwaysCooperate()
        {
            return 0;
        }

        //Strategy 3 - TitforTat1: player defects next turn after opponent's 1st defect, else they cooperate
        private static int TitForTat1(int player, int opponent)
        {
            if (Game.Turn > 0)
            {
                int opChoice = Game.TurnPlayerChoices[Game.Turn - 1, opponent, player];
                return opChoice;
            }
            return 0;
        }

        //Strategy 4 - TitforTat2: player defects next turn after opponent's 2nd consecutive defect, else they cooperate
        private static int TitForTat2(int player, int opponent)
        {
            if (Game.Turn > 1)
            {
                if (Game.TurnPlayerChoices[Game.Turn - 1, opponent, player] == 1 &&
                        Game.TurnPlayerChoices[Game.Turn - 2, opponent, player] == 1) 
                {
                    return 1;
                }
            }
            return 0;
        }

        //Strategy 5 - TitForTat3: player defects next turn after opponent's 3rd consecutive defect, else they cooperate
        private static int TitForTat3(int player, int opponent)
        {
            if (Game.Turn > 2)
            {
                if (Game.TurnPlayerChoices[Game.Turn - 1, opponent, player] == 1 &&
                        Game.TurnPlayerChoices[Game.Turn - 2, opponent, player] == 1 &&
                        Game.TurnPlayerChoices[Game.Turn - 3, opponent, player] == 1) 
                {
                    return 1;
                }
            }
            return 0;
        }

        //Incomplete
        private static int Cheater1(int player, int opponent)
        {
            if (Game.Turn > 0)
            {
                int lChoice = Game.TurnPlayerChoices[Game.Turn-1, player, opponent];
                if (lChoice == 1)
                {
                    Console.WriteLine("last pick defect, this pick coop");
                    return 0;
                }
                else
                {
                    for (int i = Game.Turn -1; i > -1; i--)
                    {
                        //testing testing
                    }
                }
            }
            return 0;
        }

        //Incomplete
        private static int Cheater2(int player, int opponent)
        {
            return 0;
        }

        //Incomplete
        private static int Cheater3(int player, int opponent)
        {
            return 0;
        }

        // Strategy 8 - Permanent Retaliation: after first opponent's defect, player will always defect 
        private static int PermanentRetaliation(int player, int opponent)
        {
            if (Game.Turn > 0)
            {
                if (Game.TurnPlayerChoices[Game.Turn - 1, player, opponent] == 1)
                {
                    return 1;
                }
                else if (Game.TurnPlayerChoices[Game.Turn-1, opponent, player] == 1)
                {
                    return 1;
                }
                
            }
            return 0;
        }

        // Strategy 9 - Random Selection: Randomly chooses to defect or cooperate
        private static int Random(int player, int opponent)
        {

            int rnd = Player.r.Next(0, 2);
            Console.WriteLine(rnd);
            return rnd;
        }
    }
}
