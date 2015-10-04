namespace GameFifteen.Console
{
    using System;

    using GameFifteen.Console.Contracts;

    public abstract class GridBorderStyle : IStyle
    {
        public abstract string Top { get; }

        public abstract string Bottom { get; }

        public abstract string Left { get; }

        public abstract string Right { get; }

        public abstract Enum Type { get; }
    }
}
