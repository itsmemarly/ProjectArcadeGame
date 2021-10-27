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
        private int enemyHealth = 2;
        private readonly int enemyMoveCost = 1;
        private readonly int enemyAttackCost = 1;
        private readonly int damageReceived = 1;
        private readonly int enemySize;

        private readonly BitmapImage enemyBitMap;
        private readonly Image enemyImage;
        private readonly Canvas enemyCanvas;
        private readonly CollisionManager enemyCollisionManager;


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

        public bool AttackRight()
        {
            return AttackIfValid(enemyX + enemySize, enemyY);
        }

        public bool AttackLeft()
        {
            return AttackIfValid(enemyX - enemySize, enemyY);
        }

        public bool AttackUp()
        {
            return AttackIfValid(enemyX, enemyY - enemySize);
        }

        public bool AttackDown()
        {
            return AttackIfValid(enemyX, enemyY + enemySize);
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


        public void DamageOnEnemy()
        {
            enemyHealth -= damageReceived;
            if (enemyHealth <= 0)
            {
                KillMonster();
            }




        }
        private void KillMonster()
        {
            enemyCanvas.Children.Remove(enemyImage);
        }


        private bool AttackIfValid(int x, int y)
        {
            //check points
            if (enemyActionPoints <= enemyAttackCost)
            {
                //check if there is something at target
                object thingAtTarget = enemyCollisionManager.getThingAt(x, y);

                if (thingAtTarget == null)
                {
                    return false;
                }
                //if there's a player attack
                else if (thingAtTarget.GetType() == typeof(Player))
                {
                    enemyActionPoints -= enemyAttackCost;
                    Player player = (Player)thingAtTarget;
                    player.DamageOnPlayer();
                    return true;

                    //do player attacking stuff
                }
            }
            return false;

        }


    }

}
