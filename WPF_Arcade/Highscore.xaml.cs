using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
    /// Interaction logic for Highscore.xaml
    /// </summary>
    public partial class Highscore : Window
    {
        public Highscore()
        {
            InitializeComponent();
        
        }

        // Retrieve Highscore data
        public string conString = "Data Source=(localdb)/MSSQLLocalDB;Initial Catalog=C:/XAMPP/HTDOCS/HBO/DIGDUG/PROJECTARCADEGAME/WPF_ARCADE/DATABASE1.MDF;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        /// <summary>
        /// Closes Highscore screen and pops up Menu screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TerugNaarMenu(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu();
            Close();
            menu.Visibility = Visibility.Visible;
        }
    }
}
