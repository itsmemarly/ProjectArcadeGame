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
        private readonly int playerDestroyTileCost = 2;
        private readonly int playerAttackCost = 2;
        private readonly int playerMoveCost = 1;

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
        //public bool DestroyTileRight()
        //{
        //    return DestroyIfValid(playerX + playerSize, playerY);
        //}

        //public bool DestroyTileLeft()
        //{
        //    return DestroyIfValid(playerX - playerSize, playerY);
        //}

        //public bool DestroyTileUp()
        //{
        //    return DestroyIfValid(playerX, playerY - playerSize);
        //}

        //public bool DestroyTileDown()
        //{
        //    return DestroyIfValid(playerX, playerY + playerSize);
        //}

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

        //private bool CanDestroyTile(int destructionX, int destructionY)
        //{
        //    if (playerMap.IsScreenCoordinateInLevel(destructionX, destructionY))
        //    {
        //        if (playerActionPoints >= playerDestroyTileCost && playerMap.isTileAtScreenCoordinate(destructionX, destructionY))
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        //private void DestroyTile(int destructionX, int destructionY)
        //{
        //    playerActionPoints -= playerDestroyTileCost;
        //    playerMap.DeleteTileAtScreenCoordinate(playerX, playerY);
        //}

        //private bool DestroyIfValid(int destructionX, int destructionY)
        //{
        //    bool moveIsValid = CanDestroyTile(destructionX, destructionY);
        //    if (moveIsValid)
        //    {
        //        DestroyTile(destructionX, destructionY);
        //    }
        //    return moveIsValid;
        //}


        //public bool Attack() 
        //{

        //}
    }
}
