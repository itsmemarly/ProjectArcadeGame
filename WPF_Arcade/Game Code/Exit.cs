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
        private BitmapImage exitBitMap = GameImageBitmaps.exit;
        private Image exitImage;
        private readonly GameLevel exitLevel;

        public Exit(int x, int y, int size, Canvas canvas, GameLevel level)
        {
            exitX = x;
            exitY = y;
            exitCanvas = canvas;
            exitSize = size;
            exitLevel = level;

            exitImage = new Image
            {
                Tag = "exit",
                Height = exitSize,
                Width = exitSize,
                Source = exitBitMap
            };

            Canvas.SetLeft(exitImage, x);
            Canvas.SetTop(exitImage, y);

            exitCanvas.Children.Add(exitImage);
        }

        public int X()
        {
            return exitX;
        }

        public int Y()
        {
            return exitY;
        }

        public void MoveTo(int x, int y)
        {
            exitX = x;
            exitY = y;
            Canvas.SetLeft(exitImage, x);
            Canvas.SetTop(exitImage, y);
        }

        // Creates and opens the win screen
        public void EndGame()
        {
            exitLevel.SaveScores();
            You_won winscreen = new You_won();
            winscreen.Visibility = Visibility.Visible;
            
        }
    }
}
