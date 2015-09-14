namespace GameFifteen.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Tile : IComparable
    {
        private string label;
        private int position;

        public Tile()
        {
        }

        public Tile(string label, int position)
        {
            this.label = label;
            this.position = position;
        }

        public string Label
        {
            get { return this.label; }
        }

        public int Position
        {
            get { return this.position; }
            set { this.position = value; }
        }

        public int CompareTo(object tile)
        {
            Tile currentTile = (Tile)tile;
            int result = this.position.CompareTo(currentTile.Position);

            return result;
        }
    }
}
