﻿namespace GameFifteen.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using GameFifteen.Common;
    using GameFifteen.Models;
    using GameFifteen.Logic.Contracts;

    public class Engine
    {
        private IRenderer renderer;
        private IUserInterface userInterface;
        private Grid grid;

        public Engine(IRenderer renderer, IUserInterface userInterface)
        {
            this.renderer = renderer;
            this.userInterface = userInterface;
            this.grid = new Grid();
        }

        public void Run()
        {
            var scoreBoard = new Scoreboard();
            var players = scoreBoard.Players;
            var grid = new Grid();
            //var renderer = new ConsoleRenderer();
            

            //Grid tiles = new Grid();
            int cnt = 0;
            string s = "restart";

            //renamed flag
            bool isSolved = false;

            while (s != "exit")
            {
                if (!isSolved)
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
                                //TODO
                                grid.Clear();
                                grid.Initialize();

                                //tiles = new Grid(); //grid.InitializeGrid();
                                //tiles = grid.ShuffleMatrix(tiles);
                                isSolved = Engine.IsMatrixSolved(grid);
                                renderer.PrintMatrix(grid);
                                break;
                            }
                        case "top":
                            {
                                renderer.PrintScoreboard(players);
                                break;
                            }
                    }

                    if (!isSolved)
                    {
                        Console.Write("Enter a number to move: ");
                        s = Console.ReadLine();

                        int destinationTileValue;

                        bool isSuccessfulParsing = Int32.TryParse(s, out destinationTileValue);

                        if (isSuccessfulParsing)
                        {
                            try
                            {
                                grid.MoveTile(destinationTileValue);
                                //Engine.MoveTiles(grid, destinationTileValue);
                                cnt++;
                                renderer.PrintMatrix(grid);
                                isSolved = Engine.IsMatrixSolved(grid);
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
                    isSolved = false;
                    cnt = 0;



                }

            }
        }

        public static bool IsMatrixSolved(Grid grid)
        {
            return grid.IsSorted;
        }

        public static string ProcessCommand(string input)
        {
            string inputToLower = input.ToLower();
            string output;

            if (inputToLower == Command.Exit.ToString().ToLower() ||
                inputToLower == Command.Restart.ToString().ToLower() || 
                inputToLower == Command.Top.ToString())
            {
                output = inputToLower;
            }
            else
            {
                throw new ArgumentException("Invalid Command!");
            }

            return output;
        }

        // should this get back here?
        //public static void MoveTiles(Grid grid, int tileValue)
        //{
        //    if (tileValue < 0 || tileValue > 15)
        //    {
        //        throw new ArgumentException("Invalid move!");
        //    }

        //    //List<Tile> resultMatrix = tiles;
        //    //Tile freeTile = tiles[GetFreeTilePosition(tiles)];
        //    //var destinationTilePosition = GetDestinationTilePosition(grid, tileValue);
        //    //Tile tile = grid.GetTileAtPosition(destinationTilePosition);
        //    int destinationTilePosition = grid.GetTilePosition(tileValue.ToString());
        //    Tile tile = grid.GetTileAtPosition(destinationTilePosition);

        //    bool areValidNeighbourTiles = grid.ValidateNeighbour(tile); //TilePositionValidation(grid.EmptyTile, tile);

        //    if (areValidNeighbourTiles)
        //    {
        //        grid.SwapTiles(tile);
        //        //swap tiles
        //        //int targetTilePosition = tile.Position;
        //        //resultMatrix[targetTilePosition].Position = freeTile.Position;
        //        //resultMatrix[freeTile.Position].Position = targetTilePosition;
        //        //resultMatrix.Sort();
        //    }
        //    else
        //    {
        //        throw new Exception("Invalid move!");
        //    }

        //   // return resultMatrix;
        //}
    }
}
