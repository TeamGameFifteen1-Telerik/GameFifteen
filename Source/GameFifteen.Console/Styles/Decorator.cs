namespace GameFifteen.Console.Styles
{
    using GameFifteen.Models;
    using GameFifteen.Models.Contracts;

    public abstract class Decorator : GameMember, IGameMember
    {
        protected Decorator(IGameMember gameMember)
        {
            this.GameMember = gameMember;
        }

        protected IGameMember GameMember { get; private set; }

        public override string Display()
        {
            return this.GameMember.Display();
        }
    }
}
