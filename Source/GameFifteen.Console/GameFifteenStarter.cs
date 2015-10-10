namespace GameFifteen.Console
{
    using System.Reflection;

    using Ninject;

    using GameFifteen.Logic.Contracts;

    /// <summary>
    /// A normal game starter object implementing Facade design pattern.
    /// </summary>
    public class GameFifteenStarter
    {
        private static GameFifteenStarter instance;

        /// <summary>
        /// Private <see cref="GameFifteenStarter"/> constructor - Singleton
        /// </summary>
        private GameFifteenStarter()
        {
        }

        /// <summary>
        /// Gets a instance of the game <see cref="GameFifteenStarter"/> class.
        /// </summary>
        /// <value>Instance of a game starter.</value>
        public static GameFifteenStarter Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameFifteenStarter();
                }

                return instance;
            }
        }

        /// <summary>
        /// Starts a new game.
        /// </summary>
        public void NewGame()
        {
            IKernel kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            var engine = kernel.Get<IEngine>();
            engine.Initialize();
        }
    }
}
