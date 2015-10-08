namespace GameFifteen.Models
{
    using System;
    using System.Linq;

    using GameFifteen.Models.Contracts;

    /// <summary>
    /// Main Player class that save its name and moves and can be compared and cloned
    /// </summary>
    public class Player : GameMember, IGameMember, IPlayer, IComparable, ICloneable
    {
        private string name;
        private int moves;

        public Player()
        {
            this.Name = "Guest";
            this.Moves = 0;
        }

        public Player(string name)
            : this()
        {
            this.Name = name;
        }

        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("Name cannot be empty.");
                }

                this.name = value;
            }
        }

        public int Moves
        {
            get
            {
                return this.moves;
            }

            set
            {
                this.moves = value;
            }
        }

        public int CompareTo(object player)
        {
            Player currentPlayer = (Player)player;
            int result = this.moves.CompareTo(currentPlayer.Moves);
            return result;
        }

        public object Clone()
        {
            var clone = new Player(this.Name);
            clone.Moves = this.Moves;
            return clone;
        }

        public override string Display()
        {
            var result = string.Format("{0}{1}{2} {3}", this.Name, " -> ", this.Moves, this.Moves == 1 ? "move" : "moves");
            return result;
        }
    }
}
