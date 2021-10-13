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
        EntityManager entityManager;
        public static bool doelBehaald;
        
        public Game()
        {
            InitializeComponent();
            GameWorld.Focus();
            
            terrain = new TileMap(30, 16, 64, "", GameWorld); //make a new tilemap
            terrain.Generate(75, 2, 25, 3, 40, 10); //fill the tilemap with terrain

            List<Enemy> enemyList = new List<Enemy>();
            enemyList.Add(new Enemy(64, 64, 1, 64, GameImageBitmaps.goblin, GameWorld, terrain)); //add new enemy

            List<Player> playerList = new List<Player>();
            playerList.Add(new Player(128, 64, 5, GameImageBitmaps.player, GameWorld, 64, terrain)); //add new player 1
            playerList.Add(new Player(128, 128, 5, GameImageBitmaps.player, GameWorld, 64, terrain)); //add new player 2
            entityManager = new EntityManager(enemyList, playerList, terrain.Seed().GetHashCode());
            

           

           /// GameRonde(); ///
            
            
        }

        


        ///// <summary>
        ///// Maakt en draait spel totdat een van beide spelers het doel heeft behaald.
        ///// </summary>
        //private void GameRonde()
        //{   
           

        //    doelBehaald = false;
        //    while (!doelBehaald)
        //    {
        //        // wat doet de speler in zijn beurt-op welke volgorde, en welke loops heb je hiervoor nodig
        //        /* "aantalBeurten" nog te bespreken
        //         * "Richting van pikhouweel" nog te bespreken
        //         * "Tegenspelers" moet nog besproken worden
        //         * "Enemies verminderen highscore van speler bij aanval naar speler" nog te bespreken
        //         * 
        //         * Speler doet op zijn beurt:
        //         * * Naar links lopen:
        //         * *  Als speler op "A" toetst:
        //         * *   Pikhouweel naar links richten;
        //         * *   Terwijl Speler[x-1] geen monster/enemy/gem/stone heeft: 
        //         * *       Speler[x--];
        //         * *    
        //         * 
        //         * * Naar rechts lopen:
        //         * *  Als speler op "D" toetst:
        //         * *   Pikhouweel naar rechts richten;
        //         * *   Terwijl Speler[x+1] geen monster/enemy/gem/stone houdt: 
        //         * *       Speler[x++];
        //         *        
        //         * * Naar beneden lopen:
        //         * *  Als speler op "S" toetst:
        //         * *   Terwijl Speler[y-1] geen monster/enemy/gem/stone houdt: 
        //         * *       Speler[y--];
        //         * 
        //         * * Naar boven lopen:
        //         * *  Als speler op "W" toetst:
        //         * *   Terwijl Speler[y+1] geen monster/enemy/gem/stone houdt: 
        //         * *       Speler[y++];
        //         * 
        //         * * Pikhouweel naar rechts gebruiken:
        //         * *  Als ingedrukte knop "M" is && pikhouweel naar rechts gericht is && (Speler[x+1] == steen || Speler[x+1] == gem):
        //         * *   Als Speler[x+1] == steen:
        //         * *    Speler[x+1] = "leegte";
        //         * *   Anders als speler[x+1] == gem:
        //         * *    Speler[x+1] = "leegte";
        //         * *    Speler[Aantal_gems]++;
        //         * 
        //         * * Pikhouweel naar links gebruiken:
        //         * *   Als ingedrukte knop "M" is && pikhouweel naar links gericht is && (Speler[x-1] == steen || Speler[x-1] == gem):
        //         * *    Als Speler[x-1] == steen:
        //         * *     Speler[x-1] = "leegte";
        //         * *    Anders als speler[x-1] == gem:
        //         * *     Speler[x-1] = "leegte";
        //         * *     Speler[Aantal_gems]++;
        //         * * Dit geldt ook voor naar boven en beneden pikhouweel gebruiken
        //         * 
        //         * * Tegenspeler rechts van speler doden met pikhouweel, mits pikhouweel naar rechts gericht is:
        //         * *  Als ingedrukte knop "K" is && pikhouweel naar recht gericht is && Speler[x+1] == tegenspeler:
        //         * *    Geef voor 1 seconde weer:
        //         * *     Speler[x+1] = "leegte";
        //         * *    Volgende beurt;
        //         * 
        //         * * Tegenspeler links van speler doden met pikhouweel, mits pikhouweel naar links gericht is:
        //         * *  Als ingedrukte knop "K" is && pikhouweel naar links gericht is && Speler[x-1] == tegenspeler:
        //         * *    Geef voor 1 seconde weer:
        //         * *     Speler[x+1] = "leegte"
        //         * *    Volgende beurt;
        //         * 
        //         * * Enemies rechts van speler doden met pikhouweel, mits pikhouweel naar rechts gericht is:
        //         * *  Als ingedrukte knop "K" is && pikhouweel naar recht gericht is && Speler[x+1] == enemy:
        //         * *    Speler[x+1] = "leegte";
        //         * 
        //         * * Enemies links van speler doden met pikhouweel, mits pikhouweel naar links gericht is;
        //         * *  Als ingedrukte knop "K" is && pikhouweel naar links gericht is && Speler[x-1] == enemy:
        //         * *    Speler[x-1] = "leegte";
        //         * 
        //         * * Doel behalen:
        //         * *  Als Speler1.DoelBehaald() || Speler2.DoelBehaald():
        //         * *   doelBehaald = true;
        //        */
        //    }
        //}

        private void GameWorld_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.R:
                    terrain.Clear();
                    terrain.RandomSeed();
                    terrain.Generate(75, 2, 25, 3, 40, 10);
                    break;
                case Key.Y:
                    entityManager.TakeEnemyTurns();
                    break;

                default:
                    break;

            }
            entityManager.TakePlayerAction(e.Key);
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


