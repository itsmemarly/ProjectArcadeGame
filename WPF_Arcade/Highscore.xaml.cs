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
            //Call private void Bind
            Bind();
        
        }
        private void Bind()
        {
            //Link to DB
            OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:/Users/jessi/Downloads/testdb_old");

            //Open connection
            con.Open();

            //Add new adapter & datatable for P1
            OleDbDataAdapter da = new OleDbDataAdapter("Select Naam1 AS Naam, Score1 AS Score from Speler1 ORDER BY Score1 DESC", con);

            //Create new DataTable (dt)

            DataTable dt = new DataTable();

            //Fill dataTable with adapter
            da.Fill(dt);

            //Link to the right datagrid
            PlayerName.DataContext = dt;


            //Repeat same steps for names & scores P2

            //Add new adapter
            OleDbDataAdapter da2 = new OleDbDataAdapter("Select Naam2 AS Naam, Score2 AS Score from Speler1 ORDER BY Score2 DESC", con);

            //Add new DataTable (dt) 2 stands for P2
            DataTable dt2 = new DataTable();

            //Fill dataTable with adapter
            da2.Fill(dt2);

            //Link to the datagrid for P2
            PlayerScore.DataContext = dt2;

            //Close conncection (con)
            con.Close();
        }


        public string conString = "Data Source=(localdb)/MSSQLLocalDB;Initial Catalog=C:/XAMPP/HTDOCS/HBO/DIGDUG/PROJECTARCADEGAME/WPF_ARCADE/DATABASE1.MDF;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";


        private void TerugNaarMenu(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu();
            Close();
            menu.Visibility = Visibility.Visible;
        }
    }
}
