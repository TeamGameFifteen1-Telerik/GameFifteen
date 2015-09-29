namespace GameFifteen.Logic
{
    using System;
    using System.Collections.Generic;

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
        private GridMemory gridMemory;
        private IDictionary<Command, Action> commands;

        public Engine(IRenderer renderer, IUserInterface userInterface, IGameInitializater gameInitializer)
        {
            this.renderer = renderer;
            this.userInterface = userInterface;
            this.gameInitializer = gameInitializer;
            this.grid = new Grid();
            gridMemory = new GridMemory();
            player = new Player();
            scoreBoard = Scoreboard.Instance;
            this.commands = FillCommands();
        }

        public void Run()
        {
            Command command;
            this.renderer.PrintMessage(GameMessages.WelcomeMessage);
            this.StartNewGame();

            while (true)
            {
                if (isGameOver)
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
            if (commands.ContainsKey(command))
            {
                commands[command]();
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
            Action processInvalidCommand = () => { throw new ArgumentException("Invalid Command!"); };

            commands.Add(Command.Restart, processRestartCommand);
            commands.Add(Command.Top, processTopCommand);
            commands.Add(Command.Exit, processExitCommand);
            commands.Add(Command.Save, processSaveCommand);
            commands.Add(Command.Load, processLoadCommand);
            commands.Add(Command.Move, processMoveCommand);
            commands.Add(Command.Invalid, processInvalidCommand);

            return this.commands;
        }

        private void StartNewGame()
        {
            this.gameInitializer.Initialize(this.grid);
            this.renderer.PrintMatrix(grid);
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
                this.ProcessExitCommand();
            }
        }

        private void SaveScore()
        {
            this.renderer.PrintMessage(GameMessages.EnterYourNameMessage);
            string playerName = this.userInterface.GetUserInput();
            this.player.Name = playerName;
            scoreBoard.AddPlayer(player);
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
            var tileLable = this.userInterface.GetDestinationTileValue();
            Tile tile = this.grid.GetTileFromLabel(tileLable.ToString());

            if (this.IsValidMove(tile))
            {
                this.grid.SwapTiles(tile);
                this.player.Moves++;
                this.renderer.PrintMatrix(grid);
                this.isGameOver = this.IsGameOver();
            }
            else
            {
                this.renderer.PrintMessage(GameMessages.InvalidMove);
            }
        }

        private bool IsValidMove(Tile tile)
        {
            if (!IsValidTileLabel(int.Parse(tile.Label)))
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

        private bool UserAgrees(string message)
        {
            this.renderer.PrintMessage(string.Format(message + " " + GameMessages.PressKeyToExit, GlobalConstants.AgreeCommand));
            Command command = this.userInterface.GetCommandFromInput();

            return command == Command.Agree;
        }
    }
}
