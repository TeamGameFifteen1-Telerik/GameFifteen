namespace GameFifteen.Models
{
    using GameFifteen.Models.Contracts;

    /// <summary>
    /// Prototype design pattern.
    /// </summary>
    public abstract class TilePrototype : GameMember, IGameMember
    {
        public abstract Tile CloneMemberwise();

        public abstract override string GetTextRepresentation();
    }
}