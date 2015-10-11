// <copyright file="IGameMember.cs" company="Telerik Academy">All rights reserved.</copyright>
// <author>Team GameFifteen-1</author>
namespace GameFifteen.Models.Contracts
{
    /// <summary>
    /// IGameMember interface.
    /// </summary>
    public interface IGameMember
    {
        /// <summary>
        /// Gets a text representation of the IGameMember object.
        /// </summary>
        /// <returns>A text representation of the IGameMember object.</returns>
        string GetTextRepresentation();
    }
}
