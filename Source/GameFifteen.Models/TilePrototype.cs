namespace GameFifteen.Models
{
    /// <summary>
    /// Prototype design pattern
    /// </summary>
    public abstract class TilePrototype
    {
        public abstract Tile CloneMemberwise();
    }
}
