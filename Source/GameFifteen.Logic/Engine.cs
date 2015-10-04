namespace GameFifteen.Logic
{
    using GameFifteen.Logic.Contracts;

    /// <summary>
    /// Implementing bridge design pattern to provides abstract game functionality.
    /// </summary>
    public abstract class Engine : IEngine
    {
        protected readonly IGameInitializater gameInitializer;

        protected Engine(IGameInitializater gameInitializer)
        {
            this.gameInitializer = gameInitializer;
        }

        /// <summary>
        /// Game initialization.
        /// </summary>
        public abstract void Initialize();
    }
}
