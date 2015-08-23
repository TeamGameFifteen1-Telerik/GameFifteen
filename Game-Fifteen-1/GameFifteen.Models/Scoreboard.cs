namespace GameFifteen.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using GameFifteen.Models;

    public class Scoreboard
    {
        private List<Player> players = new List<Player>();

        public List<Player> Players
        {
            get
            {
                return players;
            }
        }

        public void AddPlayer(Player player)
        {
            players.Add(player);
            players.Sort();
            DeleteAllExceptTopPlayers();
        }
     
        //public void PrintScoreboard()
        //{
        //    Console.WriteLine("Scoreboard:");
        //    foreach (Player player in players)
        //    {
        //        string scoreboardLine = (players.IndexOf(player) + 1).ToString() + ". " + player.Name + " --> " + player.Moves.ToString() + " moves";
        //        Console.WriteLine(scoreboardLine);
        //    }
        //}

        private void DeleteAllExceptTopPlayers()
        {
            for (int index = 0; index < players.Count(); index++)
            {
                if (index > 4)
                {
                    players.Remove(players[index]);
                }
            }
        }
    }
}
