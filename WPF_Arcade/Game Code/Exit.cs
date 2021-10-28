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
    public class Exit
    {
        private string exit;
        private int exitSize;
        private Canvas exitCanvas;
        private int exitX;
        private int exitY;
        private BitmapImage exitBitMap;
        private Image exitImage;


        public Exit(int x, int y, int size, Canvas canvas, BitmapImage bitmap)
        {
            exitX = x;
            exitY = y;
            exitBitMap = bitmap;
            exitCanvas = canvas;
            exitSize = size;

            exitImage = new Image
            {
                Tag = "exit",
                Height = exitSize,
                Width = exitSize,
                Source = exitBitMap
            };


            Canvas.SetLeft(exitImage, x * exitSize);
            Canvas.SetTop(exitImage, y * exitSize);

            exitCanvas.Children.Add(exitImage);
        }

        // Creates and opens the win screen
        public void EndGame()
        {
            You_won winscreen = new You_won();
            winscreen.Visibility = Visibility.Visible;
            
        }


        


    }
}
