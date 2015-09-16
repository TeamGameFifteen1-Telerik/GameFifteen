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
            this.player = new Player();
        }

        public void Run()
        {
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
                    this.renderer.PrintScoreboard(this.scoreBoard);
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
                this.renderer.PrintMessage(GameMessages.SolvedByDefaultMessage);
            }
            else
            {
                this.renderer.PrintMessage(string.Format(GameMessages.WinMessage, this.player.Moves));
                this.SaveScore();
                this.renderer.PrintScoreboard(this.scoreBoard);
            }

            if (this.UserAgrees(GameMessages.NewGameQuestion))
            {
                this.StartNewGame();
            }
            else
            {
                this.ProcessExitCommand();
            }
            
            //command = Command.Restart;
            //line = "restart";

            //isGameOver = false;
            //this.player.Moves = 0;
        }

        private void SaveScore()
        {
            this.renderer.PrintMessage(GameMessages.EnterYourNameMessage);
            string playerName = this.userInterface.GetUserInput();
            this.player.Name = playerName;
            scoreBoard.AddPlayer(player);
        }

        private void StartNewGame()
        {
            this.renderer.PrintMessage(GameMessages.WelcomeMessage);
            this.gameInitializer.Initialize(this.grid);
            this.renderer.PrintMatrix(grid);
            this.renderer.PrintMessage(GameMessages.EnterNumberMessage);
        }

        private void ProcessMoveCommand()
        {
            //TODO: refactor
            try
            {
                grid.MoveTile(this.userInterface.GetDestinationTileValue());
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

        private bool UserAgrees(string message)
        {
            this.renderer.PrintMessage(string.Format(message, GlobalConstants.AgreeCommand));
            Command command = this.userInterface.GetCommandFromInput();

            return command == Command.Agree;
        }

        private void ProcessExitCommand()
        {
            if (this.UserAgrees(GameMessages.ExitMessage))
            {
                this.userInterface.ExitGame();             
            }
        }
    }
}
