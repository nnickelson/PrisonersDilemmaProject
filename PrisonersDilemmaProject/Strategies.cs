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
        public static int ReturnStrategy( List<Player> playerList, int strategy, int player, int opponent)
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
                    strat = TitForTat1(playerList, player, opponent);
                    break;
                case 3:
                    strat = TitForTat2(playerList, player, opponent);
                    break;
                case 4:
                    strat = TitForTat3(playerList, player, opponent);
                    break;
                case 5:
                    strat = Cheater1(playerList, player, opponent);
                    break;
                case 6:
                    strat = Cheater2(playerList, player, opponent);
                    break;
                case 7:
                    strat = Cheater3(playerList, player, opponent);
                    break;
                case 8:
                    strat = 6;
                    break;
            }
            return strat;
        }

        //Strategy 0 - Always defect
        private static int AlwaysDefect()
        {
            return 1;
        }

        private static int AlwaysCooperate()
        {
            return 0;
        }

        private static int TitForTat1(List<Player> players,  int player, int opponent)
        {
            if (players[player].StoredOpponentChoices.Count > 1)
            {
                int count = players[player].StoredOpponentChoices.Count;
                //Console.WriteLine("returning last value = " + players[player].StoredOpponentChoices.Last()[opponent]);
                return players[player].StoredOpponentChoices[count -2][opponent];
            }
            return 0;
        }

        private static int TitForTat2(List<Player> players, int player, int opponent)
        {
            if (players[player].StoredOpponentChoices.Count > 2)
            {
                int count = players[player].StoredOpponentChoices.Count;
                //Console.WriteLine("count = {0}", count);
                //Console.WriteLine(players[player].StoredOpponentChoices[0][opponent]);
                if (players[player].StoredOpponentChoices[count-2][opponent] == 1 &&
                        players[player].StoredOpponentChoices[count - 3][opponent] == 1)
                {
                    return 1;
                }
            }
            return 0;
        }

        private static int TitForTat3(List<Player> players, int player, int opponent)
        {
            if (players[player].StoredOpponentChoices.Count > 3)
            {
                int count = players[player].StoredOpponentChoices.Count;
                if (players[player].StoredOpponentChoices[count - 2][opponent] == 1 &&
                        players[player].StoredOpponentChoices[count - 3][opponent] == 1 &&
                        players[player].StoredOpponentChoices[count - 4][opponent] == 1)
                {
                    return 1;
                }
            }
            return 0;
        }

        //Incomplete
        private static int Cheater1(List<Player> players, int player, int opponent)
        {
            if (players[player].StoredOpponentChoices.Count > 1)
            {
                int count = players[player].StoredOpponentChoices.Count;
                if (players[player].StoredPlayerChoices[count - 2][opponent] == 1)
                {
                    Console.WriteLine("last pick defect, this pick coop");
                    return 0;
                }
                else
                {
                    for (int i = count -2; i > -1; i--)
                    {

                    }
                }
            }
            return 0;
        }

        //Incomplete
        private static int Cheater2(List<Player> players, int player, int opponent)
        {
            return 0;
        }

        //Incomplete
        private static int Cheater3(List<Player> players, int player, int opponent)
        {
            return 0;
        }

        //Complete --> needs checking
        private static int PermanentRetaliation(List<Player> players, int player, int opponent)
        {
            foreach (int choice in players[player].StoredOpponentChoices[opponent]){
                if (choice == 1)
                {
                    return 1;
                }
            }
            return 0;
        }
    }
}
