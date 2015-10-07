namespace GameFifteen.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Making IEnumerble objects clonable
    /// </summary>
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> Clone<T>(this IEnumerable<T> collection) where T : ICloneable
        {
            return collection.Select(item => (T)item.Clone());
        }
    }
}
