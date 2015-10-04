namespace GameFifteen.Console
{
    using System;
    using System.Collections.Generic;

    using GameFifteen.Common;
    using GameFifteen.Logic;
    using GameFifteen.Logic.Contracts;

    /// <summary>
    /// Interacts with console user, process commands.
    /// </summary>
    public class ConsoleInterface : IUserInterface
    {
        private IDictionary<string, dynamic> arguments;
        private IDictionary<string, Command> commandStash;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleInterface"/> class.
        /// </summary>
        public ConsoleInterface()
        {
            this.arguments = new Dictionary<string, dynamic>();
            this.commandStash = new Dictionary<string, Command> 
                                    {
                                        { GlobalConstants.RestartCommand, Command.Restart },
                                        { GlobalConstants.TopCommand, Command.Top },
                                        { GlobalConstants.ExitCommand, Command.Exit },
                                        { GlobalConstants.AgreeCommand, Command.Agree },
                                        { GlobalConstants.SaveCommand, Command.Save },
                                        { GlobalConstants.LoadCommand, Command.Load },
                                        { GlobalConstants.StyleCommand, Command.Style },
                                        { GlobalConstants.SolveCommand, Command.Solve }
                                    };
        }

        /// <summary>
        /// Gets the input text from the user.
        /// </summary>
        /// <returns>The input as <see cref="System.String"/></returns>
        public string GetUserInput()
        {
            var input = Console.ReadLine();
            return input;
        }

        /// <summary>
        /// Process input from the user.
        /// </summary>
        /// <returns>A <see cref="GameFifteen.Logic.Command"/> to process.</returns>
        public Command GetCommandFromInput()
        {
            string input = this.GetUserInput().ToLower();

            if (input.Contains(GlobalConstants.StyleCommand))
            {
                string[] parameters = input.Split(new string[] { GlobalConstants.ExstenstionOperator }, StringSplitOptions.RemoveEmptyEntries);

                if (!this.commandStash.ContainsKey(parameters[0]) || parameters.Length != 2)
                {
                    return Command.Invalid;
                }

                this.arguments[GlobalConstants.GridBorderStyle] = parameters[1].Trim();

                return this.commandStash[parameters[0]];
            }

            int destinationTileValue;
            if (this.commandStash.ContainsKey(input))
            {
                return this.commandStash[input];
            }
            else if (int.TryParse(input, out destinationTileValue))
            {
                this.arguments[GlobalConstants.DestinationTileValue] = destinationTileValue;

                return Command.Move;
            }
            else
            {
                return Command.Invalid;
            }
        }

        /// <summary>
        /// Exits from the application.
        /// </summary>
        public void ExitGame()
        {
            Environment.Exit(0);
        }

        /// <summary>
        /// Returns a correct value from arguments.
        /// </summary>
        /// <param name="argument">The value to search for.</param>
        /// <exception cref="System.ArgumentException"> 
        /// Arguments doesn't contain the value. 
        /// </exception>
        /// <returns>A dynamic object.</returns>
        public dynamic GetArgumentValue(string argument)
        {
            if (!this.arguments.ContainsKey(argument))
            {
                throw new ArgumentException("No such argument");
            }

            return this.arguments[argument];
        }
    }
}
