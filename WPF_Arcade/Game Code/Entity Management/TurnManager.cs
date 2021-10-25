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
    class TurnManager
    {
        private readonly List<Enemy> turnEnemyList;
        private readonly List<Player> turnPlayerList;

        private string turnSeed;

        private int levelActivePlayerIndex = 0;
        private int turnRandomCount = int.MinValue;
        

        public TurnManager(List<Player> playerlist, List<Enemy> enemyList, string seed)
        {
            turnPlayerList = playerlist;
            turnEnemyList = enemyList;
            turnSeed = seed;
        }

        public Player ActivePlayer()
        {
            return turnPlayerList[levelActivePlayerIndex];
        }

        public void SetSeed(string seed)
        {
            turnSeed = seed;
            turnRandomCount = int.MinValue;
        }

        public void TakePlayerAction(Key key)
        {
            switch (key)
            {
                case Key.A:
                    ActivePlayer().MoveLeft();
                    break;

                case Key.D:
                    ActivePlayer().MoveRight();
                    break;

                case Key.W:
                    ActivePlayer().MoveUp();
                    break;

                case Key.S:
                    ActivePlayer().MoveDown();
                    break;

                case Key.Up:
                    ActivePlayer().AttackUp();
                    break;

                case Key.Down:
                    ActivePlayer().AttackDown();
                    break;

                case Key.Left:
                    ActivePlayer().AttackLeft();
                    break;

                case Key.Right:
                    ActivePlayer().AttackRight();
                    break;

                default:
                    break;
            }
            EndTurnIfNeeded(ActivePlayer());
        }

        private void TakeEnemyTurns()
        {
            foreach (var enemy in turnEnemyList)
            {
                if (GeneratePsuedoRandomValue(100) > 50)
                {
                    enemy.MoveLeft();
                }

                

               

                else
                {
                    enemy.MoveRight();
                }
                enemy.ResetActionPoints();
            }
        }

        private void EndTurnIfNeeded(Player player)
        {
            if (player.Actionpoints() == 0)
            {
                //changes which player is active
                levelActivePlayerIndex += 1;
                player.ResetActionPoints();

                if (levelActivePlayerIndex > turnPlayerList.Count - 1)
                {
                    levelActivePlayerIndex = 0;
                    TakeEnemyTurns();
                }

            }
        }





        //generates a value with high varience int the outputwith a small varience the input based on the seed and how many numbers have been previously generated
        private float GeneratePsuedoRandomValue(float maxValue)
        {
            string input = turnRandomCount.ToString() + turnSeed;
            turnRandomCount += 1;
            return Math.Abs(input.GetHashCode()) % maxValue;
        }
    }
}
