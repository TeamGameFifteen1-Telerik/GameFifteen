﻿// <copyright file="GlobalConstants.cs" company="Telerik Academy">All rights reserved.</copyright>
// <author>Team GameFifteen-1</author>
namespace GameFifteen.Common
{
    /// <summary>
    /// Common constants used throughout the project.
    /// </summary>
    public class GlobalConstants
    {
        public const int GridSize = 4;
        public const int TotalTilesCount = GridSize * GridSize;

        public const int HorizonaltNeighbourTile = 1;
        public const int VerticalNeighbourTile = GridSize;

        public const string DestinationTileValue = "DestinationTileValue";
        public const string GridBorderStyle = "GridBorderStyle";

        public const string ScoreboardFilePath = @"../";
    }
}
