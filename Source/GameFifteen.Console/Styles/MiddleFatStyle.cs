namespace GameFifteen.Console.Styles
{
    using System;
    using System.Linq;

    using GameFifteen.Common;
    using GameFifteen.Console.Contracts;

    public class MiddleFatStyle : GridBorderStyle, IStyle
    {
        private readonly char topSymbol = '▄';
        private readonly char bottomSymbol = '▀';
        private readonly char leftSymbol = '▌';
        private readonly char rightSymbol = '▐';
        private readonly int length = GlobalConstants.GridSize * 4;
        private Enum type;

        public MiddleFatStyle()
        {
            this.type = BorderStyleType.MiddleFat;
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
                return new string(this.topSymbol, this.length);
            }
        }

        public override string Bottom
        {
            get
            {
                return new string(this.bottomSymbol, this.length);
            }
        }

        public override string Left
        {
            get
            {
                return this.leftSymbol + " ";
            }
        }

        public override string Right
        {
            get
            {
                return " " + this.rightSymbol;
            }
        }
    }
}
