namespace GameFifteen.Models
{
    using System;
    using System.Collections.Generic;

    public sealed class Scoreboard
    {
        private const int TopPlayersCount = 4;

        private List<Player> players;

        //lazy signleton
        private static readonly Lazy<Scoreboard> scoreBoard =
            new Lazy<Scoreboard>(() => new Scoreboard());

        public static Scoreboard Instance
        {
            get
            {
                return scoreBoard.Value;
            }
        }

        private Scoreboard()
        {
            this.players = new List<Player>();
        }

        public List<Player> TopPlayers
        {
            get
            {
                return new List<Player>(this.GetTopPlayers(TopPlayersCount));
            }
        }

        public void AddPlayer(Player player)
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

        private List<Player> GetTopPlayers(int count)
        {
            this.players.Sort();

            int topCount = this.players.Count < count ? this.players.Count : count;
            var topPlayers = new List<Player>();

            for (int i = 0; i < topCount; i++)
            {
                topPlayers.Add(this.players[i]);
            }

            return topPlayers;
        }
    }
}
