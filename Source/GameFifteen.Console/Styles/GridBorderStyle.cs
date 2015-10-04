namespace GameFifteen.Console
{
    using System;

    using GameFifteen.Console.Contracts;

    /// <summary>
    /// An object that provides a style for each border on the grid.
    /// Styles are a ordered symbols in clear way.
    /// </summary>
    public abstract class GridBorderStyle : IStyle
    {
        /// <summary>
        /// Gets top-border style.
        /// </summary>
        /// <value>Top border.</value>
        public abstract string Top { get; }
        
        /// <summary>
        /// Gets bottom-border style.
        /// </summary>
        /// <value>Bottom border.</value>
        public abstract string Bottom { get; }

        /// <summary>
        /// Gets left-border style.
        /// </summary>
        /// <value>Left border.</value>
        public abstract string Left { get; }

        /// <summary>
        /// Gets right-border style.
        /// </summary>
        /// <value>Right border.</value>
        public abstract string Right { get; }

        public abstract Enum Type { get; }
    }
}
