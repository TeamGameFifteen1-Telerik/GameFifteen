// <copyright file="Player.cs" company="Telerik Academy">All rights reserved.</copyright>
// <author>Team GameFifteen-1</author>
namespace GameFifteen.Models
{
    using System;
    using System.Linq;

    using GameFifteen.Models.Contracts;

    /// <summary>
    /// Main Player class that save its name and moves and can be compared and cloned.
    /// </summary>
    public class Player : GameMember, IGameMember, IPlayer, IComparable, ICloneable
    {
        private string name;
        private int moves;

        /// <summary>
        /// Initializes a new instance of the <see cref="Player" /> class.
        /// </summary>
        public Player()
        {
            this.Name = "Guest";
            this.Moves = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Player" /> class with name.
        /// </summary>
        /// <param name="name">Name as a string.</param>
        public Player(string name)
            : this()
        {
            this.Name = name;
        }

        /// <summary>
        /// Gets or sets player's name as a string.
        /// </summary>
        /// <value>The Name property gets or set's a player's name.</value>
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

        /// <summary>
        /// Gets or sets the number of player's moves.
        /// </summary>
        /// <value>The Name property gets or set's a player's moves.</value>
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

        /// <summary>
        /// Compares two players.
        /// </summary>
        /// <param name="player">Player object.</param>
        /// <returns>Returns 1 if the current player is first, -1 if the other player is first and 0 if they're equal.</returns>
        public int CompareTo(object player)
        {
            Player currentPlayer = (Player)player;
            int result = this.moves.CompareTo(currentPlayer.Moves);
            return result;
        }

        /// <summary>
        /// Clones a Player.
        /// </summary>
        /// <returns>A clone of the current player.</returns>
        public object Clone()
        {
            var clone = new Player(this.Name);
            clone.Moves = this.Moves;
            return clone;
        }

        /// <summary>
        /// Gets a text representation of a player.
        /// </summary>
        /// <returns>A text representation of a player.</returns>
        public override string GetTextRepresentation()
        {
            var result = string.Format("{0}{1}{2} {3}", this.Name, " -> ", this.Moves, this.Moves == 1 ? "move" : "moves");
            return result;
        }
    }
}
