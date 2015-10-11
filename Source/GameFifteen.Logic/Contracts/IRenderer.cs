// <copyright file="IRenderer.cs" company="Telerik Academy">All rights reserved.</copyright>
// <author>Team GameFifteen-1</author>
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
        void RenderInitialScreen();

        /// <summary>
        /// Prints the game options.
        /// </summary>
        void RenderGameOptions();
    }
}
