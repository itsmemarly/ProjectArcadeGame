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
    /// Interaction logic for You_won.xaml
    /// </summary>
    public partial class You_won : Window
    {
        public You_won()
        {
            InitializeComponent();
        }
        private void HoofdMenu_Click(object sender, RoutedEventArgs e)
        {
            // Go back to main menu
            Menu menu = new Menu();
            menu.Visibility = Visibility.Visible;
        }

        private void QuitGame_Click(object sender, RoutedEventArgs e)
        {
            // Quit the game
        }

        
    }
}
