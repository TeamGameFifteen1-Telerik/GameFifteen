namespace GameFifteen.Logic.Contracts
{
    public interface IUserInterface
    {
        string GetUserInput();

        Command GetCommandFromInput();

        void ExitGame();

        int GetDestinationTileValue();

        string SpecialParams { get; }
    }
}
