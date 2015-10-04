namespace GameFifteen.Console
{
    /// <summary>
    /// The main object where game starts.
    /// </summary>
    public class GameFifteenMain
    {      
        /// <summary>
        /// Starts GameFifteen.
        /// </summary>
        public static void Main()
        {
            var gameStarter = GameFifteenStarter.Instance;
            gameStarter.NewGame();
        }
    }
}
