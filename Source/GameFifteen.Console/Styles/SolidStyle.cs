namespace GameFifteen.Console.Styles
{
    using System;
    using System.Linq;

    using GameFifteen.Common;
    using GameFifteen.Console.Contracts;

    public class SolidStyle : GridBorderStyle, IStyle
    {
        private readonly char symbol = '│';
        private readonly char line = '─';
        private readonly int length = (GlobalConstants.GridSize * 4) - 2;
        private Enum type;

        public SolidStyle()
        {
            this.type = BorderStyleType.Solid;
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
                return "┌" + new string(this.line, this.length) + "┐";
            }
        }

        public override string Bottom
        {
            get
            {
                return "└" + new string(this.line, this.length) + "┘";
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
