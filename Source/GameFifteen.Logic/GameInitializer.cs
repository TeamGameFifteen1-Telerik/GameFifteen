namespace GameFifteen.Logic
{
    using System;
    using System.Collections.Generic;

    using GameFifteen.Logic.Contracts;
    using GameFifteen.Models;
    using GameFifteen.Common;

    public class GameInitializer : IGameInitializater
    {
        private static Random random;

        // TODO : do something about these
        private const int MinRoundsCount = 20;
        private const int MaxRoundsCount = 50;

        static GameInitializer()
        {
            random = new Random();
        }
        public void Initialize(Grid grid)
        {
            grid.Clear();
            this.InitilizeGrid(grid);
            this.ShuffleGrid(grid);
        }

        public void InitilizeGrid(Grid grid)
        {      
            for (int i = 0; i < GlobalConstants.TotalTilesCount - 1; i++)
            {
                string tileLabel = (i + 1).ToString();
                Tile tile = new Tile(tileLabel, i, TileType.Number);
                grid.AddTile(tile);
            }

            var emptyTile = new Tile(string.Empty, GlobalConstants.TotalTilesCount - 1, TileType.Empty);
            grid.AddTile(emptyTile);
        }

        private void ShuffleGrid(Grid grid)
        {
            int rounds = random.Next(MinRoundsCount, MaxRoundsCount);

            for (int i = 0; i < rounds; i++)
            {
                this.MoveEmptyTileRandomly(grid);
            }
        }

        private void MoveEmptyTileRandomly(Grid grid)
        {
            var neighbourTiles = this.GetNeighbours(grid);
            int randomNeighbourIndex = random.Next() % neighbourTiles.Count;
            Tile targetTile = neighbourTiles[randomNeighbourIndex];

            grid.SwapTiles(targetTile);
        }

        // gets the empty tile neighbours
        private List<Tile> GetNeighbours(Grid grid)
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
