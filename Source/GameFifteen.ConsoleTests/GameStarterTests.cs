namespace GameFifteen.LogicTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using GameFifteen.Console;

    /// <summary>
    /// Class tests GameStarter functionalities.
    /// </summary>
    [TestClass]
    public class GameStarterTests
    {
        /// <summary>
        /// Method tests if GameStarter instance is unique by the hash-code of instances.
        /// </summary>
        [TestMethod]
        public void TestGameStarterInstanceToBeUnique()
        {
            var firstInstance = GameFifteenStarter.Instance;
            var secondInstance = GameFifteenStarter.Instance;
            var expected = firstInstance.GetHashCode();
            var actual = secondInstance.GetHashCode();
            Assert.AreEqual(expected, actual);
        }
    }
}
