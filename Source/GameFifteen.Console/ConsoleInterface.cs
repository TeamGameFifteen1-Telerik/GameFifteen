namespace GameFifteen.Console
{
    using System;
    using System.Collections.Generic;

    using GameFifteen.Logic;
    using GameFifteen.Logic.Contracts;
    using GameFifteen.Common;

    public class ConsoleInterface : IUserInterface
    {
        // private int destinationTileValue;
        //private string borderStyle;
        private IDictionary<string, dynamic> arguments;
        private IDictionary<string, Command> commandStash;

        public ConsoleInterface()
        {
            this.arguments = new Dictionary<string, dynamic>();
            this.commandStash = new Dictionary<string, Command> 
                                    {
                                        {GlobalConstants.RestartCommand, Command.Restart},
                                        {GlobalConstants.TopCommand, Command.Top},
                                        {GlobalConstants.ExitCommand, Command.Exit},
                                        {GlobalConstants.AgreeCommand, Command.Agree},
                                        {GlobalConstants.SaveCommand, Command.Save},
                                        {GlobalConstants.LoadCommand, Command.Load},
                                        {GlobalConstants.StyleCommand, Command.Style},
                                        {GlobalConstants.SolveCommand, Command.Solve}
                                    };
        }

        public string GetUserInput()
        {
            var input = Console.ReadLine();
            return input;
        }

        public Command GetCommandFromInput()
        {
            string input = this.GetUserInput().ToLower();

            if (input.Contains(GlobalConstants.StyleCommand))
            {
                string[] parameters = input.Split(new string[] { GlobalConstants.ExstenstionOperator }, StringSplitOptions.RemoveEmptyEntries);

                if (!commandStash.ContainsKey(parameters[0]) || parameters.Length != 2)
                {
                    return Command.Invalid;
                }

                this.arguments[GlobalConstants.BorderStyle] = parameters[1].Trim();

                return commandStash[parameters[0]];
            }

            int destinationTileValue;
            if (commandStash.ContainsKey(input))
            {
                return commandStash[input];
            }
            else if (int.TryParse(input, out destinationTileValue)) //this.destinationTileValue
            {
                this.arguments[GlobalConstants.DestinationTileValue] = destinationTileValue;

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

        //public int GetDestinationTileValue()
        //{
        //    return this.destinationTileValue;
        //}

        //public string GetBorderStyle()
        //{
        //    return this.borderStyle;
        //}

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
