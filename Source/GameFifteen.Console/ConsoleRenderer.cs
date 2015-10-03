namespace GameFifteen.Console
{
    using System;

    using GameFifteen.Logic.Contracts;
    using GameFifteen.Models;
    using GameFifteen.Common;
    using GameFifteen.Console.Contracts;
    using GameFifteen.Console.Styles;
    using GameFifteen.Models.Contracts;
    using System.Collections.Generic;

    public class ConsoleRenderer : IRenderer
    {
        //TODO refactor
        private Dictionary<string, IStyle> styles;
        private IStyleFactory borderStyleFactory;

        public ConsoleRenderer()
        {
            this.borderStyleFactory = new BorderStyleFactory();
            this.styles = new Dictionary<string, IStyle>();
            this.styles["GridBorderStyle"] = borderStyleFactory.Get(BorderStyleType.Solid);
        }

        public Dictionary<string, IStyle> Styles 
        {
            get
            {
                return new Dictionary<string, IStyle>(this.styles);
            }
        }

        public void AddStyle(params string[] styles)
        {
            if (styles.Length == 0)
            {
                throw new ArgumentNullException("You need at least one style.");
            }

            foreach (var style in styles)
            {
                switch (style)
                {
                    case "solid":
                        this.styles["GridBorderStyle"] = borderStyleFactory.Get(BorderStyleType.Solid);
                        break;
                    case "dotted":
                        this.styles["GridBorderStyle"] = borderStyleFactory.Get(BorderStyleType.Dotted);
                        break;
                    case "fat":
                        this.styles["GridBorderStyle"] = borderStyleFactory.Get(BorderStyleType.Fat);
                        break;
                    case "middlefat":
                        this.styles["GridBorderStyle"] = borderStyleFactory.Get(BorderStyleType.MiddleFat);
                        break;
                    case "double":
                        this.styles["GridBorderStyle"] = borderStyleFactory.Get(BorderStyleType.Double);
                        break;
                    case "default":
                        this.styles["GridBorderStyle"] = borderStyleFactory.Get(BorderStyleType.Default);
                        break;
                    default:
                        throw new ArgumentException();
                        break;
                }
            }
        }

        public void PrintScoreboard(Scoreboard scoreboard)
        {
            Console.WriteLine("Scoreboard:");
            foreach (Player player in scoreboard.TopPlayers)
            {
                string scoreboardLine = (scoreboard.TopPlayers.IndexOf(player) + 1).ToString() + ". " + player.Name + " --> " + player.Moves.ToString() + " moves";
                Console.WriteLine(scoreboardLine);
            }
        }

        //public void PrintMatrix(IGrid sourceMatrix)
        //{
        //    this.AddStyle("solid");
        //    this.PrintMatrix(sourceMatrix);
        //}

        public void PrintMatrix(IGrid sourceMatrix)
        {
            var style = this.styles["GridBorderStyle"] as GridBorderStyle;
            Console.WriteLine(style.Top);
            Console.Write(style.Left);
            int rowCounter = 0;
            for (int index = 0; index < GlobalConstants.TotalTilesCount; index++)
            {
                Tile currentElement = sourceMatrix.GetTileAtPosition(index);

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


        public void PrintMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
