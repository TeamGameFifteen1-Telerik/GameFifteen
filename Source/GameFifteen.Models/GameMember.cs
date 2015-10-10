namespace GameFifteen.Models
{
    using GameFifteen.Models.Contracts;

    /// <summary>
    /// Abstract game member class.
    /// </summary>
    public abstract class GameMember : IGameMember
    {
        public abstract string Display();
    }
}
