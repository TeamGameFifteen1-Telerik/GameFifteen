namespace GameFifteen.Console.Styles
{
    using System;
    using System.Linq;

    using GameFifteen.Common;
    using GameFifteen.Console.Contracts;

    /// <summary>
    /// Style that uses default borders for grid - dashes and lines.
    /// </summary>
    public class NormalStyle : GridBorderStyle, IStyle
    {
        private readonly char symbol = '-';
        private readonly char sideSymbol = '|';
        private readonly int length = (GlobalConstants.GridSize * 3) - 1;
        private Enum type;

        /// <summary>
        /// Initializes a new instance of the <see cref="NormalStyle"/> class.
        /// </summary>
        public NormalStyle()
        {
            this.type = BorderStyleType.Default;
        }

        /// <summary>
        /// Gets the type of the style.
        /// </summary>
        /// <value>Type of the style.</value>
        public override Enum Type
        {
            get
            {
                return this.type;
            }
        }

        /// <summary>
        /// Normal top-border style.
        /// </summary>
        /// <value>Sequence of dashes.</value>
        public override string Top
        {
            get
            {
                return "  " + new string(this.symbol, this.length);
            }
        }

        /// <summary>
        /// Normal bottom-border style.
        /// </summary>
        /// <value>Sequence of dashes.</value>
        public override string Bottom
        {
            get
            {
                return "  " + new string(this.symbol, this.length);
            }
        }

        /// <summary>
        /// Normal left-border style.
        /// </summary>
        /// <value>Vertical line followed by space.</value>
        public override string Left
        {
            get
            {
                return this.sideSymbol + " ";
            }
        }

        /// <summary>
        /// Normal right-border style.
        /// </summary>
        /// <value>Space followed by vertical line.</value>
        public override string Right
        {
            get
            {
                return " " + this.sideSymbol;
            }
        }
    }
}
