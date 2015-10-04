namespace GameFifteen.Console.Styles
{
    using System;
    using System.Linq;

    using GameFifteen.Common;
    using GameFifteen.Console.Contracts;

    /// <summary>
    /// Style that uses single solid line with correct corners for grid borders.
    /// </summary>
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

        /// <summary>
        /// Solid top-border style.
        /// </summary>
        /// <value>Sequence of horizontal lines.</value>
        public override string Top
        {
            get
            {
                return "┌" + new string(this.line, this.length) + "┐";
            }
        }

        /// <summary>
        /// Solid bottom-border style.
        /// </summary>
        /// <value>Sequence of horizontal lines.</value>
        public override string Bottom
        {
            get
            {
                return "└" + new string(this.line, this.length) + "┘";
            }
        }

        /// <summary>
        /// Solid left-border style.
        /// </summary>
        /// <value>Vertical line with space.</value>
        public override string Left
        {
            get
            {
                return this.symbol + " ";
            }
        }

        /// <summary>
        /// Solid right-border style.
        /// </summary>
        /// <value>Space with vertical line.</value>
        public override string Right
        {
            get
            {
                return " " + this.symbol;
            }
        }
    }
}
