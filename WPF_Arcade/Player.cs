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

        private readonly int playerSize;
        private readonly TileMap playerMap;
        private readonly int playerDestroyTileCost = 1;
        private readonly int playerAttackCost = 1;
        private readonly int playerMoveCost = 1;
        private int playerX;
        private int playerY;
        private int playerActionPoints;
        private readonly Image playerImage;
        private readonly Canvas playerCanvas;
        private readonly BitmapImage playerBitmap;
        private readonly int playerStartActionPoints;

        public Player(int x, int y, int actions, BitmapImage bitmap, Canvas canvas, int size, TileMap map)
        {
            playerX = x;
            playerY = y;
            playerActionPoints = actions;
            playerBitmap = bitmap;
            playerCanvas = canvas;
            playerMap = map;
            playerSize = size;
            playerStartActionPoints = actions;
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

        //move functions related to moving the player on the map
        public bool MoveUp()
        {
            //move the player if the move is valid
            return MoveIfValid(playerX, playerY - playerMap.TileSize());
        }

        //Same goes for all the moves by player.
        public bool MoveDown()
        {
            return MoveIfValid(playerX, playerY + playerMap.TileSize());
        }

        public bool MoveRight()
        {
            return MoveIfValid(playerX + playerMap.TileSize(), playerY);
        }

        public bool MoveLeft()
        {
            return MoveIfValid(playerX - playerMap.TileSize(), playerY);
        }

           
        //methods to take the destroy tile action
        public bool DestroyTileRight()
        {
            return DestroyIfValid(playerX + playerMap.TileSize(), playerY);
        }

        public bool DestroyTileLeft()
        {
            return DestroyIfValid(playerX - playerMap.TileSize(), playerY);
        }

        public bool DestroyTileUp()
        {
            return DestroyIfValid(playerX, playerY - playerMap.TileSize());
        }

        public bool DestroyTileDown()
        {
            return DestroyIfValid(playerX, playerY + playerMap.TileSize());
        }

        //resets the action points back to their starting value
        public void ResetActionPoints()
        {
            playerActionPoints = playerStartActionPoints;
        }


        //checks if a move a player wants to make is valid
        private bool CanMoveTo(int destinationX, int destinationY)
        {
            //first check if the destination is in the level
            if (playerMap.IsScreenCoordinateInLevel(destinationX, destinationY))
            {
                //if the destination is within the level, check if there's not a tile there and if the player has enough action points to move.
                if (!playerMap.isTileAtScreenCoordinate(destinationX, destinationY) && playerActionPoints >= playerMoveCost)
                {
                    return true;
                }
            }
            return false;
        }

        //Move to removes set ammount of action points and moves the player
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
            bool canMove = CanMoveTo(destinationX, destinationY);
            if (canMove)
            {
                MoveTo(destinationX, destinationY);
            }

            return canMove;
        }

        private bool CanDestroyTile(int destructionX, int destructionY)
        {
            if (playerMap.IsScreenCoordinateInLevel(destructionX, destructionY))
            {
                if (playerActionPoints >= playerDestroyTileCost && playerMap.isTileAtScreenCoordinate(destructionX, destructionY))
                {
                    return true;
                }
            }
            return false;
        }

        private void DestroyTile(int destructionX, int destructionY)
        {
            playerActionPoints -= playerDestroyTileCost;
            playerMap.DeleteTileAtScreenCoordinate(playerX, playerY);
        }

        private bool DestroyIfValid(int destructionX, int destructionY)
        {
            bool moveIsValid = CanDestroyTile(destructionX, destructionY);
            if (moveIsValid)
            {
                DestroyTile(destructionX, destructionY);
            }
            return moveIsValid;
        }


        //public bool Attack() 
        //{

        //}
    }
}
