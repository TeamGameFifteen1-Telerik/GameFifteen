namespace GameFifteen.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Tile : IComparable, ICloneable
    {
        private string label;
        private int position;
        private TileType type;

        public Tile()
        {
        }

        //Some of the tests rely on constructor with two arguments
        public Tile(string label, int position)
        {
            this.Label = label;
            this.Position = position;
        }

        public Tile(string label, int position, TileType type)
        {
            this.Label = label;
            this.Position = position;
            this.Type = type;
        }

        public string Label
        {
            get
            {
                return this.label;
            }

            private set
            {
                this.label = value;
            }
        }

        public int Position
        {
            get
            {
                return this.position;
            }

            set
            {
                this.position = value;
            }
        }

        public TileType Type
        {
            get
            {
                return this.type;
            }

            set
            {
                this.type = value;
            }
        }

        public int CompareTo(object tile)
        {
            Tile currentTile = (Tile)tile;
            int result = this.position.CompareTo(currentTile.Position);

            return result;
        }

        public object Clone()
        {
            return new Tile(this.Label, this.Position, this.Type);
        }
    }
}
