using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pacman.Engine;
using Microsoft.Xna.Framework;
using Pacman.Engine.Helpers;
using Pacman.Engine.Display;
using Pacman.Scenes;

namespace Pacman.Objects
{
    class PacmanObject : DisplayObject, ISteer
    {
        TileGrid _tileGrid;
        AnimatedSpriteObject _animatedSprite;
        Speed _speed;
        Direction _desiredDirection;
        Direction _previousDirection;
        Velocity _velocity;
        Rotation _rotation;
        TilePosition _tilePosition;
        Steering _steering;
        SnapToTarget _snapToTarget;
        Wrap _wrap;
        Target _target;
        Position _startPosition;

        public PacmanObject(TileGrid tileGrid) 
        {
            _tileGrid = tileGrid;
        }

        static public PacmanObject Create(TileGrid tileGrid)
        {
            var result = new PacmanObject(tileGrid);
            result.SetupAnimations();
            result.SetupTransforms();
            result.SetupProperties();
            result.SetupUpdaters();
            tileGrid.AddComponent(result);
            return result;
        }

        private void SetupTransforms()
        {
            _startPosition = _tileGrid.GetPosition(14.5f, 23f);
            Position = _startPosition.Copy();
            _tilePosition = new TilePosition(Position, _tileGrid.TileWidth, _tileGrid.TileHeight);
            Orientation = new Orientation(0);
            Scale = new Scale(1);
            AddComponent(_tilePosition);
        }

        private void SetupProperties()
        {
            _speed = new Speed(LevelScene.maxSpeed);
            _target = new Target();
            _target.ChangeState(Target.PACMAN, _tileGrid, this);
            _desiredDirection = new Direction(Direction.LEFT);
            _previousDirection = new Direction(Direction.LEFT);
            _tileGrid.AddComponent(_target);
        }

        private void SetupUpdaters()
        {
            _velocity = new Velocity() { Position = Position, Speed = _speed };
            _rotation = new Rotation() { Orientation = Orientation };
            _steering = new Steering(this, _target as ISteer);
            _wrap = new Wrap(Position, 0, 0, _tileGrid.Width, _tileGrid.Height);
            _snapToTarget = new SnapToTarget(this, _velocity, _target);
            AddComponent(_velocity);
            AddComponent(_rotation);
            AddComponent(_steering);
            AddComponent(_wrap);
            AddComponent(_snapToTarget);
        }

        private void SetupAnimations()
        {
            
            _animatedSprite = new AnimatedSpriteObject("pacman");
            _animatedSprite.AddSequence("Chomp", new[] { 0, 1, 2, 3, 4, 5, 6, 7, 6, 5, 4, 3, 2, 1 }, 200);
            _animatedSprite.AddSequence("Die", 0, 11, 1000);
            _animatedSprite.SetSequence("Chomp");
            _animatedSprite.Tint = Color.Yellow;
            AddComponent(_animatedSprite);
        }

        public Direction PreviousDirection { get { return _previousDirection; } }
        public Direction DesiredDirection { get { return _desiredDirection; } }
        public TilePosition TilePosition { get { return _tilePosition; } }
        public Speed Speed { get { return _speed; } }
        public Velocity Velocity { get { return _velocity; } }
        public Rotation Rotation { get { return _rotation; } }
    }
}
