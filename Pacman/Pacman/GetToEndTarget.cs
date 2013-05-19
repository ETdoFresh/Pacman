using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Pacman
{
    class GetToEndTarget : IDisposable
    {
        private Direction pendingDirection;
        private Tile[,] tiles;
        private Ghost ghost;

        private static List<Vector2> offsets = new List<Vector2> { new Vector2(-1, 0), new Vector2(1, 0), new Vector2(0, -1), new Vector2(0, 1) };

        public GetToEndTarget(Ghost ghost, Tile[,] tiles)
        {
            this.ghost = ghost;
            this.tiles = tiles;

            pendingDirection = ghost.Direction.Copy();

            ghost.TilePosition.ChangeTile += CalculateNextMoves;
        }

        public void CalculateNextMoves()
        {
            var direction = ghost.Direction;
            var target = ghost.Target;
            var endTarget = ghost.EndTarget;

            direction.Value = pendingDirection.Value;
            target.Position.Value = UpdateNextTile();

            var currentTilePosition = ghost.TilePosition.Value;
            var newTilePosition = currentTilePosition + direction.GetVectorOffset();

            if (tiles != null)
            {
                var closestPosition = new Vector2(1000, 1000);
                for (var i = 0; i < offsets.Count; i++)
                {
                    var offset = offsets[i];
                    var test = newTilePosition + offset;
                    if (test != currentTilePosition)
                    {
                        if (SafeZoneCheck(test, currentTilePosition))
                        {
                            if (0 <= test.X && test.X < tiles.GetLength(0))
                            {
                                if (0 <= test.Y && test.Y < tiles.GetLength(1))
                                {
                                    var tile = tiles[(int)test.X, (int)test.Y];
                                    if (tile == null || tile.IsPassable)
                                    {
                                        if (Vector2.DistanceSquared(endTarget.TilePosition.Value, test) < Vector2.DistanceSquared(endTarget.TilePosition.Value, closestPosition))
                                        {
                                            closestPosition = test;
                                            if (i == 0) pendingDirection.Value = Direction.Left;
                                            else if (i == 1) pendingDirection.Value = Direction.Right;
                                            else if (i == 2) pendingDirection.Value = Direction.Up;
                                            else if (i == 3) pendingDirection.Value = Direction.Down;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private Vector2 UpdateNextTile()
        {
            var currentTile = ghost.TilePosition.Value;
            var newPosition = currentTile + ghost.Direction.GetVectorOffset();
            return TileEngine.GetPosition(newPosition).Value;
        }

        private bool SafeZoneCheck(Vector2 Destination, Vector2 CurrentPosition)
        {
            if (CurrentPosition.Y == 11 && (Destination.X == 12 || Destination.X == 15) && Destination.Y == 10)
                return false;
            if (CurrentPosition.Y == 23 && (Destination.X == 12 || Destination.X == 15) && Destination.Y == 22)
                return false;
            else if (CurrentPosition.Y == 11 && (Destination.X == 13 || Destination.X == 14) && Destination.Y == 12)
                return false;
            else
                return true;
        }

        public void Dispose()
        {
            ghost.TilePosition.ChangeTile -= CalculateNextMoves;
        }
    }
}
