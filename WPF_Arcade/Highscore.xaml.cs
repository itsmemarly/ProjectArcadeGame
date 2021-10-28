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
        public string Namen {
            get{  return this.PlayerName.Text;}
            set {this.connection();}
        }
        public Highscore()
        {
            InitializeComponent();
           
        }

        public void connection(string namen, int score) 
        {
            MySqlConnection conn1 = new MySqlConnection("Data Source=(localdb)/MSSQLLocalDB;Initial Catalog=C:/XAMPP/HTDOCS/HBO/DIGDUG/PROJECTARCADEGAME/WPF_ARCADE/DATABASE1.MDF;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            conn1.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT Naam, Score FROM dbo.Game ORDER BY score DESC", conn1);
            MySqlDataReader reader1;
            reader1 = cmd.ExecuteReader();
            reader1.Read();
           PlayerName.Text = reader1["Naam"].ToString();
            
            //PlayerName.Text = reader1["Naam"].ToString();
            if (reader1.Read())
            {
                //PlayerName.Text = "Truus";
                // PlayerName.Content = reader1["Naam"].ToString();
               PlayerScore.Text = reader1["Score"].ToString();
            }
            else 
            {
                MessageBox.Show("Er is geen data gevonden.");
            }
            
            conn1.Close();

            return PlayerName.Text;
        }

        private void TerugNaarMenu(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu();
            Close();
            menu.Visibility = Visibility.Visible;
        }

        private void PlayerName_SourceUpdated(object sender, DataTransferEventArgs e)
        {

        }
    }
}
