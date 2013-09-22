using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pacman.Engine.Helpers;
using Pacman.Engine.Display;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Pacman.Objects
{
    class Target : CircleObject, ISteer
    {
        public enum TargetStatesWithNone { Fixed }
        public enum TargetStatesWithTileGrid { Frightened }
        public enum TargetStatesWithPacmanTileGrid { Pacman }
        public enum TargetStatesWithGhostTileGrid { Immediate }
        public enum TargetStatesWithPacman { Blinky, Pinky }
        public enum TargetStatesWithGhost { Inky, Clyde }
        static public TargetStatesWithTileGrid FRIGHTENED { get { return TargetStatesWithTileGrid.Frightened; } }
        static public TargetStatesWithGhostTileGrid IMMEDIATE { get { return TargetStatesWithGhostTileGrid.Immediate; } }
        static public TargetStatesWithPacmanTileGrid PACMAN { get { return TargetStatesWithPacmanTileGrid.Pacman; } }
        static public TargetStatesWithPacman BLINKY { get { return TargetStatesWithPacman.Blinky; } }
        static public TargetStatesWithPacman PINKY { get { return TargetStatesWithPacman.Pinky; } }
        static public TargetStatesWithGhost INKY { get { return TargetStatesWithGhost.Inky; } }
        static public TargetStatesWithGhost CLYDE { get { return TargetStatesWithGhost.Clyde; } }
        static public TargetStatesWithNone FIXED { get { return TargetStatesWithNone.Fixed; } }

        TargetState _targetState;

        public Target()
            : base(10)
        {
            ChangeState(FIXED);
            Alpha = 0.8f;
            Speed = new Speed();
            Velocity = new Velocity() { Enabled = false };
            Rotation = new Rotation() { Enabled = false };
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            _targetState.Update(gameTime);
        }

        public void ChangeState(TargetStatesWithPacman targetState, PacmanObject pacman)
        {
            _targetState = TargetState.Create(targetState, pacman, this);
        }

        public void ChangeState(TargetStatesWithNone targetState)
        {
            _targetState = TargetState.Create(targetState, this);
        }

        public void ChangeState(TargetStatesWithTileGrid targetState, TileGrid tileGrid)
        {
            _targetState = TargetState.Create(targetState, tileGrid, this);
        }

        public void ChangeState(TargetStatesWithGhostTileGrid targetState, TileGrid tileGrid, Ghost ghost)
        {
            _targetState = TargetState.Create(targetState, tileGrid, ghost, this);
        }

        public void ChangeState(TargetStatesWithPacmanTileGrid targetState, TileGrid tileGrid, PacmanObject pacman)
        {
            _targetState = TargetState.Create(targetState, tileGrid, pacman, this);
        }

        public void ChangeState(TargetStatesWithGhost targetState, PacmanObject pacman, Ghost ghost)
        {
            _targetState = TargetState.Create(targetState, pacman, ghost, this);
        }

        public Speed Speed { get; set; }
        public Velocity Velocity { get; set; }
        public Rotation Rotation { get; set; }
        public TargetState CurrentType { get { return _targetState; } }

        abstract internal class TargetState
        {
            protected TargetState() { }

            static public TargetState Create(TargetStatesWithNone targetState, Target target)
            {
                switch (targetState)
                {
                    case TargetStatesWithNone.Fixed:
                        return new FixedType();
                    default:
                        throw new Exception("Target State not valid");
                }
            }

            static public TargetState Create(TargetStatesWithPacman targetState, PacmanObject pacman, Target target)
            {
                switch (targetState)
                {
                    case TargetStatesWithPacman.Blinky:
                        return new BlinkyType(pacman, target);
                    case TargetStatesWithPacman.Pinky:
                        return new PinkyType(pacman, target);
                    default:
                        throw new Exception("Target State (with Pacman) not valid");
                }
            }

            static public TargetState Create(TargetStatesWithTileGrid targetState, TileGrid tileGrid, Target target)
            {
                switch (targetState)
                {
                    case TargetStatesWithTileGrid.Frightened:
                        return new FrightenedType(tileGrid, target);
                    default:
                        throw new Exception("Target State (with Tile Grid) not valid");
                }
            }

            static public TargetState Create(TargetStatesWithGhostTileGrid targetState, TileGrid tileGrid, Ghost ghost, Target target)
            {
                switch (targetState)
                {
                    case TargetStatesWithGhostTileGrid.Immediate:
                        return new ImmediateType(tileGrid, ghost, target);
                    default:
                        throw new Exception("Target State (with Tile Grid) not valid");
                }
            }

            static public TargetState Create(TargetStatesWithPacmanTileGrid targetState, TileGrid tileGrid, PacmanObject pacman, Target target)
            {
                switch (targetState)
                {
                    case TargetStatesWithPacmanTileGrid.Pacman:
                        return new PacmanType(tileGrid, pacman, target);
                    default:
                        throw new Exception("Target State (with Tile Grid) not valid");
                }
            }

            static public TargetState Create(TargetStatesWithGhost targetState, PacmanObject pacman, Ghost ghost, Target target)
            {
                switch (targetState)
                {
                    case TargetStatesWithGhost.Inky:
                        return new InkyType(pacman, ghost, target);
                    case TargetStatesWithGhost.Clyde:
                        return new ClydeType(pacman, ghost, target);
                    default:
                        throw new Exception("Target State (with Ghost) not valid");
                }
            }

            abstract public void Update(GameTime gameTime);
        }

        internal class ImmediateType : TargetState
        {
            static List<Vector2> offsets = new List<Vector2> { new Vector2(0, -1), new Vector2(-1, 0), new Vector2(0, 1), new Vector2(1, 0) };
            Ghost _ghost;
            TileGrid _tileGrid;
            Vector2 _ghostNextTile, _ghostPrevTile;
            TilePosition _targetTilePosition;

            public ImmediateType(TileGrid tileGrid, Ghost ghost, Target target)
            {
                _ghost = ghost;
                _tileGrid = tileGrid;
                _ghostPrevTile = _ghost.TilePosition.Vector + offsets[1];
                _ghostNextTile = _ghost.TilePosition.Vector;
                _targetTilePosition = new TilePosition(ghost.Target.Position, tileGrid.TileWidth, tileGrid.TileHeight);
                target.Tint = Color.White;
            }

            public override void Update(GameTime gameTime)
            {
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
                else
                    return true;
            }

            public void ReverseDirection()
            {
                var tempTile = _ghostPrevTile;
                _ghostPrevTile = _ghostNextTile;
                if (_ghostNextTile != _ghost.TilePosition.Vector)
                    _ghostNextTile = tempTile;
            }
        }

        internal class PacmanType : TargetState
        {
            TileGrid _tileGrid;
            PacmanObject _pacman;
            Target _target;
            Vector2 _pacmanPreviousPosition;

            public PacmanType(TileGrid tileGrid, PacmanObject pacman, Target target)
            {
                _tileGrid = tileGrid;
                _pacman = pacman;
                _target = target;
                target.Tint = Color.Yellow;
            }

            public override void Update(GameTime gameTime)
            {
                SetNewDirection();
                SetNewTarget();
            }

            private void SetNewDirection()
            {
                if (InputHelper.IsPressed(Keys.Left))
                    _pacman.DesiredDirection.Value = Direction.LEFT;
                else if (InputHelper.IsPressed(Keys.Right))
                    _pacman.DesiredDirection.Value = Direction.RIGHT;
                else if (InputHelper.IsPressed(Keys.Up))
                    _pacman.DesiredDirection.Value = Direction.UP;
                else if (InputHelper.IsPressed(Keys.Down))
                    _pacman.DesiredDirection.Value = Direction.DOWN;
            }

            private void SetNewTarget()
            {
                var pacmanPosition = _pacman.TilePosition.Vector;
                var pacmanDesiredDirection = _pacman.DesiredDirection.Offset;
                var pacmanPreviousDirection = _pacman.PreviousDirection.Offset;

                if (pacmanPosition != _pacmanPreviousPosition || pacmanDesiredDirection != pacmanPreviousDirection)
                {
                    _pacmanPreviousPosition = pacmanPosition;

                    var pacmanNewPosition = pacmanPosition + pacmanDesiredDirection;
                    if (pacmanNewPosition.X < 0 || _tileGrid.Data.GetLength(0) <= pacmanNewPosition.X ||
                        pacmanNewPosition.Y < 0 || _tileGrid.Data.GetLength(1) <= pacmanNewPosition.Y ||
                        _tileGrid.Data[(int)pacmanNewPosition.X, (int)pacmanNewPosition.Y].IsPassable)
                    {
                        pacmanNewPosition.X = pacmanNewPosition.X * _tileGrid.TileWidth + _tileGrid.TileWidth / 2;
                        pacmanNewPosition.Y = pacmanNewPosition.Y * _tileGrid.TileHeight + _tileGrid.TileHeight / 2;
                        _target.Position.Value = pacmanNewPosition;
                        _pacman.PreviousDirection.Value = _pacman.DesiredDirection.Value;
                        return;
                    }

                    pacmanNewPosition = pacmanPosition + pacmanPreviousDirection;
                    if (pacmanNewPosition.X < 0 || _tileGrid.Data.GetLength(0) <= pacmanNewPosition.X ||
                        pacmanNewPosition.Y < 0 || _tileGrid.Data.GetLength(1) <= pacmanNewPosition.Y ||
                        _tileGrid.Data[(int)pacmanNewPosition.X, (int)pacmanNewPosition.Y].IsPassable)
                    {
                        pacmanNewPosition.X = pacmanNewPosition.X * _tileGrid.TileWidth + _tileGrid.TileWidth / 2;
                        pacmanNewPosition.Y = pacmanNewPosition.Y * _tileGrid.TileHeight + _tileGrid.TileHeight / 2;
                        _target.Position.Value = pacmanNewPosition;
                    }
                }
            }
        }

        internal class BlinkyType : TargetState
        {
            PacmanObject _pacman;
            Target _target;

            public BlinkyType(PacmanObject pacman, Target target)
            {
                _pacman = pacman;
                _target = target;
                target.Tint = Color.Red;
            }

            public override void Update(GameTime gameTime)
            {
                _target.Translate(_pacman.Position);
            }
        }

        internal class PinkyType : TargetState
        {
            PacmanObject _pacman;
            Target _target;

            public PinkyType(PacmanObject pacman, Target target)
            {
                _pacman = pacman;
                _target = target;
                target.Tint = Color.Pink;
            }

            public override void Update(GameTime gameTime)
            {
                _target.Position.Value =
                    _pacman.Position.Value + _pacman.PreviousDirection.Offset * _pacman.TilePosition.TileWidth * 4;
            }
        }

        internal class InkyType : TargetState
        {
            PacmanObject _pacman;
            Ghost _blinky;
            Target _target;

            public InkyType(PacmanObject pacman, Ghost blinky, Target target)
            {
                _pacman = pacman;
                _blinky = blinky;
                _target = target;
                target.Tint = Color.Cyan;
            }

            public override void Update(GameTime gameTime)
            {
                var offset = _pacman.Position.Value + _pacman.PreviousDirection.Offset * _pacman.TilePosition.TileWidth * 2;
                var blinkyDifference = offset - _blinky.Position.Value;
                _target.Position.Value = _blinky.Position.Value + blinkyDifference * 2;
            }
        }

        internal class ClydeType : TargetState
        {
            PacmanObject _pacman;
            Ghost _clyde;
            Target _target;

            public ClydeType(PacmanObject pacman, Ghost clyde, Target target)
            {
                _pacman = pacman;
                _clyde = clyde;
                _target = target;
                target.Tint = Color.Orange;
            }

            public override void Update(GameTime gameTime)
            {
                var distance = Vector2.Distance(_clyde.Position.Value, _pacman.Position.Value);
                var tileWidth = _pacman.TilePosition.TileWidth;
                if (distance < tileWidth * 8)
                    _target.Position.Value = new Vector2(tileWidth / 2, 30 * tileWidth + tileWidth / 2);
                else
                    _target.Position.Value = _pacman.Position.Value;
            }
        }

        internal class FixedType : TargetState
        {
            public FixedType() { }
            public override void Update(GameTime gameTime) { }
        }

        internal class FrightenedType : TargetState
        {
            public FrightenedType(TileGrid tileGrid, Target target)
            {
                Random _random = new Random();
                int x = _random.Next((int)tileGrid.Width);
                int y = _random.Next((int)tileGrid.Height);
                target.Translate(x, y);
            }

            public override void Update(GameTime gameTime) { }
        }
    }
}