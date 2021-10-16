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


        public object getThingAt(int x, int y)
        {
            return collisionTileMap;
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
            if (collisionTileMap.isTileAtScreenCoordinate(x, y) || IsPlayerAt(x, y) || IsEnemyAt(x, y))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool IsPlayerAt(int x, int y)
        {
            foreach (var player in collisionPlayerList)
            {
                if (player.X() == x && player.Y() == y)
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsEnemyAt(int x, int y)
        {
            foreach (var enemy in collisionPlayerList)
            {
                if (enemy.X() == x && enemy.Y() == y)
                {
                    return true;
                }
            }

            return false;
        }


    }
}
