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
        //TileMap terrain;
        //EntityManager entityManager;
        private readonly GameLevel level;
        
        public Game()
        {
            InitializeComponent();
            GameWorld.Focus();

            level = new GameLevel(1920, 1080, 64, GameWorld);
            level.BuildLevel();

            //terrain = new TileMap(30, 16, 64, "", GameWorld); //make a new tilemap
            //terrain.Generate(75, 2, 25, 3, 40, 10); //fill the tilemap with terrain

            //List<Enemy> enemyList = new List<Enemy>();
            //enemyList.Add(new Enemy(64, 64, 1, 64, GameImageBitmaps.goblin, GameWorld, terrain)); //add new enemy

            //List<Player> playerList = new List<Player>();
            //playerList.Add(new Player(128, 64, 5, GameImageBitmaps.player, GameWorld, 64, terrain)); //add new player 1
            //playerList.Add(new Player(128, 128, 5, GameImageBitmaps.player, GameWorld, 64, terrain)); //add new player 2
            //entityManager = new EntityManager(enemyList, playerList, terrain.Seed());
        }

        private void GameWorld_KeyDown(object sender, KeyEventArgs e)
        {
            level.ProcessInput(e.Key);
            //switch (e.Key)
            //{
            //    case Key.R:
            //        terrain.Clear();
            //        terrain.RandomSeed();
            //        terrain.Generate(75, 2, 25, 3, 40, 10);
            //        break;

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

            //entityManager.TakePlayerAction(e.Key);
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


