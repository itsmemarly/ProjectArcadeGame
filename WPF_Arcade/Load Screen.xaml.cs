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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_Arcade
{
    /// <summary>
    /// Interaction logic for Load_Screen.xaml
    /// </summary>
    public partial class Load_Screen : Page
    {
        public Load_Screen()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Closes Load Screen screen and pops up Game screen from Game Slot 1
        /// </summary>
        /// <param name="sender">Mouse Click</param>
        /// <param name="e">Mouse Click</param>
        private void LoadGame1_Click(object sender, RoutedEventArgs e)
        {
            //Load game 1
        }

        /// <summary>
        /// Closes Load Screen screen and pops up Game screen from Game Slot 2
        /// </summary>
        /// <param name="sender">Mouse Click</param>
        /// <param name="e">Mouse Click</param>
        private void LoadGame2_Click(object sender, RoutedEventArgs e)
        {
            //Load game 2
        }

        /// <summary>
        /// Closes Load Screen screen and pops up Game screen from Game Slot 3
        /// </summary>
        /// <param name="sender">Mouse Click</param>
        /// <param name="e">Mouse Click</param>
        private void LoadGame3_Click(object sender, RoutedEventArgs e)
        {
            //Load game 3
        }

        /// <summary>
        /// Closes Load Screen screen and pops up Menu screen
        /// </summary>
        /// <param name="sender">Mouse Click</param>
        /// <param name="e">Mouse Click</param>
        private void MainMenu_Click(object sender, RoutedEventArgs e)
        {
            //Back to main menu
        }
    }
}
