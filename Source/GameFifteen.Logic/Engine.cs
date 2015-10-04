namespace GameFifteen.Logic
{
    using GameFifteen.Logic.Contracts;

    /// <summary>
    /// Bridge design pattern
    /// </summary>
    public abstract class Engine : IEngine
    {
        protected readonly IGameInitializater gameInitializer;

        protected Engine(IGameInitializater gameInitializer)
        {
            this.gameInitializer = gameInitializer;
        }

        public abstract void Initialize();

        public abstract void Run();
    }
}
