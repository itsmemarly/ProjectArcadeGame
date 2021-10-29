using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for Save.xaml
    /// </summary>
    public partial class Save : Window
    {

        //Link to DB
        OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\reidi\OneDrive\Documents\testdb_old.mdb");

        public Save()
        {
            InitializeComponent();
        }
          
        private void SaveGame1_Click(object sender, RoutedEventArgs e)
        {
            //SaveFile 1

            //Open connection
            con.Open();

            //Select data from database with ID 1
            OleDbDataAdapter da = new OleDbDataAdapter("Select * From Opslaan Where ID=1", con);
           
            con.Close();
        }

        private void SaveGame2_Click(object sender, RoutedEventArgs e)
        {
            //SaveFile 2


            //Open connection
            con.Open();

            //Select data from database with ID 2
            OleDbDataAdapter da = new OleDbDataAdapter("Select * From Opslaan Where ID=2", con);

            con.Close();
        }

        private void SaveGame3_Click(object sender, RoutedEventArgs e)
        {
            //SaveFile 3


            //Open connection
            con.Open();

            //Select data from database with ID 3
            OleDbDataAdapter da = new OleDbDataAdapter("Select * From Opslaan Where ID=3", con);

            con.Close();
        }
        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            //menu button
        }
    }
}
