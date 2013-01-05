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
        private List<Ghost> ghosts;

        public CollisionManager(Map map)
        {
            this.map = map;
            this.player = map.Player;
            this.ghosts = map.Ghosts;
        }

        internal void Update(GameTime gameTime)
        {
            WrapAroundEdges();
            UpdatePlayerTarget();
            UpdateGhostTarget();
            CheckWallCollision();
            CheckGhostCollision();
        }

        private void UpdateGhostTarget()
        {
            foreach (var ghost in ghosts)
            {
                var currentMapCell = map.getCurrentMapCell(ghost);
                var nextMapCell = map.getNextMapCell(ghost);

                if (nextMapCell != null && nextMapCell.IsPassable)
                    ghost.Destination = map.GetWorldCoordinates(nextMapCell);

                if (nearGhostCage(currentMapCell))
                    return;
            }
        }

        private static bool nearGhostCage(MapCell mapCell)
        {
            return (mapCell.Y == 11 || mapCell.Y == 17) && (10 < mapCell.X && mapCell.X < 17);
        }

        private void CheckGhostCollision()
        {
            var playerTile = map.GetTileCoordinates(player);
            foreach (var ghost in ghosts)
            {
                var ghostTile = map.GetTileCoordinates(ghost);
                if (playerTile.Equals(ghostTile))
                {
                    player.Die();
                    return;
                }
            }
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

            if (map.IsPassable(nextTile))
                target = map.GetWorldCoordinates(nextTile);
            else if (nextTile.Y == 14 && (nextTile.X == -1 || nextTile.X == map.MapWidth))
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
