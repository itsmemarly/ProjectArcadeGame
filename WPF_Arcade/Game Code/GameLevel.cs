using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
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

        public void SaveScores()
        {

            //Link to DB
            OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\reidi\OneDrive\Documents\testdb_old.mdb");

            //Open connection
            con.Open();

            //Add new adapter for Scores
            OleDbCommand cmd = new OleDbCommand("INSERT INTO Speler1 (Naam1, Naam2, Score1, Score2) VALUES (' Player 1 ', 'Player 2'," + levelPlayerList[0].Score() + ", " + levelPlayerList[1].Score() + ")", con);

            //Send cmd to DB
            cmd.ExecuteNonQuery();

            //Close connection
            con.Close();

        }

        //constructs the level
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

        private void GenerateTerrain()
        {
            levelTileMap.Generate(levelNoiseMap1Weight, levelNoiseMap1Scale, levelNoiseMap2Weight, levelNoiseMap2Scale, levelAirChance, levelGemChance);
        }

        private void AddPlayer(int x, int y, TextBlock scoreText, TextBlock turnText, String name)
        {
            levelPlayerList.Add(new Player(x, y, levelPlayerActions, levelTileSize, GameImageBitmaps.player, levelCanvas, levelCollisionManager, turnText, scoreText, name));
            if (levelTileMap.isTileAtScreenCoordinate(x, y))
            {
                levelTileMap.DeleteTileAtScreenCoordinate(x, y);
            }

        }

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

        private void AddEnemy(int x, int y)
        {
            levelEnemyList.Add(new Enemy(x, y, levelEnemyActions, levelTileSize, GameImageBitmaps.goblin, levelCanvas, levelCollisionManager));
        }

        private float GeneratePsuedoRandomValue(double x, double y, float maxValue)
        {
            string input = x.ToString() + y.ToString() + levelSeed;
            return Math.Abs(input.GetHashCode()) % maxValue;
        }

    }
}
