namespace GameFifteen.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using GameFifteen.Common;
    using GameFifteen.Logic.Contracts;
    using GameFifteen.Models;

    public class Engine
    {
        private IRenderer renderer;
        private IUserInterface userInterface;
        private IGameInitializater gameInitializer;
        private Grid grid;
        private Scoreboard scoreBoard;
        private Player player;
        private bool isGameOver;

        public Engine(IRenderer renderer, IUserInterface userInterface, IGameInitializater gameInitializer)
        {
            this.renderer = renderer;
            this.userInterface = userInterface;
            this.gameInitializer = gameInitializer;
            this.grid = new Grid();
            this.scoreBoard = new Scoreboard();
            //TODO: initialize player correctly using the initilizer and userInterface for name
            this.player = new Player("Pesho", 0);
        }

        public void Run()
        {
            var players = scoreBoard.Players;
            //// var renderer = new ConsoleRenderer();

            //// Grid tiles = new Grid();
            //int countPlayerMoves = 0;
            //string line = "restart";
            //// renamed flag
            // this.isGameOver = false;
            Command command;
            this.StartNewGame();

            while (true)
            {
                if (isGameOver)
                {
                    this.GameOver();
                }

                this.renderer.PrintMessage(GameMessages.EnterNumberMessage);
                command = this.userInterface.GetCommandFromInput();

                try
                {
                    this.ProcessCommand(command);
                }
                catch (Exception ex)
                {
                    this.renderer.PrintMessage(ex.Message);
                }
            }
        }

        public void ProcessCommand(Command command)
        {
            switch (command)
            {
                case Command.Restart:
                    this.StartNewGame();
                    break;
                case Command.Top:
                    this.renderer.PrintScoreboard(this.scoreBoard.Players);
                    break;
                case Command.Exit:
                    this.ProcessExitCommand();
                    break;
                case Command.Move:
                    this.ProcessMoveCommand();
                    break;
                case Command.Invalid:
                default:
                    throw new ArgumentException("Invalid Command!");
            }
        }

        private bool IsGameOver()
        {
            return this.grid.IsSorted;
        }

        //TODO : refactor
        private void GameOver()
        {  
            if (this.player.Moves == 0)
            {
                Console.WriteLine("Your matrix was solved by default :) Come on - NEXT try");
            }
            else
            {
                Console.WriteLine("Congratulations! You won the game in {0} moves.", this.player.Moves);
                Console.Write("Please enter your name for the top scoreboard: ");
                string playerName = Console.ReadLine();
                Player player = new Player(playerName, this.player.Moves);
                scoreBoard.AddPlayer(player);
                this.renderer.PrintScoreboard(this.scoreBoard.Players);
            }

            //TODO
            this.renderer.PrintMessage("New game? Press y for yes.");
            var command = this.userInterface.GetCommandFromInput();
            if (command == Command.Agree)
            {
                this.StartNewGame();
            }
            //command = Command.Restart;
            //line = "restart";

            //isGameOver = false;
            //this.player.Moves = 0;
        }

        private void StartNewGame()
        {
            this.renderer.PrintMessage(GameMessages.WelcomeMessage);
            this.gameInitializer.Initialize(this.grid);

            //isSolved = this.IsGameOver(grid);
            this.renderer.PrintMatrix(grid);
        }

        private void ProcessMoveCommand()
        {
            //TODO: refactor
            try
            {
                grid.MoveTile(this.userInterface.GetDestinationTileValue());
                //// Engine.MoveTiles(grid, destinationTileValue);
                this.player.Moves++;
                this.renderer.PrintMatrix(grid);
                this.isGameOver = this.IsGameOver();
            }
            catch (Exception exception)
            {
                this.renderer.PrintMessage(exception.Message);
                //throw new InvalidOperationException(exception.Message);
            }
        }

        private void ProcessExitCommand()
        {
            this.renderer.PrintMessage(string.Format(GameMessages.ExitMessage, GlobalConstants.AgreeCommand));
            Command command = this.userInterface.GetCommandFromInput();

            if (command == Command.Agree)
            {
                this.userInterface.ExitGame();
            }
            //else
            //{
            //    this.renderer.PrintMessage(GameMessages.EnterNumberMessage);
            //}
        }

        /*
         should this get back here?
        public static void MoveTiles(Grid grid, int tileValue)
        {
            if (tileValue < 0 || tileValue > 15)
            {
                throw new ArgumentException("Invalid move!");
            }

            //List<Tile> resultMatrix = tiles;
            //Tile freeTile = tiles[GetFreeTilePosition(tiles)];
            //var destinationTilePosition = GetDestinationTilePosition(grid, tileValue);
            //Tile tile = grid.GetTileAtPosition(destinationTilePosition);
            int destinationTilePosition = grid.GetTilePosition(tileValue.ToString());
            Tile tile = grid.GetTileAtPosition(destinationTilePosition);

            bool areValidNeighbourTiles = grid.ValidateNeighbour(tile); //TilePositionValidation(grid.EmptyTile, tile);

            if (areValidNeighbourTiles)
            {
                grid.SwapTiles(tile);
                //swap tiles
                //int targetTilePosition = tile.Position;
                //resultMatrix[targetTilePosition].Position = freeTile.Position;
                //resultMatrix[freeTile.Position].Position = targetTilePosition;
                //resultMatrix.Sort();
            }
            else
            {
                throw new Exception("Invalid move!");
            }

           // return resultMatrix;
        }*/
    }
}
