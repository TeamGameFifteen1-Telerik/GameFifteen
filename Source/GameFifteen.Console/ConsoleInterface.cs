namespace GameFifteen.Console
{
    using System;
    using System.Collections.Generic;

    using GameFifteen.Common;
    using GameFifteen.Logic;
    using GameFifteen.Logic.Contracts;

    public class ConsoleInterface : IUserInterface
    {
        private IDictionary<string, dynamic> arguments;

        public ConsoleInterface()
        {
            this.arguments = new Dictionary<string, dynamic>();
        }

        public string GetUserInput()
        {
            var input = Console.ReadLine();
            return input;
        }

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

        public void ExitGame()
        {
            Environment.Exit(0);
        }

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
