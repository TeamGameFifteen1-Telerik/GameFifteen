namespace GameFifteen.Console
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using GameFifteen.Logic.Contracts;
    using GameFifteen.Models;

    public class ConsoleRenderer : IRenderer
    {
        public void PrintScoreboard(Scoreboard scoreboard)
        {
            Console.WriteLine("Scoreboard:");
            foreach (Player player in scoreboard.Players)
            {
                string scoreboardLine = (scoreboard.Players.IndexOf(player) + 1).ToString() + ". " + player.Name + " --> " + player.Moves.ToString() + " moves";
                Console.WriteLine(scoreboardLine);
            }
        }

        public void PrintMatrix(Grid sourceMatrix)
        {
            Console.WriteLine("  ------------");
            Console.Write("| ");
            int rowCounter = 0;
            for (int index = 0; index < 16; index++)
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
                if (rowCounter == 4)
                {
                    Console.Write(" |");
                    Console.WriteLine();
                    if (index < 12)
                    {
                        Console.Write("| ");
                    }

                    rowCounter = 0;
                }
            }

            Console.WriteLine("  ------------");
        }


        public void PrintMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
