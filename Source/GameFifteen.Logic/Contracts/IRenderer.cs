namespace GameFifteen.Logic.Contracts
{
    using System.Collections.Generic;

    using GameFifteen.Models.Contracts;
    using GameFifteen.Models;

    public interface IRenderer
    {
        //// TODO: refactor
        void PrintScoreboard(Scoreboard scoreboard);

        void PrintMatrix(Grid sourceMatrix);

        void PrintMatrix(Grid sourceMatrix, GridBorderStyle style);

        void PrintMessage(string message);
    }
}
