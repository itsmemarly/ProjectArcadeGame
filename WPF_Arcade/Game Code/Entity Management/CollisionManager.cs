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

        public CollisionManager(TileMap tilemap, List<Player> playerlist, List<Enemy> enemyList)
        {
            collisionTileMap = tilemap;
            collisionPlayerList = playerlist;
            collisionEnemyList = enemyList;
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
            if (!IsInLevel(x, y))
            {
                return null;
            }
            if (collisionTileMap.isTileAtScreenCoordinate(x, y))
            {
                return collisionTileMap;
            }
            Enemy enemy = GetEnemyAt(x, y);
            if (enemy != null)
            {
                return enemy;
            }
            Player player = GetPlayerAt(x, y);
            if (player != null)
            {
                return player;
            }
            else
            {
                return null;
            }
        }

        //checks if a postion is valid to move to
        public bool IsValidDestination(int x, int y)
        {
            //only check if the the space is empty if it's in the level, otherwise we'll crash the game
            if (IsInLevel(x, y))
            {
                return IsEmpty(x, y);
            }
            else
            {
                return false;
            }
        }

        //checks if a given coordinate is within the level
        private bool IsInLevel(int x, int y)
        {
            return
                x >= 0
                && x <= collisionTileMap.Width() * collisionTileMap.TileSize()
                && y >= 0
                && y <= collisionTileMap.Height() * collisionTileMap.TileSize();
        }

        //returns true if the given coordinates are empty, or false if they're not
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

        //returns a player if there's one at those coordinates
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

        //returns an enemy if there's one at those coordinates
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

        //puts player on empty tile at the beginning of the game

        public Player PutPlayer(int x, int y)
        {
            foreach (var player in collisionPlayerList)
            {
                if (collisionTileMap.isTileAtScreenCoordinate(x, y))
                {
                    return player;
                }
            }
            return null;
        }

        //puts enemy on empty tile at the beginning of the game

        public Enemy PutEnemy(int x, int y)
        {
            foreach (var enemy in collisionEnemyList)
            {
                if (collisionTileMap.isTileAtScreenCoordinate(x, y))
                {
                    return enemy;
                }
            }
            return null;
        }


    }
}
