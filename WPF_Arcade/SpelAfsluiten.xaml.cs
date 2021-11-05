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
    /// Interaction logic for SpelAfsluiten.xaml
    /// </summary>
    public partial class SpelAfsluiten : Window
    {
        public SpelAfsluiten()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Closes Application
        /// </summary>
        /// <param name="sender">Mouse Click</param>
        /// <param name="e">Mouse Click</param>
        private void JaSluitAf(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Closes SpelAfsluiten and pops up other screen
        /// </summary>
        /// <param name="sender">Mouse Click</param>
        /// <param name="e">Mouse Click</param>
        private void NeeSluitNietAf(object sender, RoutedEventArgs e)
        {

        }
    }
}
