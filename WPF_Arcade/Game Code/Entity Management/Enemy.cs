using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace WPF_Arcade
{
    class Enemy
    {
        private int enemyActionPoints;
        private int enemyStartingActionPoints;
        private int enemyX;
        private int enemyY;
        private int enemyHealth;
        private readonly int enemyMoveCost = 1;
        private readonly int enemyAttackCost = 1;
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
            enemyStartingActionPoints = actions;
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

        public int X()
        {
            return enemyX;
        }

        public int Y()
        {
            return enemyY;
        }

        public int ActionPoints()
        {
            return enemyActionPoints;
        }


        public bool MoveUp()
        {
            return MoveIfValid(enemyX, enemyY - enemySize);
        }

        public bool MoveDown()
        {
            return MoveIfValid(enemyX, enemyY + enemySize);
        }

        public bool MoveLeft()
        {
            return MoveIfValid(enemyX - enemySize, enemyY);
        }

        public bool MoveRight()
        {
            return MoveIfValid(enemyX + enemySize, enemyY);
        }

        public void ResetActionPoints()
        {
            enemyActionPoints = enemyStartingActionPoints;
        }

        public void Attack()
        {

        }

        //private methods
        private bool CanMoveTo(int destinationX, int destinationY)
        {
            //first check if the destination is in the level
            if (enemyTileMap.IsScreenCoordinateInLevel(destinationX, destinationY))
            {
                //if the destination is within the level, check if there's not a tile there and if the player has enough action points to move.
                if (!enemyTileMap.isTileAtScreenCoordinate(destinationX, destinationY) && enemyActionPoints >= enemyMoveCost)
                {
                    return true;
                }
            }
            return false;
        }

        private void MoveTo(int destinationX, int destinationY)
        {
            enemyY = destinationY;
            enemyX = destinationX;
            enemyActionPoints -= enemyMoveCost;
            Canvas.SetLeft(enemyImage, enemyX);
            Canvas.SetTop(enemyImage, enemyY);
            
        }
        
        private bool MoveIfValid(int destinationX, int destinationY)
        {
            bool isMoveValid = CanMoveTo(destinationX, destinationY);

            if (isMoveValid)
            {
                MoveTo(destinationX, destinationY);
            }

            return isMoveValid;
        }
    }
}
