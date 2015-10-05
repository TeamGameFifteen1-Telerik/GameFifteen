namespace GameFifteen.Console
{
    using Ninject.Modules;

    using GameFifteen.Console.Styles;
    using GameFifteen.Logic;
    using GameFifteen.Logic.Contracts;
    using GameFifteen.Models;
    using GameFifteen.Models.Contracts;
    using GameFifteen.Console.Contracts;

    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IUserInterface>().To<ConsoleInterface>();
            Bind<IGameInitializater>().To<StandartGameInitializer>();
            Bind<IStyleFactory>().To<BorderStyleFactory>();
            Bind<IRenderer>().To<ConsoleRenderer>();
            Bind<IGrid>().To<Grid>();
            Bind<IPlayer>().To<Player>();
            Bind<IEngine>().To<StandartFifteenTilesEngine>();            
        }
    }
}
