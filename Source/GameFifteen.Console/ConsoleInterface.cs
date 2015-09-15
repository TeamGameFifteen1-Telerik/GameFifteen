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

        public string GetUserInput()
        {
            var input = Console.ReadLine();
            return input;
        }

        public Command GetCommandFromInput()
        {
            string input = this.GetUserInput().ToLower();

            switch (input)
            {
                case GlobalConstants.RestartCommand:
                    return Command.Restart;
                case GlobalConstants.TopCommand:
                    return Command.Top;
                case GlobalConstants.ExitCommand:
                    return Command.Exit;
                case GlobalConstants.AgreeCommand:
                    return Command.Agree;
                default:
                    if (int.TryParse(input, out this.destinationTileValue))
                    {
                        return Command.Move;
                    }

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
