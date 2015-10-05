namespace GameFifteen.Logic.Contracts
{
    using System;
    using System.Linq;
    using GameFifteen.Models.Contracts;

    /// <summary>
    /// Providing concrete initialization.
    /// </summary>
    public interface IGameInitializater
    {
        /// <summary>
        /// Initializes the grid.
        /// </summary>
        /// <param name="grid">Grid that contains tiles.</param>
        void Initialize(IGrid grid);

        /// <summary>
        /// Fills grid with tiles.
        /// </summary>
        /// <param name="grid">The empty grid.</param>
        void InitilizeGrid(IGrid grid);
    }
}
