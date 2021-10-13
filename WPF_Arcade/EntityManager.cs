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
    class EntityManager
    {
        private List<Enemy> managerEnemyList;
        private List<Player> managerPlayerList;
        private int managerSeed;
        private int managerRandomCount = int.MinValue;
        private int activePlayerIndex = 0;

       

        public EntityManager(List<Enemy> enemyList, List<Player> playerList, int seed ) {
            managerEnemyList = enemyList;
            managerPlayerList = playerList;
            managerSeed = seed;


        }

        //makes the enemy turn random
         public void TakeEnemyTurns() {
            foreach (var enemy in managerEnemyList)
            {
                if (GeneratePsuedoRandomValue(100) >50)
                {
                    enemy.MoveLeft();
                }
                
                else
                {
                    enemy.MoveRight();
                }

            }
        }
        //method to randomly turn the enemy
        public float GeneratePsuedoRandomValue(float maxValue)
        {
            string input = managerRandomCount.ToString() + managerSeed;
            managerRandomCount += 1;
            return Math.Abs(input.GetHashCode()) % maxValue;
        }
        //keys to make the player move
        public void TakePlayerAction(Key key)
        {
            switch (key)
            {
                case Key.A:
                    ActivePlayer().MoveLeft();
                    EndTurn(ActivePlayer());
                    break;
                case Key.D:
                    ActivePlayer().MoveRight();
                    EndTurn(ActivePlayer());
                    break;

<<<<<<< Updated upstream
                    case Key.W:
=======
                case Key.W:
>>>>>>> Stashed changes
                    ActivePlayer().MoveUp();
                    EndTurn(ActivePlayer());
                    break;

                case Key.S:
                    ActivePlayer().MoveDown();
                    EndTurn(ActivePlayer());
                    break;

                case Key.E:
                    ActivePlayer().DestroyTileRight();
                    EndTurn(ActivePlayer());
                    break;
<<<<<<< Updated upstream

=======
>>>>>>> Stashed changes
                case Key.C:
                    ActivePlayer().DestroyTileDown();
                    EndTurn(ActivePlayer());
                    break;
<<<<<<< Updated upstream

=======
>>>>>>> Stashed changes
                case Key.Z:
                    ActivePlayer().DestroyTileLeft();
                    EndTurn(ActivePlayer());
                    break;
<<<<<<< Updated upstream

                case Key.Q:
                    ActivePlayer().DestroyTileLeft();
                    EndTurn(ActivePlayer());
                    break;


=======
                case Key.Q:
                    ActivePlayer().DestroyTileUp();
                    EndTurn(ActivePlayer());
                    break;
>>>>>>> Stashed changes
                default: 
                    break;




            }
        }
        
        private Player ActivePlayer()
        {
            return managerPlayerList[activePlayerIndex];
        }
        //what happens if you end the turn of the player
        private void EndTurn(Player player1)
        {
            if  (player1.Actionpoints() == 0)
            {
                activePlayerIndex += 1;
                player1.ResetActionPoints();

                if (activePlayerIndex > managerPlayerList.Count - 1)
                {
                    activePlayerIndex = 0;
                    TakeEnemyTurns();
                }

            }
        }
       
        

    }

}