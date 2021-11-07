﻿using System;
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
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public Menu()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Closes Menu screen and reopens Game screen
        /// </summary>
        /// <param name="sender">Mouse Click</param>
        /// <param name="e">Mouse Click</param>
        private void TerugNaarSpel(object sender, RoutedEventArgs e)
        {
            // Game van de achtergrond oproepen
            Close(); // Zie https://docs.microsoft.com/en-us/dotnet/desktop/wpf/windows/how-to-close-window-dialog-box?view=netdesktop-5.0#close-a-modeless-window voor nadere toelichting.
        }

        /// <summary>
        /// Closes Menu screen and pops up Save screen
        /// </summary>
        /// <param name="sender">Mouse Click</param>
        /// <param name="e">Mouse Click</param>
        private void Opslaan(object sender, RoutedEventArgs e)
        {
            // Spel opslaan
        }

        /// <summary>
        /// Closes Menu screen and pops up Options screen
        /// </summary>
        /// <param name="sender">Mouse Click</param>
        /// <param name="e">Mouse Click</param>
        private void Opties(object sender, RoutedEventArgs e)
        {
            // Opties openen
            Close();
        }

        /// <summary>
        /// Closes Menu screen and pops up Startscherm screen
        /// </summary>
        /// <param name="sender">Mouse Click</param>
        /// <param name="e">Mouse Click</param>
        private void TerugNaarStartscherm(object sender, RoutedEventArgs e)
        {
            Startscherm startscherm = new Startscherm();
            Close();
            startscherm.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Closes Application
        /// </summary>
        /// <param name="sender">Mouse Click</param>
        /// <param name="e">Mouse Click</param>
        private void SpelAfsluiten(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0); 
            // Zie https://docs.microsoft.com/en-us/dotnet/api/system.environment.exit?redirectedfrom=MSDN&view=net-5.0#System_Environment_Exit_System_Int32 voor nadere toelichting
        }
    }
}
