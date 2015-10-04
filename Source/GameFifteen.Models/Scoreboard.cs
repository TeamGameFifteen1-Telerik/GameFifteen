namespace GameFifteen.Models
{
    using System;
    using System.Collections.Generic;

    using GameFifteen.Models.Contracts;

    /// <summary>
    /// Class for saving and listing top scorers.
    /// </summary>
    public sealed class Scoreboard
    {
        private const int TopPlayersCount = 4;

        //// lazy singleton
        private static readonly Lazy<Scoreboard> scoreboard =
            new Lazy<Scoreboard>(() => new Scoreboard());

        private List<IPlayer> players;

        private Scoreboard()
        {
            this.players = new List<IPlayer>();
        }

        public static Scoreboard Instance
        {
            get
            {
                return scoreboard.Value;
            }
        }

        public List<IPlayer> TopPlayers
        {
            get
            {
                return new List<IPlayer>(this.GetTopPlayers(TopPlayersCount));
            }
        }

        public void AddPlayer(IPlayer player)
        {
            if (player == null)
            {
                throw new ArgumentNullException("Player cannot be null.");
            }

            if (player.Moves == 0)
            {
                throw new ArgumentException("Player needs at least one move.");
            }

            this.players.Add(player);
        }

        public void Clear()
        {
            this.players.Clear();
        }

        private List<IPlayer> GetTopPlayers(int count)
        {
            this.players.Sort();

            int topCount = this.players.Count < count ? this.players.Count : count;
            var topPlayers = new List<IPlayer>();

            for (int i = 0; i < topCount; i++)
            {
                topPlayers.Add(this.players[i]);
            }

            return topPlayers;
        }
    }
}
