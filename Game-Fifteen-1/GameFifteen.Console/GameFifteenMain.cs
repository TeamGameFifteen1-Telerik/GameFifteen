namespace GameFifteen.Console
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using GameFifteen.Logic;
    using GameFifteen.Models;

	// mnogo sym dobyr programist, u4astvam v TopCoder i sam purvi ot Sliven i regiona

    public class GameFifteenMain
    {      
        private static void Menu()
        {
            var scoreBoard = new Scoreboard();
            var players = scoreBoard.Players;
            var grid = new Grid();
            var renderer = new ConsoleRenderer();
            var engine = new Engine();

            List<Tile> tiles = new List<Tile>();
            int cnt = 0;
            string s = "restart";
            bool flag = false;

            while (s != "exit")
            {
                if (!flag)
                {
                    switch (s)
                    {
                        case "restart":
                            {
                                string welcomeMessage = "Welcome to the game “15”. Please try to arrange the numbers sequentially. ";
                                welcomeMessage = welcomeMessage + "\nUse 'top' to view the top scoreboard, 'restart' to start a new game and 'exit'";
                                welcomeMessage = welcomeMessage + " \nto quit the game.";
                                Console.WriteLine();
                                Console.WriteLine(welcomeMessage);
                                tiles = grid.GenerateMatrix();
                                tiles = grid.ShuffleMatrix(tiles);
                                flag = Engine.IsMatrixSolved(tiles);
                                renderer.PrintMatrix(tiles);
                                break;
                            }
                        case "top":
                            {
                                renderer.PrintScoreboard(players);
                                break;
                            }
                    }
                    if (!flag)
                    {
                        Console.Write("Enter a number to move: ");
                        s = Console.ReadLine();

                        int destinationTileValue;

                        bool isSuccessfulParsing = Int32.TryParse(s, out destinationTileValue);

                        if (isSuccessfulParsing)
                        {
                            try
                            {
                                Engine.MoveTiles(tiles, destinationTileValue);
                                cnt++;
                                renderer.PrintMatrix(tiles);
                                flag = Engine.IsMatrixSolved(tiles);
                            }
                            catch (Exception exception)
                            {
                                Console.WriteLine(exception.Message);
                            }
                        }
                        else
                        {
                            try
                            {
                                s = Engine.ProcessCommand(s);
                            }
                            catch (ArgumentException exception)
                            {
                                Console.WriteLine(exception.Message);
                            }
                        }
                    }



                }
                else
                {
                    if (cnt == 0)
                    {
                        Console.WriteLine("Your matrix was solved by default :) Come on - NEXT try");
                    }
                    else
                    {
                        Console.WriteLine("Congratulations! You won the game in {0} moves.", cnt);
                        Console.Write("Please enter your name for the top scoreboard: ");
                        string playerName = Console.ReadLine();
                        Player player = new Player(playerName, cnt);
                        scoreBoard.AddPlayer(player);
                        renderer.PrintScoreboard(players);
                    }
                    s = "restart";
                    flag = false;
                    cnt = 0;



                }

            }
        }

        static void Main(string[] args)
        {


            Menu();
        }
    }
}
