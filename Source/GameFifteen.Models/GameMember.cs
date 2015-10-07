namespace GameFifteen.Models
{
    using GameFifteen.Models.Contracts;

    public abstract class GameMember : IGameMember
    {
        public abstract string Display();
    }
}
