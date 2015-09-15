namespace GameFifteen.Logic.Contracts
{
    using System.Collections.Generic;

    using GameFifteen.Models;

    public interface IRenderer
    {
        //// TODO: refactor
        void PrintScoreboard(List<Player> players);

        void PrintMatrix(Grid sourceMatrix);

        void PrintMessage(string message);
    }
}
