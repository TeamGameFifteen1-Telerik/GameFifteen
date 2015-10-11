// <copyright file="GridTests.cs" company="Telerik Academy">All rights reserved.</copyright>
// <author>Team GameFifteen-1</author>
namespace GameFifteen.GridTests
{
    using System;
    using GameFifteen.Common;
    using GameFifteen.Models;
    using GameFifteen.Models.Contracts;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    /// <summary>
    /// Class tests if public functionalities of Grid class are accurate.
    /// </summary>
    [TestClass]
    public class GridTests
    {
        /// <summary>
        /// Method tests if the AddTile() method is invoked successfully.
        /// </summary>
        [TestMethod]
        public void TestGridAddTileMethodToBeInvokedAtLeastOnce()
        {
            var fakeGrid = new Mock<IGrid>();
            fakeGrid.Setup(g => g.AddTile(new Tile()));
            fakeGrid.Object.AddTile(new Tile());
            fakeGrid.Object.AddTile(new Tile());
            fakeGrid.Verify(g => g.AddTile(It.IsAny<Tile>()), Times.AtLeast(2));
        }

        /// <summary>
        /// Method tests if the AddTile() correctly adds tiles to the grid.
        /// </summary>
        [TestMethod]
        public void TestWheatherTilesAreAddedCorrectly()
        {
            var fakeGrid = new Mock<IGrid>();
            var tile = new Tile("1", 1, TileType.Number);
            fakeGrid.Setup(g => g.AddTile(tile));
            fakeGrid.Object.AddTile(tile);
            fakeGrid.Verify(g => g.AddTile(tile), Times.AtLeastOnce());
            fakeGrid.Setup(g => g.TilesCount).Returns(1);
            Assert.AreEqual(1, fakeGrid.Object.TilesCount);
        }

        /// <summary>
        /// Method tests if the tile at the current position is with accurate property value.
        /// </summary>
        [TestMethod]
        public void TestWheatherTileAtPositionMethodReturnsCorrectValues()
        {
            var fakeGrid = new Mock<IGrid>();
            var tile = new Tile("1", 1, TileType.Number);
            fakeGrid.Setup(g => g.GetTileAtPosition(1)).Returns(new Tile("1", 1, TileType.Number));
            var exp = fakeGrid.Object.GetTileAtPosition(1);
            Assert.AreEqual(tile.Position, exp.Position);
        }

        /// <summary>
        /// Method tests if the Grid.IsSorted property has a default value of "true".
        /// </summary>
        [TestMethod]
        public void TestGridConstructorIfReturnsSortedGridOnStartup()
        {
            var actual = new Mock<IGrid>();
            var expected = true;
            actual.SetupGet(g => g.IsSorted).Returns(true);
            Assert.AreEqual(actual.Object.IsSorted, expected);
        }

        /// <summary>
        /// Method tests GetTileAtPosition method with mocking framework.
        /// </summary>
        [TestMethod]
        public void TestGridAddingTileWheatherReturnsCorrectValues()
        {
            var actual = new Mock<IGrid>();
            var mockedTile = new Mock<Tile>("1", 1, TileType.Number);
            actual.Setup(g => g.GetTileAtPosition(0)).Returns(mockedTile.Object);
            Assert.AreEqual(1, mockedTile.Object.Position);
        }

        /// <summary>
        /// Method tests if the Grid.AddTile() method returns accurate value.
        /// </summary>
        [TestMethod]
        public void TestAddTileMethodToRetturnAccurateValue()
        {
            var grid = new Grid();
            for (int i = 0; i < 5; i++)
            {
                var tile = new Tile(string.Empty + i + string.Empty, i, TileType.Number);
                grid.AddTile(tile);
            }

            var actual = grid.TilesCount;
            var expected = 5;
            Assert.AreEqual(actual, expected);
        }

        /// <summary>
        /// Method tests if Grid.Clear() method clears the collection of tiles in the grid.
        /// </summary>
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

        /// <summary>
        /// Method tests if Memento.SaveMemento() method returns accurate state of grid view.
        /// </summary>
        [TestMethod]
        public void TestMementoToReturnValidObjectState()
        {
            var grid = new Grid();
            var tile = new Tile("1", 1, TileType.Number);
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

        /// <summary>
        /// Method tests if the Grid.GetTileFromLabel() returns correct values.
        /// </summary>
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

        /// <summary>
        /// Method tests if the Grid.GetTileFromLabel() returns "null" when no data is provided.
        /// </summary>
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

        /// <summary>
        /// Method tests if the Grid.GetTileAtPosition() returns correct value of tile position.
        /// </summary>
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

        /// <summary>
        /// Method tests if the Grid.GetTileAtPosition() throws an ArgumentException when no valid tile position is provided.
        /// </summary>
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

        /// <summary>
        /// Method tests if the Grid.CanSwap() method returns "false" when current tile cannot be swapped.
        /// </summary>
        [TestMethod]
        public void TestCanSwapMethodToReturnFalseValueWhenCurrentTileCannotBeSwapped()
        {
            var grid = new Grid();
            for (int i = 0; i < 5; i++)
            {
                var tile = new Tile(string.Empty + i + string.Empty, i, TileType.Number);
                grid.AddTile(tile);
            }

            var emptyTile = new Tile(string.Empty, GlobalConstants.TotalTilesCount - 1, TileType.Empty);
            grid.AddTile(emptyTile);
            var tileToTest = grid.GetTileFromLabel("1");
            var actual = grid.CanSwap(tileToTest);
            var expected = false;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestCanSwapMethodToReturnFalseValueWhenCurrentTileCanBeSwapped()
        {
            var grid = new Grid();
            for (int i = 0; i < 5; i++)
            {
                var tile = new Tile(i.ToString(), i, TileType.Number);
                grid.AddTile(tile);
            }

            var emptyTile = new Tile(string.Empty, 5, TileType.Empty);
            grid.AddTile(emptyTile);
            var tileToTest = grid.GetTileFromLabel("4");
            var actual = grid.CanSwap(tileToTest);
            var expected = true;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestCheckIfSortedWhenGridIsSorted()
        {
            var grid = new Grid();
            var emptyTile = new Tile(string.Empty, GlobalConstants.TotalTilesCount - 1, TileType.Empty);

            for (int i = 0; i < GlobalConstants.TotalTilesCount - 1; i++)
            {
                Tile tile = emptyTile.CloneMemberwise();
                tile.Label = (i + 1).ToString();
                tile.Position = i;
                tile.Type = TileType.Number;
                grid.AddTile(tile);
            }

            grid.AddTile(emptyTile);

            bool actual = grid.IsSorted;
            var expected = true;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestCheckIfSortedWhenGridIsNotSorted()
        {
            var grid = new Grid();
            var emptyTile = new Tile(string.Empty, 0, TileType.Empty);
            grid.AddTile(emptyTile);

            for (int i = GlobalConstants.TotalTilesCount - 1; i > 0; i--)
            {
                Tile tile = emptyTile.CloneMemberwise();
                tile.Label = (i + 1).ToString();
                tile.Position = i;
                tile.Type = TileType.Number;
                grid.AddTile(tile);
            }

            bool actual = grid.IsSorted;
            var expected = false;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestSwapTiles()
        {
            var grid = new Grid();

            var emptyTile = new Tile(string.Empty, 0, TileType.Empty);
            grid.AddTile(emptyTile);

            Tile tile = emptyTile.CloneMemberwise();
            tile.Label = "1";
            tile.Position = 1;
            tile.Type = TileType.Number;
            grid.AddTile(tile);

            grid.SwapTiles(tile);

            var actual = tile.Position;
            var expected = 0;
 
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestGetEnumerator()
        {
            var grid = new Grid();

            var emptyTile = new Tile(string.Empty, 0, TileType.Empty);
            grid.AddTile(emptyTile);

            Tile tile = emptyTile.CloneMemberwise();
            tile.Label = "1";
            tile.Position = 1;
            tile.Type = TileType.Number;
            grid.AddTile(tile);

            foreach (Tile item in grid)
            {
                item.Label = "test";
            }

            var actual = emptyTile.Label;
            var expected = "test";

            Assert.AreEqual(expected, actual);
        }
    }
}
