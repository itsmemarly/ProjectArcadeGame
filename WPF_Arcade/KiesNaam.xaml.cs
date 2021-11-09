using System;
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
    public partial class KiesNaam : Window
    {
     
        public KiesNaam()
        {
            InitializeComponent();
        }


        /// Button start Spel///
        public void ButtonStartSpel(object sender, RoutedEventArgs e)
        {
            SaveNames();
            Game game = new Game();
            Close();
            game.Visibility = Visibility.Visible;
        }


        //Function to make sure the playernames are inserted into the database
        private void SaveNames()
        {
            // Get name of Player 1
            string P1 = Player1Name.Text;

            //Get name of Player 2
            string P2 = Player2Name.Text;

            //Create connection for database
            OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\reidi\OneDrive\Documents\testdb_old.mdb");

            //Open database connection
            con.Open();

            //Add new command to insert Playernames into the database
            OleDbCommand cmd = new OleDbCommand("INSERT INTO Speler1(Naam1, Naam2) VALUES ('"+P1+"','"+P2+"')", con);

            //Send command to the database
            cmd.ExecuteNonQuery();

            //Close database connection
            con.Close();


        }
    }



}