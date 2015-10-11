// <copyright file="TileTests.cs" company="Telerik Academy">All rights reserved.</copyright>
// <author>Team GameFifteen-1</author>
namespace GameFifteen.GridTests
{
    using System;
    using GameFifteen.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Class tests Tile class functionalities.
    /// </summary>
    [TestClass]
    public class TileTests
    {
        /// <summary>
        /// Method tests if Tile constructor assigns correct values of the Tile.Label property.
        /// </summary>
        [TestMethod]
        public void TestIfTileConstructorReturnsValidStateOfLabel()
        {
            var tile = new Tile("1", 1, TileType.Number);
            var actual = tile.Label;
            var expected = "1";
            Assert.AreEqual(actual, expected);
        }

        /// <summary>
        /// Method tests if Tile constructor returns valid state of Tile.Position property.
        /// </summary>
        [TestMethod]
        public void TestIfTileConstructorReturnsValidStateOfPosition()
        {
            var tile = new Tile("2", 2, TileType.Number);
            var actual = tile.Position;
            var expected = 2;
            Assert.AreEqual(actual, expected);
        }

        /// <summary>
        /// Method tests if the .CompareTo() method returns a integer value of "-1" when two tiles are provided and the first is before the second one.
        /// </summary>
        [TestMethod]
        public void TestIfCompareToMethodReturnsNegativeValueWhenFirstTileIsBeforeSecond()
        {
            var firstTile = new Tile("1", 1, TileType.Number);
            var secondTile = this.CloneTile(firstTile, "2", 2);
            var actual = firstTile.CompareTo(secondTile);
            var expected = -1;
            Assert.AreEqual(actual, expected);
        }

        /// <summary>
        /// Method tests if the .CompareTo() method returns a integer value of "1" when two tiles are provided and the first is after the second one.
        /// </summary>
        [TestMethod]
        public void TestIfCompareToMethodReturnsPositiveValueWhenSecondTileIsBeforeFirst()
        {
            var firstTile = new Tile("5", 5, TileType.Number);
            var secondTile = this.CloneTile(firstTile, "3", 3);
            var actual = firstTile.CompareTo(secondTile);
            var expected = 1;
            Assert.AreEqual(actual, expected);
        }

        /// <summary>
        /// Method tests if CompareTo() method returns an integer value of "0" when tiles are identical.
        /// </summary>
        [TestMethod]
        public void TestIfCompareToMethodReturnZeroWhenTilesHaveTheSameValues()
        {
            var firstTile = new Tile("3", 3, TileType.Number);
            var secondTile = this.CloneTile(firstTile, "3", 3);
            var actual = firstTile.CompareTo(secondTile);
            var expected = 0;
            Assert.AreEqual(actual, expected);
        }

        /// <summary>
        /// Helper method for tile comparison when two tiles are identical.
        /// </summary>
        /// <param name="tile">Tile to be cloned.</param>
        /// <param name="newLabel">String Value of the Label property for the new Tile.</param>
        /// <param name="newPosition">String Value of the Position property for the new Tile.</param>
        /// <param name="newType">Enumeration TileType specifies the type of the new cloned Tile.Default is TileType.Number.</param>
        /// <returns>New Tile with the specified input values.</returns>
        private Tile CloneTile(Tile tile, string newLabel, int newPosition, TileType newType = TileType.Number)
        {
            var newTile = tile.CloneMemberwise();
            newTile.Label = newLabel;
            newTile.Position = newPosition;
            newTile.Type = newType;

            return newTile;
        }

        [TestMethod]
        public void TestTileGetTextRepresentationOfEmptyTile()
        {
            var tile = new Tile(string.Empty, 0, TileType.Empty);

            var actual = tile.GetTextRepresentation();
            var expected = " ";
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void TestTileGetTextRepresentationOfNumberedTile()
        {
            var tile = new Tile("1", 0, TileType.Empty);

            var actual = tile.GetTextRepresentation();
            var expected = "1";
            Assert.AreEqual(actual, expected);
        }
    }
}
