namespace GameFifteen.Console.Styles
{
    using System;
    using System.Linq;

    using GameFifteen.Common;
    using GameFifteen.Console.Contracts;

    public class FatStyle : GridBorderStyle, IStyle
    {
        private readonly char symbol = '█';
        private readonly int length = GlobalConstants.GridSize * 4;
        private Enum type;

        public FatStyle()
        {
            this.type = BorderStyleType.Fat;
        }

        public override Enum Type
        {
            get
            {
                return this.type;
            }
        }

        public override string Top
        {
            get
            {
                return new string(this.symbol, this.length);
            }
        }

        public override string Bottom
        {
            get
            {
                return new string(this.symbol, this.length);
            }
        }

        public override string Left
        {
            get
            {
                return this.symbol + " ";
            }
        }

        public override string Right
        {
            get
            {
                return " " + this.symbol;
            }
        }
    }
}
