﻿namespace GameFifteen.Logic
{
    using System;
    using System.Collections.Generic;

    using GameFifteen.Common;
    using GameFifteen.Logic.Contracts;
    using GameFifteen.Models;
    using GameFifteen.Models.Contracts;

    /// <summary>
    /// Strategy design pattern: IRenderer renderer, IUserInterface userInterface
    /// Bridge design pattern: IGameInitializater gameInitializer
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
        private IDictionary<Command, Action> commands;

        public StandartFifteenTilesEngine(IRenderer renderer, IUserInterface userInterface, IGameInitializater gameInitializer, IPlayer player, IGrid grid)
            : base(gameInitializer)
        {
            this.renderer = renderer;
            this.userInterface = userInterface;
            this.player = player;
            this.scoreBoard = Scoreboard.Instance;
            this.grid = grid;
            this.gridMemory = new GridMemory();
            this.commands = this.FillCommands();
        }

        public override void Run()
        {
            Command command;
            this.renderer.PrintMessage(GameMessages.WelcomeMessage);
            this.StartNewGame();

            while (true)
            {
                if (this.isGameOver)
                {
                    this.GameOver();
                    this.AskForAnotherGame();
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
            if (this.commands.ContainsKey(command))
            {
                this.commands[command]();
            }
        }

        private IDictionary<Command, Action> FillCommands()
        {
            this.commands = new Dictionary<Command, Action>();
            Action processRestartCommand = this.ProcessRestartCommand;
            Action processTopCommand = this.ProcessTopCommand;
            Action processExitCommand = this.ProcessExitCommand;
            Action processLoadCommand = this.ProcessLoadCommand;
            Action processMoveCommand = this.ProcessMoveCommand;
            Action processSaveCommand = this.ProcessSaveCommand;
            Action processStyleCommand = this.ProcessStyleCommand;
            Action processSolveGridCommand = this.ProcessSolveGridCommand;
            Action processInvalidCommand = () => { throw new ArgumentException("Invalid Command!"); };

            this.commands.Add(Command.Restart, processRestartCommand);
            this.commands.Add(Command.Top, processTopCommand);
            this.commands.Add(Command.Exit, processExitCommand);
            this.commands.Add(Command.Save, processSaveCommand);
            this.commands.Add(Command.Load, processLoadCommand);
            this.commands.Add(Command.Move, processMoveCommand);
            this.commands.Add(Command.Style, processStyleCommand);
            this.commands.Add(Command.Solve, processSolveGridCommand);
            this.commands.Add(Command.Invalid, processInvalidCommand);

            return this.commands;
        }

        private void StartNewGame()
        {
            this.gameInitializer.Initialize(this.grid);
            this.renderer.PrintMatrix(this.grid);

            //// TODO: remove initialization
            this.player = new Player();
            this.player.Moves = 0;
        }

        private bool IsGameOver()
        {
            return this.grid.IsSorted;
        }

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
        }

        private void AskForAnotherGame()
        {
            if (this.UserAgrees(GameMessages.NewGameQuestion))
            {
                this.StartNewGame();
            }
            else
            {
                this.userInterface.ExitGame();
                //// this.ProcessExitCommand();
            }
        }

        private void SaveScore()
        {
            this.renderer.PrintMessage(GameMessages.EnterYourNameMessage);
            string playerName = this.userInterface.GetUserInput();

            if (!string.IsNullOrEmpty(playerName))
            {
                this.player.Name = playerName;
            }    
            
            this.scoreBoard.AddPlayer(this.player);
        }

        private void ProcessRestartCommand()
        {
            if (this.UserAgrees(GameMessages.RestartGameQuestion))
            {
                this.StartNewGame();
            }
        }

        private void ProcessTopCommand()
        {
            this.renderer.PrintScoreboard(this.scoreBoard);
        }

        private void ProcessMoveCommand()
        {
            var tileLable = this.userInterface.GetArgumentValue(GlobalConstants.DestinationTileValue);
            Tile tile = this.grid.GetTileFromLabel(tileLable.ToString());

            if (this.IsValidMove(tile))
            {
                this.grid.SwapTiles(tile);
                this.player.Moves++;
                this.renderer.PrintMatrix(this.grid);
                this.isGameOver = this.IsGameOver();
            }
            else
            {
                this.renderer.PrintMessage(GameMessages.InvalidMove);
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

        private void ProcessStyleCommand()
        {
            this.renderer.AddStyle(this.userInterface.GetArgumentValue(GlobalConstants.GridBorderStyle));
            this.renderer.PrintMatrix(this.grid);
        }

        private void ProcessSaveCommand()
        {
            this.gridMemory.Memento = this.grid.SaveMemento();
            this.renderer.PrintMessage(GameMessages.GameSaved);
        }

        private void ProcessLoadCommand()
        {
            if (this.gridMemory.Memento != null)
            {
                if (this.UserAgrees(GameMessages.LoadGameQuestion))
                {
                    this.grid.RestoreMemento(this.gridMemory.Memento);
                    this.renderer.PrintMatrix(this.grid);
                }
            }
            else
            {
                this.renderer.PrintMessage(GameMessages.NoGameToLoad);
            }
        }

        private void ProcessExitCommand()
        {
            if (this.UserAgrees(GameMessages.ExitMessage))
            {
                this.userInterface.ExitGame();
            }
        }

        private void ProcessSolveGridCommand()
        {
            StandartGameInitializer hack = new StandartGameInitializer();
            this.grid.Clear();
            hack.InitilizeGrid(this.grid);
            this.renderer.PrintMatrix(this.grid);
            this.player.Moves++;
            this.GameOver();
            this.AskForAnotherGame();
        }

        private bool UserAgrees(string message)
        {
            this.renderer.PrintMessage(string.Format(message + " " + GameMessages.PressKeyToExit, GlobalConstants.AgreeCommand));
            Command command = this.userInterface.GetCommandFromInput();

            return command == Command.Agree;
        }
    }
}
