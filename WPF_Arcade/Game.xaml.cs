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

    public partial class Game : Window
    {
        private GameLevel level;
        
        public Game()
        {
            InitializeComponent();
            GameWorld.Focus();

            level = new GameLevel(1920, 1064, 64, GameWorld, Player1Score, Player1TurnCounter, Player2Score, Player2TurnCounter);
            level.BuildLevel();
        }

        private void GameWorld_KeyDown(object sender, KeyEventArgs e)
        {
            level.ProcessInput(e.Key);
            if (e.Key == Key.R)
            {
                //level = new GameLevel(1920, 1080, 64, GameWorld, Player1Score, Player1TurnCounter, Player2Score, Player2TurnCounter);
            }
        }

        private void GameWorld_KeyUp(object sender, KeyEventArgs e)
        {

        }
        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu();
            Close();
            menu.Visibility = Visibility.Visible;
        }
    }

}


