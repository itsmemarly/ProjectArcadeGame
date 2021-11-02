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
    /// Interaction logic for InputName.xaml
    /// </summary>
    public partial class InputName : Window
    {
        public InputName()
        {
            InitializeComponent();
        }

        //link to database///
        public string conString = "" ;



        /// Input Name Player 1///
        private void PutYourNamePlayer1(object sender, TextCompositionEventArgs e)
        {
            string name;
            Console.Write("Vul hier je naam in...");
            name = Console.ReadLine();
        }

        /// Input Name Player 2///
        private void PutYourNamePlayer2(object sender, TextCompositionEventArgs e)
        {
            string name;
            Console.Write("Vul hier je naam in...");
            name = Console.ReadLine();
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
