namespace GameFifteen.Console
{
    using System;

    using GameFifteen.Logic.Contracts;
    using GameFifteen.Models;
    using GameFifteen.Common;
    using GameFifteen.Console.Contracts;
    using GameFifteen.Console.Styles;
    using GameFifteen.Models.Contracts;

    public class ConsoleRenderer : IRenderer
    {
        public void PrintScoreboard(Scoreboard scoreboard)
        {
            Console.WriteLine("Scoreboard:");
            foreach (Player player in scoreboard.TopPlayers)
            {
                string scoreboardLine = (scoreboard.TopPlayers.IndexOf(player) + 1).ToString() + ". " + player.Name + " --> " + player.Moves.ToString() + " moves";
                Console.WriteLine(scoreboardLine);
            }
        }

        public void PrintMatrix(IGrid sourceMatrix)
        {
            this.PrintMatrix(sourceMatrix, new SolidStyle());
        }

        public void PrintMatrix(IGrid sourceMatrix, GridBorderStyle style)
        {
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
