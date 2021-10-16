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

        //returns the tileMap if there's a tile there, an enemy if there's an enemy there, or a player if there's a player there or null if there's nothing at the position
        public object getThingAt(int x, int y)
        {
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

        private bool IsInLevel(int x, int y)
        {
            return
                x >= 0
                && x <= collisionTileMap.Width() * collisionTileMap.TileSize()
                && y >= 0
                && y <= collisionTileMap.Height() * collisionTileMap.TileSize();
        }

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


    }
}
