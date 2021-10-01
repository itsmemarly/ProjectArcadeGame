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
        public Game()
        {
            InitializeComponent();
            GameWorld.Focus();
            populateTileMap();
        }

        public static int tileSize = 64; //size of each tile in pixels
        public static int worldWith = 30; //width of the gameworld in tiles
        public static int worldHeight = 16; //height of the gameworld in tiles
        public static string worldSeed = "12fsad"; //seed used to generate the world

        private static int stoneWeight = 60; //number between 0-100 used to determine how much stone to generate versus air. A higher number means more stone
        private Rectangle[,] tileMap = new Rectangle[worldWith, worldHeight];

        static Random r = new Random();

        static string seed = r.Next().ToString();
        NoiseMap map1 = new NoiseMap(worldWith, worldHeight, 75, 2, seed);
        NoiseMap map2 = new NoiseMap(worldWith, worldHeight, 25, 3, seed);


        private void populateTileMap()
        {
            map1.populateMap();
            map2.populateMap();

            for (int x = 0; x < tileMap.GetLength(0); x++)
            {
                for (int y = 0; y < tileMap.GetLength(1); y++)
                {
                    //if the noise is smaller than the weighted value, add stone, otherwise add nothing
                    //combined noise at position as int
                    int currentNoise = (int)map1.getNoiseAt(x, y) + (int)map2.getNoiseAt(x, y);
                    if (currentNoise  < stoneWeight)
                    {
                        tileMap[x, y] = newTile();
                        BrushConverter bc = new BrushConverter();
                        Brush brush = (Brush)bc.ConvertFrom("#0000" + (((int)map1.getNoiseAt(x, y) + (int)map2.getNoiseAt(x, y)) + 16).ToString("X"));
                        tileMap[x, y].Fill = brush;
                        addRectangleAtPosition(tileMap[x, y], x * tileSize, y * tileSize);
                    }

                }
            }
        }

        //add a rectangle at a given position
        private void addRectangleAtPosition(Rectangle rectangle, int x, int y)
        {
            Canvas.SetLeft(rectangle, x);
            Canvas.SetTop(rectangle, y);
            GameWorld.Children.Add(rectangle);
        }

        //makes a new rectangle
        private Rectangle newTile()
        {
            Rectangle tile = new Rectangle
            {
                Tag = "tile",
                Height = 64, // Kaja, ik zou hier tileSize van maken
                Width = 64, // Kaja, ik zou hier tileSize van maken
                Fill = Brushes.White,
                //Stroke = Brushes.Red
            };

            return tile;
        }

        private void GameWorld_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void GameWorld_KeyUp(object sender, KeyEventArgs e)
        {

        }
    }

}


