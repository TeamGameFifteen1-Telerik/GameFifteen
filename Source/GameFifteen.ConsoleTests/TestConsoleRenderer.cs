namespace GameFifteen.ConsoleTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using GameFifteen.Console;
    using GameFifteen.Console.Styles;
    using GameFifteen.Console.Contracts;
    using GameFifteen.Common;
    using Moq;
    using GameFifteen.Logic.Contracts;
    using System.IO;

    [TestClass]
    public class TestConsoleRenderer
    {
        [TestMethod]
        public void TestThatDefaultStyleIsSolid()
        {
            ConsoleRenderer rende = new ConsoleRenderer(new BorderStyleFactory());
            IDictionary<string, IStyle> styles = rende.Styles;
            IStyle initialStyle = styles[GlobalConstants.GridBorderStyle];
            Assert.AreEqual(BorderStyleType.Solid, initialStyle.Type);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "You need to add at least one style.")]
        public void TestAddStyleToThrowWhenNoStylesPassed()
        {
            ConsoleRenderer rende = new ConsoleRenderer(new BorderStyleFactory());
            rende.AddStyle();
        }

        [TestMethod]
        public void TestAddStyleWithManyStyles()
        {
            ConsoleRenderer rende = new ConsoleRenderer(new BorderStyleFactory());
            string[] stylesToAdd = new string[] 
            {
                "solid",
                "dotted",
                "fat",
                "middlefat",
                "asterisk",
                "double"
            };

            rende.AddStyle(stylesToAdd);
            int counntOfStyles = rende.Styles.Count;
            Assert.AreEqual(1, counntOfStyles);
            string expected = stylesToAdd[stylesToAdd.Length - 1][0].ToString().ToUpper() + stylesToAdd[stylesToAdd.Length - 1].Substring(1);
            Assert.AreEqual(expected, rende.Styles[GlobalConstants.GridBorderStyle].Type.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "There's no border of that type.")]
        public void TestAddStyleToThrowwhenInvalidStyleIsPassed()
        {
            ConsoleRenderer rende = new ConsoleRenderer(new BorderStyleFactory());
            string[] stylesToAdd = new string[] 
            {
                "solid",
                "dotted",
                "pesho",
                "middlefat",
                "asterisk",
                "double"
            };

            rende.AddStyle(stylesToAdd);
        }

        [TestMethod]
        public void TestRenderMessageToProceedAllPassedText()
        {
            string pathToFile = "../../ResultsFromTests/RentererMessage.txt";


            List<string> messageToProcess = new List<string>()
            {
               GameMessages.Enter,
               GameMessages.EnterHow,
               GameMessages.EnterYourName,
               GameMessages.Goal,
               GameMessages.NewGameQuestion,
               GameMessages.Welcome,
               GameMessages.RestartGameQuestion
            };

            IRenderer rende = new ConsoleRenderer(new BorderStyleFactory());
            using (StreamWriter writer = new StreamWriter(pathToFile))
            {
                Console.SetOut(writer);
                messageToProcess.ForEach(msg => rende.RenderMessage(msg + "\r\n"));
            }

            StreamReader reader = new StreamReader(pathToFile);
            List<string> receivedMessages;
            using (reader)
            {
                receivedMessages = reader.ReadToEnd().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).Select(text => text.TrimStart()).Where(text => text != string.Empty).ToList();
            }

            Assert.AreEqual(messageToProcess.Count, receivedMessages.Count);
            for (int i = 0; i < receivedMessages.Count; i++)
            {
                Assert.AreEqual(messageToProcess[i], receivedMessages[i]);
            }
        }

        [TestMethod]
        public void TestRenderInitialScreenToPrintGameLogoAndMenuOptions()
        {
            ConsoleRenderer rende = new ConsoleRenderer(new BorderStyleFactory());
            var mockedWriter = new Mock<HelperWriter>();
            Console.SetOut(mockedWriter.Object);
            int result = 0;
            mockedWriter.Setup(w => w.Write(It.IsAny<string>())).Callback<string>(param => result++);

            rende.RenderInitialScreen();

            mockedWriter.Verify(w => w.Write(It.Is<string>(s => s.Contains(GameImages.GameLogo))), Times.AtLeastOnce());
            mockedWriter.Verify(w => w.Write(It.Is<string>(
                s => GameMessages.MenuOptions.Keys.Contains(s) ||
                     GameMessages.MenuOptions.Values.Contains(s))),
                Times.Exactly(GameMessages.MenuOptions.Count * 2));
            Assert.IsTrue(GameMessages.MenuOptions.Count * 2 <= result);
        }

        [TestMethod]
        public void TestRenderGameHowMenuOptionToBePrintedOnTheScreen()
        {
            List<string> listOfAllBullshits = new List<string>();
            GameMessages.CommandsDescription.Keys.ToList().ForEach(cmd => listOfAllBullshits.Add(cmd));
            GameMessages.CommandsDescription.Values.ToList().ForEach(cmd => listOfAllBullshits.Add(cmd));
            GameMessages.StyleCommandsDescription.Keys.ToList().ForEach(desc => listOfAllBullshits.Add(desc));
            GameMessages.StyleCommandsDescription.Values.ToList().ForEach(desc => listOfAllBullshits.Add(desc));
            listOfAllBullshits.Add(" -> ");

            var mockedWrited = new Mock<HelperWriter>();
            Console.SetOut(mockedWrited.Object);

            ConsoleRenderer rende = new ConsoleRenderer(new BorderStyleFactory());
            rende.RenderGameOptions();

            mockedWrited.Verify(w => w.Write(It.Is<string>(msg => listOfAllBullshits.Contains(msg))), Times.AtLeast(listOfAllBullshits.Count));
        }
    }
}
