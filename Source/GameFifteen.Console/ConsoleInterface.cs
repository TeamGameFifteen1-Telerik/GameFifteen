namespace GameFifteen.Console
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using GameFifteen.Logic;
    using GameFifteen.Logic.Contracts;
    using GameFifteen.Common;

    public class ConsoleInterface : IUserInterface
    {
        private int destinationTileValue;
        private IDictionary<string, Command> commandStash = new Dictionary<string, Command> 
                                                                        {
                                                                           {GlobalConstants.RestartCommand, Command.Restart},
                                                                           {GlobalConstants.TopCommand, Command.Top},
                                                                           {GlobalConstants.ExitCommand, Command.Exit},
                                                                           {GlobalConstants.AgreeCommand, Command.Agree},
                                                                           {GlobalConstants.SaveCommand, Command.Save},
                                                                           {GlobalConstants.LoadCommand, Command.Load}
                                                                        };

        public string GetUserInput()
        {
            var input = Console.ReadLine();
            return input;
        }

        public Command GetCommandFromInput()
        {
            string input = this.GetUserInput().ToLower();

            if (commandStash.ContainsKey(input))
            {
                return commandStash[input];
            }
            else if (int.TryParse(input, out this.destinationTileValue))
            {
                return Command.Move;
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

        public int GetDestinationTileValue()
        {
            return this.destinationTileValue;
        }
    }
}
