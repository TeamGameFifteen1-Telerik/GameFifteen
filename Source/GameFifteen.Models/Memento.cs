namespace GameFifteen.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Memento design pattern
    /// The 'Memento' class.
    /// </summary>
    public class Memento
    {
        public Memento(List<Tile> tiles)
        {
            this.Tiles = tiles;
        }

        public List<Tile> Tiles { get; set; }
    }
}
