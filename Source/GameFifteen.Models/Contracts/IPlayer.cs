namespace GameFifteen.Models.Contracts
{
    using System;

    public interface IPlayer : ICloneable, IGameMember
    {
        string Name { get; set; }

        int Moves { get; set; }
    }
}
