namespace GameFifteen.GridTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using GameFifteen.Models;
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
            var player = new Player();
            var anotherPlayer = new Player();
            score.AddPlayer(player);
            score.AddPlayer(anotherPlayer);
            var actual = score.TopPlayers.Count;
            var expected = 2;
            Assert.AreEqual(expected, actual);
        }
    }
}
