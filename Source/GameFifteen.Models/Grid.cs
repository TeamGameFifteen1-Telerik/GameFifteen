namespace GameFifteen.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using GameFifteen.Common;
    using GameFifteen.Models;

    public class Grid
    {
        // TODO : do something about these
        private const int MinRoundsCount = 20;
        private const int MaxRoundsCount = 50;

        private static Random random;

        private List<Tile> tiles;
        private Tile emptyTile;

        static Grid()
        {
            random = new Random();
        }

        public Grid()
        {
            this.tiles = new List<Tile>();
            
            //// this.Initialize();
        }

        public int GetTilesCount()
        {
            var result = this.tiles.Count;
            return result;
        }

        public Tile EmptyTile
        {
            get
            {
                return this.GetEmptyTile();
            }

            set
            {
                this.emptyTile = value;
            }
        }

        public void AddTile(Tile tile)
        {
            this.tiles.Add(tile);          
        }

        public bool IsSorted
        {
            get
            {
                return this.CheckIfSorted();
            }
        }
      
        public void Clear()
        {
            this.tiles.Clear();
        }

        public Tile GetTileAtPosition(int position)
        {
            var tile = this.tiles.ElementAt(position);
            return tile;
        }

        private Tile GetTileFromLabel(string tileLabel)
        {
            Tile tile = this.tiles.FirstOrDefault(t => t.Label == tileLabel);
            return tile;
        }

        /*
        private int GetTilePosition(string tileLabel)
        {
            Tile tile = this.tiles.FirstOrDefault(t => t.Label == tileLabel);
            return this.tiles.IndexOf(tile);
        }
        */

        public void Shuffle()
        {
            int rounds = random.Next(MinRoundsCount, MaxRoundsCount);

            for (int i = 0; i < rounds; i++)
            {
                this.MoveEmptyTileRandomly();
            }
        }

        //// TODO refactor method; move to engine???
        public void MoveTile(int tileLable)
        {
            if (tileLable < 0 || GlobalConstants.TOTAL_TILES_COUNT - 1 < tileLable)
            {
                throw new ArgumentException("Invalid move!");
            }

            Tile tile = this.GetTileFromLabel(tileLable.ToString());

            bool isValidNeighbour = this.ValidateNeighbour(tile);

            if (isValidNeighbour)
            {
                this.SwapTiles(tile);
            }
            else
            {
                throw new Exception("Invalid move!");
            }
        }

        private void MoveEmptyTileRandomly()
        {
            var neighbourTiles = this.GetNeighbours();
            int randomNeighbourIndex = random.Next() % neighbourTiles.Count();
            Tile targetTile = neighbourTiles[randomNeighbourIndex];

            this.SwapTiles(targetTile);
        }

        //// gets the empty tile neighbours
        private List<Tile> GetNeighbours()
        {
            List<Tile> neighbourTiles = new List<Tile>();

            foreach (Tile tile in this.tiles)
            {
                bool isValidNeighbour = this.ValidateNeighbour(tile);

                if (isValidNeighbour)
                {
                    neighbourTiles.Add(tile);
                }
            }

            return neighbourTiles;
        }

        private bool ValidateNeighbour(Tile tile)
        {
            int tilesDistance = this.EmptyTile.Position - tile.Position;

            bool isTileAtFirstColumn = (tile.Position + 1) % GlobalConstants.GRID_SIZE == 1;
            bool isTileAtLastColumn = (tile.Position + 1) % GlobalConstants.GRID_SIZE == 0;
            bool shouldMoveLeft = tilesDistance == -1;
            bool shouldMoveRight = tilesDistance == 1;
            
            bool isHorizontalNeigbour = Math.Abs(tilesDistance) == 1;
            bool isValidHorizontalNeighbour = isHorizontalNeigbour && !((isTileAtFirstColumn && shouldMoveLeft) || (isTileAtLastColumn && shouldMoveRight));

            bool isValidVerticalNeighbour = Math.Abs(tilesDistance) == GlobalConstants.GRID_SIZE;
            
            return isValidHorizontalNeighbour || isValidVerticalNeighbour;
        }

        private void SwapTiles(Tile targetTile)
        {
            int targetTilePosition = targetTile.Position;
            this.tiles[targetTile.Position].Position = this.EmptyTile.Position;
            this.tiles[this.EmptyTile.Position].Position = targetTilePosition;
            this.tiles.Sort();
        }

        private Tile GetEmptyTile()
        {
            foreach (Tile tile in this.tiles)
            {
                if (tile.Label == string.Empty)
                {
                    return tile;
                }
            }

            return null;
        }

        private bool CheckIfSorted()
        {
            for (int i = 0; i < this.tiles.Count - 1; i++)
            {
                if (this.tiles[i].Label != (this.tiles[i].Position + 1).ToString())
                {
                    return false;
                }
            }

            return true;
        }
    }
}
