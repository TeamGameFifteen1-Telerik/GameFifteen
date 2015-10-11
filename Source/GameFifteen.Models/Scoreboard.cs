// <copyright file="Scoreboard.cs" company="Telerik Academy">All rights reserved.</copyright>
// <author>Team GameFifteen-1</author>
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

        /// <summary>
        /// Prevents a default instance of the <see cref="Scoreboard" /> class from being created.
        /// </summary>
        private Scoreboard()
        {
            this.players = new List<IPlayer>();
        }

        /// <summary>
        /// Gets a Scoreboard instance. Singleton.
        /// </summary>
        /// <value>The property Instance gets a single Scoreboard instance</value>
        public static Scoreboard Instance
        {
            get
            {
                return LazyInstance.Value;
            }
        }

        /// <summary>
        /// Gets a list of the top players.
        /// </summary>
        /// <value>The TopPlayers property gets a list of the top players.</value>
        public List<IPlayer> TopPlayers
        {
            get
            {
                return new List<IPlayer>(this.GetTopPlayers(TopPlayersCount));
            }
        }

        /// <summary>
        /// Adds a player to the scoreboard's list of players.
        /// </summary>
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

        /// <summary>
        /// Clears scoreboard.
        /// </summary>
        public void Clear()
        {
            this.players.Clear();
        }

        /// <summary>
        /// Gets a text representation of the scoreboard.
        /// </summary>
        /// <returns>Scoreboard as a string.</returns>
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

        /// <summary>
        /// Gets top players - with least moves.
        /// </summary>
        /// <param name="count">Count of the top players to be displayed.</param>
        /// <returns>A list of players.</returns>
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
