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
        public  int worldSeed = 1215; //seed used to generate the world

        private static int stoneWeight = 51; //number between 0-100 used to determine how much stone to generate versus air. 
        private Rectangle[,] tileMap = new Rectangle[worldWith, worldHeight]; 
        

        private void populate2DArrayWithSmoothNoise(int[,] noise)
        {
            //loop over the entire array and for each int: 
            for (int x = 0; x < noise.GetLength(0); x++)
            {
                for (int y = 0; y < noise.GetLength(1); y++)
                {
                    //first we get the value for the 12 surrounding points.
                    float[] surroundingPoints  = new float[] {
                    generatePsuedoRandomValue(x + 1, y, worldSeed, 100),
                    generatePsuedoRandomValue(x - 1, y, worldSeed, 100),
                    generatePsuedoRandomValue(x, y + 1, worldSeed, 100),
                    generatePsuedoRandomValue(x, y - 1, worldSeed, 100),
                    generatePsuedoRandomValue(x + 1, y + 1, worldSeed, 100),
                    generatePsuedoRandomValue(x - 1, y - 1, worldSeed, 100),
                    generatePsuedoRandomValue(x - 1, y + 1, worldSeed, 100),
                    generatePsuedoRandomValue(x + 2, y, worldSeed, 100),
                    generatePsuedoRandomValue(x - 2, y, worldSeed, 100),
                    generatePsuedoRandomValue(x, y + 2, worldSeed, 100),
                    generatePsuedoRandomValue(x, y - 2, worldSeed, 100) };


                    //and then we take the average of these points for our position to create smooth noise
                    float sum = 0;
                    for (int i = 0; i < surroundingPoints.Length; i++)
                    {
                        sum += surroundingPoints[i];
                    }

                    noise[x, y] = (int)(sum / surroundingPoints.Length);
                }
            }
        }

        private float generatePsuedoRandomValue(float x, float y, float seed, float maxValue)
        {
            //to generate a unique value with strong variance we simply combine the x, y and seed into a string and take the hash
            //then we take the modulo of that hash and our desired range to get a number smaller than that range
            //we also use Math.abs to make sure the number is positive
            string input = (x.ToString() + y.ToString() + seed.ToString());
            return Math.Abs(input.GetHashCode()) % maxValue;
        }

        private void populateTileMap()
        {
            //generate smooth noise
            int[,] noise = new int[worldWith, worldHeight];
            populate2DArrayWithSmoothNoise(noise);

            for (int x = 0; x < tileMap.GetLength(0); x++)
            {
                for (int y = 0; y < tileMap.GetLength(1); y++)
                {
                    //if the noise is smaller than the weighted value, add stone, otherwise add nothing
                    if (noise[x, y] < stoneWeight)
                    {
                        tileMap[x, y] = newTile();
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
                Height = 64,
                Width = 64,
                Fill = Brushes.White,
                Stroke = Brushes.Red
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


