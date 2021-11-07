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
    class Player
    {

        // Player's Properties
        private readonly int playerAttackCost = 2;
        private readonly int playerMoveCost = 1;
        private int playerHealth = 3;

        private int playerX;
        private int playerY;
        private int playerScore = 0;

        private readonly int playerSize;
        private int playerActionPoints;
        private readonly int playerStartActionPoints;
        private readonly string playerName;
        private readonly Image playerImage;
        private readonly Canvas playerCanvas;
        private readonly BitmapImage playerBitmap;
        private readonly CollisionManager playerCollisionManager;
        private readonly TextBlock playerTurnCounter;
        private readonly TextBlock playerScoreLabel;

        public Player(int x, int y, int actions, int size, BitmapImage bitmap, Canvas canvas, CollisionManager collisionmanager, TextBlock turnCounter, TextBlock Score, String name)
        {
            playerX = x;
            playerY = y;
            playerActionPoints = actions;
            playerBitmap = bitmap;
            playerCanvas = canvas;
            playerSize = size;
            playerStartActionPoints = actions;
            playerCollisionManager = collisionmanager;
            playerTurnCounter = turnCounter;
            playerScoreLabel = Score;
            playerName = name;

            playerImage = new Image
            {
                Tag = name,
                Height = playerSize,
                Width = playerSize,
                Source = playerBitmap
            };

            Canvas.SetLeft(playerImage, playerX);
            Canvas.SetTop(playerImage, playerY);
            playerCanvas.Children.Add(playerImage);
        }

        //getters

        /// <summary>
        /// Gets Player's Action Points (read: remaining Turns)
        /// </summary>
        /// <returns>Player's Action Points (read: remaining Turns)</returns>
        public int Actionpoints()
        {
            return playerActionPoints;
        }

        /// <summary>
        /// Gets Player's x coordinate
        /// </summary>
        /// <returns>Player's x coordinate</returns>
        public int X()
        {
            return playerX;
        }

        /// <summary>
        /// Gets Player's y coordinate
        /// </summary>
        /// <returns>Player's y coordinate</returns>
        public int Y()
        {
            return playerY;
        }

        /// <summary>
        /// Checks if Player can move 1 up
        /// </summary>
        /// <returns>true if Player is not hindered to move 1 up, false if otherwise</returns>
        public bool MoveUp()
        {
            //move the player if the move is valid
            return MoveIfValid(playerX, playerY - playerSize);
        }

        /// <summary>
        /// Checks if Player can move 1 down
        /// </summary>
        /// <returns>true if Player is not hindered to move 1 down, false if otherwise</returns>
        public bool MoveDown()
        {
            return MoveIfValid(playerX, playerY + playerSize);
        }

        /// <summary>
        /// Checks if Player can move 1 right
        /// </summary>
        /// <returns>true if Player is not hindered to move 1 right, false if otherwise</returns>
        public bool MoveRight()
        {
            return MoveIfValid(playerX + playerSize, playerY);
        }

        /// <summary>
        /// Checks if Player can move 1 left
        /// </summary>
        /// <returns>true if Player is not hindered to move 1 left, false if otherwise</returns>
        public bool MoveLeft()
        {
            return MoveIfValid(playerX - playerSize, playerY);
        }

        /// <summary>
        /// Checks if Player can attack/mine 1 right
        /// </summary>
        /// <returns>true if Player is not hindered to attack/mine 1 right, false if otherwise</returns>
        public bool AttackRight()
        {
            return AttackIfValid(playerX + playerSize, playerY);
        }

        /// <summary>
        /// Checks if Player can attack/mine 1 left
        /// </summary>
        /// <returns>true if Player is not hindered to attack/mine 1 left, false if otherwise</returns>
        public bool AttackLeft()
        {
            return AttackIfValid(playerX - playerSize, playerY);
        }

        /// <summary>
        /// Checks if Player can attack/mine 1 up
        /// </summary>
        /// <returns>true if Player is not hindered to attack/mine 1 up, false if otherwise</returns>
        public bool AttackUp()
        {
            return AttackIfValid(playerX, playerY - playerSize);
        }

        /// <summary>
        /// Checks if Player can attack/mine 1 down
        /// </summary>
        /// <returns>true if Player is not hindered to attack/mine 1 down, false if otherwise</returns>
        public bool AttackDown()
        {
            return AttackIfValid(playerX, playerY + playerSize);
        }

        /// <summary>
        /// Resets Player's Turns back to starting value and displays this amount
        /// </summary>
        public void ResetActionPoints()
        {
            playerActionPoints = playerStartActionPoints;
            playerTurnCounter.Text = playerName + " : " + playerActionPoints.ToString() + "/5";
        }

        /// <summary>
        /// Updates and displays Player's remaining Turns
        /// </summary>
        /// <param name="val">Player's remaining Turns</param>
        private void UpdateActionPoints(int val)
        {
            playerActionPoints += val;
            playerTurnCounter.Text = playerName + " : " + playerActionPoints.ToString() + "/5";
        }

        /// <summary>
        /// Changes Player's coordinates to given destination coordinates and updates Canvas accordingly
        /// </summary>
        /// <param name="destinationX">x coordinate of destination</param>
        /// <param name="destinationY">y coordinate of destination</param>
        private void MoveTo(int destinationX, int destinationY)
        {
            playerY = destinationY;
            playerX = destinationX;
            UpdateActionPoints(-playerMoveCost);
            Canvas.SetTop(playerImage, playerY);
            Canvas.SetLeft(playerImage, playerX);
        }

        /// <summary>
        /// Checks if Player can move to the destination indicated by the destination's coordinates
        /// </summary>
        /// <param name="destinationX">x coordinate of destination</param>
        /// <param name="destinationY">y coordinate of destination</param>
        /// <returns></returns>
        private bool MoveIfValid(int destinationX, int destinationY)
        {
            // check if nothing stands in the destinated location, so Player can move to the destinated location
            bool canMove = playerCollisionManager.IsValidDestination(destinationX, destinationY);
            if (canMove)
            {
                MoveTo(destinationX, destinationY);
            }

            return canMove;
        }

        /// <summary>
        /// Checks if Player can attack/mine/achieve Exit to the destination indicated by the destination's coordinates
        /// </summary>
        /// <param name="x">x coordinate of destination</param>
        /// <param name="y">y coordinate of destination</param>
        /// <returns></returns>
        private bool AttackIfValid(int x, int y)
        {
            //if you have enough action points left to attack
            if (playerActionPoints >= playerAttackCost)
            {
                //see what's at the destination of your attack
                object thingAtTarget = playerCollisionManager.getThingAt(x, y);

                //check if there's even something at the target
                if (thingAtTarget == null)
                {
                    return false;
                }

                // attacks other player
                else if (thingAtTarget.GetType() == typeof(Player))
                {
                    UpdateActionPoints(-playerAttackCost);
                    return true;
                }
                
                // attack enemy
                else if (thingAtTarget.GetType() == typeof(Enemy))
                {
                    UpdateActionPoints(-playerAttackCost);
                    Enemy enemy = (Enemy)thingAtTarget;
                    bool kill = enemy.DamageOnEnemy();

                    // if Player kills Enemy, then Player gets 30 points added to Its current Score
                    if (kill)
                    {
                        AddToScore(PlayerActionScores.destroyEnemy);
                    }
                    return true;

                    //get points
                    //respawn enemy?

                }
                else if (thingAtTarget.GetType() == typeof(TileMap))
                {
                    UpdateActionPoints(-playerAttackCost);
                    TileMap map = (TileMap)thingAtTarget;
                    string targetTileType = map.getTileTypeAtScreenCoordinate(x, y);

                    // if Player mines a gem vein, then Player gets 10 points added to Its current Score
                    if (targetTileType == "gem")
                    {
                        AddToScore(PlayerActionScores.destroyGem);
                    }
                    else if (targetTileType == "stone")
                    {
                        AddToScore(PlayerActionScores.destroyStone);
                    }

                    map.DeleteTileAtScreenCoordinate(x, y);
                    return true;
                }

                // if Player is at the Exit, then Player gets 100 points added to Its current Score and the Game is over
                else if (thingAtTarget.GetType()== typeof(Exit))
                {
                    Exit exit = (Exit)thingAtTarget;
                    AddToScore(PlayerActionScores.win);
                    exit.EndGame();
                }
            }

            //if none of the other return statements were reached, it means that we attack nothing. Thus we will return false
            return false;
        }

        //Damages player, kills him if necissary 
        public void DamageOnPlayer()
        {
            playerHealth -= 1;

            if (playerHealth <= 0)
            {
                KillPlayer();
            }
            else
            {
                //move player backwards
            }
        }

        /// <summary>
        /// Increases Player's Score by given amount, and updates Player's Score Label to show this increment
        /// </summary>
        /// <param name="amount">Given amount to add up to Score, depending on the situation (Win adds 100 up to Score, and
        /// destroyed gem adds 10 up to Score)</param>
        private void AddToScore(int amount)
        {
            playerScore += amount;
            playerScoreLabel.Text = playerName + " Score: " + playerScore.ToString();
        }

        /// <summary>
        /// Kills Player and removes It from the Canvas
        /// </summary>
        private void KillPlayer()
        {
            playerCanvas.Children.Remove(playerImage);
        }

        /// <summary>
        /// Sets the Player as active and makes It visible on the Canvas
        /// </summary>
        public void SetActive()
        {
            playerImage.Opacity = 1;
        }

        /// <summary>
        /// Sets the Player as inactive and makes It invisible on the Canvas
        /// </summary>
        public void SetInactive()
        {
            playerImage.Opacity = 0.5;
        }
    }
}
