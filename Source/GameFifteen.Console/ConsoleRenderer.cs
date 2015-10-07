﻿namespace GameFifteen.Console
{
    using System;
    using System.Collections.Generic;

    using GameFifteen.Common;
    using GameFifteen.Console.Contracts;
    using GameFifteen.Console.Styles;
    using GameFifteen.Logic.Contracts;
    using GameFifteen.Models;
    using GameFifteen.Models.Contracts;

    /// <summary>
    /// Provides drawing functions for grid.
    /// </summary>
    public class ConsoleRenderer : IRenderer
    {
        private const int ConsoleWindowWidth = 81;
        private const int ConsoleWindowHeight = 25;

        private const int InitialGridX = ConsoleWindowWidth / 2 - GlobalConstants.GridSize * 2 - 2;
        private const int InitialGridY = 3;

        private const int InitialGameOptionsX = 0;
        private const int InitialGameOptionsY = 0;

        private const int InitialWelcomeMessageX = 0;
        private const int InitialWelcomeMessageY = 0;

        private const int InitialGameOptionsCommandsX = ConsoleWindowWidth / 2 - 20;

        private const int GameMessagesY = 11;

        private const int InitialScoreboardX = ConsoleWindowWidth / 2 - GlobalConstants.GridSize * 2;
        private const int InitialScoreboardY = 0;

        private const string Goal = "Goal: ";
        private const string Commands = "Commands: ";
        private const string Scoreboard = "Scoreboard:";
        private const string NoTopPlayers = "No top players yet.";

        private const ConsoleColor UserMessagesColor = ConsoleColor.White;
        private const ConsoleColor GameMessagesColor = ConsoleColor.Yellow;
        private const ConsoleColor ExplanationsColor = ConsoleColor.Green;

        private Dictionary<string, IStyle> styles;
        private IStyleFactory borderStyleFactory;

        private int previousX;
        private int previousY;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleRenderer"/> class.
        /// </summary>
        /// <param name="borderStyleFactory">Style that will be used for drawing border.</param>
        public ConsoleRenderer(IStyleFactory borderStyleFactory)
        {
            this.borderStyleFactory = borderStyleFactory;
            this.styles = new Dictionary<string, IStyle>()
            {
                { GlobalConstants.GridBorderStyle, this.borderStyleFactory.Get(BorderStyleType.Solid) }
            };
        }

        /// <summary>
        /// Gets collection of styles.
        /// </summary>
        /// <value>Pair key-value collection.</value>
        public Dictionary<string, IStyle> Styles
        {
            get
            {
                return new Dictionary<string, IStyle>(this.styles);
            }
        }

        /// <summary>
        /// Adding styles to the style collection.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">When empty style are passed.</exception>
        /// <param name="styles">Styles to add.</param>
        public void AddStyle(params string[] styles)
        {
            if (styles.Length == 0)
            {
                throw new ArgumentNullException("You need to add at least one style.");
            }

            foreach (var style in styles)
            {
                string capitalStyle = style[0].ToString().ToUpper() + style.Substring(1);
                BorderStyleType styleType;
                if (Enum.TryParse(capitalStyle, out styleType))
                {
                    IStyle borderStyle = this.borderStyleFactory.Get(styleType);
                    this.styles[GlobalConstants.GridBorderStyle] = borderStyle;
                }
                else
                {
                    throw new ArgumentException("There's no border of that type.");
                }
            }
        }

        /// <summary>
        /// Prints message on the console.
        /// </summary>
        /// <param name="message">Text to be printed.</param>
        public void RenderMessage(string message)
        {
            int x = 0;
            int y = this.previousY + 1;
            this.ClearConsolePart(x, y, ConsoleWindowWidth - x, 1);
            this.PrintOnPosition(x, y, message, GameMessagesColor);
            this.ResetConsoleColor();
            this.ResetCursorToPreviousPosition(this.previousX, this.previousY);
            this.ClearConsolePart(this.previousX, this.previousY, ConsoleWindowWidth - this.previousX, 1);

            //Console.WriteLine(message);
        }

        /// <summary>
        /// Prints game screen log. 
        /// </summary>
        /// <param name="menuStartPositionX">Horizontal position of the menu.</param>
        /// <param name="menuStartPositionY">Vertical position of the menu.</param>
        public void RenderInitialScreen(int menuStartPositionX = GlobalConstants.MenuStartPositionX, int menuStartPositionY = GlobalConstants.MenuStartPositionY)
        {
            this.SetInitialConsoleSize();
            this.ClearConsole();
            this.PrintOnPosition(0, 0, GameMessages.GameLogo, GameMessagesColor);
            this.RenderGameMenu(menuStartPositionX, menuStartPositionY);
        }

        /// <summary>
        /// Prints game options on the console.
        /// </summary>
        public void RenderGameOptions()
        {
            int x = InitialGameOptionsX;
            int y = InitialGameOptionsY;

            this.ClearConsole();
            this.PrintOnPosition(x, y++, Goal, GameMessagesColor);
            this.PrintOnPosition(x, y++, GameMessages.Goal, ExplanationsColor);
            y = Console.CursorTop + 1;
            this.PrintOnPosition(x, y++, Commands, GameMessagesColor);

            foreach (var command in GameMessages.CommandsDescription)
            {
                x = InitialGameOptionsCommandsX;
                this.PrintOnPosition(x, y, string.Format("{0,-15}", command.Key));
                this.PrintOnPosition(Console.CursorLeft, y, " -> ", GameMessagesColor);
                this.PrintOnPosition(Console.CursorLeft, y++, command.Value, ExplanationsColor);
            }

            foreach (var command in GameMessages.StyleCommandsDescription)
            {
                x = InitialGameOptionsCommandsX;
                this.PrintOnPosition(x, y, string.Format("{0,15}", command.Key), UserMessagesColor);
                this.PrintOnPosition(Console.CursorLeft, y, " -> ", GameMessagesColor);
                this.PrintOnPosition(Console.CursorLeft, y++, command.Value, ExplanationsColor);
            }

            this.PrintOnPosition(x, ++y, GameMessages.EnterCommand, GameMessagesColor);
            this.ResetConsoleColor();
            this.SaveCursorCurrentPosition();
        }

        private void RenderGameMenu(int menuStartPositionX = GlobalConstants.MenuStartPositionX, int menuStartPositionY = GlobalConstants.MenuStartPositionY)
        {
            this.PrintOnPosition(menuStartPositionX, menuStartPositionY, GameMessages.Enter, GameMessagesColor);

            int position = 0;
            foreach (var option in GameMessages.MenuOptions)
            {
                this.PrintOnPosition(menuStartPositionX, menuStartPositionY + position + 1, option.Key, UserMessagesColor);
                this.PrintOnPosition(menuStartPositionX + 5, menuStartPositionY + position + 1, " to ", GameMessagesColor);

                this.PrintOnPosition(menuStartPositionX + 10, menuStartPositionY + position + 1, option.Value, ExplanationsColor);
                position++;
            }

            this.PrintOnPosition(menuStartPositionX, menuStartPositionY + GameMessages.MenuOptions.Count + 3, string.Empty, UserMessagesColor);
            this.SaveCursorCurrentPosition();
        }

        /// <summary>
        /// Prints scores of players. 
        /// </summary>
        /// <param name="scoreboard">Uses a <see cref="GameFifteen.Models.Scoreboard"/> that contains players with scores.</param>
        public void RenderScoreboard(Scoreboard scoreboard)
        {
            int x = InitialScoreboardX;
            int y = InitialScoreboardY;

            this.ClearConsole();
            this.PrintOnPosition(x, y++, Scoreboard, GameMessagesColor);

            if (scoreboard.TopPlayers.Count == 0)
            {
                this.PrintOnPosition(x, y++, NoTopPlayers, UserMessagesColor);
            }
            else
            {
                int position = 1;
                foreach (Player player in scoreboard.TopPlayers)
                {
                    this.PrintOnPosition(x, y, position.ToString() + ". ", GameMessagesColor);
                    this.PrintOnPosition(Console.CursorLeft, y, player.Name, UserMessagesColor);
                    this.PrintOnPosition(Console.CursorLeft, y, " -> ", GameMessagesColor);
                    this.PrintOnPosition(Console.CursorLeft, y, player.Moves.ToString(), UserMessagesColor);
                    this.PrintOnPosition(Console.CursorLeft, y++, " moves", GameMessagesColor);
                    position++;
                }
            }

            this.PrintOnPosition(x, y++, string.Empty, UserMessagesColor);
            this.SaveCursorCurrentPosition();
        }

        /// <summary>
        /// Prints the grid on the console.
        /// </summary>
        /// <param name="grid">Using <see cref="GameFifteen.Models.Contracts.IGrid"/> that contains a list of tiles.</param>
        public void RenderPlayScreen(IGrid grid)
        {
            int x = InitialGridX;
            int y = InitialGridY;

            this.ClearConsole();
            this.PrintOnPosition(InitialWelcomeMessageX, InitialWelcomeMessageX, GameMessages.Welcome, ExplanationsColor);
            this.RenderGrid(x, y, grid);
        }

        private void RenderGrid(int x, int y, IGrid grid)
        {
            //this.ClearConsolePart(InitialGridX, InitialGridY, GlobalConstants.GridSize * 2, GlobalConstants.GridSize);
            this.RenderBorder(x + 1, y - 1);

            for (int i = 0, colCounter = 1; i < GlobalConstants.TotalTilesCount; i++, colCounter++)
            {
                Tile currentTile = grid.GetTileAtPosition(i);
                string stringFormat = currentTile.Label.Length < 2 ? " {0}" : "{0}";

                x = currentTile.Label.Length < 2 ? x + stringFormat.Length - 1 : x + stringFormat.Length;

                string tileFormat = string.Format(stringFormat, currentTile.Label);
                this.PrintOnPosition(x, y, tileFormat);

                if (colCounter == GlobalConstants.GridSize)
                {
                    x = InitialGridX;
                    y++;
                    colCounter = 0;
                }
            }

            this.PrintOnPosition(0, ++y, GameMessages.EnterNumberToMove, GameMessagesColor);
            this.ResetConsoleColor();
            this.SaveCursorCurrentPosition();
        }

        private void RenderBorder(int x, int y)
        {
            GridBorderStyle style;
            if (!this.styles.ContainsKey(GlobalConstants.GridBorderStyle))
            {
                style = this.borderStyleFactory.Get(BorderStyleType.Default) as GridBorderStyle;
            }

            style = this.styles[GlobalConstants.GridBorderStyle] as GridBorderStyle;

            this.PrintOnPosition(x, y, style.Top);

            for (int i = 0; i < GlobalConstants.GridSize + 1; i++)
            {
                this.PrintOnPosition(x, ++y, style.Left);
                this.PrintOnPosition(x + GlobalConstants.GridSize * 3 + 2, y, style.Right);
            }

            this.PrintOnPosition(x, y, style.Bottom);
            this.SaveCursorCurrentPosition();
        }

        private void PrintOnPosition(int x, int y, string text, ConsoleColor color = ConsoleColor.White)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = color;
            Console.Write(text);
        }

        private void SetInitialConsoleSize()
        {
            Console.BufferWidth = Console.WindowWidth = ConsoleWindowWidth;
            Console.BufferHeight = Console.WindowHeight = ConsoleWindowHeight;
        }

        private void SaveCursorCurrentPosition()
        {
            this.previousX = Console.CursorLeft;
            this.previousY = Console.CursorTop;
        }

        private void ResetCursorToPreviousPosition(int previousX, int previousY)
        {
            Console.SetCursorPosition(previousX, previousY);
        }

        private void ResetConsoleColor()
        {
            Console.ForegroundColor = ConsoleColor.White;
        }

        private void ClearConsolePart(int x, int y, int width, int height)
        {
            int currentLineCursor = y;
            Console.SetCursorPosition(x, y);
            for (int i = 0; i < height; i++)
            {
                Console.SetCursorPosition(x, y + i);
                Console.Write(new string(' ', width));
            }

            Console.SetCursorPosition(x, currentLineCursor);
        }

        private void ClearConsole()
        {
            Console.Clear();
        }
    }
}
