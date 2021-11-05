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
    /// Interaction logic for Save.xaml
    /// </summary>
    public partial class Save : Window
    {
        public Save()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Saves current Game in Game Slot 1
        /// </summary>
        /// <param name="sender">Mouse Click</param>
        /// <param name="e">Mouse Click</param>
        private void SaveGame1_Click(object sender, RoutedEventArgs e)
        {
            //SaveFile 1
        }

        /// <summary>
        /// Saves current Game in Game Slot 2
        /// </summary>
        /// <param name="sender">Mouse Click</param>
        /// <param name="e">Mouse Click</param>
        private void SaveGame2_Click(object sender, RoutedEventArgs e)
        {
            //SaveFile 2
        }

        /// <summary>
        /// Saves current Game in Game Slot 3
        /// </summary>
        /// <param name="sender">Mouse Click</param>
        /// <param name="e">Mouse Click</param>
        private void SaveGame3_Click(object sender, RoutedEventArgs e)
        {
            //SaveFile 3
        }

        /// <summary>
        /// Closes Save screen and pops up Menu screen
        /// </summary>
        /// <param name="sender">Mouse Click</param>
        /// <param name="e">Mouse Click</param>
        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            //menu button
        }
    }
}
