namespace GameFifteen.Console.Styles
{
    using System;
    using System.Linq;

    using GameFifteen.Common;
    using GameFifteen.Console.Contracts;

    public class DefaultStyle : GridBorderStyle, IStyle
    {
        private readonly char symbol = '-';
        private readonly char sideSymbol = '|';
        private readonly int length = (GlobalConstants.GridSize * 3) - 1;
        private Enum type;

        public DefaultStyle()
        {
            this.type = BorderStyleType.Default;
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
                return "  " + new string(this.symbol, this.length);
            }
        }

        public override string Bottom
        {
            get
            {
                return "  " + new string(this.symbol, this.length);
            }
        }

        public override string Left
        {
            get
            {
                return this.sideSymbol + " ";
            }
        }

        public override string Right
        {
            get
            {
                return " " + this.sideSymbol;
            }
        }
    }
}
