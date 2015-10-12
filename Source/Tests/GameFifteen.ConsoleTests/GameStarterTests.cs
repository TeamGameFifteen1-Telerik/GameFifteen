// <copyright file="GameStarterTests.cs" company="Telerik Academy">All rights reserved.</copyright>
// <author>Team GameFifteen-1</author>
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
            Assert.AreEqual(firstInstance, secondInstance);
        }
    }
}
