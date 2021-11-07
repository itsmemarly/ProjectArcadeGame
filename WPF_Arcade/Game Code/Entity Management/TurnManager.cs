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

        /// <summary>
        /// Gets the active Player
        /// </summary>
        /// <returns>the active Player</returns>
        public Player ActivePlayer()
        {
            return turnPlayerList[levelActivePlayerIndex];
        }

        /// <summary>
        /// Sets the Seed of the Terrain for the Turn Manager
        /// </summary>
        /// <param name="seed">the Seed of the Terrain</param>
        public void SetSeed(string seed)
        {
            turnSeed = seed;
            turnRandomCount = int.MinValue;
        }

        /// <summary>
        /// Translates User Input (WASD/Arrows) into action on Terrain if possible, and ends the active Player's turn if needed
        /// </summary>
        /// <param name="key">
        /// User Input:
        ///     WASD keys to move Player to up, left, down and right respectively;
        ///     Arrows up, down, left, right to let Player attack/mine up, down, left and right respectively.
        /// </param>
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

        /// <summary>
        /// Makes all Enemies randomly move to left or to right in their turn
        /// </summary>
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

        /// <summary>
        /// Ends the turn of the Player if the Player has no Action Points (read: Turn counts) left
        /// </summary>
        /// <param name="player">the Player</param>
        private void EndTurnIfNeeded(Player player)
        {
            if (player.Actionpoints() == 0)
            {
                //changes which player is active
                levelActivePlayerIndex += 1;
                player.ResetActionPoints();
                player.SetInactive();

                if (levelActivePlayerIndex > turnPlayerList.Count - 1)
                {
                    levelActivePlayerIndex = 0;

                    // Gives all Enemies a Turn to perform a move to left or to right
                    TakeEnemyTurns();
                }

                ActivePlayer().SetActive();
            }
        }

        /// <summary>
        /// Generates a pseudo random value based on the seed and the amount of times pseudo random values have previously been generated
        /// </summary>
        /// <param name="maxValue">Maximum of pseudo random value</param>
        /// <returns>pseudo random value</returns>
        private float GeneratePsuedoRandomValue(float maxValue)
        {
            string input = turnRandomCount.ToString() + turnSeed;
            turnRandomCount += 1;
            return Math.Abs(input.GetHashCode()) % maxValue;
        }
    }
}
