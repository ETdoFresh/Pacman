using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pacman.Engine;
using Microsoft.Xna.Framework;
using Pacman.Engine.Helpers;
using Pacman.Engine.Display;

namespace Pacman.Objects
{
    class Steering : GameObject
    {
        ISteer _character;
        ISteer _target;
        ISteerType _steerType;
        ISteerType _rotationType;

        public delegate void ArriveAtTarget();
        public event ArriveAtTarget OnArriveAtTarget = delegate { };

        public Steering(ISteer character, DisplayObject target) : this(character, new SteerTarget(target)) { }
        public Steering(ISteer character, ISteer target)
        {
            _character = character;
            _target = target;
            _steerType = new DynamicSeek(character, target, this, 100);
            _rotationType = new DynamicLookWhereYouAreGoing(character, target, MathHelper.ToRadians(1080));
        }

        public override void Update(GameTime gameTime)
        {
            if (Enabled)
            {
                base.Update(gameTime);
                _steerType.Update(gameTime);
                _rotationType.Update(gameTime);
            }
        }

        public interface ISteerType
        {
            void Update(GameTime gameTime);
        }

        public class DynamicSeek : ISteerType
        {
            ISteer _character;
            ISteer _target;
            Steering _steering;
            float _acceleration;
            bool _isMoving;

            public DynamicSeek(ISteer character, ISteer target, Steering steering, float acceleration)
            {
                _character = character;
                _target = target;
                _steering = steering;
                _acceleration = acceleration;
            }

            public void Update(GameTime gameTime)
            {
                Vector2 velocity;
                velocity = _target.Position.Value - _character.Position.Value;
                if (velocity != Vector2.Zero)
                {
                    _isMoving = true;
                    velocity.Normalize();
                    velocity *= _acceleration;
                    _character.Velocity.Value += velocity;
                }
                else
                {
                    if (_isMoving)
                    {
                        _isMoving = false;
                        _steering.OnArriveAtTarget();
                    }
                }
            }
        }

        public class DyanmicAlign : ISteerType
        {
            protected ISteer _character;
            protected ISteer _target;
            protected float _spin;
            protected float _timeToTarget = 0.1f;

            public DyanmicAlign(ISteer character, ISteer target, float spin)
            {
                _character = character;
                _target = target;
                _spin = spin;
            }

            public virtual void Update(GameTime gameTime)
            {
                float sign = _target.Orientation.Value - _character.Orientation.Value;
                float rotation = Math.Abs(sign);
                sign = rotation / sign;
                if (rotation > 0)
                {
                    rotation = Math.Min(_spin, rotation / _timeToTarget);
                    _character.Rotation.Value = rotation * sign;
                }
                else
                    _character.Rotation.Value = 0;
            }
        }

        public class DynamicLookWhereYouAreGoing : DyanmicAlign
        {
            public DynamicLookWhereYouAreGoing(ISteer character, ISteer target, float spin)
                : base(character, target, spin) { }

            public override void Update(GameTime gameTime)
            {
                if (_character.Velocity.Value.LengthSquared() > 0)
                {
                    var orientation = (float)(Math.Atan2(_character.Velocity.Value.Y, _character.Velocity.Value.X));
                    if (Math.Abs(_target.Orientation.Value - orientation) <= MathHelper.ToRadians(270))
                        _target.Orientation.Value = orientation;
                }
                base.Update(gameTime);
            }
        }
    }
}
