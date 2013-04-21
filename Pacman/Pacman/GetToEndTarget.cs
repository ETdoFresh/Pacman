﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Pacman
{
    class GetToEndTarget : IDisposable
    {
        private NextTile nextTile;
        private EndTarget endTarget;
        private Direction direction;
        private Direction pendingDirection;
        private Tile[,] tiles;
        private Ghost ghost;

        private static List<Vector2> offsets = new List<Vector2> { new Vector2(-1, 0), new Vector2(1, 0), new Vector2(0, -1), new Vector2(0, 1) };

        public GetToEndTarget(Ghost ghost, Direction direction, NextTile nextTile, EndTarget endTarget, Tile[,] tiles)
        {
            this.ghost = ghost;
            this.direction = direction;
            this.nextTile = nextTile;
            this.endTarget = endTarget;
            this.tiles = tiles;

            pendingDirection = new Direction(Direction.Left);

            ghost.TilePosition.ChangeTile += CalculateNextMoves;
        }

        private void CalculateNextMoves()
        {
            direction.Value = pendingDirection.Value;
            nextTile.UpdateNextTile();

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

        public void Dispose()
        {
            ghost.TilePosition.ChangeTile -= CalculateNextMoves;
        }
    }
}