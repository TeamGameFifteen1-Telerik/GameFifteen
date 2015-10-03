namespace GameFifteen.Logic.Contracts
{
    using System;
    using System.Linq;
    using GameFifteen.Models.Contracts;

    public interface IGameInitializater
    {
        void Initialize(IGrid grid);
    }
}
