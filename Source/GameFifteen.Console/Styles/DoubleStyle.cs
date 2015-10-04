namespace GameFifteen.Console.Styles
{
    using System;
    using System.Linq;

    using GameFifteen.Common;
    using GameFifteen.Console.Contracts;

    public class DoubleStyle : GridBorderStyle, IStyle
    /// <summary>
    /// Style that uses a double line and double lined corners for grid borders.
    /// </summary>
    public class DoubleStyle : GridBorderStyle
    {
        private readonly char symbol = '║';
        private readonly char line = '═';
        private readonly int length = (GlobalConstants.GridSize * 4) - 2;
        private Enum type;

        public DoubleStyle()
        {
            this.type = BorderStyleType.Double;
        }

        public override Enum Type
        {
            get
            {
                return this.type;
            }
        }

        /// <summary>
        /// Double-line top-border style.
        /// </summary>
        /// <value>Double line with two corners.</value>
        public override string Top
        {
            get
            {
                return "╔" + new string(this.line, this.length) + "╗";
            }
        }

        /// <summary>
        /// Double-line bottom-border style.
        /// </summary>
        /// <value>Double line with two corners.</value>
        public override string Bottom
        {
            get
            {
                return "╚" + new string(this.line, this.length) + "╝";
            }
        }

        /// <summary>
        /// Double-line left-border style.
        /// </summary>
        /// <value>Vertical double line followed by space.</value>
        public override string Left
        {
            get
            {
                return this.symbol + " ";
            }
        }

        /// <summary>
        /// Double-line right-border style.
        /// </summary>
        /// <value>Space followed by vertical double line.</value>
        public override string Right
        {
            get
            {
                return " " + this.symbol;
            }
        }
    }
}
