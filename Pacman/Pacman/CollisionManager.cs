using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Pacman
{
    class CollisionManager
    {
        private Player player;
        private Map map;
        private Vector2 previousInputDirection;

        public CollisionManager(Map map)
        {
            this.map = map;
            this.player = map.Player;
        }

        internal void Update(GameTime gameTime)
        {
            WrapAroundEdges();
            UpdatePlayerTarget();
            CheckWallCollision();
        }

        private void UpdatePlayerTarget()
        {
            var currentInputDirection = player.InputDirection;
            var currentTile = map.GetTileCoordinates(player);
            var currentTilePosition = map.GetWorldCoordinates(currentTile);
            var target = currentTilePosition;
            var nextTile = currentTile + currentInputDirection;

            if (currentInputDirection.Length() > 1)
            {
                nextTile = currentTile;
                nextTile.X += currentInputDirection.X;
                if (!map.IsPassable(nextTile))
                {
                    nextTile = currentTile;
                    nextTile.Y += currentInputDirection.Y;
                    currentInputDirection.X = 0;
                }
                else
                    currentInputDirection.Y = 0;
            }

            if (!map.IsPassable(nextTile))
            {
                currentInputDirection = previousInputDirection;
                nextTile = currentTile + currentInputDirection;
            }

            if (currentInputDirection.X < 0 && player.Position.X <= currentTilePosition.X)
            {
                if (0 <= nextTile.X && nextTile.X < map.MapWidth && map.IsPassable(nextTile))
                    target.X = map.GetWorldCoordinates(nextTile).X;
            }
            else if (currentInputDirection.X > 0 && player.Position.X >= currentTilePosition.X)
            {
                if (0 <= nextTile.X && nextTile.X < map.MapWidth && map.IsPassable(nextTile))
                    target.X = map.GetWorldCoordinates(nextTile).X;
            }
            
            if (currentInputDirection.Y < 0 && player.Position.Y <= currentTilePosition.Y)
            {
                if (0 <= nextTile.Y && nextTile.Y < map.MapHeight && map.IsPassable(nextTile))
                    target.Y = map.GetWorldCoordinates(nextTile).Y;
            }
            else if (currentInputDirection.Y > 0 && player.Position.Y >= currentTilePosition.Y)
            {
                if (0 <= nextTile.Y && nextTile.Y < map.MapHeight && map.IsPassable(nextTile))
                    target.Y = map.GetWorldCoordinates(nextTile).Y;
            }

            if (nextTile.Y == 14 && (nextTile.X == -1 || nextTile.X == map.MapWidth))
                target = map.GetWorldCoordinates(nextTile);

            previousInputDirection = currentInputDirection;
            player.Destination = target;
        }

        private void CheckWallCollision()
        {
            
        }

        private void WrapAroundEdges()
        {
            var mapLimitX = map.Width - 1;
            var mapLimitY = map.Height - 1;

            if (player.X > mapLimitX)
                player.X = 0;
            else if (player.X < 0)
                player.X = mapLimitX;

            if (player.Y > mapLimitY)
                player.Y = 0;
            else if (player.Y < 0)
                player.Y = mapLimitY;
        }
    }
}
