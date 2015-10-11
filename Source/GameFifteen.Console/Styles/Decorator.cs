// <copyright file="Decorator.cs" company="Telerik Academy">All rights reserved.</copyright>
// <author>Team GameFifteen-1</author>
namespace GameFifteen.Console.Styles
{
    using GameFifteen.Models;
    using GameFifteen.Models.Contracts;

    /// <summary>
    /// Abstract decorator (Decorator design pattern).
    /// </summary>
    public abstract class Decorator : GameMember, IGameMember
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Decorator" /> class.
        /// </summary>
        /// <param name="gameMember">IGameMember object.</param>
        protected Decorator(IGameMember gameMember)
        {
            this.GameMember = gameMember;
        }

        /// <summary>
        /// Gets IGameMember object to be decorated.
        /// </summary>
        protected IGameMember GameMember { get; private set; }

        /// <summary>
        /// Gets a text representation of the decorator.
        /// </summary>
        /// <returns>A text representation of the decorator.</returns>
        public override string GetTextRepresentation()
        {
            return this.GameMember.GetTextRepresentation();
        }
    }
}
