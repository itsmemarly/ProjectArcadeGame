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
    /// Interaction logic for Startscherm.xaml
    /// </summary>
    public partial class Startscherm : Window
    {
        public Startscherm()
        {
            InitializeComponent();
            MyGrid.Focus();
        }

        private void ButtonStartSpel(object sender, RoutedEventArgs e)
        {
            Game game = new Game();
            Close();
            game.Visibility = Visibility.Visible;
        }

        private void ButtonLaadVorigSpel(object sender, RoutedEventArgs e)
        {
            // Vorig spel laden
            Close();
        }

        private void ButtonUitleg(object sender, RoutedEventArgs e)
        {
            Tutorial tutorial = new Tutorial();
            Close();
            tutorial.Visibility = Visibility.Visible;
        }

        private void ButtonOpties(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonHighScore(object sender, RoutedEventArgs e)
        {
            Highscore highscore = new Highscore();
            Close();
            highscore.Visibility = Visibility.Visible;
        }

        private void ButtonCredits(object sender, RoutedEventArgs e)
        {
            Credits credits = new Credits();
            Close();
            credits.Visibility = Visibility.Visible;
        }
    }
}
