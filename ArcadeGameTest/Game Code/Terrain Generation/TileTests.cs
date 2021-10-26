using Microsoft.VisualStudio.TestTools.UnitTesting;
using WPF_Arcade;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPF_Arcade.Tests
{
    [TestClass()]
    public class TileTests
    {
        Game game;
        Tile tile;

        public TileTests()
        {
            game = new Game();
            tile = new Tile("test", 64, 0, 0, game.Canvas(), GameImageBitmaps.stone);
        }
        
        [TestMethod()]
        public void TileTest()
        {
            Assert.IsNotNull(tile);
        }

        [TestMethod()]
        public void TypeTest()
        {
            Assert.Fail();
        }
    }
}