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
            Ghost _ghost;
            Target _target;
            TileGrid _tileGrid;
            TilePosition _targetTilePosition;

            public Immediate(Ghost ghost, Target target, TileGrid tileGrid)
            {
                _ghost = ghost;
                _target = target;
                _tileGrid = tileGrid;
                _targetTilePosition = new TilePosition(Position, tileGrid.TileWidth, tileGrid.TileHeight);
                Tint = Color.White;
                CalculatePosition();
            }

            public override void Update(GameTime gameTime)
            {
                base.Update(gameTime);
                CalculatePosition();
            }

            private void CalculatePosition()
            {
                var ghostPosition = _ghost.TilePosition.Vector;
                var ghostDirection = _ghost.Direction.Offset;
                var ghostNewPosition = ghostPosition + ghostDirection;

                if (ghostNewPosition.X < 0 || _tileGrid.Data.GetLength(0) <= ghostNewPosition.X ||
                    ghostNewPosition.Y < 0 || _tileGrid.Data.GetLength(1) <= ghostNewPosition.Y ||
                    _tileGrid.Data[(int)ghostNewPosition.X, (int)ghostNewPosition.Y].IsPassable)
                {
                    ghostNewPosition.X = ghostNewPosition.X * _tileGrid.TileWidth + _tileGrid.TileWidth / 2;
                    ghostNewPosition.Y = ghostNewPosition.Y * _tileGrid.TileHeight + _tileGrid.TileHeight / 2;
                    Position.Value = ghostNewPosition;
                    return;
                }
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
            public Pinky()
            {
                Tint = Color.Pink;
            }
        }

        internal class Inky : Target
        {
            public Inky()
            {
                Tint = Color.Cyan;
            }
        }

        internal class Clyde : Target
        {
            public Clyde()
            {
                Tint = Color.Orange;
            }
        }
    }
}
