using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pacman.Engine.Helpers;
using Pacman.Engine.Display;
using Microsoft.Xna.Framework;

namespace Pacman.Objects
{
    abstract class Target : CircleObject, ISteer
    {
        public Speed Speed { get; set; }
        public Velocity Velocity { get; set; }
        public Rotation Rotation { get; set; }

        public Target()
            : base(10)
        {
            Alpha = 0.5f;
            Speed = new Speed(0);
            Velocity = new Velocity() { Position = Position, Speed = Speed };
            Rotation = new Rotation() { Orientation = Orientation };
        }

        internal class Pacman : Target
        {
            public Pacman()
            {
                Tint = Color.Yellow;
            }
        }

        internal class Immediate : Target
        {
            static List<Vector2> offsets = new List<Vector2> { new Vector2(0, -1), new Vector2(-1, 0), new Vector2(0, 1), new Vector2(1, 0) };

            Ghost _ghost;
            TileGrid _tileGrid;
            Vector2 _ghostNextTile, _ghostPrevTile;
            TilePosition _targetTilePosition;

            public Immediate(Ghost ghost, TileGrid tileGrid)
            {
                _ghost = ghost;
                _tileGrid = tileGrid;
                Tint = Color.White;
                _ghostNextTile = _ghostPrevTile = _ghost.TilePosition.Vector;
                _targetTilePosition = new TilePosition(ghost.Target.Position, tileGrid.TileWidth, tileGrid.TileHeight);
            }

            public override void Update(GameTime gameTime)
            {
                base.Update(gameTime);
                _targetTilePosition.Update(gameTime);
                CalculatePosition();
            }

            private void CalculatePosition()
            {
                var ghostPosition = _ghost.TilePosition.Vector;

                if (ghostPosition == _ghostNextTile || (ghostPosition - _ghostNextTile).LengthSquared() >= 16)
                {
                    var closestPosition = new Vector2(1000, 1000);
                    for (var i = 0; i < offsets.Count; i++)
                    {
                        var offset = offsets[i];
                        var test = ghostPosition + offset;
                        if (test != _ghostPrevTile)
                        {
                            if (SafeZoneCheck(test))
                            {
                                if (test.X < 0 || _tileGrid.Data.GetLength(0) <= test.X ||
                                    test.Y < 0 || _tileGrid.Data.GetLength(1) <= test.Y ||
                                    _tileGrid.Data[(int)test.X, (int)test.Y].IsPassable)
                                {
                                    if (Vector2.DistanceSquared(_targetTilePosition.Vector, test)
                                        < Vector2.DistanceSquared(_targetTilePosition.Vector, closestPosition))
                                    {
                                        closestPosition = test;
                                        if (i == 0) _ghost.Direction.Value = Direction.UP;
                                        else if (i == 1) _ghost.Direction.Value = Direction.LEFT;
                                        else if (i == 2) _ghost.Direction.Value = Direction.DOWN;
                                        else if (i == 3) _ghost.Direction.Value = Direction.RIGHT;
                                    }
                                }
                            }
                        }
                    }

                    var ghostNewPosition = ghostPosition + _ghost.Direction.Offset;
                    _ghostPrevTile = ghostPosition;
                    _ghostNextTile = ghostNewPosition;

                    ghostNewPosition.X = ghostNewPosition.X * _tileGrid.TileWidth + _tileGrid.TileWidth / 2;
                    ghostNewPosition.Y = ghostNewPosition.Y * _tileGrid.TileHeight + _tileGrid.TileHeight / 2;
                    _ghost.ImmediateTarget.Position.Value = ghostNewPosition;
                }
            }

            private bool SafeZoneCheck(Vector2 destination)
            {
                var ghostTile = _ghost.TilePosition.Vector;
                if (ghostTile.Y == 11 && (destination.X == 12 || destination.X == 15) && destination.Y == 10)
                    return false;
                if (ghostTile.Y == 23 && (destination.X == 12 || destination.X == 15) && destination.Y == 22)
                    return false;
                else if (ghostTile.Y == 11 && (destination.X == 13 || destination.X == 14) && destination.Y == 12)
                    return false;
                else
                    return true;
            }
        }

        internal class Blinky : Target
        {
            public Blinky(Objects.Pacman pacman)
            {
                Position = pacman.Position;
                Tint = Color.Red;
            }
        }

        internal class Pinky : Target
        {
            Objects.Pacman _pacman;

            public Pinky(Objects.Pacman pacman)
            {
                _pacman = pacman;
                Tint = Color.Pink;
            }

            public override void Update(GameTime gameTime)
            {
                base.Update(gameTime);
                Position.Value =
                    _pacman.Position.Value + _pacman.PreviousDirection.Offset * _pacman.TilePosition.TileWidth * 4;
            }
        }

        internal class Inky : Target
        {
            Objects.Pacman _pacman;
            Objects.Ghost _blinky;

            public Inky(Objects.Pacman pacman, Objects.Ghost blinky)
            {
                _pacman = pacman;
                _blinky = blinky;
                Tint = Color.Cyan;
            }

            public override void Update(GameTime gameTime)
            {
                base.Update(gameTime);
                var offset = _pacman.Position.Value + _pacman.PreviousDirection.Offset * _pacman.TilePosition.TileWidth * 2;
                var blinkyDifference = offset - _blinky.Position.Value;
                Position.Value = _blinky.Position.Value + blinkyDifference * 2;
            }
        }

        internal class Clyde : Target
        {
            Objects.Pacman _pacman;
            Objects.Ghost _clyde;

            public Clyde(Objects.Pacman pacman, Objects.Ghost clyde)
            {
                _pacman = pacman;
                _clyde = clyde;
                Tint = Color.Orange;
            }

            public override void Update(GameTime gameTime)
            {
                base.Update(gameTime);
                var distance = Vector2.Distance(_clyde.Position.Value, _pacman.Position.Value);
                var tileWidth = _pacman.TilePosition.TileWidth;
                if (distance < tileWidth * 8)
                    Position.Value = new Vector2(tileWidth / 2, 30 * tileWidth + tileWidth / 2);
                else
                    Position.Value = _pacman.Position.Value;
            }
        }
    }
}