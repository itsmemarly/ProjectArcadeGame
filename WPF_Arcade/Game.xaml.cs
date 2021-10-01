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
    /// <summary>
    /// Interaction logic for Game.xaml
    /// </summary>
    /// 

    //TODO: refactor this mess!!
    public partial class Game : Window
    {
        public Game()
        {
            InitializeComponent();
            GameWorld.Focus();
            TileMap terrain = new TileMap(30, 16, "", GameWorld); //make a new tilemap
            terrain.Generate(75, 2, 25, 3, 40); //fill the tilemap with terrain
        }

        private void GameWorld_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void GameWorld_KeyUp(object sender, KeyEventArgs e)
        {

        }
    }

}


