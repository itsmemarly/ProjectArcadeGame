using DocumentFormat.OpenXml.Office.CustomUI;
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
    /// Interaction logic for Highscore.xaml
    /// </summary>
    public partial class Highscore : Window
    {

        public Highscore()
        {
            InitializeComponent();
            Bind();
        }
        private void Bind()
        {
            //Link to DB
            OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\reidi\OneDrive\Documents\testdb_old.mdb");

            //Open connection
            con.Open();

            //Add new adapter & datatable for P1
            OleDbDataAdapter da = new OleDbDataAdapter("Select Naam1 AS Naam, Score1 AS Score from Speler1 ORDER BY Score1 DESC", con);
            DataTable dt = new DataTable();
            //Fill datatable with adapter
            da.Fill(dt);

            //Link to the right datagrid
            PlayerName.DataContext = dt;

            OleDbDataAdapter da2 = new OleDbDataAdapter("Select Naam2 AS Naam, Score2 AS Score from Speler1 ORDER BY Score2 DESC", con);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            PlayerScore.DataContext = dt2;
            con.Close();
        }

        private void TerugNaarMenu(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu();
            Close();
            menu.Visibility = Visibility.Visible;
        }
}
}
