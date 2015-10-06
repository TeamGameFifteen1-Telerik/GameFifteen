using System;

namespace GameFifteen.Models.Contracts
{
    public interface IPlayer : ICloneable
    {
        string Name { get; set; }

        int Moves { get; set; }
    }
}
