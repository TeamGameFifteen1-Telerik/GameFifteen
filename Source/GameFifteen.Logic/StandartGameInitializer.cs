namespace GameFifteen.Logic
{
    using System;
    using System.Collections.Generic;

    using GameFifteen.Common;
    using GameFifteen.Logic.Contracts;
    using GameFifteen.Models;
    using GameFifteen.Models.Contracts;

    public class StandartGameInitializer : IGameInitializater
    {
        // TODO : do something about these
        private const int MinRoundsCount = 20;
        private const int MaxRoundsCount = 50;

        private static Random random;

        static StandartGameInitializer()
        {
            random = new Random();
        }

        public void Initialize(IGrid grid)
        {
            grid.Clear();
            this.InitilizeGrid(grid);
            this.ShuffleGrid(grid);
        }

        public void InitilizeGrid(IGrid grid)
        {
            var emptyTile = new Tile(string.Empty, GlobalConstants.TotalTilesCount - 1, TileType.Empty);          
            //// grid.EmptyTile = emptyTile;

            for (int i = 0; i < GlobalConstants.TotalTilesCount - 1; i++)
            {
                Tile tile = emptyTile.CloneMemberwise();    //// new Tile(tileLabel, i, TileType.Number);
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

        // gets the empty tile neighbours
        private List<Tile> GetNeighbours(IGrid grid)
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
