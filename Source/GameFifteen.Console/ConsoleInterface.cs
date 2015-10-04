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

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleInterface"/> class.
        /// </summary>
        public ConsoleInterface()
        {
            this.arguments = new Dictionary<string, dynamic>();
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
            if (input.Length > 1)
            {
                input = input[0].ToString().ToUpper() + input.Substring(1);
            }

            int destinationTileValue;
            Command command;
            if (int.TryParse(input, out destinationTileValue))
            {
                this.arguments[GlobalConstants.DestinationTileValue] = destinationTileValue;

                return Command.Move;
            } 
            else if (Enum.TryParse(input, out command))
            {
                return command;
            }
            else if (input.StartsWith(Command.Style.ToString()))
            {
                string[] parameters = input.Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);

                if (parameters[0] != Command.Style.ToString() || parameters.Length != 2)
                {
                    return Command.Invalid;
                }

                this.arguments[GlobalConstants.GridBorderStyle] = parameters[1].Trim();

                return Command.Style;
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
