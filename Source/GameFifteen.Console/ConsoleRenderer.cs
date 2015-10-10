namespace GameFifteen.Console
{
    using System;
    using System.Collections.Generic;
    using System.Text;

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
        private IDictionary<string, IStyle> styles;
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
            this.ClearConsolePart(x, y, RenderConstants.ConsoleWindowWidth - x, 1);
            this.PrintOnPosition(x, y, message, RenderConstants.GameMessagesColor);
            this.ResetConsoleColor();
            this.ResetCursorToPreviousPosition(this.previousX, this.previousY);
            this.ClearConsolePart(this.previousX, this.previousY, RenderConstants.ConsoleWindowWidth - this.previousX, 1);
        }

        /// <summary>
        /// Prints game screen log. 
        /// </summary>
        /// <param name="menuStartPositionX">Horizontal position of the menu.</param>
        /// <param name="menuStartPositionY">Vertical position of the menu.</param>
        public void RenderInitialScreen()
        {
            this.SetInitialConsoleSize();
            this.ClearConsole();
            this.PrintOnPosition(0, 0, GameImages.GameLogo, RenderConstants.GameMessagesColor);
            this.RenderGameMenu(RenderConstants.MenuStartPositionX, RenderConstants.MenuStartPositionY);
        }

        /// <summary>
        /// Prints game options on the console.
        /// </summary>
        public void RenderGameOptions()
        {
            int x = RenderConstants.InitialGameOptionsX;
            int y = RenderConstants.InitialGameOptionsY;

            this.ClearConsole();
            this.PrintOnPosition(x, y++, RenderConstants.Goal, RenderConstants.GameMessagesColor);
            this.PrintOnPosition(x, y++, GameMessages.Goal, RenderConstants.ExplanationsColor);
            y = Console.CursorTop + 1;
            this.PrintOnPosition(x, y++, RenderConstants.Commands, RenderConstants.GameMessagesColor);

            foreach (var command in GameMessages.CommandsDescription)
            {
                x = RenderConstants.InitialGameOptionsCommandsX;
                this.PrintOnPosition(x, y, string.Format("{0,-15}", command.Key));
                this.PrintOnPosition(Console.CursorLeft, y, " -> ", RenderConstants.GameMessagesColor);
                this.PrintOnPosition(Console.CursorLeft, y++, command.Value, RenderConstants.ExplanationsColor);
            }

            foreach (var command in GameMessages.StyleCommandsDescription)
            {
                x = RenderConstants.InitialGameOptionsCommandsX;
                this.PrintOnPosition(x, y, string.Format("{0,15}", command.Key), RenderConstants.UserMessagesColor);
                this.PrintOnPosition(Console.CursorLeft, y, " -> ", RenderConstants.GameMessagesColor);
                this.PrintOnPosition(Console.CursorLeft, y++, command.Value, RenderConstants.ExplanationsColor);
            }

            this.PrintOnPosition(x, ++y, GameMessages.EnterCommand, RenderConstants.GameMessagesColor);
            this.ResetConsoleColor();
            this.SaveCursorCurrentPosition();
        }

        /// <summary>
        /// Prints scores of players. 
        /// </summary>
        /// <param name="scoreboard">Uses a <see cref="GameFifteen.Models.Scoreboard"/> that contains players with scores.</param>
        public void RenderScoreboard(Scoreboard scoreboard)
        {
            int x = RenderConstants.InitialScoreboardX;
            int y = RenderConstants.InitialScoreboardY;

            this.ClearConsole();
            this.PrintOnPosition(x, y++, RenderConstants.Scoreboard, RenderConstants.GameMessagesColor);

            if (scoreboard.TopPlayers.Count == 0)
            {
                this.PrintOnPosition(x, y++, RenderConstants.NoTopPlayers, RenderConstants.UserMessagesColor);
            }
            else
            {
                int position = 1;
                var scoreboardLines = scoreboard.GetTextRepresentation().Split('|');

                foreach (string line in scoreboardLines)
                {
                    var playerLine = line.Split(new string[] { "->" }, StringSplitOptions.None);
                    this.PrintOnPosition(x, y, position.ToString() + ". ", RenderConstants.GameMessagesColor);
                    this.PrintOnPosition(Console.CursorLeft, y, playerLine[0], RenderConstants.UserMessagesColor);
                    this.PrintOnPosition(Console.CursorLeft, y, " -> ", RenderConstants.GameMessagesColor);
                    this.PrintOnPosition(Console.CursorLeft, y++, playerLine[1], RenderConstants.UserMessagesColor);
                    position++;
                }
            }

            this.PrintOnPosition(x, y++, string.Empty, RenderConstants.UserMessagesColor);
            this.SaveCursorCurrentPosition();
        }

        /// <summary>
        /// Prints the grid on the console.
        /// </summary>
        /// <param name="grid">Using <see cref="GameFifteen.Models.Contracts.IGrid"/> that contains a list of tiles.</param>
        public void RenderPlayScreen(IGameMember grid)
        {
            int x = RenderConstants.InitialGridX;
            int y = RenderConstants.InitialGridY;

            this.ClearConsole();
            this.PrintOnPosition(RenderConstants.InitialWelcomeMessageX, RenderConstants.InitialWelcomeMessageY, GameMessages.Welcome, RenderConstants.ExplanationsColor);
            this.RenderGrid(x, y, grid);
        }

        private void RenderGameMenu(int menuStartPositionX, int menuStartPositionY)
        {
            this.PrintOnPosition(RenderConstants.MenuStartPositionX, RenderConstants.MenuStartPositionY, GameMessages.Enter, RenderConstants.GameMessagesColor);

            int position = 0;
            foreach (var option in GameMessages.MenuOptions)
            {
                this.PrintOnPosition(menuStartPositionX, menuStartPositionY + position + 1, option.Key, RenderConstants.UserMessagesColor);
                this.PrintOnPosition(menuStartPositionX + 5, menuStartPositionY + position + 1, " to ", RenderConstants.GameMessagesColor);

                this.PrintOnPosition(menuStartPositionX + 10, menuStartPositionY + position + 1, option.Value, RenderConstants.ExplanationsColor);
                position++;
            }

            this.PrintOnPosition(menuStartPositionX, menuStartPositionY + GameMessages.MenuOptions.Count + 3, string.Empty, RenderConstants.UserMessagesColor);
            this.SaveCursorCurrentPosition();
        }

        private void RenderGrid(int x, int y, IGameMember grid)
        {
            string[] gridLines = grid.GetTextRepresentation().Split('|');

            if (this.styles.ContainsKey(GlobalConstants.GridBorderStyle))
            {
                var style = this.styles[GlobalConstants.GridBorderStyle] as GridBorderStyle;
                var gridWithBorder = new GridWithBorder(grid, style);
                gridLines = gridWithBorder.GetTextRepresentation().Split('|');
            }

            foreach (var line in gridLines)
            {
                this.PrintOnPosition(x, y++, line);
            }

            this.PrintOnPosition(0, ++y, GameMessages.EnterNumberToMove, RenderConstants.GameMessagesColor);
            this.ResetConsoleColor();
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
            Console.BufferWidth = Console.WindowWidth = RenderConstants.ConsoleWindowWidth;
            Console.BufferHeight = Console.WindowHeight = RenderConstants.ConsoleWindowHeight;
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
