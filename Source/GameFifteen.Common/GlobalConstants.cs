namespace GameFifteen.Common
{
    public class GlobalConstants
    {
        public const int HorizonaltNeighbourTile = 1;
        public const int VerticalNeighbourTile = 4;
        public const int GridSize = 4;
        public const int TotalTilesCount = 16;

        public const string DestinationTileValue = "DestinationTileValue";
        public const string GridBorderStyle = "GridBorderStyle";

        /// TODO: extract from here?
        public const string ExstenstionOperator = "=";

        /// Remove? Use enum?
        public const string StartCommand = "start";
        public const string RestartCommand = "restart";
        public const string TopCommand = "top";
        public const string ExitCommand = "exit";
        public const string AgreeCommand = "y";
        public const string SaveCommand = "save";
        public const string LoadCommand = "load";
        public const string StyleCommand = "style";
        public const string SolveCommand = "solve";
        public const string HowCommand = "how";

        public const int MenuStartPositionX = 47;
        public const int MenuStartPositionY = 15;
    }
}
