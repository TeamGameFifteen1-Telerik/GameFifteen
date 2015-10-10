namespace GameFifteen.ConsoleTests
{
    using System;
    using System.IO;
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
        public void TestMethod2()
        {
            StreamReader reader = new StreamReader("..//..//testCommands.txt");
            Console.SetIn(reader);
            ConsoleInterface interf = new ConsoleInterface();
            string result = interf.GetUserInput();
            result = interf.GetUserInput();
            Assert.AreEqual("solve", result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "No such argument")]
        public void TestToThrowExceptonWhenInvalidArgumentIsPassed()
        {
            ConsoleInterface interf = new ConsoleInterface();
            dynamic result = interf.GetArgumentValue("InvlaidArgument");
        }

        [TestMethod]
        public void TestToGetArgumnetValueReturnCorectValue()
        {
            StreamReader reader = new StreamReader("../../styleCommands.txt");
            Console.SetIn(reader);

            ConsoleInterface interf = new ConsoleInterface();
            dynamic result;

            var styleCommands = reader.ReadToEnd().Split(new string[] { "\r\n", "style=" }, StringSplitOptions.RemoveEmptyEntries);

            //// reset reader to get input again
            reader.DiscardBufferedData();
            reader.BaseStream.Seek(0, SeekOrigin.Begin);
            reader.BaseStream.Position = 0;
            bool actual;

            for (int i = 0; i < styleCommands.Length; i++)
            {
                interf.GetCommandFromInput();
                result = interf.GetArgumentValue(GlobalConstants.GridBorderStyle);
                actual = (result as string == styleCommands[i]);
                Assert.IsTrue(actual);
            }
        }

        [TestMethod]
        public void TestGetArgumentToReturnCorrectNumberToMove()
        {
            StreamReader reader = new StreamReader("../../testCommands/Commandmoves.txt");
            Console.SetIn(reader);

            ConsoleInterface interf = new ConsoleInterface();
            dynamic result;

            var numberCommands = reader.ReadToEnd().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            reader.DiscardBufferedData();
            reader.BaseStream.Seek(0, SeekOrigin.Begin);
            reader.BaseStream.Position = 0;

            bool actual;

            for (int i = 0; i < numberCommands.Length; i++)
            {
                interf.GetCommandFromInput();
                result = interf.GetArgumentValue(GlobalConstants.DestinationTileValue);
                actual = (result as int? == int.Parse(numberCommands[i]));
                Assert.IsTrue(actual);
            }
        }

        [TestMethod]
        public void TestGetCommandFromInputToReturnInvalidCommand()
        {
            StreamReader reader = new StreamReader("../../testCommands/UndefinedTextCommand.txt");
            Console.SetIn(reader);

            ConsoleInterface userInterface = new ConsoleInterface();

            var invalidComomands = reader.ReadToEnd().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            reader.DiscardBufferedData();
            reader.BaseStream.Seek(0, SeekOrigin.Begin);
            reader.BaseStream.Position = 0;

            for (int i = 0; i < invalidComomands.Length; i++)
            {
                Assert.AreEqual(Command.Invalid, userInterface.GetCommandFromInput());
            }
        }

        [TestMethod]
        public void TestGetCommandFromInputToReturnInvalidCommandWhenInvalidStyleCommandIsWrong()
        {
            StreamReader reader = new StreamReader("../../testCommands/WrongStyleCommands.txt");
            Console.SetIn(reader);

            ConsoleInterface userInterface = new ConsoleInterface();

            var numberCommands = reader.ReadToEnd().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            reader.DiscardBufferedData();
            reader.BaseStream.Seek(0, SeekOrigin.Begin);
            reader.BaseStream.Position = 0;

            for (int i = 0; i < numberCommands.Length; i++)
            {
                Assert.AreEqual(Command.Invalid, userInterface.GetCommandFromInput());
            }
        }

        [TestMethod]
        public void TestGetCommandFromInputToReturnCorrectCommands()
        {
            StreamReader reader = new StreamReader("../../testCommands/CorrectCommands.txt");
            Console.SetIn(reader);

            ConsoleInterface userInterface = new ConsoleInterface();

            var numberCommands = reader.ReadToEnd().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            reader.DiscardBufferedData();
            reader.BaseStream.Seek(0, SeekOrigin.Begin);
            reader.BaseStream.Position = 0;

            
            string correctCommand;
            for (int i = 0; i < numberCommands.Length; i++)
            {
                correctCommand = (numberCommands[i][0].ToString().ToUpper() + numberCommands[i].Substring(1));
                Command command = (Command)Enum.Parse(typeof(Command), correctCommand);
                Assert.AreEqual(command, userInterface.GetCommandFromInput());
            }
        }

    }
}
