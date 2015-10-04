namespace GameFifteen.Logic
{
    /// <summary>
    /// Commands for processing.
    /// </summary>
    public enum Command
    {
        /// <summary>
        /// Start command - game begins.
        /// </summary>
        Start,

        /// <summary>
        /// Restarts current game and starts new one.
        /// </summary>
        Restart,
        
        /// <summary>
        /// Shows top players.
        /// </summary>
        Top,

        /// <summary>
        /// Game ends.
        /// </summary>
        Exit,

        /// <summary>
        /// User agrees with prompt message.
        /// </summary>
        Yes,

        /// <summary>
        /// Moves tiles.
        /// </summary>

        Move,

        /// <summary>
        /// Invalid command.
        /// </summary>
        Invalid,

        /// <summary>
        /// Save game state.
        /// </summary>
        Save,

        /// <summary>
        /// Load last game save.
        /// </summary>
        Load,

        /// <summary>
        /// Changes grid border style.
        /// </summary>
        Style,

        /// <summary>
        /// Resolves the grid a.k.a. hack.
        /// </summary>
        Solve,
        
        /// <summary>
        /// Shows help commands.
        /// </summary>
        How
    }
}
