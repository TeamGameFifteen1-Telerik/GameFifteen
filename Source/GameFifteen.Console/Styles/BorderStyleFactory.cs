namespace GameFifteen.Console.Styles
{
    using System;

    using GameFifteen.Console.Contracts;

    public class BorderStyleFactory : IStyleFactory
    {
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
                case BorderStyleType.MiddleFat:
                    return new MiddleFatStyle();
                case BorderStyleType.Double:
                    return new DoubleStyle();
                case BorderStyleType.Default:
                    return new DefaultStyle();
                case BorderStyleType.Asteriks:
                    return new AsteriskStyle();
                default:
                    throw new ArgumentException("There's no border of that type.");
            }
        }
    }
}
