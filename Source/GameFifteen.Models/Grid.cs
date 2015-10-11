// <copyright file="Grid.cs" company="Telerik Academy">All rights reserved.</copyright>
// <author>Team GameFifteen-1</author>
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
        /// Initializes a new instance of the <see cref="Grid" /> class.
        /// </summary>
        public Grid()
        {
            this.tiles = new List<Tile>();
        }

        /// <summary>
        /// Gets the number of tiles in a grid.
        /// </summary>
        /// <value>The TilesCount property gets the number of tiles in a grid.</value>
        public int TilesCount
        {
            get
            {
                return this.tiles.Count;
            }
        }

        /// <summary>
        /// Gets a value indicating whether a grid is sorted.
        /// </summary>
        /// <value>The IsSorted property gets a boolean to show if grid is sorted.</value>
        public bool IsSorted
        {
            get
            {
                return this.CheckIfSorted();
            }
        }

        /// <summary>
        /// Adds a tile to the grid.
        /// </summary>
        /// <param name="tile">Tile object.</param>
        public void AddTile(Tile tile)
        {
            this.tiles.Add(tile);          
        }

        /// <summary>
        /// Clears grid.
        /// </summary>
        public void Clear()
        {
            this.tiles.Clear();
        }

        /// <summary>
        /// Gets a tile at certain position.
        /// </summary>
        /// <param name="position">Position as integer.</param>
        /// <returns>The tile at the given position.</returns>
        public Tile GetTileAtPosition(int position)
        {
            var tile = this.tiles.ElementAt(position);
            return tile;
        }

        /// <summary>
        /// Gets tile by given label.
        /// </summary>
        /// <param name="tileLabel">The tile's label as string.</param>
        /// <returns>The tile with the given label.</returns>
        public Tile GetTileFromLabel(string tileLabel)
        {
            Tile tile = this.tiles.FirstOrDefault(t => t.Label == tileLabel);
            return tile;
        }

        /// <summary>
        /// Swaps a numbered tile with the empty tile.
        /// </summary>
        /// <param name="targetTile">The numbered tile.</param>
        public void SwapTiles(Tile targetTile)
        {
            int emptyTilePosition = this.GetEmptyTile().Position;
            int targetTilePosition = targetTile.Position;
            this.tiles[targetTile.Position].Position = emptyTilePosition;
            this.tiles[emptyTilePosition].Position = targetTilePosition;
            this.tiles.Sort();
        }

        /// <summary>
        /// Boolean to show if tiles can be swapped.
        /// </summary>
        /// <param name="tile">A numbered tile to swap with the empty one.</param>
        /// <returns>True if tiles can be swapped, false otherwise.</returns>
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

        /// <summary>
        /// Foreach functionality of the grid.
        /// </summary>
        /// <returns>IEnumerator object.</returns>
        public IEnumerator GetEnumerator()
        {
            foreach (Tile tile in this.tiles)
            {
                yield return tile;
            }
        }

        /// <summary>
        /// Saves grid state.
        /// </summary>
        /// <returns>Memento object containing the Grid's tiles at the moment the method is called.</returns>
        public Memento SaveMemento()
        {
            var tiles = this.tiles.Clone<Tile>().ToList();

            return new Memento(tiles);
        }

        /// <summary>
        /// Restores saved grid state.
        /// </summary>
        /// <param name="memento">Memento object.</param>
        public void RestoreMemento(Memento memento)
        {
            this.tiles = memento.Tiles.Clone<Tile>().ToList();
        }

        /// <summary>
        /// Gets a text representation for the grid.
        /// </summary>
        /// <returns>The grid as a string.</returns>
        public override string GetTextRepresentation()
        {
            var sb = new StringBuilder();
            var lines = new List<string>();

            for (int i = 0, colCounter = 1; i < this.TilesCount; i++, colCounter++)
            {
                Tile currentTile = this.GetTileAtPosition(i);
                int emptyFillerLength = (this.TilesCount - 1).ToString().Length - currentTile.GetTextRepresentation().Length;
                string emptyFiller = new string(' ', emptyFillerLength);

                sb.AppendFormat("{0}{1} ", emptyFiller, currentTile.GetTextRepresentation());

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
