﻿namespace GameFifteen.Logic
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
       // private IDictionary<Command, Action> commands;

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
            //this.commands = this.FillCommands();
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

        /// <summary>
        /// Execute commands.
        /// </summary>
        /// <param name="command">Command to execute.</param>
        public void ExecuteCommand(Command command)
        {
            try
            {
                this.ExecuteSpecificCommand(command);
                //if (this.commands.ContainsKey(command))
                //{
                //    this.commands[command]();
                //}
            }
            catch (Exception ex)
            {
                this.renderer.RenderMessage(ex.Message);
            }
        }

        private void ExecuteSpecificCommand(Command command)
        {
            if (command == Command.Invalid)
            {
                throw new ArgumentException("Invalid Command!");
            }

            string methodName = "Execute" + command.ToString() + "Command";
            var methodInfo = this.GetType().GetMethod(methodName, BindingFlags.NonPublic
                | BindingFlags.Instance);
            methodInfo.Invoke(this, null);
        }

        private void Run()
        {
            while (true)
            {
                if (this.isGameOver)
                {
                    this.GameOver();
                    this.AskForAnotherGame();
                }

                Command command = this.userInterface.GetCommandFromInput();
                this.ExecuteCommand(command);
            }
        }

        //private IDictionary<Command, Action> FillCommands()
        //{
        //    this.commands = new Dictionary<Command, Action>();
        //    Action ExecuteStartCommand = this.ExecuteStartCommand;
        //    Action ExecuteRestartCommand = this.ExecuteRestartCommand;
        //    Action ExecuteTopCommand = this.ExecuteTopCommand;
        //    Action ExecuteExitCommand = this.ExecuteExitCommand;
        //    Action ExecuteLoadCommand = this.ExecuteLoadCommand;
        //    Action ExecuteMoveCommand = this.ExecuteMoveCommand;
        //    Action ExecuteSaveCommand = this.ExecuteSaveCommand;
        //    Action ExecuteStyleCommand = this.ExecuteStyleCommand;
        //    Action ExecuteSolveGridCommand = this.ExecuteSolveCommand;
        //    Action ExecuteHowCommand = this.ExecuteHowCommand;
        //    Action ExecuteInvalidCommand = () => { throw new ArgumentException("Invalid Command!"); };

        //    this.commands.Add(Command.Start, this.ExecuteStartCommand);
        //    this.commands.Add(Command.Restart, ExecuteRestartCommand);
        //    this.commands.Add(Command.Top, ExecuteTopCommand);
        //    this.commands.Add(Command.Exit, ExecuteExitCommand);
        //    this.commands.Add(Command.Save, ExecuteSaveCommand);
        //    this.commands.Add(Command.Load, ExecuteLoadCommand);
        //    this.commands.Add(Command.Move, ExecuteMoveCommand);
        //    this.commands.Add(Command.Style, ExecuteStyleCommand);
        //    this.commands.Add(Command.Solve, ExecuteSolveGridCommand);
        //    this.commands.Add(Command.How, ExecuteHowCommand);
        //    this.commands.Add(Command.Invalid, ExecuteInvalidCommand);

        //    return this.commands;
        //}

        private void ExecuteMenuCommand()
        {
            this.renderer.RenderInitialScreen();
        }

        private void ExecuteStartCommand()
        {
            this.gameInitializer.Initialize(this.grid);
            //this.renderer.RenderMessage(GameMessages.Welcome);
            this.renderer.RenderGrid(this.grid);
            //this.player = new Player();
            this.player.Moves = 0;
            this.isGameStarted = true;
            this.Run();
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

            if (this.IsValidMove(tile))
            {
                this.grid.SwapTiles(tile);
                this.player.Moves++;
                this.renderer.RenderGrid(this.grid);
                this.isGameOver = this.IsGameOver();
            }
            else
            {
                this.renderer.RenderMessage(GameMessages.InvalidMove);
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
            this.renderer.AddStyle(this.userInterface.GetArgumentValue(GlobalConstants.GridBorderStyle));
            if (this.isGameStarted)
            {
                this.renderer.RenderGrid(this.grid);
            }
        }

        private void ExecuteSaveCommand()
        {
            if (this.isGameStarted)
            {
                this.gridMemory.Memento = this.grid.SaveMemento();
                this.renderer.RenderMessage(GameMessages.GameSaved);
            }
            else
            {
                throw new InvalidOperationException("No game to save.");
            }
        }

        private void ExecuteLoadCommand()
        {
            if (this.gridMemory.Memento != null)
            {
                if (this.UserAgrees(GameMessages.LoadGameQuestion))
                {
                    this.grid.RestoreMemento(this.gridMemory.Memento);
                    this.renderer.RenderGrid(this.grid);
                }
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
                this.gameInitializer.InitilizeGrid(this.grid);
                this.renderer.RenderGrid(this.grid);
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
