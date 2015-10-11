// <copyright file="GameMember.cs" company="Telerik Academy">All rights reserved.</copyright>
// <author>Team GameFifteen-1</author>
namespace GameFifteen.Models
{
    using GameFifteen.Models.Contracts;

    /// <summary>
    /// Abstract game member class.
    /// </summary>
    public abstract class GameMember : IGameMember
    {
        /// <summary>
        /// Gets a text representation of the IGameMember object.
        /// </summary>
        /// <returns>A text representation of the IGameMember object.</returns>
        public abstract string GetTextRepresentation();
    }
}
