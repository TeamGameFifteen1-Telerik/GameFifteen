namespace GameFifteen.GridTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using GameFifteen.Models;
    using GameFifteen.Models.Contracts;
    [TestClass]
    public class ScoreBoardTests
    {
        [TestMethod]
        public void TestingScoreBoardImplementationToReturnOnlyOneInstance()
        {
            var expected = Scoreboard.Instance.GetHashCode();
            var actual = Scoreboard.Instance.GetHashCode();
            Assert.AreEqual(expected, actual);
        }
        
        [TestMethod]
        public void TestingScoreBoardDefaultFunctionality()
        {
            var actual = Scoreboard.Instance.TopPlayers.Count;
            var expected = 0;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestingScoreBoardAddingPlayerFunctionality()
        {
            var score = Scoreboard.Instance;
            score.Clear();
            IPlayer player = new Player();
            var anotherPlayer = new Player();
            player.Moves = 8;
            anotherPlayer.Moves = 10;
            score.AddPlayer(player);
            score.AddPlayer(anotherPlayer);
            var actual = score.TopPlayers.Count;
            var expected = 2;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddPlayerShouldThrowWhenPlayerMovesAreZero()
        {
            var score = Scoreboard.Instance;
            score.Clear();
            var player = new Player();
            score.AddPlayer(player);
        }

        [TestMethod]
        public void TestScoreBoardShouldReturnTopPlayersInTheRightOrder()
        {
            var score = Scoreboard.Instance;
            score.Clear();
            for (int i = 0; i < 5; i++)
            {
                var player = new Player();
                player.Moves = 5 - i;
                player.Name = "Player" + (i + 1);
                score.AddPlayer(player);
            }

            var topPlayers = score.TopPlayers;
            Assert.AreEqual("Player5", topPlayers[0].Name);
            Assert.AreEqual("Player4", topPlayers[1].Name); 
        }

        [TestMethod]
        public void TestClearShouldReturnEmptyScoreBoard()
        {
            var score = Scoreboard.Instance;
            for (int i = 0; i < 5; i++)
            {
                var player = new Player();
                player.Moves = 5 - i;
                player.Name = "Player" + (i + 1);
                score.AddPlayer(player);
            }

            score.Clear();
            var expected = 0;
            var actual = score.TopPlayers.Count;

            Assert.AreEqual(expected, actual);
        }
    }
}
