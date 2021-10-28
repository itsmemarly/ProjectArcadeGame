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
        private Canvas tileCanvas;
        private int tileX;
        private int tileY;
        private BitmapImage tileBitmap;
        private Image tileImage;
        // Mogelijk tileImage (XML as property van de Tile)

        public Tile(string type, int size, int x, int y, Canvas canvas, BitmapImage bitmap) 
        {
            // Coordinates of new Tile
            tileX = x;
            tileY = y;

            // Canvas of which new Tile is part
            tileCanvas = canvas;

            // Physical properties of new Tile
            tileType = type;
            tileSize = size;

            // Visual properties of new Tile
            tileImage = new Image
            {
                Tag = tileType,
                Height = tileSize,
                Width = tileSize,
                Source = bitmap
            };

            Canvas.SetLeft(tileImage, x * tileSize);
            Canvas.SetTop(tileImage, y * tileSize);

            // Manual implementation of new Tile
            tileCanvas.Children.Add(tileImage);
        }

       public void Destroy()
       {
            tileCanvas.Children.Remove(tileImage);
            
       }
    }
}
