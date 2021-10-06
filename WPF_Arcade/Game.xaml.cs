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
    /// Interaction logic for Game.xaml
    /// </summary>
    /// 

    //TODO: refactor this mess!!
    public partial class Game : Window
    {
        TileMap terrain;

        public static int beurten = 0;
        
        public Game()
        {
            InitializeComponent();
            GameWorld.Focus();
            Games(beurten);
            
        }

        /// <summary>
        /// Maakt spel per beurt totdat alle beurten op zijn
        /// </summary>
        /// <param name="aantalBeurten">aantal beurten dat er gespeeld zal worden</param>
        private void Games(int aantalBeurten)
        {
            for (int i = 0; i < aantalBeurten; i++)
            {
                terrain = new TileMap(30, 16, 64, "", GameWorld); //make a new tilemap
                terrain.Generate(75, 2, 25, 3, 40, 10); //fill the tilemap with terrain

                // wat doet de speler in zijn beurt-op welke volgorde, en welke loops heb je hiervoor nodig
                /*
                 * Speler doet op zijn beurt:
                 * * Naar links lopen:
                 * *  Als speler op linkerpijl toetst:
                 * *   Pikhouweel naar links richten;
                 * *   Terwijl Speler[x-1] geen monster/enemy/gem/stone heeft: 
                 * *       Speler[x--];
                 * *    
                 * 
                 * * Naar rechts lopen:
                 * *  Als speler op rechterpijl toetst:
                 * *   Pikhouweel naar rechts richten;
                 * *   Terwijl Speler[x+1] geen monster/enemy/gem/stone houdt: 
                 * *       Speler[x++];
                 *        
                 * * Naar beneden lopen:
                 * *  Als speler op benedenpijl toetst:
                 * *   Terwijl Speler[y-1] geen monster/enemy/gem/stone houdt: 
                 * *       Speler[y--];
                 * 
                 * * Naar boven lopen:
                 * *  Als speler op bovenpijl toetst:
                 * *   Terwijl Speler[y+1] geen monster/enemy/gem/stone houdt: 
                 * *       Speler[y++];
                 * 
                 * * Pikhouweel naar rechts gebruiken:
                 * *  Als ingedrukte knop "M" is && pikhouweel naar rechts gericht is && (Speler[x+1] == steen || Speler[x+1] == gem):
                 * *   Als Speler[x+1] == steen:
                 * *    Speler[x+1] = "leegte";
                 * *   Anders als speler[x+1] == gem:
                 * *    Speler[x+1] = "leegte";
                 * *    Speler[Aantal_gems]++;
                 * 
                 * * Pikhouweel naar links gebruiken:
                 * *   Als ingedrukte knop "M" is && pikhouweel naar links gericht is && (Speler[x-1] == steen || Speler[x-1] == gem):
                 * *    Als Speler[x-1] == steen:
                 * *     Speler[x-1] = "leegte";
                 * *    Anders als speler[x-1] == gem:
                 * *     Speler[x-1] = "leegte";
                 * *     Speler[Aantal_gems]++;
                 * 
                 * * Tegenspeler rechts van speler doden met pikhouweel, mits pikhouweel naar rechts gericht is:
                 * *  Als ingedrukte knop "K" is && pikhouweel naar recht gericht is && Speler[x+1] == tegenspeler:
                 * *    Geef voor 1 seconde weer:
                 * *     Speler[x+1] = "leegte";
                 * *    Volgende beurt;
                 * 
                 * * Tegenspeler links van speler doden met pikhouweel, mits pikhouweel naar links gericht is:
                 * *  Als ingedrukte knop "K" is && pikhouweel naar links gericht is && Speler[x-1] == tegenspeler:
                 * *    Geef voor 1 seconde weer:
                 * *     Speler[x+1] = "leegte"
                 * *    Volgende beurt;
                 * 
                 * * Enemies rechts van speler doden met pikhouweel, mits pikhouweel naar rechts gericht is:
                 * *  Als ingedrukte knop "K" is && pikhouweel naar recht gericht is && Speler[x+1] == enemy:
                 * *    Speler[x+1] = "leegte";
                 * 
                 * * Enemies links van speler doden met pikhouweel, mits pikhouweel naar links gericht is;
                 * *  Als ingedrukte knop "K" is && pikhouweel naar links gericht is && Speler[x-1] == enemy:
                 * *    Speler[x-1] = "leegte";
                 * 
                */
            }
        }

        private void GameWorld_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.R:
                    terrain.Clear();
                    terrain.RandomSeed();
                    terrain.Generate(75, 2, 25, 3, 40, 10);
                    break;

                default:
                    break;

            }
        }

        private void GameWorld_KeyUp(object sender, KeyEventArgs e)
        {

        }
        private void MainMenu_Click(object sender, RoutedEventArgs e)
        {
            // Go to main menu
        }
    }

}


