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
    class Enemy
    {
        private int enemyActionPoints;
        private int enemyX;
        private int enemyY;
        private int enemyHealth;
        private readonly int enemyMoveCost = 2;
        private readonly int enemyAttackCost = 2;
        private readonly int enemySize;

        private TileMap enemyTileMap;
        private readonly BitmapImage enemyBitMap;
        private readonly Image enemyImage;
        private readonly Canvas enemyCanvas;


        public Enemy(int x, int y, int actions, int size, BitmapImage bitmap, Canvas canvas, TileMap tilemap)
        {
            enemyX = x;
            enemyY = y;
            enemyActionPoints = actions;
            enemySize = size;
            enemyTileMap = tilemap;
            enemyBitMap = bitmap;
            enemyCanvas = canvas;

            enemyImage = new Image
            {
                Tag = "enemyImage",
                Height = enemySize,
                Width = enemySize,
                Source = enemyBitMap
            };

            Canvas.SetLeft(enemyImage, x);
            Canvas.SetTop(enemyImage, y);

            enemyCanvas.Children.Add(enemyImage);
        }

        public void takeTurn()
        {

        }

        public void MoveUp()
        {
            bool targetTileExists = enemyTileMap.isTileAtScreenCoordinate(enemyX, enemyY - enemySize);

            if (enemyActionPoints > enemyMoveCost && !targetTileExists)
            {
                Move(enemyX, enemyY - enemySize);
            }
        }

        public void MoveDown()
        {
            bool targetTileExists = enemyTileMap.isTileAtScreenCoordinate(enemyX, enemyY + enemySize);

            if (CanMoveTo(targetTileExists))
            {
                Move(enemyX, enemyY + enemySize);
            }
        }

        public void MoveLeft()
        {
            bool targetTileExists = enemyTileMap.isTileAtScreenCoordinate(enemyX - enemySize, enemyY);

            if (CanMoveTo(targetTileExists))
            {
                Move(enemyX - enemySize, enemyY);
            }
        }
        public void MoveRight()
        {
            bool targetTileExists = enemyTileMap.isTileAtScreenCoordinate(enemyX + enemySize, enemyY);

            if (CanMoveTo(targetTileExists))
            {
                Move(enemyX + enemySize, enemyY);
            }
        }

        public void Attack()
        {

        }

        public void DoStuff()
        {
            
        
        }

        private bool CanMoveTo(bool targetTileExists)
        {
            return enemyActionPoints > enemyMoveCost && !targetTileExists;
        }

        private void Move(int destinationX, int destinationY)
        {
            enemyY = destinationY;
            enemyX = destinationX;
            enemyActionPoints -= enemyMoveCost;
            Canvas.SetLeft(enemyImage, enemyX);
            Canvas.SetTop(enemyImage, enemyY);
            
        }
    }
}
