namespace GameFifteen.Models
{
    using System.Collections.Generic;

    public class Memento
    {
        public Memento(List<Tile> tiles)
        {
            this.Tiles = tiles;
        }

        public List<Tile> Tiles { get; set; }
    }
}
