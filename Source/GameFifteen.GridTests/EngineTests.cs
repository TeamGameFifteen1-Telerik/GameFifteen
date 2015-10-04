namespace GameFifteen.GridTests
{
    using System;

    using GameFifteen.Console;
    using GameFifteen.Logic;
    using GameFifteen.Models;
    using GameFifteen.Console.Styles;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class EngineTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestingEngineProcessCommandToThrowExceptionWhenInvalidInputCommandIsProvided()
        {
            var borderStyleFactory = new BorderStyleFactory();
            var renderer = new ConsoleRenderer(borderStyleFactory);
            var userInterface = new ConsoleInterface();
            var gameInitializer = new StandartGameInitializer();
            var player = new Player();
            var grid = new Grid();
            var engine = new StandartFifteenTilesEngine(renderer, userInterface, gameInitializer, player, grid);
            engine.ProcessCommand(Command.Invalid);
        }
    }
}
