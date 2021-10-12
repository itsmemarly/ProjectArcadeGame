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
        TileMap terrain;
        Player player;
 
        public Game()
        {
            InitializeComponent();
            GameWorld.Focus();
            terrain = new TileMap(30, 16, 64, "", GameWorld); //make a new tilemap
            terrain.Generate(75, 2, 6, 3, 40, 10); //fill the tilemap with terrain

            player = new Player(128, 256, 30, GameImageBitmaps.player, GameWorld, 64, terrain);
        }

        private void GameWorld_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.R:
                    terrain.Clear();
                    terrain.RandomSeed();
                    terrain.Generate(75, 2, 25, 3, 40, 10);
                    break;

                case Key.W:
                    player.MoveUp();
                    break;

                case Key.S:
                    player.MoveDown();
                    break;

                case Key.D:
                    player.MoveRight();
                    break;

                case Key.A:
                    player.MoveLeft();
                    break;

                case Key.C:
                    player.DestroyTileRight();
                    break;

                case Key.Q:
                    player.DestroyTileLeft();
                    break;

                case Key.E:
                    player.DestroyTileUp();
                    break;

                case Key.Z:
                    player.DestroyTileDown();
                    break;

             

                default:
                    break;

            }
        }

        private void GameWorld_KeyUp(object sender, KeyEventArgs e)
        {

        }
        private void MainMenu_Click(object sender, RoutedEventArgs e)
        {
            // Go to main menu
        }
    }

}


