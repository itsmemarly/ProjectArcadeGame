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
        /// <summary>
        /// Sets the Seed of the TileMap
        /// </summary>
        /// <param name="seed">the Seed of the TileMap</param>
        public void SetSeed(string seed)
        {
            worldSeed = seed;
        }

        //methods to get values
        /// <summary>
        /// Gets the width of the TileMap
        /// </summary>
        /// <returns>the width of the TileMap</returns>
        public int Width()
        {
            return worldWidth;
        }

        /// <summary>
        /// Gets the height of the TileMap
        /// </summary>
        /// <returns>the height of the TileMap</returns>
        public int Height()
        {
            return worldHeight;
        }

        /// <summary>
        /// Gets the Seed of the TileMap
        /// </summary>
        /// <returns>the Seed of the TileMap</returns>
        public string Seed()
        {
            return worldSeed;
        }

        /// <summary>
        /// Gets the size of the Tiles that are in the TileMap
        /// </summary>
        /// <returns>the size of the Tiles that are in the TileMap</returns>
        public int TileSize()
        {
            return worldTileSize;
        }

        //accesible functionality

        /// <summary>
        /// Translates the graphical coordinate of the Tile to the mathematical coordinate of the Tile
        /// </summary>
        /// <param name="x">the graphical coordinate of the Tile</param>
        /// <returns>the mathematical coordinate of the Tile</returns>
        public int ToTileCoordinate(int x)
        {
            return x / worldTileSize;
        }

        /// <summary>
        /// Checks if the given position contains a Tile
        /// </summary>
        /// <param name="x">graphical x coordinate of the Tile</param>
        /// <param name="y">graphical y coordinate of the Tile</param>
        /// <returns>true if the given position contains a Tile, false if the given position does not contain a Tile</returns>
        public bool isTileAtScreenCoordinate(int x, int y)
        {
            return IsTile(ToTileCoordinate(x), ToTileCoordinate(y));
        }

        /// <summary>
        /// Checks first if a Tile is located at the given location, 
        /// and if so gets the value of the kind of type of the located Tile
        /// </summary>
        /// <param name="x">graphical x coordinate of the Tile</param>
        /// <param name="y">graphical y coordinate of the Tile</param>
        /// <returns>the value of the kind of type of the located Tile</returns>
        public string getTileTypeAt(int x, int y)
        {
            if (tileMap[x, y] == null)
            {
                return "void";
            }
            return tileMap[x, y].Type();
        }

        /// <summary>
        /// Gets the value of the kind of type of located Tile at the graphical location
        /// </summary>
        /// <param name="x">graphical x coordinate of the Tile</param>
        /// <param name="y">graphical y coordinate of the Tile</param>
        /// <returns>the value of the kind of type of located Tile at the graphical location</returns>
        public string getTileTypeAtScreenCoordinate(int x, int y)
        {
            return getTileTypeAt(ToTileCoordinate(x), ToTileCoordinate(y));
        }

        /// <summary>
        /// Checks if there's a Tile at the given position
        /// </summary>
        /// <param name="x">x coordinate of the given position</param>
        /// <param name="y">y coordinate of the given position</param>
        /// <returns>
        /// true if there's a Tile at the given position, 
        /// false if there's not a Tile at the given position
        /// </returns>
        public bool IsTile(int x, int y)
        {
            return tileMap[x, y] != null;
        }

        /// <summary>
        /// Destroys Tile from the TileMap and from the Canvas at the given mathematical position
        /// </summary>
        /// <param name="x">x coordinate of the position</param>
        /// <param name="y">y coordinate of the position</param>
        public void DeleteTile(int x, int y)
        {
            tileMap[x, y].Destroy();
            tileMap[x, y] = null;
        }

        /// <summary>
        /// Destroys Tile from the TileMap and from the Canvas
        /// </summary>
        /// <param name="x">graphical x coordinate of the position</param>
        /// <param name="y">graphical y coordinate of the position</param>
        public void DeleteTileAtScreenCoordinate(int x, int y)
        {
            DeleteTile(ToTileCoordinate(x), ToTileCoordinate(y));
        }

        /// <summary>
        /// Checks if a Tile is in the Canvas and in the Level at the given position
        /// </summary>
        /// <param name="x">graphical x coordinate of the position</param>
        /// <param name="y">graphical y coordinate of the position</param>
        /// <returns>
        /// true if the Tile is in the Canvas and in the Level at the given position,
        /// false if the Tile is not in the Canvas and not in the Level at the given position
        /// </returns>
        public bool IsScreenCoordinateInLevel(int x, int y)
        {
            bool xInLevel = ToTileCoordinate(x) >= 0 && ToTileCoordinate(x) < tileMap.GetLength(0);
            bool yInLevel = ToTileCoordinate(y) >= 0 && ToTileCoordinate(y) < tileMap.GetLength(1);
            if (xInLevel && yInLevel)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Generates the TileMap of the Level
        /// </summary>
        /// <param name="weightMap1">weight of NoiseMap 1</param>
        /// <param name="resolutionMap1">resolution of NoiseMap 1</param>
        /// <param name="weightMap2">weight of NoiseMap 2</param>
        /// <param name="resolutionMap2">resolution of NoiseMap 2</param>
        /// <param name="airWeight">air weight in the generated TileMap</param>
        /// <param name="gemWeight">gem weight in the generated TileMap</param>
        public void Generate(int weightMap1, float resolutionMap1, int weightMap2, float resolutionMap2, int airWeight, int gemWeight)
        {
            //weightmap1 and weightmap2 determine what the range is for each map,
            //and thus how strong their influence is on the final terrain generation
            //for more information about the resolution check NoiseMap.cs
            //the airWeight determines how likely each tile is to be empty/air

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

        /// <summary>
        /// Clears the entire TileMap of all of Its Tiles
        /// </summary>
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

        /// <summary>
        /// Creates a random Seed as a string
        /// </summary>
        public void RandomSeed()
        {
            
            worldSeed = r.Next().ToString();
        }

        //not accesible functionality
    }
}
