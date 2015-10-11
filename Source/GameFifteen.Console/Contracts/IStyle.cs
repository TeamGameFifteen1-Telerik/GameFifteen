// <copyright file="IStyle.cs" company="Telerik Academy">All rights reserved.</copyright>
// <author>Team GameFifteen-1</author>
namespace GameFifteen.Console.Contracts
{
    using System;

    /// <summary>
    /// Provide style extensions.
    /// </summary>
    public interface IStyle
    {
        /// <summary>
        /// Gets type of style.
        /// </summary>
        /// <value>Style type.</value>
        Enum Type { get; }
    }
}
