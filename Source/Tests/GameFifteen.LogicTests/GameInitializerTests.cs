// <copyright file="GameInitializerTests.cs" company="Telerik Academy">All rights reserved.</copyright>
// <author>Team GameFifteen-1</author>
namespace GameFifteen.LogicTests
{
    using System;

    using GameFifteen.Common;
    using GameFifteen.Console;
    using GameFifteen.Console.Styles;
    using GameFifteen.Logic;
    using GameFifteen.Logic.Contracts;
    using GameFifteen.Models;
    using GameFifteen.Models.Contracts;
    using GameFifteen.Tests.Common;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

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

        [TestMethod]
        public void InitializeGridMethodShouldFillGridWithTotalTiles()
        {
            IGrid grid = new Grid();
            var gameInitializer = new StandartGameInitializer();

            gameInitializer.Initialize(grid);

            var actual = grid.TilesCount;
            var expected = GlobalConstants.TotalTilesCount;
            Assert.AreEqual(expected, actual);
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

        [TestMethod]
        public void TestThatThisEngineCannotBeTested()
        {
            var mockedConsole = new Mock<HelperReader>();
            var mockedInterface = new Mock<IUserInterface>(MockBehavior.Loose);
            mockedConsole.SetupSequence(c => c.ReadLine()).Returns("start").Returns("exit").Returns("no");
            
            Console.SetIn(mockedConsole.Object);

            //mockedInterface.SetupSequence(i => i.GetCommandFromInput()).Returns(Command.Start).Returns(Command.Exit).Returns(Command.Yes);
            IRenderer rende = new ConsoleRenderer(new BorderStyleFactory());
            IUserInterface consoleInterface = new ConsoleInterface();
            IGameInitializater gameInit = new StandartGameInitializer();
            IPlayer player = new Player();
            IGrid grid = new Grid();
            StandartGameFifteenEngine engine = new StandartGameFifteenEngine(rende, consoleInterface, gameInit, player, grid);
            engine.Initialize();
        }

        [TestMethod]
        public void TestNormalBehaviorOnGameEngine()
        {
            var mockedConsole = new Mock<HelperReader>();
            var mockedInterface = new Mock<IUserInterface>(MockBehavior.Loose);
            mockedConsole.SetupSequence(c => c.ReadLine()).Returns("start").Returns("solve").Returns("pesho").Returns("yes").Returns("style=double").Returns("exit").Returns("no");

            Console.SetIn(mockedConsole.Object);

            IRenderer rende = new ConsoleRenderer(new BorderStyleFactory());
            IUserInterface consoleInterface = new ConsoleInterface();
            IGameInitializater gameInit = new StandartGameInitializer();
            IPlayer player = new Player();
            IGrid grid = new Grid();
            StandartGameFifteenEngine engine = new StandartGameFifteenEngine(rende, consoleInterface, gameInit, player, grid);
            engine.Initialize();
        }

        [TestMethod]
        public void TestEngineToSaveScoreFromPlayerAndPrintIt()
        {
            var mockedConsole = new Mock<HelperReader>();
            var mockedWriter = new Mock<HelperWriter>();
            var mockedInterface = new Mock<IUserInterface>(MockBehavior.Loose);
            mockedConsole.SetupSequence(c => c.ReadLine())
                .Returns("start")
                .Returns("solve")
                .Returns("pesho")
                .Returns("yes")
                .Returns("1")
                .Returns("2")
                .Returns("3")
                .Returns("4")
                .Returns("save")
                .Returns("5")
                .Returns("5")
                .Returns("6")
                .Returns("restart")
                .Returns("7")
                .Returns("8")
                .Returns("9")
                .Returns("10")
                .Returns("11")
                .Returns("load")
                .Returns("yes")
                .Returns("12")
                .Returns("14")
                .Returns("13")
                .Returns("solve")
                .Returns("pesho")
                .Returns("yes")
                .Returns("top")
                .Returns("game")
                .Returns("exit")
                .Returns("no");

            Console.SetIn(mockedConsole.Object);
            Console.SetOut(mockedWriter.Object);
            IRenderer rende = new ConsoleRenderer(new BorderStyleFactory());
            IUserInterface consoleInterface = new ConsoleInterface();
            IGameInitializater gameInit = new StandartGameInitializer();
            IPlayer player = new Player();
            IGrid grid = new Grid();
            StandartGameFifteenEngine engine = new StandartGameFifteenEngine(rende, consoleInterface, gameInit, player, grid);
            engine.Initialize();

            mockedWriter.Verify(w => w.Write(It.Is<string>(s => s == "pesho ")), Times.AtLeast(2));
        }

        [TestMethod]
        public void TestEngineToWhenHowCommandIsPassedTPrintDescription()
        {
            var mockedConsole = new Mock<HelperReader>();
            var mockedWriter = new Mock<HelperWriter>();
            var mockedInterface = new Mock<IUserInterface>(MockBehavior.Loose);
            mockedConsole.SetupSequence(c => c.ReadLine())
                .Returns("start")
                .Returns("how")
                .Returns("game")
                .Returns("load")
                .Returns("yes")
                .Returns("exit")
                .Returns("no");

            Console.SetIn(mockedConsole.Object);
            Console.SetOut(mockedWriter.Object);
            IRenderer rende = new ConsoleRenderer(new BorderStyleFactory());
            IUserInterface consoleInterface = new ConsoleInterface();
            IGameInitializater gameInit = new StandartGameInitializer();
            IPlayer player = new Player();
            IGrid grid = new Grid();
            StandartGameFifteenEngine engine = new StandartGameFifteenEngine(rende, consoleInterface, gameInit, player, grid);
            engine.Initialize();

            mockedWriter.Verify(w => w.Write(It.Is<string>(s => s.Contains("->"))), Times.AtLeast(GameMessages.CommandsDescription.Count));
        }
    }
}
