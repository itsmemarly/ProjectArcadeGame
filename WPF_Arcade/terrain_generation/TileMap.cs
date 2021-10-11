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
    public class TileMap
    {
        private readonly int worldWidth; //width of the gameworld in tiles
        private readonly int worldHeight; //height of the gameworld in tiles
        private readonly int worldTileSize;
        private readonly Canvas worldCanvas;
        private string worldSeed; //seed used to generate the world
        private NoiseMap map1;
        private NoiseMap map2;
        private Tile[,] tileMap;

        private readonly Random r = new Random();

        public TileMap(int width, int height, int tileSize, string seed, Canvas world)
        {
            worldWidth = width;
            worldHeight = height;
            worldTileSize = tileSize;
            worldSeed = seed;
            worldCanvas = world;
            tileMap = new Tile[width, height];
        }

        //methods to set values
        public void SetSeed(string seed)
        {
            worldSeed = seed;
        }

        //methods to get values
        public int Width()
        {
            return worldWidth;
        }

        public int Height()
        {
            return worldHeight;
        }

        public string Seed()
        {
            return worldSeed;
        }

        public int TileSize()
        {
            return worldTileSize;
        }

        //accesible functionality
        public int ToTileCoordinate(int x)
        {
            return x / worldTileSize;
        }
        public bool isTileAtScreenCoordinate(int x, int y)
        {
            return IsTile(ToTileCoordinate(x), ToTileCoordinate(y));
        }
        public bool IsTile(int x, int y)
        {
            return tileMap[x, y] != null;
            
        }

        public void DeleteTile(int x, int y)
        {
            tileMap[x, y].Destroy();
            tileMap[x, y] = null;
        }

        public void DeleteTileAtScreenCoordinate(int x, int y)
        {
            DeleteTile(ToTileCoordinate(x), ToTileCoordinate(y));
        }

        //weightmap1 and weightmap2 determine what the range is for each map, and thus how strong their influence is on the final terrain generation
        //for more information about the resolution check NoiseMap.cs
        //the airWeight determines how likely each tile is to be empty/air

        public void Generate(int weightMap1, float resolutionMap1, int weightMap2, float resolutionMap2, int airWeight, int gemWeight)
        {
            //first we make two noise maps with differing weights and resolutions.
            //combining two different maps gives us finer control over aspects of the terrain
            //it also makes more interesting terrain.
            //a map with a high resolution will make big open/closed areas with lower local variance
            //a map with a low resolution will give a higher local variance with less cohesion.
            //Just think small open/closed areas
            map1 = new NoiseMap(worldWidth, worldHeight, weightMap1, resolutionMap1, worldSeed);
            map2 = new NoiseMap(worldWidth, worldHeight, weightMap2, resolutionMap2, worldSeed);

            //populate the maps
            map1.PopulateMap();
            map2.PopulateMap();

            //we also need to define the tiles we're using

            //now we loop over every tile in the level
            for (int x = 0; x < tileMap.GetLength(0); x++)
            {
                for (int y = 0; y < tileMap.GetLength(1); y++)
                {
                    //first we add together the values of the different noisemaps in this position to calculate the cumulative value
                    float totalNoiseAtPosition = map1.GetNoiseAt(x, y) + map2.GetNoiseAt(x, y);

                    //this combined value is a range of 0 to weightMap1 + weightMap2
                    //to create empty areas we simply ignore all the tiles where the noise does not exceed a certain value
                    if (totalNoiseAtPosition > airWeight)
                    {
                        Tile tile;
                        if (map1.GeneratePsuedoRandomValue(x, y, 100) < gemWeight)
                        {
                            tile = new Tile("gem", worldTileSize, x, y, worldCanvas, GameImageBitmaps.gem);
                        }
                        else
                        {
                            //make a new stone tile and add it to the tilemap
                            tile = new Tile("stone", worldTileSize, x, y, worldCanvas, GameImageBitmaps.stone);
                        }
                        tileMap[x, y] = tile;
                    }
                }
            }

        }

        public void Clear()
        {
            for (int x = 0; x < tileMap.GetLength(0); x++)
            {
                for (int y = 0; y < tileMap.GetLength(1); y++)
                {
                    if (tileMap[x, y] != null)
                    {
                        DeleteTile(x, y);
                    }
                }
            }
        }

        public void RandomSeed()
        {
            
            worldSeed = r.Next().ToString();
        }

        //not accesible functionality
    }
}
