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
            int playerTileX = playerX / playerSize;
            int playerTileY = playerY / playerSize;
            if (playerActionPoints >= playerMoveCost && !playerMap.IsTile(playerTileX, playerTileY - 1))
            {
                playerY -= playerSize;
                playerActionPoints -= playerMoveCost;
                Canvas.SetTop(playerImage, playerY);
                return true;
            }
            else
            {
                return false;     
            }

        }

        public bool MoveDown()
        {
            int playerTileX = playerX / playerSize;
            int playerTileY = playerY / playerSize;
            if (playerActionPoints >= playerMoveCost && !playerMap.IsTile(playerTileX, playerTileY + 1))
            {
                playerY += playerSize;
                playerActionPoints -= playerMoveCost;
                Canvas.SetTop(playerImage, playerY);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool MoveRight()
        {
            int playerTileX = playerX / playerSize;
            int playerTileY = playerY / playerSize;
            if (playerActionPoints >= playerMoveCost && !playerMap.IsTile(playerTileX + 1, playerTileY))
            {
                playerX += playerSize;
                playerActionPoints -= playerMoveCost;
                Canvas.SetLeft(playerImage, playerX);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool MoveLeft()
        {
            int playerTileX = playerX / playerSize;
            int playerTileY = playerY / playerSize;
            if (playerActionPoints >= playerMoveCost && !playerMap.IsTile(playerTileX - 1, playerTileY))
            {
                playerX -= playerSize;
                playerActionPoints -= playerMoveCost;
                Canvas.SetLeft(playerImage, playerX);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DestroyTileRight()
        {
            int playerTileX = playerX / playerSize;
            int playerTileY = playerY / playerSize;
            if (playerActionPoints >= playerMoveCost && playerMap.IsTile(playerTileX +1, playerTileY))
            {
                playerMap.DeleteTile(playerTileX + 1, playerTileY);
                return true;

            }
            else
            {
                return false;
            }
        }
        public bool DestroyTileLeft()
        {
            int playerTileX = playerX / playerSize;
            int playerTileY = playerY / playerSize;
            if (playerActionPoints >= playerMoveCost && playerMap.IsTile(playerTileX - 1, playerTileY))
            {
                playerMap.DeleteTile(playerTileX - 1, playerTileY);
                return true;

            }
            else
            {
                return false;
            }
        }

        public bool DestroyTileUp()
        {
            int playerTileX = playerX / playerSize;
            int playerTileY = playerY / playerSize;
            if (playerActionPoints >= playerMoveCost && playerMap.IsTile(playerTileX , playerTileY -1))
            {
                playerMap.DeleteTile(playerTileX , playerTileY -1);
                return true;

            }
            else
            {
                return false;
            }
        }

        public bool DestroyTileDown()
        {
            int playerTileX = playerX / playerSize;
            int playerTileY = playerY / playerSize;
            if (playerActionPoints >= playerMoveCost && playerMap.IsTile(playerTileX, playerTileY + 1))
            {
                playerMap.DeleteTile(playerTileX, playerTileY + 1);
                return true;

            }
            else
            {
                return false;
            }
        }


        //public bool Attack() 
        //{

        //}

        //public void TakeTurn()
        //{

        //}


    }
}
