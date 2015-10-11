// <copyright file="IGrid.cs" company="Telerik Academy">All rights reserved.</copyright>
// <author>Team GameFifteen-1</author>
namespace GameFifteen.Models.Contracts
{
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// IGrid interface.
    /// </summary>
    public interface IGrid : IGameMember
    {
        /// <summary>
        /// Gets the number of tiles in a grid.
        /// </summary>
        /// <value>The TilesCount property gets the number of tiles in a grid.</value>
        int TilesCount { get; }

        /// <summary>
        /// Gets a value indicating whether a grid is sorted.
        /// </summary>
        /// <value>The IsSorted property gets a boolean to show if grid is sorted.</value>
        bool IsSorted { get; }

        /// <summary>
        /// Adds a tile to the grid.
        /// </summary>
        /// <param name="tile">Tile object.</param>
        void AddTile(Tile tile);

        /// <summary>
        /// Clears grid.
        /// </summary>
        void Clear();

        /// <summary>
        /// Gets a tile at certain position.
        /// </summary>
        /// <param name="position">Position as integer.</param>
        /// <returns>The tile at the given position.</returns>
        Tile GetTileAtPosition(int position);

        /// <summary>
        /// Gets tile by given label.
        /// </summary>
        /// <param name="tileLabel">The tile's label as string.</param>
        /// <returns>The tile with the given label.</returns>
        Tile GetTileFromLabel(string tileLabel);

        /// <summary>
        /// Swaps a numbered tile with the empty tile.
        /// </summary>
        /// <param name="targetTile">The numbered tile.</param>
        void SwapTiles(Tile targetTile);

        /// <summary>
        /// Boolean to show if tiles can be swapped.
        /// </summary>
        /// <param name="tile">A numbered tile to swap with the empty one.</param>
        /// <returns>True if tiles can be swapped, false otherwise.</returns>
        bool CanSwap(Tile tile);

        /// <summary>
        /// Foreach functionality of the grid.
        /// </summary>
        /// <returns>IEnumerator object.</returns>
        IEnumerator GetEnumerator();

        /// <summary>
        /// Saves grid state.
        /// </summary>
        /// <returns>Memento object containing the Grid's tiles at the moment the method is called.</returns>
        Memento SaveMemento();

        /// <summary>
        /// Restores saved grid state.
        /// </summary>
        /// <param name="memento">Memento object.</param>
        void RestoreMemento(Memento memento);
    }
}
