// <copyright file="TilePrototype.cs" company="Telerik Academy">All rights reserved.</copyright>
// <author>Team GameFifteen-1</author>
namespace GameFifteen.Models
{
    using GameFifteen.Models.Contracts;

    /// <summary>
    /// Prototype design pattern.
    /// </summary>
    public abstract class TilePrototype : GameMember, IGameMember
    {
        /// <summary>
        /// Clones a tile member wise.
        /// </summary>
        /// <returns>A member wise clone of the tile.</returns>
        public abstract Tile CloneMemberwise();

        /// <summary>
        /// Gets a text representation of the tile.
        /// </summary>
        /// <returns>A text representation of the tile.</returns>
        public abstract override string GetTextRepresentation();
    }
}