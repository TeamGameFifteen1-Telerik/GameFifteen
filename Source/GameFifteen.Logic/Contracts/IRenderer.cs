namespace GameFifteen.Logic.Contracts
{
    using System.Collections.Generic;

    using GameFifteen.Models;

    public interface IRenderer
    {
        //// TODO: refactor
        void PrintScoreboard(Scoreboard scoreboard);

        void PrintMatrix(Grid sourceMatrix);

        void PrintMessage(string message);
    }
}
