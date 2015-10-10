namespace GameFifteen.Console.Styles
{
    using System;
    using System.Linq;

    using GameFifteen.Common;
    using GameFifteen.Console.Contracts;

    /// <summary>
    /// Style that uses a sequence symbols that present a line think than <see cref="SolidStyle"/> and thin than <see cref="FatStyle"/>.
    /// Uses for grid borders.
    /// </summary>
    public class MiddleFatStyle : GridBorderStyle, IStyle
    {
        private readonly char topSymbol = '▄';
        private readonly char bottomSymbol = '▀';
        private readonly char leftSymbol = '▌';
        private readonly char rightSymbol = '▐';
        private readonly int length = (GlobalConstants.GridSize * 4) - (GlobalConstants.GridSize - 4);
        private Enum type;

        /// <summary>
        /// Initializes a new instance of the <see cref="MiddleFatStyle"/> class.
        /// </summary>
        public MiddleFatStyle()
        {
            this.type = BorderStyleType.Middlefat;
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
        /// MiddleFat top-border style.
        /// </summary>
        /// <value>Sequence of special symbols.</value>
        public override string Top
        {
            get
            {
                return new string(this.topSymbol, this.length);
            }
        }

        /// <summary>
        /// MiddleFat bottom-border style.
        /// </summary>
        /// <value>Sequence of special symbols.</value>
        public override string Bottom
        {
            get
            {
                return new string(this.bottomSymbol, this.length);
            }
        }

        /// <summary>
        /// MiddleFat left-border style.
        /// </summary>
        /// <value>Special symbol followed by space.</value>
        public override string Left
        {
            get
            {
                return this.leftSymbol + " ";
            }
        }
        
        /// <summary>
        /// MiddleFat right-border style.
        /// </summary>
        /// <value>Space followed by special symbol.</value>
        public override string Right
        {
            get
            {
                return " " + this.rightSymbol;
            }
        }
    }
}
