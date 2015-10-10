namespace GameFifteen.Console.Styles
{
    using GameFifteen.Models;
    using GameFifteen.Models.Contracts;

    /// <summary>
    /// Abstract decorator (Decorator design pattern)
    /// </summary>
    public abstract class Decorator : GameMember, IGameMember
    {
        /// <summary>
        /// Decorator constructor.
        /// </summary>
        /// <param name="gameMember"></param>
        protected Decorator(IGameMember gameMember)
        {
            this.GameMember = gameMember;
        }

        protected IGameMember GameMember { get; private set; }

        public override string GetTextRepresentation()
        {
            return this.GameMember.GetTextRepresentation();
        }
    }
}
