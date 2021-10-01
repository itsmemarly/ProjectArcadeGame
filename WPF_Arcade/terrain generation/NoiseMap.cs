using System;
using System.Collections.Generic;
using System.Text;

namespace WPF_Arcade
{
    public class NoiseMap
    {
        private float[,] map;
        private int mapHeight;
        private int mapWidth;
        private int mapMaxValue;
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
        public void SetSeed(string seed)
        {
            mapSeed = seed;
        }

        //methods to get values
        //each of these mentions can be called to get information about the noise map
        public int Height()
        {
            return mapHeight;
        }

        public int Width()
        {
            return mapWidth;
        }

        public string Seed()
        {
            return mapSeed;
        }

        public int MaxValue()
        {
            return mapMaxValue;
        }

        public float GetNoiseAt(int x, int y)
        {
            return map[x, y];
        }

        //accessable functionality

        //this method loops over the entire tilemap which is a 2d array
        //it then fills each position with a value
        //these values combined form the noise map
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

        //this method is the core of creating a noise map.
        //it zooms in on 2d array of purely random values and then uses cosine interpolation to fill the empty space in between
        //it only uses the four nearest values as that's all that cosine interpolation needs to get a good result
        //zooming in happens by deviding by the zoom scale, then flooring/ cieling the number to get the nearest whole integer.
        //There's probably faster/ cleaner ways to do it but this is what I chose
        public float GetSmoothNoiseAtPosition(int x, int y, float scale, int maxValue)
        {
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
            distance = x / scale - Convert.ToSingle(Math.Floor(x / scale));
            
            //and finally interpolates between these points to get the value of the current point.
            //I don't know if this results in prefect accuracy but it's good enough for our application
            return SineInterpolate(a, b, distance);
        }

        //not accessable functionality

        //this is the cosine interpolation used to generate the data between the random data points
        //if you want to understand the math I reccomend this amazing writeup: http://paulbourke.net/miscellaneous/interpolation/
        //basically we're calculating a desired point on a cosine wave that intersects with the two given datapoints
        private float SineInterpolate(float input1, float input2, float step)

        {
            double stepd = (double)step;
            double stepSquared = step * step;
            float stepSmooth = Convert.ToSingle(1 - Math.Cos(stepd * Math.PI)) / 2;

            return input1 * (1 - stepSmooth) + input2 * stepSmooth;
        }

        //a simple method that combines the imputs in a string and then generates a hash value for that string
        //this is desirable because hashvalues have strong variance in output with slight variance in input
        //a string with only a single differing letter will generate a widely differing number as output
        //the hash value will be an interger anywhere in the range of possible integers
        //that's why we use Math.Abs to make it positive and we take the modulo of the range to ensure it's below the desired maximum
        //we use this instead of Random() because we want the information to be chaotic but it still needs to be predictable
        private float GeneratePsuedoRandomValue(double x, double y, float maxValue)
        {
            string input = x.ToString() + y.ToString() + mapSeed;
            return Math.Abs(input.GetHashCode()) % maxValue;
        }

    }
}
