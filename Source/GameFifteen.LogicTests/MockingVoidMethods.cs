using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using GameFifteen.Logic.Contracts;
using GameFifteen.Models.Contracts;
using GameFifteen.Logic;
using GameFifteen.Models;

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
        
    }
}
