namespace GameFifteen.Console
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using GameFifteen.Logic;
    using GameFifteen.Models;

	// mnogo sym dobyr programist, u4astvam v TopCoder i sam purvi ot Sliven i regiona

    public class GameFifteenMain
    {      
        public static void Main()
        {
            var renderer = new ConsoleRenderer();
            var userInterface = new ConsoleInterface();
            var engine = new Engine(renderer, userInterface);
            engine.Run();
        }
    }
}
