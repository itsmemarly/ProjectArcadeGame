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
    /// Interaction logic for Tutorial.xaml
    /// </summary>
    public partial class Tutorial : Window
    {
        public Tutorial()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Closes Tutorial screen and pops up Game screen
        /// </summary>
        /// <param name="sender">Mouse Click</param>
        /// <param name="e">Mouse Click</param>
        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            //Start the game
            Game game = new Game();
            Close();
            game.Visibility = Visibility.Visible;
        }
    }
}
