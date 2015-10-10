namespace GameFifteen.Models
{
    using System;
    using System.Collections.Generic;

    using GameFifteen.Models.Contracts;

    /// <summary>
    /// Class for saving and listing top scorers.
    /// </summary>
    public sealed class Scoreboard : GameMember, IGameMember
    {
        private const int TopPlayersCount = 4;

        // lazy singleton
        private static readonly Lazy<Scoreboard> LazyInstance = new Lazy<Scoreboard>(() => new Scoreboard());

        private List<IPlayer> players;

        private Scoreboard()
        {
            this.players = new List<IPlayer>();
        }

        public static Scoreboard Instance
        {
            get
            {
                return LazyInstance.Value;
            }
        }

        public List<IPlayer> TopPlayers
        {
            get
            {
                return new List<IPlayer>(this.GetTopPlayers(TopPlayersCount));
            }
        }

        /// <param name="player">IPlayer type to be added to the Scoreboard.</param>
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

            this.players.Add((IPlayer)player.Clone());
        }

        public void Clear()
        {
            this.players.Clear();
        }

        public override string GetTextRepresentation()
        {
            var lines = new List<string>();
            for (int i = 0; i < this.TopPlayers.Count; i++)
            {
                var currentPlayer = this.TopPlayers[i];
                lines.Add(currentPlayer.GetTextRepresentation());
            }

            return string.Join("|", lines);
        }

        /// <param name="count">Count of the top players to be displayed</param>
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
