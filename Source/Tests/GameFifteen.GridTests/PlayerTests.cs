// <copyright file="PlayerTests.cs" company="Telerik Academy">All rights reserved.</copyright>
// <author>Team GameFifteen-1</author>
namespace GameFifteen.GridTests
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    using GameFifteen.Models;
    using GameFifteen.Models.Contracts;

    /// <summary>
    /// Class tests Player class functionalities.
    /// </summary>
    [TestClass]
    public class PlayerTests
    {
        /// <summary>
        /// Method tests if the Player constructor returns default("Guest") value for the Player.Name property.
        /// </summary>
        [TestMethod]
        public void TestPlayerConstructorIfReturnsValidNameState()
        {
            var player = new Player();
            var actual = player.Name;
            var expected = "Guest";
            Assert.AreEqual(actual, expected);
        }

        /// <summary>
        /// Method tests if the Player constructor returns default(0) value for the Player.Moves property.
        /// </summary>
        [TestMethod]
        public void TestPlayerConstructorIfReturnValidMovesState()
        {
            var player = new Player();
            var actual = player.Moves;
            var expected = 0;
            Assert.AreEqual(actual, expected);
        }

        /// <summary>
        /// Method tests if the Player constructor assigns correct value for the Player.Name property.
        /// </summary>
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
