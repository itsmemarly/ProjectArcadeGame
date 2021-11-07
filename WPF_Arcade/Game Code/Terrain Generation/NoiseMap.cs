using System;
using System.Collections.Generic;
using System.Text;

namespace WPF_Arcade
{
    public class NoiseMap
    {
        private readonly float[,] map;
        private readonly int mapHeight;
        private readonly int mapWidth;
        private readonly int mapMaxValue;
        private float mapResolution;
        private string mapSeed;

        public NoiseMap(int height, int width, int maxValue, float resolution, string seed)
        {
            mapHeight = height;
            mapWidth = width;
            mapMaxValue = maxValue;
            mapResolution = resolution;
            mapSeed = seed;

            map = new float[mapHeight, mapWidth];
        }

        //methods to set values

        /// <summary>
        /// Sets the Seed of the Terrain for the Noise Map
        /// </summary>
        /// <param name="seed">the Seed of the Terrain</param>
        public void SetSeed(string seed)
        {
            mapSeed = seed;
        }

        //methods to get values
        //each of these mentions can be called to get information about the noise map

        /// <summary>
        /// Gets the height of the Noise Map
        /// </summary>
        /// <returns>the height of the Noise Map</returns>
        public int Height()
        {
            return mapHeight;
        }

        /// <summary>
        /// Gets the width of the Noise Map
        /// </summary>
        /// <returns>the width of the Noise Map</returns>
        public int Width()
        {
            return mapWidth;
        }

        /// <summary>
        /// Gets the Seed of the Noise Map
        /// </summary>
        /// <returns>the Seed of the Noise Map</returns>
        public string Seed()
        {
            return mapSeed;
        }

        /// <summary>
        /// Gets the maximum value found in the Noise Map
        /// </summary>
        /// <returns>the maximum value found in the Noise Map</returns>
        public int MaxValue()
        {
            return mapMaxValue;
        }

        /// <summary>
        /// Gets the value of the given position in the Noise Map
        /// </summary>
        /// <param name="x">index of the row of the given position</param>
        /// <param name="y">index of the column of the given position</param>
        /// <returns>the value of the given position in the Noise Map</returns>
        public float GetNoiseAt(int x, int y)
        {
            return map[x, y];
        }

        //accessable functionality

        /// <summary>
        /// Loops over the TileMap and fills each position with a value, which combined form the NoiseMap
        /// </summary>
        public void PopulateMap()
        { 
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {

                    map[x, y] = GetSmoothNoiseAtPosition(x, y, mapResolution, mapMaxValue);
                }
            }
        }

        /// <summary>
        /// Zooms in on the two dimensional array of purely random values and then uses cosine interpolation to fill the empty space in between.
        /// </summary>
        /// <param name="x">x coordinate of the given position</param>
        /// <param name="y">y coordinate of the given position</param>
        /// <param name="scale">multiplication factor necessary to translate mathematical data to Canvas design data and vice versa</param>
        /// <param name="maxValue">value necessary to zoom in enough on the two dimensional array</param>
        /// <returns>the smoothened noise value at the given position</returns>
        public float GetSmoothNoiseAtPosition(int x, int y, float scale, int maxValue)
        {
            //this method is the core of creating a noise map.
            //it zooms in on 2d array of purely random values and then uses cosine interpolation to fill the empty space in between
            //it only uses the four nearest values as that's all that cosine interpolation needs to get a good result
            //zooming in happens by deviding by the zoom scale, then flooring/ cieling the number to get the nearest whole integer.
            //There's probably faster/ cleaner ways to do it but this is what I chose

            //generate the four nearest data points
            float bottomRight = GeneratePsuedoRandomValue(Math.Floor(x / scale), Math.Floor(y / scale), maxValue);
            float topRight = GeneratePsuedoRandomValue(Math.Floor(x / scale), Math.Ceiling(y / scale), maxValue);
            float bottomLeft = GeneratePsuedoRandomValue(Math.Ceiling(x / scale), Math.Ceiling(y / scale), maxValue);
            float topLeft = GeneratePsuedoRandomValue(Math.Ceiling(x / scale), Math.Floor(y / scale), maxValue);

            //first it interpolates two nearby points
            float distance = y / scale - Convert.ToSingle(Math.Floor(y / scale));
            float a = SineInterpolate(bottomRight, topRight, distance);

            //then another two points
            float b = SineInterpolate(topLeft, bottomLeft, distance);


            //and finally interpolates between these points to get the value of the current point.
            //I don't know if this results in prefect accuracy but it's good enough for our application
            distance = x / scale - Convert.ToSingle(Math.Floor(x / scale));
            return SineInterpolate(a, b, distance);
        }

        /// <summary>
        /// Combines the coordinates of the given position and the NoiseMap's Seed into a string and generates a predictable hash value of that string
        /// </summary>
        /// <param name="x">x coordinate of the given position</param>
        /// <param name="y">y coordinate of the given position</param>
        /// <param name="maxValue">Maximum value necessary to prevent the hash value from becoming impredictably large</param>
        /// <returns>hash value of the string containing the combination of the coordinates of the given position and the NoiseMap's Seed</returns>
        public float GeneratePsuedoRandomValue(double x, double y, float maxValue)
        {
            //a simple method that combines the imputs in a string and then generates a hash value for that string
            //this is desirable because hashvalues have strong variance in output with slight variance in input
            //a string with only a single differing letter will generate a widely differing number as output
            //the hash value will be an interger anywhere in the range of possible integers
            //that's why we use Math.Abs to make it positive and we take the modulo of the range to ensure it's below the desired maximum
            //we use this instead of Random() because we want the information to be chaotic but it still needs to be predictable

            string input = x.ToString() + y.ToString() + mapSeed;
            return Math.Abs(input.GetHashCode()) % maxValue;
        }


        //not accessable functionality

        /// <summary>
        /// Cosine interpolator used to calculate the desired point on a cosine wave that intersects with the two given datapoints
        /// </summary>
        /// <param name="input1">data point 1</param>
        /// <param name="input2">data point 2</param>
        /// <param name="step">value which defines where to estimate the desired point on the cosine wave</param>
        /// <returns>the desired point</returns>
        private float SineInterpolate(float input1, float input2, float step)
        {
            //this is the cosine interpolation used to generate the data between the random data points
            //if you want to understand the math I reccomend this amazing writeup: http://paulbourke.net/miscellaneous/interpolation/
            //basically we're calculating a desired point on a cosine wave that intersects with the two given datapoints

            double stepd = (double)step;
            double stepSquared = step * step;
            float stepSmooth = Convert.ToSingle(1 - Math.Cos(stepd * Math.PI)) / 2;

            return input1 * (1 - stepSmooth) + input2 * stepSmooth;
        }
    }
}
