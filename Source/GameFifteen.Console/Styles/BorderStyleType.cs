// <copyright file="BorderStyleType.cs" company="Telerik Academy">All rights reserved.</copyright>
// <author>Team GameFifteen-1</author>
namespace GameFifteen.Console.Styles
{
    /// <summary>
    /// Special border type that can be used for grid.
    /// </summary>
    public enum BorderStyleType
    {
        /// <summary>
        /// Type that using dot as drawing style symbol.
        /// </summary>
        Dotted,

        /// <summary>
        /// Type that using a double line for border style.
        /// </summary>
        Double,

        /// <summary>
        /// Type that using symbols presenting a 'fat' line.
        /// </summary>
        Fat,

        /// <summary>
        /// Type that using symbols presenting a thin line than 'fat' line.
        /// </summary>
        Middlefat,

        /// <summary>
        /// Type that using a solid single line for border style.
        /// </summary>
        Solid,

        /// <summary>
        /// Type that using a asterisk symbol for border style.
        /// </summary>
        Asterisk
    }
}
