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

        public Player(int x, int y, int actions, BitmapImage bitmap, Canvas canvas, int size, TileMap map)
        {
            playerX = x;
            playerY = y;
            playerActionPoints = actions;
            playerBitmap = bitmap;
            playerCanvas = canvas;
            playerMap = map;
            playerSize = size;

            playerImage = new Image
            {
                Tag = "playerImage",
                Height = size,
                Width = size,
                Source = bitmap
            };
            Canvas.SetLeft(playerImage, playerX);
            Canvas.SetTop(playerImage, playerY);
            playerCanvas.Children.Add(playerImage);
        }


        //public void doStuff()
        //{

        //}



        public bool MoveUp()
        {
            bool targetTileExist = playerMap.isTileAtScreenCoordinate(playerX, playerY - playerSize);            //Selects place on grid player wants to move to.
            if (CanMoveTo(targetTileExist))                                                                      //Checks if there's a tile blocking the players move.
            {
                MoveTo(playerX, playerY - playerSize);                                                           //Moves player and removes action point.
                return true;                                                                                     //Same goes for all the moves by player.
            }
            else
            {
                return false;     
            }

        }

        public bool MoveDown()
        {
            bool targetTileExist = playerMap.isTileAtScreenCoordinate(playerX, playerY + playerSize);
            if (CanMoveTo(targetTileExist))
                {
                MoveTo(playerX, playerY + playerSize);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool MoveRight()
        {
            bool targetTileExist = playerMap.isTileAtScreenCoordinate(playerX + playerSize, playerY);
            if (CanMoveTo(targetTileExist))
            {
                MoveTo(playerX + playerSize, playerY);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool MoveLeft()
        {
            bool targetTileExist = playerMap.isTileAtScreenCoordinate(playerX - playerSize, playerY);
            if (CanMoveTo(targetTileExist))
            {
                MoveTo(playerX - playerSize, playerY);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DestroyTileRight()
        {
            bool targetTileExist = playerMap.isTileAtScreenCoordinate(playerX + playerSize, playerY);              //Selects place on grid player wants to destroy a tile.
            if (CanDestroyTile(targetTileExist))                                                                   //Checks if there is a tile.
            {
                playerMap.DeleteTileAtScreenCoordinate(playerX + playerSize, playerY);                             //Destroy's tile
                playerActionPoints -= playerDestroyTileCost;                                                       //Lowers action points
                return true;                                                                                       //Same goes for all Destroy's by player 

            }
            else
            {
                return false;
            }
        }
        public bool DestroyTileLeft()
        {
            bool targetTileExist = playerMap.isTileAtScreenCoordinate(playerX - playerSize, playerY);
            if (CanDestroyTile(targetTileExist))
            {
                playerMap.DeleteTileAtScreenCoordinate(playerX - playerSize, playerY);
                playerActionPoints -= playerDestroyTileCost;
                return true;

            }
            else
            {
                return false;
            }
        }

        public bool DestroyTileUp()
        {
            bool targetTileExist = playerMap.isTileAtScreenCoordinate(playerX, playerY - playerSize);
            if (CanDestroyTile(targetTileExist))
            {
                playerMap.DeleteTileAtScreenCoordinate(playerX , playerY - playerSize);
                playerActionPoints -= playerDestroyTileCost;
                return true;

            }
            else
            {
                return false;
            }
        }

        public bool DestroyTileDown()
        {
            bool targetTileExist = playerMap.isTileAtScreenCoordinate(playerX, playerY + playerSize);
            if (CanDestroyTile(targetTileExist))
            {
                playerMap.DeleteTileAtScreenCoordinate(playerX, playerY + playerSize);
                playerActionPoints -= playerDestroyTileCost;
                return true;

            }
            else
            {
                return false;
            }
        }

        //These 2 below check if you have enough action points and wheter there is a tile.

        private bool CanMoveTo(bool targetTileExist)
        {
            return playerActionPoints >= playerMoveCost && !targetTileExist;
        }

        private bool CanDestroyTile(bool targetTileExist)
        {
            return playerActionPoints >= playerMoveCost && targetTileExist;
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

        //private void DestroyTile(int destructionX, int destructionY)
        //{
        //    playerY = destructionY;
        //    playerX = destructionX;
        //    playerActionPoints -= playerDestroyTileCost;
        //    playerMap.DeleteTileAtScreenCoordinate(playerX, playerY)
        //}



        //public bool Attack() 
        //{

        //}

        //public void TakeTurn()
        //{

        //}


    }
}
