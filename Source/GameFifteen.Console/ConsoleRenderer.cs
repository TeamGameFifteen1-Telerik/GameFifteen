namespace GameFifteen.Console
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

        //// TODO refactor
        private Dictionary<string, IStyle> styles;
        private IStyleFactory borderStyleFactory;

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
                    /*try
                    {*/
                        IStyle borderStyle = this.borderStyleFactory.Get(styleType);
                        this.styles[GlobalConstants.GridBorderStyle] = borderStyle;
                    /*}
                    catch (Exception ex)
                    {
                    }*/
                }
                else
                {
                    throw new ArgumentException("There's no border of that type.");
                }
            }
        }

        /// <summary>
        /// Prints scores of players. 
        /// </summary>
        /// <param name="scoreboard">Uses a <see cref="GameFifteen.Models.Scoreboard"/> that contains players with scores.</param>
        public void RenderScoreboard(Scoreboard scoreboard)
        {
            Console.WriteLine("Scoreboard:");
            if (scoreboard.TopPlayers.Count == 0)
            {
                Console.WriteLine("No top players yet.");
            }
            else
            {
                foreach (Player player in scoreboard.TopPlayers)
                {
                    string scoreboardLine = (scoreboard.TopPlayers.IndexOf(player) + 1).ToString() + ". " + player.Name + " --> " + player.Moves.ToString() + " moves";
                    Console.WriteLine(scoreboardLine);
                }
            }
        }

        /// <summary>
        /// Prints the grid on the console.
        /// </summary>
        /// <param name="grid">Using <see cref="GameFifteen.Models.Contracts.IGrid"/> that contains a list of tiles.</param>
        public void RenderGrid(IGrid grid)
        {
            this.Clear();
            this.RenderMessage(GameMessages.Welcome);

            int x = InitialGridX;
            int y = InitialGridY;

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

            this.PrintOnPosition(0, ++y, GameMessages.EnterCommand);
        }

        /// <summary>
        /// Prints game options on the console.
        /// </summary>
        public void RenderGameOptions()
        {
            this.RenderMessage("Goal: ");
            this.RenderMessage(GameMessages.Goal);
            this.RenderMessage("Commands: ");
            foreach (var command in GameMessages.CommandsDescription)
            {
                Console.WriteLine("{0,-15}{1}{2}", command.Key, " -> ", command.Value);
            }

            foreach (var command in GameMessages.StyleCommandsDescription)
            {
                Console.WriteLine("{0,15}{1}{2}", command.Key, " -> ", command.Value);
            }
        }

        /// <summary>
        /// Prints message on the console.
        /// </summary>
        /// <param name="message">Text to be printed.</param>
        public void RenderMessage(string message)
        {
            Console.WriteLine(message);
        }

        /// <summary>
        /// Prints game screen log. 
        /// </summary>
        /// <param name="menuStartPositionX">Horizontal position of the menu.</param>
        /// <param name="menuStartPositionY">Vertical position of the menu.</param>
        public void RenderInitialScreen(int menuStartPositionX = GlobalConstants.MenuStartPositionX, int menuStartPositionY = GlobalConstants.MenuStartPositionY)
        {
            this.SetInitialConsoleSize();
            this.PrintOnPosition(0, 0, GameMessages.GameLogo, ConsoleColor.Yellow);
            this.RenderGameMenu(menuStartPositionX, menuStartPositionY);
        }

        private void RenderGameMenu(int menuStartPositionX = GlobalConstants.MenuStartPositionX, int menuStartPositionY = GlobalConstants.MenuStartPositionY)
        {
            this.PrintOnPosition(menuStartPositionX, menuStartPositionY, GameMessages.Enter, ConsoleColor.Yellow);

            int position = 0;
            foreach (var option in GameMessages.MenuOptions)
            {
                this.PrintOnPosition(menuStartPositionX, menuStartPositionY + position + 1, option.Key, ConsoleColor.White);
                this.PrintOnPosition(menuStartPositionX + 5, menuStartPositionY + position + 1, " to ", ConsoleColor.Yellow);

                this.PrintOnPosition(menuStartPositionX + 10, menuStartPositionY + position + 1, option.Value, ConsoleColor.Green);
                position++;
            }

            this.PrintOnPosition(menuStartPositionX, menuStartPositionY + GameMessages.MenuOptions.Count + 3, string.Empty, ConsoleColor.White);
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

        private void Clear()
        {
            Console.Clear();
        }
    }
}
