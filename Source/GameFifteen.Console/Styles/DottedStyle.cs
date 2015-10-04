namespace GameFifteen.Console.Styles
{
    using System;
    using System.Linq;
    using GameFifteen.Common;
    using GameFifteen.Console.Contracts;

    public class DottedStyle : GridBorderStyle, IStyle
    /// <summary>
    /// Style that uses a dot for drawing grid borders.
    /// </summary>
    public class DottedStyle : GridBorderStyle
    {
        private readonly char symbol = '·';
        private readonly int length = GlobalConstants.GridSize * 4;
        private Enum type;

        public DottedStyle()
        {
            this.type = BorderStyleType.Dotted;
        }

        public override Enum Type
        {
            get
            {
                return this.type;
            }
        }

        /// <summary>
        /// Dotted top-border style.
        /// </summary>
        /// <value>Sequence of dots.</value>
        public override string Top
        {
            get
            {
                return new string(this.symbol, this.length);
            }
        }

        /// <summary>
        /// Dotted bottom-border style.
        /// </summary>
        /// <value>Sequence of dots.</value>
        public override string Bottom
        {
            get
            {
                return new string(this.symbol, this.length);
            }
        }

        /// <summary>
        /// Dotted left-border style.
        /// </summary>
        /// <value>Dot followed by space.</value>
        public override string Left
        {
            get 
            {
                return this.symbol + " "; 
            }
        }

        /// <summary>
        /// Dotted right-borders style.
        /// </summary>
        /// <value>Space followed by dot.</value>
        public override string Right
        {
            get
            {
                return " " + this.symbol;
            }
        }
    }
}
