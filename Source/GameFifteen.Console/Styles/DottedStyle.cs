﻿// <copyright file="DottedStyle.cs" company="Telerik Academy">All rights reserved.</copyright>
// <author>Team GameFifteen-1</author>
namespace GameFifteen.Console.Styles
{
    using System;
    using System.Linq;
    using GameFifteen.Common;
    using GameFifteen.Console.Contracts;

    /// <summary>
    /// Style that uses a dot for drawing grid borders.
    /// </summary>
    public class DottedStyle : GridBorderStyle, IStyle
    {
        // 6 -> 2
        // 9 -> 
        private readonly char symbol = '·';
        private readonly int length = (GlobalConstants.GridSize * 4) - (GlobalConstants.GridSize - 4);
        private Enum type;

        /// <summary>
        /// Initializes a new instance of the <see cref="DottedStyle"/> class.
        /// </summary>
        public DottedStyle()
        {
            this.type = BorderStyleType.Dotted;
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
