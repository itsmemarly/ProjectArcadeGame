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

        /// <summary>
        /// Closes Startscherm screen and pops up Game screen
        /// </summary>
        /// <param name="sender">Mouse Click</param>
        /// <param name="e">Mouse Click</param>
        private void ButtonStartSpel(object sender, RoutedEventArgs e)
        {
            Game game = new Game();
            Close();
            game.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Closes Startscherm screen and pops up old Game screen
        /// </summary>
        /// <param name="sender">Mouse Click</param>
        /// <param name="e">Mouse Click</param>
        private void ButtonLaadVorigSpel(object sender, RoutedEventArgs e)
        {
            // Vorig spel laden
            Close();
        }

        /// <summary>
        /// Closes Startscherm screen and pops up Tutorial screen
        /// </summary>
        /// <param name="sender">Mouse Click</param>
        /// <param name="e">Mouse Click</param>
        private void ButtonUitleg(object sender, RoutedEventArgs e)
        {
            Tutorial tutorial = new Tutorial();
            Close();
            tutorial.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Closes Startscherm screen and pops up Options screen
        /// </summary>
        /// <param name="sender">Mouse Click</param>
        /// <param name="e">Mouse Click</param>
        private void ButtonOpties(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Closes Startscherm screen and pops up Highscore screen
        /// </summary>
        /// <param name="sender">Mouse Click</param>
        /// <param name="e">Mouse Click</param>
        private void ButtonHighScore(object sender, RoutedEventArgs e)
        {
            Highscore highscore = new Highscore();
            Close();
            highscore.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Closes Startscherm screen and pops up Credits screen
        /// </summary>
        /// <param name="sender">Mouse Click</param>
        /// <param name="e">Mouse Click</param>
        private void ButtonCredits(object sender, RoutedEventArgs e)
        {
            Credits credits = new Credits();
            Close();
            credits.Visibility = Visibility.Visible;
        }
    }
}
