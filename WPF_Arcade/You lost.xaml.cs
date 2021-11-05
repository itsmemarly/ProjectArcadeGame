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
    /// Interaction logic for You_lost.xaml
    /// </summary>
    public partial class You_lost : Window
    {
        public You_lost()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Closes You lost screen and pops up Menu screen
        /// </summary>
        /// <param name="sender">Mouse Click</param>
        /// <param name="e">Mouse Click</param>
        private void TerugNaarMenu(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Closes You lost screen and pops up SpelAfsluiten screen
        /// </summary>
        /// <param name="sender">Mouse Click</param>
        /// <param name="e">Mouse Click</param>
        private void StopHetSpel(object sender, RoutedEventArgs e)
        {

        }
    }
}
