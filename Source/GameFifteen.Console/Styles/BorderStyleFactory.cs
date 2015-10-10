namespace GameFifteen.Console.Styles
{
    using System;

    using GameFifteen.Console.Contracts;

    /// <summary>
    /// Implements Simple Factory Pattern witch provides a creation of concrete object.
    /// </summary>
    public class BorderStyleFactory : IStyleFactory
    {
        /// <summary>
        /// Returns a concrete style type.
        /// </summary>
        /// <param name="type">Type of the concrete object.</param>
        /// <exception cref="System.ArgumentException">If type is different than <see cref="BorderStyleType"/></exception>
        /// <returns>Concrete Style type as <see cref="IStyle"/></returns>
        public IStyle Get(Enum type)
        {
            switch ((BorderStyleType)type)
            {
                case BorderStyleType.Solid:
                    return new SolidStyle();
                case BorderStyleType.Dotted:
                    return new DottedStyle();
                case BorderStyleType.Fat:
                    return new FatStyle();
                case BorderStyleType.Middlefat:
                    return new MiddleFatStyle();
                case BorderStyleType.Double:
                    return new DoubleStyle();
                case BorderStyleType.Asterisk:
                    return new AsteriskStyle();
                default:
                    throw new ArgumentException("There's no border of that type.");
            }
        }
    }
}
