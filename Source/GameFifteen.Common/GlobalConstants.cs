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

        public const int MenuStartPositionX = 47;
        public const int MenuStartPositionY = 15;
    }
}
