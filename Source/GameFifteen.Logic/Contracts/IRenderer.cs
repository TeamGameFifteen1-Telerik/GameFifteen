namespace GameFifteen.Logic.Contracts
{
    using GameFifteen.Models;
    using GameFifteen.Models.Contracts;

    public interface IRenderer
    {
        //// TODO: refactor
        void AddStyle(params string[] styles);

        void RenderScoreboard(Scoreboard scoreboard);

        void RenderGrid(IGrid grid);

        void RenderMessage(string message);
    }
}
