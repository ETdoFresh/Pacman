using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pacman.Engine.Display;
using Pacman.Engine.Helpers;
using Microsoft.Xna.Framework;

namespace Pacman.Objects
{
    class SnapToTarget : GameObject
    {
        DisplayObject _source;
        Velocity _sourceVelocity;
        DisplayObject _target;

        Vector2 _previousVelocity;

        public SnapToTarget(DisplayObject source, Velocity sourceVelocity, DisplayObject target)
        {
            _source = source;
            _sourceVelocity = sourceVelocity;
            _target = target;
            _previousVelocity = sourceVelocity.Value;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            var distance = _target.Position.Value - _source.Position.Value;
            if (distance.Length() < _previousVelocity.Length() * gameTime.ElapsedGameTime.TotalSeconds)
            {
                _source.Position.Value = _target.Position.Value;
                _sourceVelocity.Value = Vector2.Zero;
            }

            _previousVelocity = _sourceVelocity.Value;
        }
    }
}
