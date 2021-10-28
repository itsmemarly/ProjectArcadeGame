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
    public static class GameImageBitmaps
    {
        public static readonly BitmapImage stone = new BitmapImage(new Uri(@"pack://application:,,,/Media/Sprites/stone_64px.png"));
        public static readonly BitmapImage gem = new BitmapImage(new Uri(@"pack://application:,,,/Media/Sprites/gem_vein_64px.png"));
        public static readonly BitmapImage player = new BitmapImage(new Uri(@"pack://application:,,,/Media/Sprites/player_64px.png"));
        public static readonly BitmapImage goblin = new BitmapImage(new Uri(@"pack://application:,,,/Media/Sprites/goblin_64px.png"));
        public static readonly BitmapImage exit = new BitmapImage(new Uri(@"pack://application,,,/Media/Sprites/gem_64px.png"));

    }
}
