namespace GameFifteen.ModelTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using GameFifteen.Models;
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
            var tile = new Tile("1", 1);
            actual.AddTile(tile);
            Assert.AreEqual(actual.GetTileAtPosition(0),tile);
        }

        [TestMethod]
        public void TestAddTileMethodToRetturnAccurateValue()
        {
            var grid = new Grid();
            for (int i = 0; i < 5; i++)
            {
                var tile = new Tile(""+ i +"",i);
                grid.AddTile(tile);
            }
            var actual = grid.GetTilesCount();
            var expected = 5;
            Assert.AreEqual(actual,expected);

        }

        [TestMethod]
        public void TestClearMethodToRetturnZeroValue()
        {
            var grid = new Grid();
            var tile = new Tile("1",1);
            grid.AddTile(tile);
            grid.Clear();
            var actual = grid.GetTilesCount();
            var expected = 0;
            Assert.AreEqual(actual, expected);
        }
        


    }
}
