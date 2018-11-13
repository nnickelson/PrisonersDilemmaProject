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
        public static int ReturnStrategy(int strategy, int player, int opponent, List<Player> pList)
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
                    strat = Cheater1(player, opponent, pList);
                    break;
                case 6:
                    strat = Cheater2(player, opponent, pList);
                    break;
                case 7:
                    strat = Cheater3(player, opponent, pList);
                    break;
                case 8:
                    strat = PermanentRetaliation(player, opponent);
                    break;
                case 9:
                    strat = Random(player, opponent);
                    break;
                case 10:
                    strat = Conditional(player, opponent);
                    break;
            }
            return strat;
        }

        //Strategy 0 - Always Defect
        private static int AlwaysDefect()
        {
            return 1;
        }

        //Strategy 1 - Always Cooperate
        private static int AlwaysCooperate()
        {
            return 0;
        }

        //Strategy 2 - TitforTat1: player defects next turn after opponent's 1st defect, else they cooperate
        private static int TitForTat1(int player, int opponent)
        {
            if (Game.Turn > 0)
            {
                int opChoice = Game.TurnPlayerChoices[Game.Turn - 1, opponent, player];
                return opChoice;
            }
            return 0;
        }

        //Strategy 3 - TitforTat2: player defects next turn after opponent's 2nd consecutive defect, else they cooperate
        private static int TitForTat2(int player, int opponent)
        {
            if (Game.Turn > 1)
                if (Game.TurnPlayerChoices[Game.Turn - 1, opponent, player] == 1 &&
                        Game.TurnPlayerChoices[Game.Turn - 2, opponent, player] == 1) 
                    return 1;
            return 0;
        }

        //Strategy 4 - TitForTat3: player defects next turn after opponent's 3rd consecutive defect, else they cooperate
        private static int TitForTat3(int player, int opponent)
        {
            if (Game.Turn > 2)
                if (Game.TurnPlayerChoices[Game.Turn - 1, opponent, player] == 1 &&
                        Game.TurnPlayerChoices[Game.Turn - 2, opponent, player] == 1 &&
                        Game.TurnPlayerChoices[Game.Turn - 3, opponent, player] == 1) 
                    return 1;
            return 0;
        }

        // Strategy 5 - Cheater1 : Tries to defect one time and checks for retaliation
        // If there is retaliation, the player will try to cooperate
        // If cooperation fails, then fallabck to defection as a last resort
        private static int Cheater1(int player, int opponent, List<Player> pList)
        {
            int rtValue = 0;
            if (Game.Turn == 0)
            {
                pList[player].playerCheck1[opponent] = 0;
                pList[player].playerCheck2[opponent] = 0;
            }
            if (Game.Turn > 0 && pList[player].playerCheck1[opponent] == 0)
            {
                int num = checkLastNTurns(player, opponent, 3);
                if (pList[player].playerCheck2[opponent] == 0)
                    pList[player].playerCheck2[opponent] = num;
                else
                    pList[player].playerCheck2[opponent] = 2;
            }
            if (Game.Turn > 0)
            {
                int num = pList[player].playerCheck2[opponent];
                if (num == 0)
                {
                    if (pList[player].playerCheck1[opponent] == 0)  rtValue = 0; 
                    if (pList[player].playerCheck1[opponent] == 1)  rtValue = 1; 
                    if (pList[player].playerCheck1[opponent] == 2)  rtValue = 0; 
                }
                if (num == 1)  rtValue = 0;   
                if (num == 2)  rtValue = 1; 
            }
            pList[player].playerCheck1[opponent] = (pList[player].playerCheck1[opponent] + 1) % 3;
            return rtValue;
        }

        // Strategy 6 - Incomplete
        private static int Cheater2(int player, int opponent, List<Player> pList)
        {
            int rtValue = 0;
            if (Game.Turn == 0)
            {
                pList[player].playerCheck1[opponent] = 0;
                pList[player].playerCheck2[opponent] = 0;
            }
            if (Game.Turn > 0 && pList[player].playerCheck1[opponent] == 0)
            {
                int num = checkLastNTurns(player, opponent, 4);
                if (pList[player].playerCheck2[opponent] == 0)
                    pList[player].playerCheck2[opponent] = num;
                else
                    pList[player].playerCheck2[opponent] = 2;
            }
            if (Game.Turn > 0)
            {
                int num = pList[player].playerCheck2[opponent];
                if (num == 0)
                {
                    if (pList[player].playerCheck1[opponent] == 0) rtValue = 0;
                    if (pList[player].playerCheck1[opponent] == 1) rtValue = 1;
                    if (pList[player].playerCheck1[opponent] == 2) rtValue = 1;
                    if (pList[player].playerCheck1[opponent] == 3) rtValue = 0;
                }
                if (num == 1) rtValue = 0;
                if (num == 2) rtValue = 1;
            }
            pList[player].playerCheck1[opponent] = (pList[player].playerCheck1[opponent] + 1) % 4;
            return rtValue;
        }
        // Strategy 7 - Incomplete
        private static int Cheater3(int player, int opponent, List<Player> pList)
        {
            int rtValue = 0;
            if (Game.Turn == 0)
            {
                pList[player].playerCheck1[opponent] = 0;
                pList[player].playerCheck2[opponent] = 0;
            }
            if (Game.Turn > 0 && pList[player].playerCheck1[opponent] == 0)
            {
                int num = checkLastNTurns(player, opponent, 5);
                if (pList[player].playerCheck2[opponent] == 0)
                    pList[player].playerCheck2[opponent] = num;
                else
                    pList[player].playerCheck2[opponent] = 2;
            }
            if (Game.Turn > 0)
            {
                int num = pList[player].playerCheck2[opponent];
                if (num == 0)
                {
                    if (pList[player].playerCheck1[opponent] == 0) rtValue = 0;
                    if (pList[player].playerCheck1[opponent] == 1) rtValue = 1;
                    if (pList[player].playerCheck1[opponent] == 2) rtValue = 1;
                    if (pList[player].playerCheck1[opponent] == 3) rtValue = 1;
                    if (pList[player].playerCheck1[opponent] == 4) rtValue = 0;
                }
                if (num == 1) rtValue = 0;
                if (num == 2) rtValue = 1;
            }
            pList[player].playerCheck1[opponent] = (pList[player].playerCheck1[opponent] + 1) % 5;
            return rtValue;
        }

        // Strategy 8 - Permanent Retaliation: after first opponent's defect, player will always defect 
        private static int PermanentRetaliation(int player, int opponent)
        {
            if (Game.Turn > 0)
                if (Game.TurnPlayerChoices[Game.Turn - 1, player, opponent] == 1) 
                    return 1;
                else if (Game.TurnPlayerChoices[Game.Turn - 1, opponent, player] == 1) 
                    return 1;
            return 0;
        }

        // Strategy 9 - Random Selection: Randomly chooses to defect or cooperate
        private static int Random(int player, int opponent)
        {
            int rnd = Player.r.Next(0, 2);
            //Console.WriteLine(rnd);
            return rnd;
        }

        //Strategy 10 - Conditional Probability
        private static int Conditional(int player, int opponent)
        {
            if (Game.Turn < 2) return 0;
            int totalC = 0;
            int totalD = 0;
            int numC_afterC = 0;
            int numD_afterC = 0;
            int numC_afterD = 0;
            int numD_afterD = 0;
            for (int i = 0; i < Game.Turn - 1; i++)
            {
                for (int j = 0; j < Game.NumPlayers; j++)
                {
                    if (j == opponent) continue;
                    if (Game.TurnPlayerChoices[i, j, opponent] == 0)
                    {
                        if (Game.TurnPlayerChoices[i + 1, opponent, j] == 0)
                            numC_afterC++;
                        else
                            numD_afterC++;
                        totalC++;

                    }
                    if (Game.TurnPlayerChoices[i, j, opponent] == 1)
                    {
                        if (Game.TurnPlayerChoices[i + 1, opponent, j] == 0)
                            numC_afterD++;
                        else
                            numD_afterD++;
                        totalD++;
                    }
                }
            }
            //Console.WriteLine("Opponent " + "  " + opponent + " percentages");
            //Console.WriteLine("Coop after Coop = " + (numC_afterC * 100.0 / totalC));
            //Console.WriteLine("Def after Coop = " + (numD_afterC * 100.0 / totalC));
            //Console.WriteLine("Coop after Def = " + (numC_afterD * 100.0 / totalD));
            //Console.WriteLine("Def after Def = " + (numD_afterD * 100.0 / totalD));
            //Console.WriteLine("--------------------------------------------------");
            
            if (totalD != 0)
                if (numC_afterD*1.0 / totalD > 0.5) return 1;
            if (totalC != 0)
                if (numD_afterC*1.0 / totalC > 0.5) return 1;
            return 0;
        }


        // Check last N turns for defection
        private static int checkLastNTurns(int player, int opponent, int turns)
        {
            for (int i = Game.Turn - 1; i > Game.Turn - 1 - turns; i--)
                if (Game.TurnPlayerChoices[i,opponent,player] == 1)
                    return 1;
            return 0;
        }
    }
}
