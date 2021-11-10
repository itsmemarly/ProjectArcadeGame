using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace WPF_Arcade
{
    class Enemy
    {
        // Enemy's Properties
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

        /// <summary>
        /// Gets the x coordinate of the Enemy
        /// </summary>
        /// <returns>x coordinate of the Enemy</returns>
        public int X()
        {
            return enemyX;
        }

        /// <summary>
        /// Gets the y coordinate of the Enemy
        /// </summary>
        /// <returns>y coordinate of the Enemy</returns>
        public int Y()
        {
            return enemyY;
        }

        /// <summary>
        /// Gets the ActionPoints (read: amount of remaining moves) of the Enemy
        /// </summary>
        /// <returns>ActionPoints (read: amount of remaining moves) of the Enemy</returns>
        public int ActionPoints()
        {
            return enemyActionPoints;
        }

        /// <summary>
        /// Checks if the Enemy can move up by 1
        /// </summary>
        /// <returns>true if the Enemy can move up by 1, false if the Enemy can not move up by 1</returns>
        public bool MoveUp()
        {
            return MoveIfValid(enemyX, enemyY - enemySize);
        }

        /// <summary>
        /// Checks if the Enemy can move down by 1
        /// </summary>
        /// <returns>true if the Enemy can move down by 1, false if the Enemy can not move down by 1</returns>
        public bool MoveDown()
        {
            return MoveIfValid(enemyX, enemyY + enemySize);
        }

        /// <summary>
        /// Checks if the Enemy can move left by 1
        /// </summary>
        /// <returns>true if the Enemy can move left by 1, false if the Enemy can not move left by 1</returns>
        public bool MoveLeft()
        {
            return MoveIfValid(enemyX - enemySize, enemyY);
        }

        /// <summary>
        /// Checks if the Enemy can move right by 1
        /// </summary>
        /// <returns>true if the Enemy can move right by 1, false if the Enemy can not move right by 1</returns>
        public bool MoveRight()
        {
            return MoveIfValid(enemyX + enemySize, enemyY);
        }

        /// <summary>
        /// Checks if the Enemy can attack right by 1
        /// </summary>
        /// <returns>true if the Enemy can attack right by 1, false if the Enemy can not attack right by 1</returns>
        public bool AttackRight()
        {
            return AttackIfValid(enemyX + enemySize, enemyY);
        }

        /// <summary>
        /// Checks if the Enemy can attack left by 1
        /// </summary>
        /// <returns>true if the Enemy can attack left by 1, false if the Enemy can not attack left by 1</returns>
        public bool AttackLeft()
        {
            return AttackIfValid(enemyX - enemySize, enemyY);
        }

        /// <summary>
        /// Checks if the Enemy can attack up by 1
        /// </summary>
        /// <returns>true if the Enemy can attack up by 1, false if the Enemy can not attack up by 1</returns>
        public bool AttackUp()
        {
            return AttackIfValid(enemyX, enemyY - enemySize);
        }

        /// <summary>
        /// Checks if the Enemy can attack down by 1
        /// </summary>
        /// <returns>true if the Enemy can attack down by 1, false if the Enemy can not attack down by 1</returns>
        public bool AttackDown()
        {
            return AttackIfValid(enemyX, enemyY + enemySize);
        }

        /// <summary>
        /// Resets the Enemy's ActionPoints (read: amount of Turns) to 1
        /// </summary>
        public void ResetActionPoints()
        {
            enemyActionPoints = enemyStartingActionPoints;
        }

        /// <summary>
        /// Moves the Enemy to the given position
        /// </summary>
        /// <param name="destinationX">x coordinate of the position</param>
        /// <param name="destinationY">y coordinate of the position</param>
        private void MoveTo(int destinationX, int destinationY)
        {
            enemyY = destinationY;
            enemyX = destinationX;
            enemyActionPoints -= enemyMoveCost;
            Canvas.SetLeft(enemyImage, enemyX);
            Canvas.SetTop(enemyImage, enemyY);
        }

        /// <summary>
        /// Checks if the Enemy can move to the given position
        /// </summary>
        /// <param name="destinationX">x coordinate of the position</param>
        /// <param name="destinationY">y coordinate of the position</param>
        /// <returns></returns>
        private bool MoveIfValid(int destinationX, int destinationY)
        {
            bool isMoveValid = enemyCollisionManager.IsValidDestination(destinationX, destinationY);

            if (isMoveValid)
            {
                MoveTo(destinationX, destinationY);
            }

            return isMoveValid;
        }

        /// <summary>
        /// Decreases the Enemy's Health by 1, and deletes the Enemy if Its Health is empty
        /// </summary>
        /// <returns>true if the Enemy's Health is gone, false if the Enemy's Health is not gone</returns>
        public bool DamageOnEnemy()
        {
            enemyHealth -= damageReceived;
            if (enemyHealth <= 0)
            {
                KillMonster();
                enemyCollisionManager.DeleteEnemey(enemyX,enemyY);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Removes the Enemy from the Canvas
        /// </summary>
        private void KillMonster()
        {
            enemyCanvas.Children.Remove(enemyImage);
        }

        /// <summary>
        /// Checks if the Enemy can attack a Player, if a Player exists and is located at the given position
        /// </summary>
        /// <param name="x">x coordinate of the given position</param>
        /// <param name="y">y coordinate of the given position</param>
        /// <returns>true if these statements are all true:
        ///     The Enemy has enough Action Points (read: remaining Turns) to be able to perform an attack;
        ///     A Thing exists at the given position;
        ///     The Thing turns out to be a Player.
        /// false if only one or none of the above statements are true</returns>
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
