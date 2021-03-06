﻿// <copyright file="StyleTests.cs" company="Telerik Academy">All rights reserved.</copyright>
// <author>Team GameFifteen-1</author>
namespace GameFifteen.GridTests
{
    using System;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using GameFifteen.Common;
    using GameFifteen.Console;
    using GameFifteen.Console.Contracts;
    using GameFifteen.Console.Styles;
    using GameFifteen.Models.Contracts;
    using GameFifteen.Models;

    [TestClass]
    public class StyleTests
    {
        private readonly GridBorderStyle[] styles = new GridBorderStyle[] 
           { 
                new DottedStyle(), new DoubleStyle(), new FatStyle(), 
                new MiddleFatStyle(), new SolidStyle(), new AsteriskStyle()
           };

        private readonly BorderStyleType[] styleTypes = new BorderStyleType[] 
        { 
            BorderStyleType.Dotted, BorderStyleType.Double, BorderStyleType.Fat, 
            BorderStyleType.Middlefat,  BorderStyleType.Solid, BorderStyleType.Asterisk
        };

        private GridBorderStyle gridStyle;

        [TestMethod]
        public void TestSolidStyle()
        {
            this.gridStyle = new SolidStyle();
            string allBorders = this.gridStyle.Bottom + this.gridStyle.Top + this.gridStyle.Left + this.gridStyle.Right;
            bool result = allBorders.All(ch => "─│┌┐└┘ ".Contains(ch));
            Assert.AreEqual(true, result);
            Assert.AreEqual(BorderStyleType.Solid, this.gridStyle.Type);
        }

        [TestMethod]
        public void TestAsteriskStyle()
        {
            this.gridStyle = new AsteriskStyle();
            string allBorders = this.gridStyle.Bottom + this.gridStyle.Top + this.gridStyle.Left + this.gridStyle.Right;
            bool result = allBorders.All(ch => ch == '*' || ch == ' ');
            Assert.AreEqual(true, result);
            Assert.AreEqual(BorderStyleType.Asterisk, this.gridStyle.Type);
        }

        [TestMethod]
        public void TestDottedStyle()
        {
            this.gridStyle = new DottedStyle();
            string allBorders = this.gridStyle.Bottom + this.gridStyle.Top + this.gridStyle.Left + this.gridStyle.Right;
            bool result = allBorders.All(ch => ch == '·' || ch == ' ');
            Assert.AreEqual(true, result);
            Assert.AreEqual(BorderStyleType.Dotted, this.gridStyle.Type);
        }

        [TestMethod]
        public void TestDoubleStyle()
        {
            this.gridStyle = new DoubleStyle();
            string allBorders = this.gridStyle.Bottom + this.gridStyle.Top + this.gridStyle.Left + this.gridStyle.Right;
            bool result = allBorders.All(ch => "║═╔╗╚╝ ".Contains(ch));
            Assert.AreEqual(true, result);
            Assert.AreEqual(BorderStyleType.Double, this.gridStyle.Type);
        }

        [TestMethod]
        public void TestFatStyle()
        {
            this.gridStyle = new FatStyle();
            string allBorders = this.gridStyle.Bottom + this.gridStyle.Top + this.gridStyle.Left + this.gridStyle.Right;
            bool result = allBorders.All(ch => ch == '█' || ch == ' ');
            Assert.AreEqual(true, result);
            Assert.AreEqual(BorderStyleType.Fat, this.gridStyle.Type);
        }

        [TestMethod]
        public void TestMiddleFatStyle()
        {
            this.gridStyle = new MiddleFatStyle();
            string allBorders = this.gridStyle.Bottom + this.gridStyle.Top + this.gridStyle.Left + this.gridStyle.Right;
            bool result = allBorders.All(ch => "▄▀▐▌ ".Contains(ch));
            Assert.AreEqual(true, result);
            Assert.AreEqual(BorderStyleType.Middlefat, this.gridStyle.Type);
        }

        [TestMethod]
        public void TestStyleHorizontalBordersLength()
        {
            int minLength = GlobalConstants.GridSize * 3;
            int maxLength = GlobalConstants.GridSize * 4;
            bool result;
            for (int i = 0; i < this.styles.Length; i++)
            {
                result = minLength <= this.styles[i].Bottom.Length && this.styles[i].Bottom.Length <= maxLength;
                Assert.AreEqual(true, result);
                result = minLength <= this.styles[i].Top.Length && this.styles[i].Top.Length <= maxLength;
                Assert.AreEqual(true, result);
            }
        }

        [TestMethod]
        public void TestStyleVerticalBordersLengthMustContainsOnlySymbolAndSpace()
        {
            int borderWidth = 2;
            bool result;
            for (int i = 0; i < this.styles.Length; i++)
            {
                result = this.styles[i].Left.Length == borderWidth;
                Assert.AreEqual(true, result);
                result = this.styles[i].Right.Length == borderWidth;
                Assert.AreEqual(true, result);
            }
        }

        [TestMethod]
        public void TestBorderStyleFactory()
        {
            IStyleFactory styleFactory = new BorderStyleFactory();
            IStyle gridBorderStyle;

            for (int i = 0; i < this.styleTypes.Length; i++)
            {
                gridBorderStyle = styleFactory.Get(this.styleTypes[i]);
                Assert.AreEqual(this.styles[i].Type, gridBorderStyle.Type);
            }
        }

        [TestMethod]
        public void TestBorderStyleFactoryGetMethodWithNumbers()
        {
            IStyleFactory styleFactory = new BorderStyleFactory();
            IStyle gridBorderStyle;

            for (int i = 0; i < this.styleTypes.Length; i++)
            {
                gridBorderStyle = styleFactory.Get((BorderStyleType)i);
                Assert.AreEqual(this.styles[i].Type, gridBorderStyle.Type);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "There's no border of that type.")]
        public void TestBorderStyleFactoryWithInvalidParmaeter()
        {
            IStyleFactory styleFactory = new BorderStyleFactory();
            IStyle gridBorderStyle = styleFactory.Get(ConsoleKey.A);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "There's no border of that type.")]
        public void TestBorderStyleFactoryWithInvalidNumber()
        {
            IStyleFactory styleFactory = new BorderStyleFactory();
            IStyle gridBorderStyle = styleFactory.Get((BorderStyleType)213);
        }

        [TestMethod]
        public void TestGridWithBorderToReturnCorrectResult()
        {
            IGameMember grid = new Grid();
            GridWithBorder gridBorder;
            for (int i = 0; i < this.styles.Length; i++)
            {
                var currentBorderStyleForTesting = this.styles[i];
                gridBorder = new GridWithBorder(grid, currentBorderStyleForTesting);
                string result = gridBorder.GetTextRepresentation();
                Assert.AreEqual(true, result.Contains(currentBorderStyleForTesting.Top) &&
                                result.Contains(currentBorderStyleForTesting.Bottom) &&
                                result.Contains(currentBorderStyleForTesting.Right) &&
                                result.Contains(currentBorderStyleForTesting.Left));
            }
        }
    }
}
