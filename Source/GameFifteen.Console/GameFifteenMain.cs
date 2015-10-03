namespace GameFifteen.Console
{
    using GameFifteen.Logic;

    public class GameFifteenMain
    {      
        public static void Main()
        {
            StartNewGame();
        }

        public static void StartNewGame()
        {
            var renderer = new ConsoleRenderer();
            var userInterface = new ConsoleInterface();
            var gameInitializer = new GameInitializer();
            var engine = new StandartFifteenTilesEngine(renderer, userInterface, gameInitializer);
            engine.Run();
        }
    }
}
