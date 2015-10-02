namespace GameFifteen.ModelTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using GameFifteen.Models;
    using GameFifteen.Common;
    [TestClass]
    public class GridTests
    {
        [TestMethod]
        public void TestGridConstructorIfReturnsSortedGridOnStartup()
        {
            var actual = new Grid();
            var expected = true;
            Assert.AreEqual(actual.IsSorted, expected);
        }

        [TestMethod]
        public void TestGridAddingTileWheatherReturnsCorrectValues()
        {
            var actual = new Grid();
            var tile = new Tile("1", 1, TileType.Number);
            actual.AddTile(tile);
            Assert.AreEqual(actual.GetTileAtPosition(0), tile);
        }

        [TestMethod]
        public void TestAddTileMethodToRetturnAccurateValue()
        {
            var grid = new Grid();
            for (int i = 0; i < 5; i++)
            {
                var tile = new Tile("" + i + "", i, TileType.Number);
                grid.AddTile(tile);
            }
            var actual = grid.TilesCount;
            var expected = 5;
            Assert.AreEqual(actual, expected);

        }

        [TestMethod]
        public void TestClearMethodToRetturnZeroValue()
        {
            var grid = new Grid();
            var tile = new Tile("1", 1, TileType.Number);
            grid.AddTile(tile);
            grid.Clear();
            var actual = grid.TilesCount;
            var expected = 0;
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void TestMementoToReturnValidObjectState()
        {
            var grid = new Grid();
            var tile = new Tile("1",1,TileType.Number);
            var emptyTile = new Tile(string.Empty, GlobalConstants.TotalTilesCount - 1, TileType.Empty);
            var anotherTile = new Tile("2", 2, TileType.Number);
            grid.AddTile(tile);
            grid.AddTile(emptyTile);
            Memento memento = grid.SaveMemento();

            grid.AddTile(anotherTile);
            grid.RestoreMemento(memento);

            var actual = grid.TilesCount;
            var expected = 2;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestGetTileFromLabelMethodToReturnValidTile()
        {
            var grid = new Grid();
            var tile = new Tile("1", 1, TileType.Number);
            var anotherTile = new Tile("2", 2, TileType.Number);
            grid.AddTile(tile);
            grid.AddTile(anotherTile);
            var actual = grid.GetTileFromLabel("2");
            var expected = anotherTile;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestGetTileFromLabelMethodToReturnNullWhenNoValidDataIsProvided()
        {
            var grid = new Grid();
            var tile = new Tile("1", 1, TileType.Number);
            var anotherTile = new Tile("2", 2, TileType.Number);
            grid.AddTile(tile);
            grid.AddTile(anotherTile);
            var actual = grid.GetTileFromLabel("3");
            Assert.AreEqual(null, actual);
        }

        [TestMethod]
        public void TestGetTileAtPositionMethodToReturnValidPositionOfTile()
        {
            var grid = new Grid();
            var tile = new Tile("3", 3, TileType.Number);
            var anotherTile = new Tile("4", 4, TileType.Number);
            grid.AddTile(tile);
            grid.AddTile(anotherTile);
            var actual = grid.GetTileAtPosition(1);
            var expected = anotherTile;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestGetTileAtPositionMethodToThrowWhenNoValidPositionIsProvided()
        {
            var grid = new Grid();
            var tile = new Tile("3", 3, TileType.Number);
            var anotherTile = new Tile("4", 4, TileType.Number);
            grid.AddTile(tile);
            grid.AddTile(anotherTile);
            var actual = grid.GetTileAtPosition(2);
            var expected = anotherTile;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestCanSwapMethodToReturnFalseValueWhenCurrentTileCannotBeSwapped()
        {
            var grid = new Grid();
            for (int i = 0; i < 5; i++)
            {
                var tile = new Tile("" + i + "", i, TileType.Number);
                grid.AddTile(tile);
            }
            var emptyTile = new Tile(string.Empty, GlobalConstants.TotalTilesCount - 1, TileType.Empty);
            grid.AddTile(emptyTile);
            var tileToTest = grid.GetTileFromLabel("1");
            var actual = grid.CanSwap(tileToTest);
            var expected = false;
            Assert.AreEqual(expected, actual);
        }
    }
}
