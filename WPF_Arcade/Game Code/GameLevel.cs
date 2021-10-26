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
        private readonly CollisionManager levelCollisionManager;
        private readonly TurnManager levelTurnManager;
        private readonly TileMap levelTileMap;

        private readonly List<Enemy> levelEnemyList;
        private readonly List<Player> levelPlayerList;

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

        public GameLevel(int width, int height, int tileSize, Canvas canvas)
        {
            //set the properties to the right value
            levelWidth = width;
            levelHeight = height;
            levelTileSize = tileSize;
            levelCanvas = canvas;

            //create the internal properties
            //determine the size of the level in tiles
            int levelTileMapTileWidth = (int)Math.Floor((double)(levelWidth / levelTileSize));
            int levelTileMapTileHeight = (int)Math.Floor((double)(levelHeight / levelTileSize));

            //make a new tileMap that fits the size of the level
            levelTileMap = new TileMap(levelTileMapTileWidth, levelTileMapTileHeight, levelTileSize, levelSeed, levelCanvas);

            levelPlayerList = new List<Player>();
            levelEnemyList = new List<Enemy>();

            levelCollisionManager = new CollisionManager(levelTileMap, levelPlayerList, levelEnemyList);
            levelTurnManager = new TurnManager(levelPlayerList, levelEnemyList, levelSeed);
        }
        //getters

        public string Seed()
        {
            return levelSeed;
        }

        //setters
        public void SetSeed(string seed)
        {
            levelSeed = seed;
            levelTileMap.SetSeed(seed);
            levelTurnManager.SetSeed(seed);
        }

        public void ProcessInput(Key key)
        {
            levelTurnManager.TakePlayerAction(key);
        }

        //constructs the level
        public void BuildLevel()
        {
            //set the seed to a random value to ensure the level is random
            Random r = new Random();
            SetSeed(r.Next().ToString());

            GenerateTerrain();
            AddPlayer(64, 64);
            AddPlayer(64, 128);
            AddEnemy(64, 448);

            levelTurnManager.SetActivePlayerVisual();
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
            levelEnemyList.Add(new Enemy(x, y, levelEnemyActions, levelTileSize, GameImageBitmaps.goblin, levelCanvas, levelCollisionManager));
        }

    }
}
