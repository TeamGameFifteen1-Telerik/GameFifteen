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
            grid.Initialize();

            //// tiles = new Grid(); //grid.InitializeGrid();
            //// tiles = grid.ShuffleMatrix(tiles);
            //TODO
            Console.Write("Enter a number to move: ");

        }
    }
}
