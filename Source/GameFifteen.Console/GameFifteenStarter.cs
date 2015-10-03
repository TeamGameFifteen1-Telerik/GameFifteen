﻿namespace GameFifteen.Console
{
    using GameFifteen.Logic;
    using GameFifteen.Models;

    /// <summary>
    /// Facade design pattern
    /// </summary>
    public class GameFifteenStarter
    {
        private static GameFifteenStarter instance;

        private GameFifteenStarter()
        {
        }

        public static GameFifteenStarter Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameFifteenStarter();
                }

                return instance;
            }
        }

        public void NewGame()
        {
            var renderer = new ConsoleRenderer();
            var userInterface = new ConsoleInterface();
            var gameInitializer = new StandartGameInitializer();
            var player = new Player();
            var grid = new Grid();
            var engine = new StandartFifteenTilesEngine(renderer, userInterface, gameInitializer, player, grid);
            engine.Run();
        }
    }
}