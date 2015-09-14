namespace GameFifteen.Console
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using GameFifteen.Logic;
    using GameFifteen.Logic.Contracts;

    public class ConsoleInterface : IUserInterface
    {
        public string GetUserInput()
        {
            var input = Console.ReadLine();
            return input;
        }

        public Command GetCommandFromInput()
        {
            throw new NotImplementedException();

            string input = this.GetUserInput().ToLower();

            /*
            switch (input)
            {
            }
             */
        }
    }
}
