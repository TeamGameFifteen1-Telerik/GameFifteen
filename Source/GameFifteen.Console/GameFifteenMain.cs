namespace GameFifteen.Console
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using GameFifteen.Logic;
    using GameFifteen.Models;

    public class GameFifteenMain
    {      
        public static void Main()
        {
            var renderer = new ConsoleRenderer();
            var userInterface = new ConsoleInterface();
            var gameInitializer = new GameInitializer();
            var engine = new Engine(renderer, userInterface, gameInitializer);
            engine.Run();
        }
    }
}
