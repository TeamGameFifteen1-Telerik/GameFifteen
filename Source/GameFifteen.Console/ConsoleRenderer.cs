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
                    IStyle borderStyle = this.borderStyleFactory.Get(styleType);
                    this.styles[GlobalConstants.GridBorderStyle] = borderStyle;
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
            foreach (Player player in scoreboard.TopPlayers)
            {
                string scoreboardLine = (scoreboard.TopPlayers.IndexOf(player) + 1).ToString() + ". " + player.Name + " --> " + player.Moves.ToString() + " moves";
                Console.WriteLine(scoreboardLine);
            }
        }

        /// <summary>
        /// Prints the grid on the console.
        /// </summary>
        /// <param name="grid">Using <see cref="GameFifteen.Models.Contracts.IGrid"/> that contains a list of tiles.</param>
        public void RenderGrid(IGrid grid)
        {
            GridBorderStyle style;
            if (!this.styles.ContainsKey(GlobalConstants.GridBorderStyle))
            {
                style = this.borderStyleFactory.Get(BorderStyleType.Default) as GridBorderStyle;
            }

            style = this.styles[GlobalConstants.GridBorderStyle] as GridBorderStyle;

            Console.WriteLine(style.Top);
            Console.Write(style.Left);
            int rowCounter = 0;
            for (int index = 0; index < GlobalConstants.TotalTilesCount; index++)
            {
                Tile currentElement = grid.GetTileAtPosition(index);

                if (currentElement.Label == string.Empty)
                {
                    Console.Write("   ");
                }
                else if (int.Parse(currentElement.Label) < 10)
                {
                    Console.Write(' ' + currentElement.Label + ' ');
                }
                else
                {
                    Console.Write(currentElement.Label + ' ');
                }

                rowCounter++;
                if (rowCounter == GlobalConstants.GridSize)
                {
                    Console.Write(style.Right);
                    Console.WriteLine();
                    if (index < 12)
                    {
                        Console.Write(style.Left);
                    }

                    rowCounter = 0;
                }
            }

            Console.WriteLine(style.Bottom);
        }

        /// <summary>
        /// Prints message on the console.
        /// </summary>
        /// <param name="message">Text to be printed.</param>
        public void RenderMessage(string message)
        {
            Console.WriteLine(message);
        }
        public void RenderInitialScreen(int menuStartPositionX = GlobalConstants.MenuStartPositionX, int menuStartPositionY = GlobalConstants.MenuStartPositionY)
        {
            this.PrintOnPosition(0, 0, GameMessages.GameLogo, ConsoleColor.Yellow);
            this.RenderGameMenu(menuStartPositionX, menuStartPositionY);
        }

        private void RenderGameMenu(int menuStartPositionX = GlobalConstants.MenuStartPositionX, int menuStartPositionY = GlobalConstants.MenuStartPositionY)
        {
            this.PrintOnPosition(menuStartPositionX, menuStartPositionY, GameMessages.Enter, ConsoleColor.Yellow);

            var options = new Dictionary<string, string>()
            {
                {"START", "Start new game" },
                {"HOW", "See game options"},
                {"TOP", "Get top scores"},
                {"EXIT", "Quit"}
            };

            int position = 0;
            foreach (var option in options)
            {
                this.PrintOnPosition(menuStartPositionX, menuStartPositionY + position + 1, option.Key, ConsoleColor.White);
                this.PrintOnPosition(menuStartPositionX + 5, menuStartPositionY + position + 1, " to ", ConsoleColor.Yellow);

                this.PrintOnPosition(menuStartPositionX + 10, menuStartPositionY + position + 1, option.Value, ConsoleColor.Green);
                position++;
            }

            this.PrintOnPosition(menuStartPositionX, menuStartPositionY + options.Count + 3, string.Empty, ConsoleColor.White);
        }

        private void PrintOnPosition(int x, int y, string text, ConsoleColor color = ConsoleColor.White)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = color;
            Console.Write(text);
        }
    }
}
