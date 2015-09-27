namespace GameFifteen.Models
{
    using System.Collections.Generic;

    public class Memento
    {
        public Memento(List<Tile> tiles, Tile emptyTile)
        {
            this.Tiles = tiles;
            this.EmptyTile = emptyTile;
        }

        public List<Tile> Tiles { get; set; }

        public Tile EmptyTile { get; set; }

    }
}
