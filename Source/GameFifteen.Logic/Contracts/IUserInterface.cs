namespace GameFifteen.Logic.Contracts
{
    public interface IUserInterface
    {
        string GetUserInput();

        Command GetCommandFromInput();

        void ExitGame();

        dynamic GetArgumentValue(string argument);
    }
}
