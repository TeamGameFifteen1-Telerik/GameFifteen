namespace GameFifteen.Logic.Contracts
{
    using GameFifteen.Common;
    using GameFifteen.Models;
    using GameFifteen.Models.Contracts;

    /// <summary>
    /// Provides print methods.
    /// </summary>
    public interface IRenderer
    {
        //// TODO: refactor

        /// <summary>
        /// Adds style in collection.
        /// </summary>
        /// <param name="styles">Several styles or just one.</param>
        void AddStyle(params string[] styles);

        /// <summary>
        /// Prints players and theirs scores.
        /// </summary>
        /// <param name="scoreboard">Scoreboard that contains players.</param>
        void RenderScoreboard(Scoreboard scoreboard);

        /// <summary>
        /// Prints the grid.
        /// </summary>
        /// <param name="grid">The grid with tiles.</param>
        void RenderPlayScreen(IGameMember grid);

        /// <summary>
        /// Prints information message to user.
        /// </summary>
        /// <param name="message">The message.</param>
        void RenderMessage(string message);

        /// <summary>
        /// Prints the initial screen with game logo.
        /// </summary>
        /// <param name="menuStartPositionX">Horizontal menu position.</param>
        /// <param name="menuStartPositionY">Vertical menu position.</param>
        void RenderInitialScreen(int menuStartPositionX = GlobalConstants.MenuStartPositionX, int menuStartPositionY = GlobalConstants.MenuStartPositionY);

        void RenderGameOptions();
    }
}
