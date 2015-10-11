// <copyright file="StandartGameFifteenEngineTests.cs" company="Telerik Academy">All rights reserved.</copyright>
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
    using GameFifteen.ConsoleTests;
    using GameFifteen.Models;

    [TestClass]
    public class StandartGameFifteenEngineTests
    {
        /// <summary>
        /// Method verifies that the used IEngine.Initialize() method is successfully invoked.
        /// </summary>
        [TestMethod]
        public void TileEngineInitializeMethodShouldBeInvokedOnce()
        {
            var mockedEngine = new Mock<IEngine>();
            mockedEngine.Setup(x => x.Initialize());
            mockedEngine.Object.Initialize();
            mockedEngine.Verify(x => x.Initialize(), Times.Exactly(1));
        }

        [TestMethod]
        public void EngineShouldBeInstanceOfTypeIEngine()
        {
            var fakeInitializer = new Mock<IGameInitializater>().Object as IGameInitializater;
            var fakeUserInterface = new Mock<IUserInterface>().Object as IUserInterface;
            var fakeRenderer = new Mock<IRenderer>().Object as IRenderer;
            var fakePlayer = new Mock<IPlayer>().Object as IPlayer;
            var fakeGrid = new Mock<IGrid>().Object as IGrid;

            var engine = new StandartGameFifteenEngine(fakeRenderer, fakeUserInterface, fakeInitializer, fakePlayer, fakeGrid);
           
            Assert.IsInstanceOfType(engine, typeof(IEngine));
        }
    }
}
