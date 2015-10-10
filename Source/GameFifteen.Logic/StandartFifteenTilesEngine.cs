namespace GameFifteen.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    using GameFifteen.Common;
    using GameFifteen.Logic.Contracts;
    using GameFifteen.Models;
    using GameFifteen.Models.Contracts;

    /// <summary>
    /// Game logic implementation.
    /// Strategy design pattern: IRenderer renderer, IUserInterface userInterface
    /// Bridge design pattern: IGameInitializer gameInitializer.
    /// </summary>
    public class StandartFifteenTilesEngine : Engine, IEngine
    {
        private IRenderer renderer;
        private IUserInterface userInterface;
        private Scoreboard scoreBoard;
        private IPlayer player;
        private IGrid grid;
        private GridMemory gridMemory;
        private bool isGameOver;
        private bool isGameStarted;

        /// <summary>
        /// Initializes a new instance of the <see cref="StandartFifteenTilesEngine"/> class.
        /// </summary>
        /// <param name="renderer">Object to print.</param>
        /// <param name="userInterface">Interacting with user.</param>
        /// <param name="gameInitializer">Initializing the game.</param>
        /// <param name="player">The player.</param>
        /// <param name="grid">Grid with tiles.</param>
        public StandartFifteenTilesEngine(IRenderer renderer, IUserInterface userInterface, IGameInitializater gameInitializer, IPlayer player, IGrid grid)
            : base(gameInitializer)
        {
            this.renderer = renderer;
            this.userInterface = userInterface;
            this.player = player;
            this.scoreBoard = Scoreboard.Instance;
            this.grid = grid;
            this.gridMemory = new GridMemory();
        }

        /// <summary>
        /// Initializes start screen.
        /// </summary>
        public override void Initialize()
        {
            this.ExecuteMenuCommand();
            while (!this.isGameStarted)
            {
                Command command = this.userInterface.GetCommandFromInput();
                this.ExecuteCommand(command);
            }
        }

        private void Run()
        {
            while (true)
            {
                this.SaveCurrentGameState();
                if (this.isGameOver)
                {
                    this.GameOver();
                    this.AskForAnotherGame();
                }

                Command command = this.userInterface.GetCommandFromInput();
                this.ExecuteCommand(command);
            }
        }

        /// <summary>
        /// Execute commands.
        /// </summary>
        /// <param name="command">Command to execute.</param>
        private void ExecuteCommand(Command command)
        {
            try
            {
                this.ExecuteSpecificCommand(command);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    this.renderer.RenderMessage(ex.InnerException.Message);
                }
                else
                {
                    this.renderer.RenderMessage(ex.Message);
                }
            }
        }

        private void ExecuteSpecificCommand(Command command)
        {
            if (command == Command.Invalid || (!this.isGameStarted && command == Command.Move))
            {
                throw new ArgumentException("Invalid Command!");
            }

            string methodName = "Execute" + command.ToString() + "Command";
            var methodInfo = this.GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);

            try
            {
                methodInfo.Invoke(this, null);
            }
            catch (InvalidOperationException ex)
            {
                this.renderer.RenderMessage(ex.Message);
            }
        }

        private void ExecuteMenuCommand()
        {
            this.renderer.RenderInitialScreen();
        }

        private void ExecuteStartCommand()
        {
            this.GameInitializer.Initialize(this.grid);
            this.renderer.RenderPlayScreen(this.grid);
            this.player.Moves = 0;
            this.isGameStarted = true;
            this.Run();
        }

        private void SaveCurrentGameState()
        {
            this.gridMemory.CurrentMemento = this.grid.SaveMemento();
        }

        private bool IsGameOver()
        {
            return this.grid.IsSorted;
        }

        private void GameOver()
        {
            this.isGameStarted = false;
            this.isGameOver = false;

            if (this.player.Moves == 0)
            {
                this.renderer.RenderMessage(GameMessages.SolvedByDefault);
            }
            else
            {
                this.renderer.RenderMessage(string.Format(GameMessages.Win, this.player.Moves));
                this.SaveScore();
                this.renderer.RenderScoreboard(this.scoreBoard);
            }
        }

        private void AskForAnotherGame()
        {
            if (this.UserAgrees(GameMessages.NewGameQuestion))
            {
                this.ExecuteStartCommand();
            }
            else
            {
                this.userInterface.ExitGame();
            }
        }

        private void SaveScore()
        {
            this.renderer.RenderMessage(GameMessages.EnterYourName);
            string playerName = this.userInterface.GetUserInput();

            if (!string.IsNullOrEmpty(playerName))
            {
                this.player.Name = playerName;
            }

            this.scoreBoard.AddPlayer(this.player);
        }

        private void ExecuteGameCommand()
        {
            if (this.isGameStarted)
            {
                this.LoadMemento(this.gridMemory.CurrentMemento);
            }
            else
            {
                throw new InvalidOperationException("The game hasn't started yet.");
            }
        }

        private void ExecuteHowCommand()
        {
            this.renderer.RenderGameOptions();
        }

        private void ExecuteTopCommand()
        {
            this.renderer.RenderScoreboard(this.scoreBoard);
        }

        private void ExecuteRestartCommand()
        {
            if (this.UserAgrees(GameMessages.RestartGameQuestion))
            {
                this.ExecuteStartCommand();
            }
        }

        private void ExecuteMoveCommand()
        {
            var tileLable = this.userInterface.GetArgumentValue(GlobalConstants.DestinationTileValue);
            Tile tile = this.grid.GetTileFromLabel(tileLable.ToString());

            if (tile != null && this.IsValidMove(tile))
            {
                this.grid.SwapTiles(tile);
                this.player.Moves++;
                this.renderer.RenderPlayScreen(this.grid);
                this.isGameOver = this.IsGameOver();
            }
            else
            {
                throw new InvalidOperationException("Invalid move!");
            }
        }

        private bool IsValidMove(Tile tile)
        {
            if (!this.IsValidTileLabel(int.Parse(tile.Label)))
            {
                return false;
            }

            return this.grid.CanSwap(tile);
        }

        private bool IsValidTileLabel(int tileLabel)
        {
            if (0 < tileLabel && tileLabel < GlobalConstants.TotalTilesCount)
            {
                return true;
            }

            return false;
        }

        private void ExecuteStyleCommand()
        {
            try
            {
                this.renderer.AddStyle(this.userInterface.GetArgumentValue(GlobalConstants.GridBorderStyle));
            }
            catch (Exception ex)
            {
                this.renderer.RenderMessage(ex.Message);
                return;
            }

            if (this.isGameStarted)
            {
                this.renderer.RenderPlayScreen(this.grid);
            }
        }

        private void ExecuteSaveCommand()
        {
            if (this.isGameStarted)
            {
                this.gridMemory.SavedMemento = this.grid.SaveMemento();
                this.renderer.RenderMessage(GameMessages.GameSaved);
            }
            else
            {
                throw new InvalidOperationException("No game to save.");
            }
        }

        private void ExecuteLoadCommand()
        {
            if (this.UserAgrees(GameMessages.LoadGameQuestion))
            {
                this.LoadMemento(this.gridMemory.SavedMemento);
            }
        }

        private void LoadMemento(Memento memento)
        {
            if (memento != null)
            {
                this.grid.RestoreMemento(memento);
                this.renderer.RenderPlayScreen(this.grid);
            }
            else
            {
                throw new InvalidOperationException("No game to load.");
            }
        }

        private void ExecuteSolveCommand()
        {
            if (this.isGameStarted)
            {
                this.grid.Clear();
                this.GameInitializer.InitilizeGrid(this.grid);
                this.renderer.RenderPlayScreen(this.grid);
                this.player.Moves++;
                this.GameOver();
                this.AskForAnotherGame();
            }
            else
            {
                throw new InvalidOperationException("No grid to solve.");
            }
        }

        private void ExecuteExitCommand()
        {
            if (this.UserAgrees(GameMessages.Exit))
            {
                this.userInterface.ExitGame();
            }
        }

        private bool UserAgrees(string message)
        {
            this.renderer.RenderMessage(string.Format(message + " " + GameMessages.PressKeyToExit, Command.Yes.ToString().ToUpper()));
            Command command = this.userInterface.GetCommandFromInput();

            return command == Command.Yes;
        }
    }
}
