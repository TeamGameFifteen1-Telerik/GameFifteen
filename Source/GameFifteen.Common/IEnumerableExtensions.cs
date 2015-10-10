namespace GameFifteen.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Extension methods for IEnumerable collection.
    /// </summary>
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Clones objects in IEnumerable collection.
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="collection">IEnumerable collection</param>
        /// <returns>A collection with cloned objects</returns>
        public static IEnumerable<T> Clone<T>(this IEnumerable<T> collection) where T : ICloneable
        {
            return collection.Select(item => (T)item.Clone());
        }
    }
}
