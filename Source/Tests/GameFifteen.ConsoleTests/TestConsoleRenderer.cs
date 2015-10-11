// <copyright file="TestConsoleRenderer.cs" company="Telerik Academy">All rights reserved.</copyright>
// <author>Team GameFifteen-1</author>
namespace GameFifteen.ConsoleTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    using GameFifteen.Console;
    using GameFifteen.Console.Styles;
    using GameFifteen.Common;
    using GameFifteen.Models;
    using GameFifteen.Models.Contracts;

    [TestClass]
    public class TestConsoleRenderer
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "You need to add at least one style.")]
        public void TestAddStyleToThrowWhenNoStylesPassed()
        {
            ConsoleRenderer rende = new ConsoleRenderer(new BorderStyleFactory());
            rende.AddStyle();
        }

        [TestMethod]
        public void TestAddStyleMethodPassedManyStylesExpectToNotThrowAnException()
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
            var mockedWriter = new Mock<HelperWriter>();
            Console.SetOut(mockedWriter.Object);

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

            ConsoleRenderer rende = new ConsoleRenderer(new BorderStyleFactory());
            messageToProcess.ForEach(msg => rende.RenderMessage(msg));

            mockedWriter.Verify(w => w.Write(It.Is<string>(str => messageToProcess.Contains(str))), Times.Exactly(messageToProcess.Count));
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

        [TestMethod]
        public void TestRenderScoreBoardWhenNoPlayersInScoreBoard()
        {
            var mockedWrited = new Mock<HelperWriter>();
            Console.SetOut(mockedWrited.Object);

            ConsoleRenderer rende = new ConsoleRenderer(new BorderStyleFactory());
            Scoreboard scoreBoard = Scoreboard.Instance;

            rende.RenderScoreboard(scoreBoard);

            mockedWrited.Verify(w => w.Write(It.Is<string>(str => str == "No top players yet.")), Times.AtLeastOnce);
        }

        [TestMethod]
        public void TestRenderScoreboardToPrintCorrectPlayers()
        {
            var mockedWrited = new Mock<HelperWriter>();
            Console.SetOut(mockedWrited.Object);

            ConsoleRenderer rende = new ConsoleRenderer(new BorderStyleFactory());
            Scoreboard scoreBoard = Scoreboard.Instance;

            IDictionary<int, string> playerNamesWithMoves = new SortedDictionary<int, string>() 
            {
                {202, "john"},
                {444,  "largeBox"},
                {391, "gamer"},
                {20, "hacker"},
                {1023, "infinity"}
            };

            IPlayer player;

            foreach (var item in playerNamesWithMoves.Reverse())
            {
                player = new Player(item.Value);
                player.Moves = item.Key;
                scoreBoard.AddPlayer(player);
            }

            rende.RenderScoreboard(scoreBoard);

            mockedWrited.Verify(w => w.Write(It.Is<string>(str => str.Contains("->"))), Times.Exactly(scoreBoard.TopPlayers.Count));
            mockedWrited.Verify(w => w.Write(It.Is<string>(str => 
                playerNamesWithMoves.Values.Any(name => str.Contains(name)) ||
                playerNamesWithMoves.Keys.Any(moves => int.TryParse(str, out moves)))),
                Times.AtLeast(scoreBoard.TopPlayers.Count));
        }

        [TestMethod]
        public void TestRenderPlayScreenToWorkCorrectly()
        {
            var mockedWrited = new Mock<HelperWriter>();
            Console.SetOut(mockedWrited.Object);

            ConsoleRenderer rende = new ConsoleRenderer(new BorderStyleFactory());
            Scoreboard scoreBoard = Scoreboard.Instance;
            Grid grid = new Grid();
            int timesToBePrinted = GlobalConstants.GridSize * GlobalConstants.GridSize;
            for (int i = 0; i < timesToBePrinted - 1; i++)
            {
                grid.AddTile(new Tile((i + 1).ToString(), i, TileType.Number));
            }

            grid.AddTile(new Tile(" ", 15, TileType.Empty));

            rende.RenderPlayScreen(grid);

            mockedWrited.Verify(w => w.Write(It.IsAny<string>()), Times.AtLeast(timesToBePrinted / GlobalConstants.GridSize));
        }
    }
}
