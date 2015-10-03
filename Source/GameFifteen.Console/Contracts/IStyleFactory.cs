namespace GameFifteen.Console.Contracts
{
    using System;

    public interface IStyleFactory
    {
        IStyle Get(Enum type);
    }
}
