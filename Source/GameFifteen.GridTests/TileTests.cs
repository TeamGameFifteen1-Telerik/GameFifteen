﻿namespace GameFifteen.GridTests
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using GameFifteen.Models;

    [TestClass]
    public class TileTests
    {
        [TestMethod]
        public void TestIfTileConstructorReturnsValidStateOfLabel()
        {
            var tile = new Tile("1", 1, TileType.Number);
            var actual = tile.Label;
            var expected = "1";
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void TestIfTileConstructorReturnsValidStateOfPosition()
        {
            var tile = new Tile("2", 2, TileType.Number);
            var actual = tile.Position;
            var expected = 2;
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void TestIfCompareToMethodReturnsNegativeValueWhenFirstTileIsBeforeSecond()
        {
            var firstTile = new Tile("1", 1, TileType.Number);
            var secondTile = this.CloneTile(firstTile, "2", 2);
            var actual = firstTile.CompareTo(secondTile);
            var expected = -1;
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void TestIfCompareToMethodReturnsPositiveValueWhenSecondTileIsBeforeFirst()
        {
            var firstTile = new Tile("5", 5, TileType.Number);
            var secondTile = this.CloneTile(firstTile,"3", 3);
            var actual = firstTile.CompareTo(secondTile);
            var expected = 1;
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void TestIfCompareToMethodReturnZeroWhenTiles()
        {
            var firstTile = new Tile("3", 3, TileType.Number);
            var secondTile = this.CloneTile(firstTile, "3", 3);
            var actual = firstTile.CompareTo(secondTile);
            var expected = 0;
            Assert.AreEqual(actual,expected);
        }

        private Tile CloneTile(Tile tile, string newLabel, int newPosition, TileType newType = TileType.Number)
        {
            var newTile = tile.CloneMemberwise();
            newTile.Label = newLabel;
            newTile.Position = newPosition;
            newTile.Type = newType;

            return newTile;
        }
    }
}
