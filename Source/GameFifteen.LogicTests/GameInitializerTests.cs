namespace GameFifteen.LogicTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using GameFifteen.Logic;
    using GameFifteen.Logic.Contracts;
    using GameFifteen.Models.Contracts;
    using System.Collections.Generic;
    [TestClass]
    public class GameInitializerTests
    {
        [TestMethod]
        public void GameInitializerMethodShouldBeInvokedOnce()
        {
            var fakeInitializer = new Mock<IGameInitializater>();
            var fakeGrid = new Mock<IGrid>();
            fakeInitializer.Setup(i => i.Initialize(It.IsAny<IGrid>()));
            fakeInitializer.Object.Initialize(fakeGrid.Object);
            fakeInitializer.Verify(i=>i.Initialize(fakeGrid.Object),Times.Exactly(1));
        }

        [TestMethod]
        public void InitializeGridMethodShouldBeInvokedOnceAndShouldFillDummyList()
        {
            var mockedInitializer = new Mock<IGameInitializater>();
            var fakeGrid = new Mock<IGrid>();
            mockedInitializer.Setup(m => m.InitilizeGrid(It.IsAny<IGrid>()));
            mockedInitializer.Object.InitilizeGrid(fakeGrid.Object);
            mockedInitializer.Verify(m => m.InitilizeGrid(fakeGrid.Object), Times.Exactly(1));
        }

        [TestMethod]
        public void TileEngineInitializeMethodShouldBeInvokedOnce()
        {
            var mEngine = new Mock<IEngine>();
            mEngine.Setup(x => x.Initialize());
            mEngine.Object.Initialize();
            mEngine.Verify(x => x.Initialize(), Times.Exactly(1));
        }
    }
}
