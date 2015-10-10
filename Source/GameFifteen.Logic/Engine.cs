namespace GameFifteen.Logic
{
    using GameFifteen.Logic.Contracts;

    /// <summary>
    /// Implementing bridge design pattern to provides abstract game functionality.
    /// </summary>
    public abstract class Engine : IEngine
    {
        protected readonly IGameInitializater GameInitializer;

        /// <summary>
        /// Abstract engine constructor
        /// </summary>
        /// <param name="gameInitializer">Game initializer</param>
        protected Engine(IGameInitializater gameInitializer)
        {
            this.GameInitializer = gameInitializer;
        }

        /// <summary>
        /// Game initialization.
        /// </summary>
        public abstract void Initialize();
    }
}
