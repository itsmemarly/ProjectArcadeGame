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
        public int worldSeed = 1215; //seed used to generate the world

        private static int stoneWeight = 60; //number between 0-100 used to determine how much stone to generate versus air. A higher number means more stone
        private Rectangle[,] tileMap = new Rectangle[worldWith, worldHeight];

        Random r = new Random();
        

        private void populate2DArrayWithSmoothNoise(int[,] noise, float scale)
        {
            int seed = r.Next(1, 1000);
            //loop over the entire array and for each int: 
            for (int x = 0; x < noise.GetLength(0); x++)
            {
                for (int y = 0; y < noise.GetLength(1); y++)
                {
                   //generate two noise values each at a different resolution and combine the layers for more interesting generation
                    noise[x, y] = (int)getSmoothNoiseInRange(x, y, 2, seed, 75)+ (int)getSmoothNoiseInRange(x, y, 3, seed+1, 25);
                }
            }
        }

        private float getSmoothNoiseInRange(int x, int y, float scale, int seed, int maxValue)
        {
            //generate the four nearest data points
            float bottomRight = generatePsuedoRandomValue(Math.Floor(x / scale), Math.Floor(y / scale), seed, maxValue);
            float topRight = generatePsuedoRandomValue(Math.Floor(x / scale), Math.Ceiling(y / scale), seed, maxValue);
            float bottomLeft = generatePsuedoRandomValue(Math.Ceiling(x / scale), Math.Ceiling(y / scale), seed, maxValue);
            float topLeft = generatePsuedoRandomValue(Math.Ceiling(x / scale), Math.Floor(y / scale), seed, maxValue);

            float distance = y / scale - Convert.ToSingle(Math.Floor(y / scale));
            float a = sineInterpolate(bottomRight, topRight, distance);


            float b = sineInterpolate(topLeft, bottomLeft, distance);
            distance = x / scale - Convert.ToSingle(Math.Floor(x / scale));
            return sineInterpolate(a, b, distance);
        }

        //decided to be fancy and use sine interpolation to smooth out my noise for hopefully some nicer shapes
        //used the writeup here as a reference for my implementation, go read it if you want to understand the math: http://paulbourke.net/miscellaneous/interpolation/
        //if you don't care about the math you just need to know that this helps me smooth out the noise
        private float sineInterpolate(float input1, float input2, float step)

        {
            double stepd = (double)step;
            double stepSquared = step * step;
            float stepSmooth = Convert.ToSingle(1 - Math.Cos(stepd * Math.PI)) / 2;

            return input1 * (1 - stepSmooth) + input2 * stepSmooth;
        }

        //to generate a unique value with strong variance we simply combine the x, y and seed into a string and take the hash
        //then we take the modulo of that hash and our desired range to get a number smaller than that range
        //we also use Math.abs to make sure the number is positive
        private float generatePsuedoRandomValue(double x, double y, float seed, float maxValue)
        {
            string input = (x.ToString() + y.ToString() + seed.ToString());
            return Math.Abs(input.GetHashCode()) % maxValue;
        }

        private void populateTileMap()
        {
            //generate smooth noise
            int[,] noise = new int[worldWith, worldHeight];
            populate2DArrayWithSmoothNoise(noise, 8);

            for (int x = 0; x < tileMap.GetLength(0); x++)
            {
                for (int y = 0; y < tileMap.GetLength(1); y++)
                {
                    //if the noise is smaller than the weighted value, add stone, otherwise add nothing
                    // Kaja, dit snap ik niet.
                    if (noise[x, y] < stoneWeight)
                    {
                        tileMap[x, y] = newTile();
                        BrushConverter bc = new BrushConverter();
                        Brush brush = (Brush)bc.ConvertFrom("#0000" + (noise[x, y] + 16).ToString("X"));
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

        public void emptyTileMap()
        {

        }

        private void GameWorld_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void GameWorld_KeyUp(object sender, KeyEventArgs e)
        {

        }
    }
}


