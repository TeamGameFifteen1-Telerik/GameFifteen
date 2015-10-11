// <copyright file="Bindings.cs" company="Telerik Academy">All rights reserved.</copyright>
// <author>Team GameFifteen-1</author>
namespace GameFifteen.Console
{
    using Ninject.Modules;

    using GameFifteen.Console.Contracts;
    using GameFifteen.Console.Styles;
    using GameFifteen.Logic;
    using GameFifteen.Logic.Contracts;
    using GameFifteen.Models;
    using GameFifteen.Models.Contracts;

    /// <summary>
    /// Dependency injection
    /// Binds interfaces to concrete implementations.
    /// </summary>
    public class Bindings : NinjectModule
    {
        /// <summary>
        /// Binds interfaces to concrete implementations.
        /// </summary>
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
