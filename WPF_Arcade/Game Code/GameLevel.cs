﻿using System;
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
    public class GameLevel
    {

        //variables the class takes in the constructor
        private readonly int levelWidth;
        private readonly int levelHeight;
        private readonly int levelTileSize;
        private readonly Canvas levelCanvas;
        private readonly TextBlock levelPlayer1ScoreText;
        private readonly TextBlock levelPlayer1TurnText;
        private readonly TextBlock levelPlayer2ScoreText;
        private readonly TextBlock levelPlayer2TurnText;

        //variables determined in the constructor
        private readonly CollisionManager levelCollisionManager;
        private readonly TurnManager levelTurnManager;
        private readonly TileMap levelTileMap;

        private readonly List<Enemy> levelEnemyList;
        private readonly List<Player> levelPlayerList;
        private readonly Exit levelExit;

        //properites of the generated terrain
        //these have been picked after some experimentation because they generate the kind of terrain I think works well with the game
        private readonly int levelNoiseMap1Weight = 75; //how strongly the first noise map affects terrain generation
        private readonly int levelNoiseMap2Weight = 25; //how strongly the second noise map affects terrain generation
        private readonly int levelNoiseMap1Scale = 2; //Scale influences how big the patches of stone or air are
        private readonly int levelNoiseMap2Scale = 3;
        private readonly int levelAirChance = 40; //the value a number needs to exceed to become air
        private readonly int levelGemChance = 10; //the chance in percentages to generate a gem in a stone tile


        //determines aspects of the generated entities
        private readonly int levelPlayerActions = 5;
        private readonly int levelEnemyActions = 1; //curently not implemented for heigher values
        private readonly int levelEnemyChance = 10;

        //variables internal to the class
        private string levelSeed = "";
        private int levelRandomCount = int.MinValue;

        public GameLevel(int width, int height, int tileSize, Canvas canvas, TextBlock P1Score, TextBlock P1Turn, TextBlock P2Score, TextBlock P2Turn)
        {
            //set the properties to the right value
            levelWidth = width;
            levelHeight = height;
            levelTileSize = tileSize;
            levelCanvas = canvas;

            levelPlayer1ScoreText = P1Score;
            levelPlayer1TurnText = P1Turn;
            levelPlayer2ScoreText = P2Score;
            levelPlayer2TurnText = P2Turn;

            //create the internal properties
            //determine the size of the level in tiles
            int levelTileMapTileWidth = (int)Math.Floor((double)(levelWidth / levelTileSize));
            int levelTileMapTileHeight = (int)Math.Floor((double)(levelHeight / levelTileSize));

            //make a new tileMap that fits the size of the level
            levelTileMap = new TileMap(levelTileMapTileWidth, levelTileMapTileHeight, levelTileSize, levelSeed, levelCanvas);

            levelPlayerList = new List<Player>();
            levelEnemyList = new List<Enemy>();

            levelExit = new Exit(0, 0, levelTileSize, levelCanvas, this);

            levelCollisionManager = new CollisionManager(levelTileMap, levelPlayerList, levelEnemyList, levelExit);
            levelTurnManager = new TurnManager(levelPlayerList, levelEnemyList, levelSeed);
        }
        //getters

        /// <summary>
        /// Gets the Level's Seed
        /// </summary>
        /// <returns>the Level's Seed</returns>
        public string Seed()
        {
            return levelSeed;
        }

        //setters
        /// <summary>
        /// Sets the Level's Seed
        /// </summary>
        /// <param name="seed">the Level's Seed</param>
        public void SetSeed(string seed)
        {
            levelSeed = seed;
            levelTileMap.SetSeed(seed);
            levelTurnManager.SetSeed(seed);
        }

        /// <summary>
        /// Processes the Players' moves based on user input (WASD/Arrow keys)
        /// </summary>
        /// <param name="key">user input (WASD/Arrow keys)</param>
        public void ProcessInput(Key key)
        {
            levelTurnManager.TakePlayerAction(key);
        }

        /// <summary>
        /// Saves the Players' Scores
        /// </summary>
        public void SaveScores()
        {

        }

        /// <summary>
        /// Constructs the Level
        /// </summary>
        public void BuildLevel()
        {
            //set the seed to a random value to ensure the level is random
            Random r = new Random();
            SetSeed(r.Next().ToString());

            GenerateTerrain();
            PlacePlayers();
            PlaceEnemies(levelEnemyChance);
            PlaceExit();
        }

        /// <summary>
        /// Generates the visual Terrain
        /// </summary>
        private void GenerateTerrain()
        {
            levelTileMap.Generate(levelNoiseMap1Weight, levelNoiseMap1Scale, levelNoiseMap2Weight, levelNoiseMap2Scale, levelAirChance, levelGemChance);
        }

        /// <summary>
        /// Adds Player to the Player List, puts Player at the given position and removes Tile at the given position if needed
        /// </summary>
        /// <param name="x">graphical x coordinate of Player</param>
        /// <param name="y">graphical y coordinate of Player</param>
        /// <param name="scoreText">TextBlock to view Player's Score in the UI</param>
        /// <param name="turnText">TextBlock to view Player's remaining Turns in comparison to initial turn amount in the UI</param>
        /// <param name="name">Player's Name (either "Player 1" or "Player 2")</param>
        private void AddPlayer(int x, int y, TextBlock scoreText, TextBlock turnText, String name)
        {
            levelPlayerList.Add(new Player(x, y, levelPlayerActions, levelTileSize, GameImageBitmaps.player, levelCanvas, levelCollisionManager, turnText, scoreText, name));
            if (levelTileMap.isTileAtScreenCoordinate(x, y))
            {
                levelTileMap.DeleteTileAtScreenCoordinate(x, y);
            }
        }

        /// <summary>
        /// Places the Players at their desired positions
        /// </summary>
        private void PlacePlayers()
        {
            //calculate starting x position for the players
            //first caluclate the middle of the screen
            int halfWidth = levelWidth / 2;
            int nearestTileToMiddle = halfWidth - (halfWidth % levelTileSize);

            AddPlayer(nearestTileToMiddle - levelTileSize, 64, levelPlayer1ScoreText, levelPlayer1TurnText, "Player 1");
            AddPlayer(nearestTileToMiddle + levelTileSize, 64, levelPlayer2ScoreText, levelPlayer2TurnText, "Player 2");

            foreach (var player in levelPlayerList)
            {
                player.SetInactive();
            }
            levelPlayerList[0].SetActive();
        }

        /// <summary>
        /// Places the Enemies where no Tile is present. This positioning depends on luck as well
        /// </summary>
        /// <param name="chance">the luck factor</param>
        private void PlaceEnemies(int chance)
        {
            Random r = new Random();

            //loop over the level to find empty tile coordinates and put them all in a list;
            for (int x = 0; x < levelTileMap.Width(); x++)
            {
                for (int y = 0; y < levelTileMap.Height(); y++)
                {
                    if (!levelTileMap.IsTile(x, y) && GeneratePsuedoRandomValue(x, y, 100) < chance)
                    {
                        AddEnemy(x * levelTileSize, y * levelTileSize);
                    }
                }
            }
        }

        /// <summary>
        /// Places the Gem at its destined coordinates in the Terrain, and removes any interfering Tile
        /// </summary>
        private void PlaceExit()
        {
            //first caluclate the middle of the screen
            int halfWidth = levelWidth / 2;
            int nearestTileToMiddle = halfWidth - (halfWidth % levelTileSize);

            int bottomOfScreen = levelHeight - (levelHeight % levelTileSize) - levelTileSize;
            //bottomOfScreen = 128;
            levelExit.MoveTo(nearestTileToMiddle, bottomOfScreen);

            if (levelTileMap.isTileAtScreenCoordinate(nearestTileToMiddle, bottomOfScreen))
            {
                levelTileMap.DeleteTileAtScreenCoordinate(nearestTileToMiddle, bottomOfScreen);
            }
        }

        /// <summary>
        /// Adds Enemy to the Enemy List and puts It at Its given position
        /// </summary>
        /// <param name="x">x coordinate of the position</param>
        /// <param name="y">y coordinate of the position</param>
        private void AddEnemy(int x, int y)
        {
            levelEnemyList.Add(new Enemy(x, y, levelEnemyActions, levelTileSize, GameImageBitmaps.goblin, levelCanvas, levelCollisionManager));
        }

        /// <summary>
        /// Generates a pseudo random value based on the absolute hash of the combination of the coordinates of the Enemy's position and the Seed, 
        /// however this value is not larger than maxValue
        /// </summary>
        /// <param name="x">x coordinate of the position of the Enemy</param>
        /// <param name="y">y coordinate of the position of the Enemy</param>
        /// <param name="maxValue">The limit to how large the pseudo random value can be</param>
        /// <returns>the pseudo random value</returns>
        private float GeneratePsuedoRandomValue(double x, double y, float maxValue)
        {
            string input = x.ToString() + y.ToString() + levelSeed;
            return Math.Abs(input.GetHashCode()) % maxValue;
        }
    }
}
