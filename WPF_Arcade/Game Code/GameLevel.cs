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
    class GameLevel
    {

        //variables the class takes in the constructor
        private readonly int levelWidth;
        private readonly int levelHeight;
        private readonly int levelTileSize;
        private readonly Canvas levelCanvas;

        //variables determined in the constructor
        private readonly List<Enemy> levelEnemyList;
        private readonly List<Player> levelPlayerList;
        private readonly CollisionManager levelCollisionManager;
        private readonly TileMap levelTileMap;
        private readonly int levelTileMapTileWidth;
        private readonly int levelTileMapTileHeight;

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
        private readonly int levelPlayerCount = 2;
        private readonly int levelEnemyCount = 4;

        //variables internal to the class
        private string levelSeed = "";
        private int levelRandomCount = int.MinValue;
        private int levelActivePlayerIndex = 0;

        public GameLevel(int width, int height, int tileSize, Canvas canvas)
        {
            //set the properties to the right value
            levelWidth = width;
            levelHeight = height;
            levelTileSize = tileSize;
            levelCanvas = canvas;

            //create the internal properties
            //determine the size of the level in tiles
            levelTileMapTileWidth = (int)Math.Floor((double)(levelWidth / levelTileSize));
            levelTileMapTileHeight = (int)Math.Floor((double)(levelHeight / levelTileSize));

            //make a new tileMap that fits the size of the level
            levelTileMap = new TileMap(levelTileMapTileWidth, levelTileMapTileHeight, levelTileSize, levelSeed, levelCanvas);

            levelPlayerList = new List<Player>();
            levelEnemyList = new List<Enemy>();

            levelCollisionManager = new CollisionManager(levelTileMap, levelPlayerList, levelEnemyList);

        }
        //getters
        public Player ActivePlayer()
        {
            return levelPlayerList[levelActivePlayerIndex];
        }

        public string Seed()
        {
            return levelSeed;
        }

        //setters
        public void SetSeed(string seed)
        {
            levelSeed = seed;
        }

        public void ProcessInput(Key key)
        {
            TakePlayerAction(key);
        }

        //constructs the level
        public void BuildLevel()
        {
            //set the seed to a random value to ensure the level is random
            SetSeed(GeneratePsuedoRandomValue(int.MaxValue).ToString());
            //GenerateTerrain();
            AddPlayer(64, 64);
            AddPlayer(64, 128);
        }

        private void GenerateTerrain()
        {
            levelTileMap.Generate(levelNoiseMap1Weight, levelNoiseMap1Scale, levelNoiseMap2Weight, levelNoiseMap2Scale, levelAirChance, levelGemChance);
        }

        private void AddPlayer(int x, int y)
        {
            levelPlayerList.Add(new Player(x, y, levelPlayerActions, levelTileSize, GameImageBitmaps.player, levelCanvas, levelCollisionManager));
        }

        private void AddEnemy(int x, int y)
        {
            levelEnemyList.Add(new Enemy(x, y, levelEnemyActions, levelTileSize, GameImageBitmaps.goblin, levelCanvas, levelTileMap));
        }


        //MANAGING THE LEVEL
        private void TakePlayerAction(Key key)
        {
            switch (key)
            {
                case Key.A:
                    ActivePlayer().MoveLeft();
                    break;
                case Key.D:
                    ActivePlayer().MoveRight();
                    break;
                case Key.W:
                    ActivePlayer().MoveUp();
                    break;
                case Key.S:
                    ActivePlayer().MoveDown();
                    
                    break;

                default:
                    break;
            }
            EndTurnIfNeeded(ActivePlayer());
        }

        private void TakeEnemyTurns()
        {
            foreach (var enemy in levelEnemyList)
            {
                if (GeneratePsuedoRandomValue(100) > 50)
                {
                    enemy.MoveLeft();
                }

                else
                {
                    enemy.MoveRight();
                }
                enemy.ResetActionPoints();
            }
        }

        private void EndTurnIfNeeded(Player player)
        {
            if (player.Actionpoints() == 0)
            {
                levelActivePlayerIndex += 1;
                player.ResetActionPoints();

                if (levelActivePlayerIndex > levelPlayerList.Count - 1)
                {
                    levelActivePlayerIndex = 0;
                    TakeEnemyTurns();
                }

            }
        }

        //generates a value with high varience int the outputwith a small varience the input based on the seed and how many numbers have been previously generated
        private float GeneratePsuedoRandomValue(float maxValue)
        {
            string input = levelRandomCount.ToString() + levelSeed;
            levelRandomCount += 1;
            return Math.Abs(input.GetHashCode()) % maxValue;
        }

    }
}
