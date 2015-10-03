namespace GameFifteen.GridTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using GameFifteen.Logic;
    using GameFifteen.Console;
    [TestClass]
    public class EngineTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestingEngineProcessCommandToThrowExceptionWhenInvalidInputCommandIsProvided()
        {
            var renderer = new ConsoleRenderer();
            var userInterface = new ConsoleInterface();
            var gameInitializer = new GameInitializer();
            var engine = new StandartFifteenTilesEngine(renderer,userInterface,gameInitializer);
            engine.ProcessCommand(Command.Invalid);
        }
    }
}
