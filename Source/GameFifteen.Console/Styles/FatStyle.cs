namespace GameFifteen.Console.Styles
{
    using System;
    using System.Linq;

    using GameFifteen.Common;
    using GameFifteen.Console.Contracts;

    /// <summary>
    /// Using a rectangular solid symbol presenting a thick lines for grid borders.
    /// </summary>
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

        /// <summary>
        /// Fat top-border style.
        /// </summary>
        /// <value>Sequence of special symbols.</value>
        public override string Top
        {
            get
            {
                return new string(this.symbol, this.length);
            }
        }

        /// <summary>
        /// Fat bottom-border style.
        /// </summary>
        /// <value>Sequence of special symbols.</value>
        public override string Bottom
        {
            get
            {
                return new string(this.symbol, this.length);
            }
        }

        /// <summary>
        /// Fat left-border style.
        /// </summary>
        /// <value>Special symbol followed by space.</value>
        public override string Left
        {
            get
            {
                return this.symbol + " ";
            }
        }

        /// <summary>
        /// Fat right-border style.
        /// </summary>
        /// <value>Space followed by special symbol.</value>
        public override string Right
        {
            get
            {
                return " " + this.symbol;
            }
        }
    }
}
