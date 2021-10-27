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
    class Player
    {
        private readonly int playerAttackCost = 2;
        private readonly int playerMoveCost = 1;
        private int playerHealth = 3;

        private int playerX;
        private int playerY;

        private readonly int playerSize;
        private int playerActionPoints;
        private readonly int playerStartActionPoints;
        private readonly Image playerImage;
        private readonly Canvas playerCanvas;
        private readonly BitmapImage playerBitmap;
        private readonly CollisionManager playerCollisionManager;




        public Player(int x, int y, int actions, int size, BitmapImage bitmap, Canvas canvas, CollisionManager collisionmanager)
        {
            playerX = x;
            playerY = y;
            playerActionPoints = actions;
            playerBitmap = bitmap;
            playerCanvas = canvas;
            playerSize = size;
            playerStartActionPoints = actions;
            playerCollisionManager = collisionmanager;



            playerImage = new Image
            {
                Tag = "playerImage",
                Height = playerSize,
                Width = playerSize,
                Source = playerBitmap
            };

            Canvas.SetLeft(playerImage, playerX);
            Canvas.SetTop(playerImage, playerY);
            playerCanvas.Children.Add(playerImage);
        }

        //getters
        public int Actionpoints()
        {
            return playerActionPoints;
        }

        public int X()
        {
            return playerX;
        }

        public int Y()
        {
            return playerY;
        }

        //move functions related to moving the player on the map
        public bool MoveUp()
        {
            //move the player if the move is valid
            return MoveIfValid(playerX, playerY - playerSize);
        }

        //Same goes for all the moves by player.
        public bool MoveDown()
        {
            return MoveIfValid(playerX, playerY + playerSize);
        }

        public bool MoveRight()
        {
            return MoveIfValid(playerX + playerSize, playerY);
        }

        public bool MoveLeft()
        {
            return MoveIfValid(playerX - playerSize, playerY);
        }

        //methods to take the destroy tile action
        public bool AttackRight()
        {
            return AttackIfValid(playerX + playerSize, playerY);
        }

        public bool AttackLeft()
        {
            return AttackIfValid(playerX - playerSize, playerY);
        }

        public bool AttackUp()
        {
            return AttackIfValid(playerX, playerY - playerSize);
        }

        public bool AttackDown()
        {
            return AttackIfValid(playerX, playerY + playerSize);
        }

        ////resets the action points back to their starting value
        public void ResetActionPoints()
        {
            playerActionPoints = playerStartActionPoints;
        }

        private void MoveTo(int destinationX, int destinationY)
        {
            playerY = destinationY;
            playerX = destinationX;
            playerActionPoints -= playerMoveCost;
            Canvas.SetTop(playerImage, playerY);
            Canvas.SetLeft(playerImage, playerX);
        }

        private bool MoveIfValid(int destinationX, int destinationY)
        {
            bool canMove = playerCollisionManager.IsValidDestination(destinationX, destinationY);
            if (canMove)
            {
                MoveTo(destinationX, destinationY);
            }

            return canMove;
        }

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
                    playerActionPoints -= playerAttackCost;
                    return true;
                }
                
                // attack enemy
                else if (thingAtTarget.GetType() == typeof(Enemy))
                {
                    playerActionPoints -= playerAttackCost;
                    Enemy enemy = (Enemy)thingAtTarget;
                    enemy.DamageOnEnemy();
                    return true;

                    //get points
                    //respawn enemy?

                }
                else if (thingAtTarget.GetType() == typeof(TileMap))
                {
                    playerActionPoints -= playerAttackCost;
                    TileMap map = (TileMap)thingAtTarget;
                    map.DeleteTileAtScreenCoordinate(x, y);
                    return true;
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

        // kills player
        private void KillPlayer()
        {
            playerCanvas.Children.Remove(playerImage);
        }

    }
}