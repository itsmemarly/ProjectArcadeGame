using System;
using System.Collections.Generic;
using System.Text;

namespace WPF_Arcade
{
    class CollisionManager
    {
        private readonly TileMap collisionTileMap;
        private readonly List<Enemy> collisionEnemyList;
        private readonly List<Player> collisionPlayerList;
        private Exit collisionExit;

        public CollisionManager(TileMap tilemap, List<Player> playerlist, List<Enemy> enemyList, Exit exit)
        {
            collisionTileMap = tilemap;
            collisionPlayerList = playerlist;
            collisionEnemyList = enemyList;
            collisionExit = exit;
        }

        /// <summary>
        ///     returns the tileMap if there's a tile there,
        ///     an enemy if there's an enemy there,
        ///     or a player if there's a player there
        ///     or null if there's nothing at the position or if the postition is outside the level
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public object getThingAt(int x, int y)
        {

            // check if Thing is in the generated Level
            if (!IsInLevel(x, y))
            {
                return null;
            }

            // check if Thing is a Tile at the given coordinates/standing in the way
            if (collisionTileMap.isTileAtScreenCoordinate(x, y))
            {
                return collisionTileMap;
            }

            // check if Thing is an Enemy at the given coordinates/standing in the way
            Enemy enemy = GetEnemyAt(x, y);
            if (enemy != null)
            {
                return enemy;
            }

            // check if other Player stands in the way
            Player player = GetPlayerAt(x, y);
            if (player != null)
            {
                return player;
            }

            // Lastly, check if Thing is the Exit, otherwise return NULL
            if (collisionExit.X() == x && collisionExit.Y() == y)
            {
                return collisionExit;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Checks if a postion is valid to move to
        /// </summary>
        /// <param name="x">x coordinate of the position</param>
        /// <param name="y">y coordinate of the position</param>
        /// <returns></returns>
        public bool IsValidDestination(int x, int y)
        {
            // only check if the space is empty if it's in the level, otherwise we'll crash the game
            if (IsInLevel(x, y))
            {
                return IsEmpty(x, y);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if a given position is within the Level
        /// </summary>
        /// <param name="x">x coordinate of the position</param>
        /// <param name="y">y coordinate of the position</param>
        /// <returns>true if the Thing is in the Game Level, and false if the Thing is not in the Game Level</returns>
        private bool IsInLevel(int x, int y)
        {
            return
                x >= 0
                && x <= (collisionTileMap.Width() * collisionTileMap.TileSize()) -1
                && y >= 0
                && y <= (collisionTileMap.Height() * collisionTileMap.TileSize()) -1;
        }

        /// <summary>
        /// Checks if a given position is yet empty
        /// </summary>
        /// <param name="x">x coordinate of the position</param>
        /// <param name="y">y coordinate of the position</param>
        /// <returns>true if the given position is empty, or false if it is filled with a Tile, Player or Enemy</returns>
        private bool IsEmpty(int x, int y)
        {
            if (collisionTileMap.isTileAtScreenCoordinate(x, y) || GetPlayerAt(x, y) != null || GetEnemyAt(x, y) != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Checks if one of the Players resides at the given position
        /// </summary>
        /// <param name="x">x coordinate of the position</param>
        /// <param name="y">y coordinate of the position</param>
        /// <returns>returns a Player if there's one at the given position, and returns NULL if there's none at the given position</returns>
        private Player GetPlayerAt(int x, int y)
        {
            foreach (var player in collisionPlayerList)
            {
                if (player.X() == x && player.Y() == y)
                {
                    return player;
                }
            }

            return null;
        }

        /// <summary>
        /// Checks if an Enemy resides at the given position
        /// </summary>
        /// <param name="x">x coordinate of the position</param>
        /// <param name="y">y coordinate of the position</param>
        /// <returns>returns an Enemy if there's one at the given position, and returns NULL if there's none at the given position</returns>
        private Enemy GetEnemyAt(int x, int y)
        {
            foreach (var enemy in collisionEnemyList)
            {
                if (enemy.X() == x && enemy.Y() == y)
                {
                    return enemy;
                }
            }

            return null;
        }

        /// <summary>
        /// Deletes an Enemy from the given position
        /// </summary>
        /// <param name="x">x coordinate of the position</param>
        /// <param name="y">y coordinate of the position</param>
        public void DeleteEnemey(int x, int y)
        {
            Enemy enemy = GetEnemyAt(x, y);
            collisionEnemyList.Remove(enemy);
        }
    }
}
