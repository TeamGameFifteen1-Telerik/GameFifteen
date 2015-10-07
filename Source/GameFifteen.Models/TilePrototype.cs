namespace GameFifteen.Models
{
    using GameFifteen.Models.Contracts;

    /// <summary>
    /// Prototype design pattern
    /// </summary>
    public abstract class TilePrototype : IGameMember
    {
        public abstract Tile CloneMemberwise();

        public abstract string Display();
    }
}
