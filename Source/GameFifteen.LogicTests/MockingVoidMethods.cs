using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using GameFifteen.Logic.Contracts;
using GameFifteen.Models.Contracts;
using GameFifteen.Logic;
using GameFifteen.Models;
using GameFifteen.Console;

namespace GameFifteen.LogicTests
{
    [TestClass]
    public class MockingVoidMethods
    {
        [TestMethod]
        public void TestGridAddTileMethodToBeInvokedAtLeastOnce()
        {
            var fakeGrid = new Mock<IGrid>();
            fakeGrid.Setup(g => g.AddTile(new Tile()));
            fakeGrid.Object.AddTile(new Tile());
            fakeGrid.Object.AddTile(new Tile());
            fakeGrid.Verify(g => g.AddTile(It.IsAny<Tile>()), Times.AtLeast(2));
        }

        [TestMethod]

        public void TestGameInitilizeToBeInvokedAtLeastOne()
        {
            IGrid grid = new Grid();
            var fakeGameInit = new Mock<IGameInitializater>();
            fakeGameInit.Setup(i => i.InitilizeGrid(grid));
            fakeGameInit.Object.InitilizeGrid(grid);
            fakeGameInit.Verify(i => i.InitilizeGrid(grid), Times.AtLeastOnce());
        }

       [TestMethod]
        public void TestWheatherTilesAreAddedCorrectly()
       {
           var fGrid = new Mock<IGrid>();
            var tile = new Tile("1",1,TileType.Number);
            fGrid.Setup(g => g.AddTile(tile));
            fGrid.Object.AddTile(tile);
            fGrid.Verify(g => g.AddTile(tile), Times.AtLeastOnce());
            fGrid.Setup(g => g.TilesCount).Returns(1);
            Assert.AreEqual(1, fGrid.Object.TilesCount);

            
       }

       [TestMethod]
        public void TestWheatherTileAtPositionMethodReturnsCorrectValues()
       {
           var fGrid = new Mock<IGrid>();
           var tile = new Tile("1", 1, TileType.Number);
           fGrid.Setup(g => g.GetTileAtPosition(1)).Returns(new Tile("1", 1, TileType.Number));
           var exp = fGrid.Object.GetTileAtPosition(1);
           Assert.AreEqual(tile.Position, exp.Position);
       }
    }
}
