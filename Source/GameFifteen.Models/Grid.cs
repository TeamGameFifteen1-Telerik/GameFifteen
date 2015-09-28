namespace GameFifteen.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using GameFifteen.Common;
    using System.Collections;

    public class Grid : IEnumerable
    {
        private List<Tile> tiles;
        private Tile emptyTile;

        public Grid()
        {
            this.tiles = new List<Tile>();
        }

        public int TilesCount
        {
            get
            {
                return this.tiles.Count;
            }
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

        public Tile GetTileFromLabel(string tileLabel)
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

        public void SwapTiles(Tile targetTile)
        {
            int targetTilePosition = targetTile.Position;
            this.tiles[targetTile.Position].Position = this.EmptyTile.Position;
            this.tiles[this.EmptyTile.Position].Position = targetTilePosition;
            this.tiles.Sort();
        }

        public bool CanSwap(Tile tile)
        {
            int tilesDistance = this.EmptyTile.Position - tile.Position;

            bool isTileAtFirstColumn = (tile.Position + 1) % GlobalConstants.GridSize == 1;
            bool isTileAtLastColumn = (tile.Position + 1) % GlobalConstants.GridSize == 0;
            bool shouldMoveLeft = tilesDistance == -1;
            bool shouldMoveRight = tilesDistance == 1;

            bool isHorizontalNeigbour = Math.Abs(tilesDistance) == 1;
            bool isValidHorizontalNeighbour = isHorizontalNeigbour && !((isTileAtFirstColumn && shouldMoveLeft) || (isTileAtLastColumn && shouldMoveRight));

            bool isValidVerticalNeighbour = Math.Abs(tilesDistance) == GlobalConstants.GridSize;

            return isValidHorizontalNeighbour || isValidVerticalNeighbour;
        }

        public IEnumerator GetEnumerator()
        {
            foreach (var tile in this.tiles)
            {
                yield return tile;
            }
        }

        public Memento SaveMemento()
        {
            Tile emptyTile = (Tile)this.EmptyTile.Clone();
            var tiles = this.tiles.Clone<Tile>().ToList();

            return new Memento(tiles, emptyTile);
        }

        public void RestoreMemento(Memento memento)
        {
            this.tiles = memento.Tiles.Clone<Tile>().ToList();
            this.EmptyTile = memento.EmptyTile;
        }

        private Tile GetEmptyTile()
        {
            return this.tiles.FirstOrDefault(t => t.Type == TileType.Empty);
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
