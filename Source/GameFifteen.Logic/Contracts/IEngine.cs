namespace GameFifteen.Logic.Contracts
{
    /// <summary>
    /// Provides game initialization.
    /// </summary>
    public interface IEngine
    {
        /// <summary>
        /// Initializes the game.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Starts the game.
        /// </summary>
        void Run();
    }
}
