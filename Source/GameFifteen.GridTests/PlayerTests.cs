namespace GameFifteen.GridTests
{
    using System;
    using GameFifteen.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PlayerTests
    {
        [TestMethod]
        public void TestPlayerConstructorIfReturnsValidNameState()
        {
            var player = new Player();
            var actual = player.Name;
            var expected = "Guest";
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void TestPlayerConstructorIfReturnValidMovesState()
        {
            var player = new Player();
            var actual = player.Moves;
            var expected = 0;
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void TestPlayerConstructorIfInputIsProvidedReturnsValidNameState()
        {
            var player = new Player("Pesho");
            var actual = player.Name;
            var expected = "Pesho";
            Assert.AreEqual(actual, expected);
        }
    }
}
