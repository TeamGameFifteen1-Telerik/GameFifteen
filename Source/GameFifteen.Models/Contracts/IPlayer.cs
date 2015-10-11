// <copyright file="IPlayer.cs" company="Telerik Academy">All rights reserved.</copyright>
// <author>Team GameFifteen-1</author>
namespace GameFifteen.Models.Contracts
{
    using System;

    /// <summary>
    /// IPlayer interface.
    /// </summary>
    public interface IPlayer : ICloneable, IGameMember
    {
        /// <summary>
        /// Gets or sets player's name as a string.
        /// </summary>
        /// <value>The Name property gets or set's a player's name.</value>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the number of player's moves.
        /// </summary>
        /// <value>The Name property gets or set's a player's moves.</value>
        int Moves { get; set; }
    }
}
