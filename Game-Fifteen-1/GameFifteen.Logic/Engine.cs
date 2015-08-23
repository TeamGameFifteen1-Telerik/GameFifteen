namespace GameFifteen.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using GameFifteen.Common;
    using GameFifteen.Models;

    public class Engine
    {
        public static List<Tile> MoveTiles(List<Tile> tiles, int tileValue)
        {
            if (tileValue < 0 || tileValue > 15)
            {
                throw new ArgumentException("Invalid move!");
            }

            List<Tile> resultMatrix = tiles;
            Tile freeTile = tiles[GetFreeTilePosition(tiles)];
            Tile tile = tiles[GetDestinationTilePosition(tiles, tileValue)];

            bool areValidNeighbourTiles = TilePositionValidation(tiles, freeTile, tile);

            if (areValidNeighbourTiles)
            {
                int targetTilePosition = tile.Position;
                resultMatrix[targetTilePosition].Position = freeTile.Position;
                resultMatrix[freeTile.Position].Position = targetTilePosition;
                resultMatrix.Sort();
            }
            else
            {
                throw new Exception("Invalid move!");
            }

            return resultMatrix;
        }

        public static bool IsMatrixSolved(List<Tile> tiles)
        {
            int count = 0;
            foreach (Tile tile in tiles)
            {
                int tileLabelInt = 0;
                Int32.TryParse(tile.Label,out tileLabelInt);
                if (tileLabelInt == (tile.Position + 1))
                {
                    count++;
                }
            }

            if (count == 15)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static int GetDestinationTilePosition(List<Tile> tiles, int tileValue)
        {
            int result = 0;
            for (int index = 0; index < tiles.Count; index++)
            {
                int parsedLabel = 0;
                bool successfulParsing = Int32.TryParse(tiles[index].Label, out parsedLabel);
                if (successfulParsing && tileValue == parsedLabel)
                {
                    result = index;
                }
            }
            return result;
        }

        private static bool TilePositionValidation(List<Tile> tiles, Tile freeTile, Tile tile)
        {
            bool areValidNeighbourTiles = AreValidNeighbourTiles(freeTile, tile);

            return areValidNeighbourTiles;
        }

        private static bool AreValidNeighbourTiles(Tile freeTile, Tile tile)
        {
            int tilesDistance = freeTile.Position - tile.Position;
            int tilesAbsoluteDistance = Math.Abs(tilesDistance);
            bool isValidHorizontalNeighbour =
                (tilesAbsoluteDistance == GlobalConstants.HORIZONTAL_NEIGHBOUR_TILE && !(((tile.Position + 1) % GlobalConstants.MATRIX_SIZE == 1 && tilesDistance == -1) || ((tile.Position + 1) % GlobalConstants.MATRIX_SIZE == 0 && tilesDistance == 1)));
            bool isValidVerticalNeighbour = (tilesAbsoluteDistance == GlobalConstants.VERTICAL_NEIGHBOUR_TILE);
            bool validNeigbour = isValidHorizontalNeighbour || isValidVerticalNeighbour;

            return validNeigbour;
        }

        private static int GetFreeTilePosition(List<Tile> tiles)
        {
            int result = 0;
            for (int index = 0; index < tiles.Count; index++)
            {
                if (tiles[index].Label == String.Empty)
                {
                    result = index;
                }
            }
            return result;
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
    }
}
