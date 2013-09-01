using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pacman.Objects;
using Pacman.Engine.Display;
using Microsoft.Xna.Framework;

namespace Pacman.Objects
{
    class ShiftEyesToDirection : GameObject
    {
        Ghost _ghost;
        Direction.DirectionValue _previousDirection;

        public ShiftEyesToDirection(Ghost ghost)
        {
            _ghost = ghost;
            SetEyesByDirection();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (_ghost.Direction.Value != _previousDirection)
            {
                SetEyesByDirection();
                _previousDirection = _ghost.Direction.Value;
            }
        }

        private void SetEyesByDirection()
        {
            int eyeOffset = 16;
            int pupilOffset = 21;
            if (_ghost.Direction.Value == Direction.DirectionValue.UP)
            {
                _ghost.Eyes.ChangeIndex(eyeOffset + 0);
                _ghost.Pupils.ChangeIndex(pupilOffset + 0);
            }
            else if (_ghost.Direction.Value == Direction.DirectionValue.DOWN)
            {
                _ghost.Eyes.ChangeIndex(eyeOffset + 1);
                _ghost.Pupils.ChangeIndex(pupilOffset + 1);
            }
            else if (_ghost.Direction.Value == Direction.DirectionValue.LEFT)
            {
                _ghost.Eyes.ChangeIndex(eyeOffset + 2);
                _ghost.Pupils.ChangeIndex(pupilOffset + 2);
            }
            else if (_ghost.Direction.Value == Direction.DirectionValue.RIGHT)
            {
                _ghost.Eyes.ChangeIndex(eyeOffset + 3);
                _ghost.Pupils.ChangeIndex(pupilOffset + 3);
            }
        }
    }
}
