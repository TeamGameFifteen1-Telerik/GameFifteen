namespace GameFifteen.Models
{
    using System;
    using System.Linq;

    using GameFifteen.Models.Contracts;

    /// <summary>
    /// Main Player class that save its name and moves and can be compared and cloned
    /// </summary>
    public class Player : IPlayer, IComparable, ICloneable
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
    }
}
