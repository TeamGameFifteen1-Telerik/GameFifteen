namespace GameFifteen.ConsoleTests
{
    using System;
    using System.IO;
    using GameFifteen.Console;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

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
    }
}
