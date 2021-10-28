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

        OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\xampp\htdocs\HBO\digdug\ProjectArcadeGame\arcadeGame.accdb");
        public Highscore()
        {
            InitializeComponent();
            Bind();
        }
        private void Bind()
        {
            con.Open();
            OleDbDataAdapter da = new OleDbDataAdapter("Select * from Speler1", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
        }
    }
}
