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
        private string exitType;
        private int exitSize;
        private Canvas exitCanvas;
        private int exitX;
        private int exitY;
        private BitmapImage exitBitmap;
        private Image exitImage;

        public Exit(int size, int x, int y, Canvas canvas, BitmapImage bitmap)
        {
            exitX = x;
            exitY = y;

            exitCanvas = canvas;
                
            exitSize = size;

            exitImage = new Image
            {
                Tag = "exitType",
                Height = exitSize,
                Width = exitSize,
                Source = bitmap,
            };


            Canvas.SetLeft(exitImage, x * exitSize);
            Canvas.SetTop(exitImage, y * exitSize);

            exitCanvas.Children.Add(exitImage);
        }




        



    }
}
