using GameFifteen.Common;
using GameFifteen.Logic.Contracts;
using GameFifteen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFifteen.Logic
{
    public class GameInitializer : IGameInitializater
    {
        public void Initialize(Grid grid)
        {
            //// TODO
            grid.Clear();
            this.InitializeGrid(grid);

            //// tiles = new Grid(); //grid.InitializeGrid();
            //// tiles = grid.ShuffleMatrix(tiles);

        }

        public void InitializeGrid(Grid grid)
        {
            for (int i = 0; i < GlobalConstants.TOTAL_TILES_COUNT - 1; i++)
            {
                string tileLabel = (i + 1).ToString();
                Tile tile = new Tile(tileLabel, i, TileType.Number);
                grid.AddTile(tile);
            }

            // TODO: move?cal
            var emptyTile = new Tile(string.Empty, GlobalConstants.TOTAL_TILES_COUNT - 1, TileType.Empty);
            grid.AddTile(emptyTile);
            grid.EmptyTile = emptyTile;
            grid.Shuffle();
        }
    }
}
