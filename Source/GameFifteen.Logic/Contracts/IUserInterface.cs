// <copyright file="IUserInterface.cs" company="Telerik Academy">All rights reserved.</copyright>
// <author>Team GameFifteen-1</author>
namespace GameFifteen.Logic.Contracts
{
    /// <summary>
    /// Provides interacting with user methods.
    /// </summary>
    public interface IUserInterface
    {
        /// <summary>
        /// Retrieves the user input.
        /// </summary>
        /// <returns>User input as string.</returns>
        string GetUserInput();

        /// <summary>
        /// Process input from user.
        /// </summary>
        /// <returns>Command from user input.</returns>
        Command GetCommandFromInput();

        /// <summary>
        /// Application exit.
        /// </summary>
        void ExitGame();

        /// <summary>
        /// Retrieves a correct argument to process.
        /// </summary>
        /// <param name="argument">Search by this value.</param>
        /// <returns>Correct argument. In most cases - string.</returns>
        dynamic GetArgumentValue(string argument);
    }
}
