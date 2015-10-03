namespace GameFifteen.Console
{
    public class GameFifteenMain
    {      
        public static void Main()
        {
            var gameStarter = GameFifteenStarter.Instance;
            gameStarter.NewGame();
        }
    }
}
