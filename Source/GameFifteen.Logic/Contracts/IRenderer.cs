namespace GameFifteen.Logic.Contracts
{
    using System.Collections.Generic;

    using GameFifteen.Models;
    using GameFifteen.Models.Contracts;

    public interface IRenderer
    {
        //// TODO: refactor
        void PrintScoreboard(Scoreboard scoreboard);

        void PrintMatrix(IGrid grid);

        void PrintMessage(string message);
    }
}
