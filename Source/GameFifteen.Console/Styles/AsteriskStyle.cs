namespace GameFifteen.Console.Styles
{
    using System;
    using System.Linq;

    using GameFifteen.Common;
    using GameFifteen.Console.Contracts;
    
    /// <summary>
    /// Style that uses a normal symbol 'asterisk'(*) for grid borders.
    /// </summary>
    public class AsteriskStyle : GridBorderStyle, IStyle
    {
        private readonly char symbol = '*';
        private readonly int length = GlobalConstants.GridSize * 4;
        private Enum type;

        /// <summary>
        /// Initializes a new instance of the <see cref="AsteriskStyle"/> class.
        /// </summary>
        public AsteriskStyle()
        {
            this.type = BorderStyleType.Asterisk;
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
        /// Asterisk top-border style.
        /// </summary>
        /// <value>Sequence of asterisks.</value>
        public override string Top
        {
            get
            {
                return new string(this.symbol, this.length);
            }
        }

        /// <summary>
        /// Asterisk bottom-border style.
        /// </summary>
        /// <value>Sequence of asterisks.</value>
        public override string Bottom
        {
            get
            {
                return new string(this.symbol, this.length);
            }
        }

        /// <summary>
        /// Asterisk left-border style.
        /// </summary>
        /// <value>Asterisk followed by space.</value>
        public override string Left
        {
            get
            {
                return this.symbol + " ";
            }
        }

        /// <summary>
        /// Asterisk right-border style.
        /// </summary>
        /// <value>Space followed by asterisk.</value>
        public override string Right
        {
            get
            {
                return " " + this.symbol;
            }
        }
    }
}
