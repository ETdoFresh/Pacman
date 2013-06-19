using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DisplayLibrary;
using Microsoft.Xna.Framework;

namespace Pacman
{
    class AnimatedTowardDirection : IDisposable
    {
        private Direction direction;
        private Direction previousDirection;
        private AnimatedSprite animatedSprite;

        public AnimatedTowardDirection(Direction direction, AnimatedSprite animatedSprite)
        {
            this.direction = direction;
            previousDirection = new Direction(direction.Value);
            this.animatedSprite = animatedSprite;

            Runtime.GameUpdate += updateAnimationBasedOnDirection;
        }

        private void updateAnimationBasedOnDirection(GameTime gameTime)
        {
            if (animatedSprite != null && direction.Value != previousDirection.Value)
            {
                previousDirection.Value = direction.Value;
                if (direction.Value == Direction.Left) animatedSprite.SetSequence("Left");
                else if (direction.Value == Direction.Right) animatedSprite.SetSequence("Right");
                else if (direction.Value == Direction.Up) animatedSprite.SetSequence("Up");
                else if (direction.Value == Direction.Down) animatedSprite.SetSequence("Down");
            }
        }

        public void Dispose()
        {
            Runtime.GameUpdate -= updateAnimationBasedOnDirection;
            direction = null;
            previousDirection = null;
            animatedSprite = null;
        }
    }
}
