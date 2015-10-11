// <copyright file="IStyleFactory.cs" company="Telerik Academy">All rights reserved.</copyright>
// <author>Team GameFifteen-1</author>
namespace GameFifteen.Console.Contracts
{
    using System;

    /// <summary>
    /// Define a way to get correct IStyle.
    /// </summary>
    public interface IStyleFactory
    {
        /// <summary>
        /// Gets a concrete style type.
        /// </summary>
        /// <param name="type">Type for checking.</param>
        /// <returns>Style type.</returns>
        IStyle Get(Enum type);
    }
}
