// <copyright file="GameInitializerTests.cs" company="Telerik Academy">All rights reserved.</copyright>
// <author>Team GameFifteen-1</author>
namespace GameFifteen.LogicTests
{
    using System;
    using System.Collections.Generic;
    using GameFifteen.Logic;
    using GameFifteen.Logic.Contracts;
    using GameFifteen.Models.Contracts;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using GameFifteen.Models;
    using GameFifteen.Common;
    using GameFifteen.Console;
    using GameFifteen.Console.Styles;

    /// <summary>
    /// This class tests if the methods in GameFifteen.Logic module projects are successfully invoked.
    /// </summary>
    [TestClass]
    public class GameInitializerTests
    {
        /// <summary>
        /// Method verifies that the used IGameInitializer.Initialize() method is successfully invoked.
        /// </summary>
        [TestMethod]
        public void GameInitializerMethodShouldBeInvokedOnce()
        {
            var fakeInitializer = new Mock<IGameInitializater>();
            var fakeGrid = new Mock<IGrid>();
            fakeInitializer.Setup(i => i.Initialize(It.IsAny<IGrid>()));
            fakeInitializer.Object.Initialize(fakeGrid.Object);
            fakeInitializer.Verify(i => i.Initialize(fakeGrid.Object), Times.Exactly(1));
        }

        /// <summary>
        /// Method verifies that the used IGameInitializer.InitializeGrid() method is successfully invoked.
        /// </summary>
        [TestMethod]
        public void TestInitializeGridMethodShouldBeInvokedOnce()
        {
            var mockedInitializer = new Mock<IGameInitializater>();
            var fakeGrid = new Mock<IGrid>();
            mockedInitializer.Setup(m => m.InitilizeGrid(It.IsAny<IGrid>()));
            mockedInitializer.Object.InitilizeGrid(fakeGrid.Object);
            mockedInitializer.Verify(m => m.InitilizeGrid(fakeGrid.Object), Times.Exactly(1));
        }

        /// <summary>
        /// Method verifies that the used IEngine.Initialize() method is successfully invoked.
        /// </summary>
        [TestMethod]
        public void TestTileEngineInitializeMethodShouldBeInvokedOnce()
        {
            var mockedEngine = new Mock<IEngine>();
            mockedEngine.Setup(x => x.Initialize());
            mockedEngine.Object.Initialize();
            mockedEngine.Verify(x => x.Initialize(), Times.Exactly(1));
        }

        [TestMethod]
        public void TestCorrectBehaviorFromGameInitializatorToNotThrow()
        {
            StandartGameInitializer gameinit = new StandartGameInitializer();
            Grid grid = new Grid();
            gameinit.Initialize(grid);
            Assert.AreEqual(GlobalConstants.GridSize * GlobalConstants.GridSize, grid.TilesCount);
        }

        [TestMethod]
        public void TestStandartGameFifteenEngineNotToThrowWhenItsCreated()
        {
            IRenderer rende = new ConsoleRenderer(new BorderStyleFactory());
            IUserInterface consoleInterface = new ConsoleInterface();
            IGameInitializater gameInit = new StandartGameInitializer();
            IPlayer player = new Player();
            IGrid grid = new Grid();
            StandartGameFifteenEngine engine = new StandartGameFifteenEngine(rende, consoleInterface, gameInit, player, grid);
        }
    }
}
