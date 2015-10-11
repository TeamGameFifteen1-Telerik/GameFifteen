// <copyright file="ConsoleInterfaceTests.cs" company="Telerik Academy">All rights reserved.</copyright>
// <author>Team GameFifteen-1</author>
namespace GameFifteen.ConsoleTests
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Collections.Generic;
    using GameFifteen.Common;
    using GameFifteen.Console;
    using GameFifteen.Logic;
    using GameFifteen.Logic.Contracts;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public class ConsoleInterfaceTests
    {
        [TestMethod]
        public void TestGetUserInputToReturnCorrectValue()
        {
            var mockedReader = new Mock<HelperReader>();
            Console.SetIn(mockedReader.Object);

            IList<string> input = new List<string>() { "start", "solve", "pesho", "yes", "solve", "pesho", "no " };

            int indexOfCommand = 0;
            mockedReader.Setup(r => r.ReadLine()).Returns(() => input[indexOfCommand]);

            IUserInterface interf = new ConsoleInterface();
            string result;
            for (; indexOfCommand < input.Count; indexOfCommand++)
            {
                result = interf.GetUserInput();
                Assert.AreEqual(input[indexOfCommand], result);
            }

            mockedReader.Verify(r => r.ReadLine(), Times.Exactly(input.Count));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "No such argument")]
        public void TestToThrowExceptonWhenInvalidArgumentIsPassed()
        {
            IUserInterface interf = new ConsoleInterface();
            dynamic result = interf.GetArgumentValue("InvlaidArgument");
        }

        [TestMethod]
        public void TestToGetArgumnetValueReturnCorectValue()
        {
            var mockedReader = new Mock<HelperReader>();
            Console.SetIn(mockedReader.Object);

            IList<string> styleCommands =
                new List<string>() { "style=dotted", "style=solid", "style=fat", "style=double" };

            int indexOfCommands = 0;
            mockedReader.Setup(r => r.ReadLine()).Returns(() => styleCommands[indexOfCommands]);

            IUserInterface interf = new ConsoleInterface();
            dynamic result;
            bool actual;

            for (; indexOfCommands < styleCommands.Count; indexOfCommands++)
            {
                interf.GetCommandFromInput();
                result = interf.GetArgumentValue(GlobalConstants.GridBorderStyle);
                actual = (result as string == styleCommands[indexOfCommands].Replace("style=", string.Empty));
                Assert.IsTrue(actual);
            }

            mockedReader.Verify(r => r.ReadLine(), Times.Exactly(styleCommands.Count));
        }

        [TestMethod]
        public void TestGetArgumentToReturnCorrectNumberToMove()
        {
            var mockedReader = new Mock<HelperReader>();
            Console.SetIn(mockedReader.Object);

            IList<int> moveCommands = new List<int>() { 1, 2, 2, 3, 4, 58, 1001, 60123, 1231, 123, 12, 31, 312, 3131312, 123, 1 };

            IUserInterface interf = new ConsoleInterface();
            dynamic result;

            int indexOfCommand = 0;
            mockedReader.Setup(r => r.ReadLine()).Returns(() => moveCommands[indexOfCommand].ToString());

            bool actual;
            for (; indexOfCommand < moveCommands.Count; indexOfCommand++)
            {
                interf.GetCommandFromInput();
                result = interf.GetArgumentValue(GlobalConstants.DestinationTileValue);
                actual = (result as int? == moveCommands[indexOfCommand]);
                Assert.IsTrue(actual);
            }

            mockedReader.Verify(r => r.ReadLine(), Times.Exactly(moveCommands.Count));
        }

        [TestMethod]
        public void TestGetCommandFromInputToReturnInvalidCommand()
        {
            var mockedReader = new Mock<TextReader>();
            Console.SetIn(mockedReader.Object);

            IList<string> undefindedCommands =
                new List<string>() { "Undefined!", "Fake", "No Hack", "CHeat", "beer" };

            IUserInterface userInterface = new ConsoleInterface();

            int indexOfCommand = 0;
            mockedReader.Setup(r => r.ReadLine()).Returns(() => undefindedCommands[indexOfCommand]);

            for (; indexOfCommand < undefindedCommands.Count; indexOfCommand++)
            {
                Assert.AreEqual(Command.Invalid, userInterface.GetCommandFromInput());
            }

            mockedReader.Verify(r => r.ReadLine(), Times.Exactly(undefindedCommands.Count));
        }

        [TestMethod]
        public void TestGetCommandFromInputToReturnInvalidCommandWhenInvalidStyleCommandIsWrong()
        {
            var mockedReader = new Mock<HelperReader>();
            IList<String> invalidStyleCommands =
                new List<string>() { "style=Sod=sd", " styles", "style solid", "style=set=solid", "set style=solid", "solidStyle" };

            IUserInterface userInterface = new ConsoleInterface();
            Console.SetIn(mockedReader.Object);

            int indexOfCommand = 0;
            mockedReader.Setup(r => r.ReadLine()).Returns(() => invalidStyleCommands[indexOfCommand]);

            for (; indexOfCommand < invalidStyleCommands.Count; indexOfCommand++)
            {
                Assert.AreEqual(Command.Invalid, userInterface.GetCommandFromInput());
            }

            mockedReader.Verify(r => r.ReadLine(), Times.Exactly(invalidStyleCommands.Count));
        }

        [TestMethod]
        public void TestGetCommandFromInputToReturnCorrectCommands()
        {
            var mock = new Mock<HelperReader>();
            Console.SetIn(mock.Object);
            IList<string> commandstoPr =
                 new List<string>() { "start", "how", "top", "save", "load", "restart", "exit", "menu", "solve", "restart", "exit" };

            int index = 0;
            mock.Setup(r => r.ReadLine()).Returns(() => commandstoPr[index]);

            IUserInterface userInterface = new ConsoleInterface();

            string correctCommand;
            for (; index < commandstoPr.Count; index++)
            {
                correctCommand = (commandstoPr[index][0].ToString().ToUpper() + commandstoPr[index].Substring(1));
                Command command = (Command)Enum.Parse(typeof(Command), correctCommand);
                Assert.AreEqual(command, userInterface.GetCommandFromInput());
            }

            mock.Verify(r => r.ReadLine(), Times.Exactly(commandstoPr.Count));
        }
    }
}
