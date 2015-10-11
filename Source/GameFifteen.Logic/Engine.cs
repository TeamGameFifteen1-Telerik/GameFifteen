// <copyright file="Engine.cs" company="Telerik Academy">All rights reserved.</copyright>
// <author>Team GameFifteen-1</author>
namespace GameFifteen.Logic
{
    using GameFifteen.Logic.Contracts;

    /// <summary>
    /// Implementing bridge design pattern to provides abstract game functionality.
    /// </summary>
    public abstract class Engine : IEngine
    {
        /// <summary>
        /// <see cref="IGameInitializater"/> readonly field.
        /// </summary>
        protected readonly IGameInitializater GameInitializer;

        /// <summary>
        /// Initializes a new instance of the <see cref="Engine" /> class.
        /// </summary>
        /// <param name="gameInitializer">Game initializer.</param>
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
