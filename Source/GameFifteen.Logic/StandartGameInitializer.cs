// <copyright file="StandartGameInitializer.cs" company="Telerik Academy">All rights reserved.</copyright>
// <author>Team GameFifteen-1</author>
namespace GameFifteen.Logic
{
    using System;
    using System.Collections.Generic;

    using GameFifteen.Common;
    using GameFifteen.Logic.Contracts;
    using GameFifteen.Models;
    using GameFifteen.Models.Contracts;

    /// <summary>
    /// Game creator.
    /// </summary>
    public class StandartGameInitializer : IGameInitializater
    {
        private const int MinRoundsCount = 1120;
        private const int MaxRoundsCount = 1150;

        private static Random random;

        /// <summary>
        /// Initializes static members of the <see cref="StandartGameInitializer" /> class.
        /// </summary>
        static StandartGameInitializer()
        {
            random = new Random();
        }

        /// <summary>
        /// Full initialization of grid.
        /// </summary>
        /// <param name="grid">The empty grid.</param>
        public void Initialize(IGrid grid)
        {
            grid.Clear();
            this.InitilizeGrid(grid);
            this.ShuffleGrid(grid);
        }

        /// <summary>
        /// Fills grid with tiles.
        /// </summary>
        /// <param name="grid">The empty grid.</param>
        public void InitilizeGrid(IGrid grid)
        {
            var emptyTile = new Tile(string.Empty, GlobalConstants.TotalTilesCount - 1, TileType.Empty);          
  
            for (int i = 0; i < GlobalConstants.TotalTilesCount - 1; i++)
            {
                Tile tile = emptyTile.CloneMemberwise();
                tile.Label = (i + 1).ToString();
                tile.Position = i;
                tile.Type = TileType.Number;
                grid.AddTile(tile);
            }

            grid.AddTile(emptyTile);
        }

        private void ShuffleGrid(IGrid grid)
        {
            int rounds = random.Next(MinRoundsCount, MaxRoundsCount);

            for (int i = 0; i < rounds; i++)
            {
                this.MoveEmptyTileRandomly(grid);
            }
        }

        private void MoveEmptyTileRandomly(IGrid grid)
        {
            var neighbourTiles = this.GetNeighbours(grid);
            int randomNeighbourIndex = random.Next() % neighbourTiles.Count;
            Tile targetTile = neighbourTiles[randomNeighbourIndex];
            grid.SwapTiles(targetTile);
        }

        /// <summary>
        /// Gets the empty tile neighbors.
        /// </summary>
        /// <param name="grid">IGrid object.</param>
        /// <returns>A list of neighbors.</returns>
        private IList<Tile> GetNeighbours(IGrid grid)
        {
            List<Tile> neighbourTiles = new List<Tile>();

            foreach (Tile tile in grid)
            {
                bool isValidNeighbour = grid.CanSwap(tile);

                if (isValidNeighbour)
                {
                    neighbourTiles.Add(tile);
                }
            }

            return neighbourTiles;
        }
    }
}
