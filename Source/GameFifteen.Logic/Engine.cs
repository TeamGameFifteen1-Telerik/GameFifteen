namespace GameFifteen.Logic
{
    using GameFifteen.Logic.Contracts;

    public abstract class Engine : IEngine
    {
        protected readonly IGameInitializater gameInitializer;

        protected Engine(IGameInitializater gameInitializer)
        {
            this.gameInitializer = gameInitializer;
        }

        public abstract void Run();
    }
}
