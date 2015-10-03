namespace GameFifteen.Models
{
    using System;

    /// <summary>
    /// Prototype design pattern
    /// </summary>
    public class Tile : TilePrototype, IComparable, ICloneable
    {
        private string label;
        private int position;
        private TileType type;

        public Tile()
        {
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

            set
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

        public override Tile CloneMemberwise()
        {
            return this.MemberwiseClone() as Tile;
        }

        public object Clone()
        {
            return new Tile(this.Label, this.Position, this.Type);
        }
    }
}
