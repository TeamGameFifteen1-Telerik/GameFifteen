namespace GameFifteen.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using GameFifteen.Common;
    using GameFifteen.Models.Contracts;

    /// <summary>
    /// Memento design pattern
    /// The 'Originator' class.
    /// </summary>
    public class Grid : GameMember, IGrid, IEnumerable, IGameMember
    {
        private List<Tile> tiles;

        /// <summary>
        /// Grid constructor.
        /// </summary>
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

        public bool IsSorted
        {
            get
            {
                return this.CheckIfSorted();
            }
        }

        public void AddTile(Tile tile)
        {
            this.tiles.Add(tile);          
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

        public void SwapTiles(Tile targetTile)
        {
            int emptyTilePosition = this.GetEmptyTile().Position;
            int targetTilePosition = targetTile.Position;
            this.tiles[targetTile.Position].Position = emptyTilePosition;
            this.tiles[emptyTilePosition].Position = targetTilePosition;
            this.tiles.Sort();
        }

        public bool CanSwap(Tile tile)
        {
            int tilesDistance = this.GetEmptyTile().Position - tile.Position;

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
            foreach (Tile tile in this.tiles)
            {
                yield return tile;
            }
        }

        public Memento SaveMemento()
        {
            var tiles = this.tiles.Clone<Tile>().ToList();

            return new Memento(tiles);
        }

        public void RestoreMemento(Memento memento)
        {
            this.tiles = memento.Tiles.Clone<Tile>().ToList();
        }

        public override string Display()
        {
            var sb = new StringBuilder();
            var lines = new List<string>();

            for (int i = 0, colCounter = 1; i < this.TilesCount; i++, colCounter++)
            {
                Tile currentTile = this.GetTileAtPosition(i);
                int emptyFillerLength = (this.TilesCount - 1).ToString().Length - currentTile.Display().Length;
                string emptyFiller = new string(' ', emptyFillerLength);

                sb.AppendFormat("{0}{1} ", emptyFiller, currentTile.Display());

                if (colCounter == GlobalConstants.GridSize)
                {
                    lines.Add(sb.ToString());
                    sb.Clear();
                    colCounter = 0;
                }
            }

            return string.Join("|", lines);
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
