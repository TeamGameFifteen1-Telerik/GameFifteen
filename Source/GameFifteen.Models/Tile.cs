// <copyright file="Tile.cs" company="Telerik Academy">All rights reserved.</copyright>
// <author>Team GameFifteen-1</author>
namespace GameFifteen.Models
{
    using System;

    using GameFifteen.Models.Contracts;

    /// <summary>
    /// Prototype design pattern.
    /// </summary>
    public class Tile : TilePrototype, IComparable, ICloneable, IGameMember
    {
        private string label;
        private int position;
        private TileType type;

        /// <summary>
        /// Initializes a new instance of the <see cref="Tile" /> class.
        /// </summary>
        public Tile()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Tile" /> class with given parameters.
        /// </summary>
        /// <param name="label">Tile labels as string.</param>
        /// <param name="position">Tile position as integer.</param>
        /// <param name="type">Tile type as <see cref="TileType" /> enumeration.</param>
        public Tile(string label, int position, TileType type)
        {
            this.Label = label;
            this.Position = position;
            this.Type = type;
        }

        /// <summary>
        /// Gets or sets tile's label as a string.
        /// </summary>
        /// <value>The property Label gets or sets tile's label as a string.</value>
        public string Label
        {
            get
            {
                return this.label;
            }

            set
            {
                this.label = value;
            }
        }

        /// <summary>
        /// Gets or sets tile's position as an integer.
        /// </summary>
        /// <value>The property Position gets or sets tile's position as an integer.</value>
        public int Position
        {
            get
            {
                return this.position;
            }

            set
            {
                this.position = value;
            }
        }

        /// <summary>
        /// Gets or sets tile's type.
        /// </summary>
        /// <value>The property Type gets or sets tile's type.</value>
        public TileType Type
        {
            get
            {
                return this.type;
            }

            set
            {
                this.type = value;
            }
        }

        /// <summary>
        /// Compares current tile to other.
        /// </summary>
        /// <param name="tile">Other tile.</param>
        /// <returns>Returns 1 if the current tile is first, -1 if the other tile is first and 0 if they're equal.</returns>
        public int CompareTo(object tile)
        {
            Tile currentTile = (Tile)tile;
            int result = this.position.CompareTo(currentTile.Position);

            return result;
        }

        /// <summary>
        /// Clones a tile member wise.
        /// </summary>
        /// <returns>A member wise clone of the tile.</returns>
        public override Tile CloneMemberwise()
        {
            return this.MemberwiseClone() as Tile;
        }

        /// <summary>
        /// Clones a tile.
        /// </summary>
        /// <returns>A new tile - clone of the current one.</returns>
        public object Clone()
        {
            return new Tile(this.Label, this.Position, this.Type);
        }

        /// <summary>
        /// Gets a text representation of the tile.
        /// </summary>
        /// <returns>A text representation of the tile.</returns>
        public override string GetTextRepresentation()
        {
            return this.Label == string.Empty 
                ? " "
                : this.Label;
        }
    }
}
