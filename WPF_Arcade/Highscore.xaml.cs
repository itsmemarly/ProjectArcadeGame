using System;
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
    /// Interaction logic for Highscore.xaml
    /// </summary>
    public partial class Highscore : Window
    {
        public Highscore()
        {
            InitializeComponent();
        }

        public string conString = "Data Source=(localdb)/MSSQLLocalDB;Initial Catalog=C:/XAMPP/HTDOCS/HBO/DIGDUG/PROJECTARCADEGAME/WPF_ARCADE/DATABASE1.MDF;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";


        private void TerugNaarMenu(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu();
            Close();
            menu.Visibility = Visibility.Visible;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
