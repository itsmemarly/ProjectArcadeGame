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

namespace WPF_Arcade
{
    public class Tile
    {
        private string tileType;
        private int tileSize;

        public Tile(string type, int size)
        {
            tileType = type;
            tileSize = size;
        }

       

        public void Create(int x, int y, Canvas canvas)
        {
            Rectangle tile = new Rectangle
            {
                Tag = "tile",
                Height = tileSize,
                Width = tileSize,
                Fill = Brushes.White,
                Stroke = Brushes.Red
            };

            Canvas.SetLeft(tile, x * tileSize);
            Canvas.SetTop(tile, y * tileSize);
            canvas.Children.Add(tile);
        }
    }
}
