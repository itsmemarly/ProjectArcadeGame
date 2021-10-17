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

        private readonly BitmapImage enemyBitMap;
        private readonly Image enemyImage;
        private readonly Canvas enemyCanvas;
        CollisionManager enemyCollisionManager;


        public Enemy(int x, int y, int actions, int size, BitmapImage bitmap, Canvas canvas, CollisionManager collisionManager)
        {
            enemyX = x;
            enemyY = y;
            enemyActionPoints = actions;
            enemyStartingActionPoints = actions;
            enemySize = size;
            enemyBitMap = bitmap;
            enemyCanvas = canvas;
            enemyCollisionManager = collisionManager;

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

        //private methods

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
            bool isMoveValid = enemyCollisionManager.IsValidDestination(destinationX, destinationY);

            if (isMoveValid)
            {
                MoveTo(destinationX, destinationY);
            }

            return isMoveValid;
        }
    }
}
