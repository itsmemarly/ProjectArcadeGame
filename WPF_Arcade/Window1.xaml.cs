﻿using System;
using System.Collections.Generic;
using System.Data.OleDb;
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
    /// Interaction logic for InputName.xaml
    /// </summary>
    public partial class InputName : Window
    {
        public InputName()
        {
            InitializeComponent();
        }

        //link to database///
        public string conString = "Data Source=(localdb)/MSSQLLocalDB;Initial Catalog=C:/Users/jessi/Downloads/testdb_oldIntegrated Security=True;ConnectTimeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
      


        /// Input Name Player 1///
        public void PutYourNamePlayer1(object sender, TextCompositionEventArgs e)
        {
            string UserName1;
            UserName1 = Console.ReadLine();
            Console.ReadKey();
        }

        /// Input Name Player 2///
        public void PutYourNamePlayer2(object sender, TextCompositionEventArgs e)
        {
            string UserName2;
            UserName2 = Console.ReadLine();
            Console.ReadKey();
        }

        /// Button start Spel///
        public void ButtonStartSpel(object sender, RoutedEventArgs e)
        {
            Game game = new Game();
            Close();
            game.Visibility = Visibility.Visible;
        }


    }



}
