namespace GameFifteen.GridTests
{
    using System;
    using GameFifteen.Models;
    using GameFifteen.Models.Contracts;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Class tests ScoreBoard class functionalities.
    /// </summary>
    [TestClass]
    public class ScoreBoardTests
    {
        /// <summary>
        /// Method tests if a second single instance can be instantiated.
        /// </summary>
        [TestMethod]
        public void TestingScoreBoardImplementationToReturnOnlyOneInstance()
        {
            var firstInstance = Scoreboard.Instance;
            var secondInstance = Scoreboard.Instance;
            var actual = firstInstance.GetHashCode();
            var expected = secondInstance.GetHashCode();
            Assert.AreEqual(expected, actual);
        }
        
        /// <summary>
        /// Method tests if the default value for the Scoreboard.TopPlayers property is zero.
        /// </summary>
        [TestMethod]
        public void TestingScoreBoardDefaultFunctionality()
        {
            var actual = Scoreboard.Instance.TopPlayers.Count;
            var expected = 0;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Method tests if Scoreboard.AddPlayer() method successfully adds a player.
        /// </summary>
        [TestMethod]
        public void TestingScoreBoardAddingPlayerFunctionality()
        {
            var score = Scoreboard.Instance;
            score.Clear();
            IPlayer player = new Player();
            IPlayer anotherPlayer = new Player();
            player.Moves = 8;
            anotherPlayer.Moves = 10;
            score.AddPlayer(player);
            score.AddPlayer(anotherPlayer);
            var actual = score.TopPlayers.Count;
            var expected = 2;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Method tests if a player with no moves could be added. Expects ArgumentException.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddPlayerShouldThrowWhenPlayerMovesAreZero()
        {
            var score = Scoreboard.Instance;
            score.Clear();
            IPlayer player = new Player();
            score.AddPlayer(player);
        }

        /// <summary>
        /// Method tests if players added to the scoreboard are added in the correct order. 
        /// </summary>
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

        /// <summary>
        /// Method tests if Scoreboard.Clear() method clears the scoreboard successfully.
        /// </summary>
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
