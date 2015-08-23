namespace GameFifteen.Console
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using GameFifteen.Models;

    public class ConsoleRenderer
    {
        public void PrintScoreboard(List<Player> players)
        {
            Console.WriteLine("Scoreboard:");
            foreach (Player player in players)
            {
                string scoreboardLine = (players.IndexOf(player) + 1).ToString() + ". " + player.Name + " --> " + player.Moves.ToString() + " moves";
                Console.WriteLine(scoreboardLine);
            }
        }

        public void PrintMatrix(List<Tile> sourceMatrix)
        {
            Console.WriteLine("  ------------");
            Console.Write("| ");
            int rowCounter = 0;
            for (int index = 0; index < 16; index++)
            {
                Tile currentElement = sourceMatrix.ElementAt(index);

                if (currentElement.Label == String.Empty)
                {
                    Console.Write("   ");
                }
                else if (Int32.Parse(currentElement.Label) < 10)
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
    }
}
