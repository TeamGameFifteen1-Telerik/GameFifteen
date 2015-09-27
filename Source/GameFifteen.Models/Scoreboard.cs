﻿namespace GameFifteen.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using GameFifteen.Models;

    public sealed class Scoreboard
    {
        private List<Player> players = new List<Player>();
        //lazy signleton
        private static readonly Lazy<Scoreboard> scoreBoard =
            new Lazy<Scoreboard>( () => new Scoreboard());

        public static Scoreboard Instance
        {
            get
            {
                return scoreBoard.Value;
            }
        }

        private Scoreboard()
        {

        }

        public List<Player> Players
        {
            get
            {
                return this.players;
            }
        }

        public void AddPlayer(Player player)
        {
            this.players.Add(player);
            this.players.Sort();
            this.DeleteAllExceptTopPlayers();
        }

        /*
        public void PrintScoreboard()
        {
            Console.WriteLine("Scoreboard:");
            foreach (Player player in players)
            {
                string scoreboardLine = (players.IndexOf(player) + 1).ToString() + ". " + player.Name + " --> " + player.Moves.ToString() + " moves";
                Console.WriteLine(scoreboardLine);
            }
        }
        */

        private void DeleteAllExceptTopPlayers()
        {
            for (int index = 0; index < this.players.Count(); index++)
            {
                if (index > 4)
                {
                    this.players.Remove(this.players[index]);
                }
            }
        }
    }
}
