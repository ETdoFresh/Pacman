using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DisplayLibrary;
using Microsoft.Xna.Framework;

namespace Pacman
{
    class StartStopAnimation : IDisposable
    {
        private Velocity velocity;
        private AnimatedSprite animatedSprite;
        private Boolean isStopped = true;

        public StartStopAnimation(Velocity velocity, AnimatedSprite animatedSprite)
        {
            this.velocity = velocity;
            this.animatedSprite = animatedSprite;

            Runtime.GameUpdate += UpdateStartStop;
        }

        private void UpdateStartStop(GameTime gameTime)
        {
            if (velocity.Value == Vector2.Zero && !isStopped)
            {
                isStopped = true;
                animatedSprite.Pause();
            }
            else if (velocity.Value != Vector2.Zero && isStopped)
            {
                isStopped = false;
                animatedSprite.Play();
            }
        }

        public void Dispose()
        {
            Runtime.GameUpdate -= UpdateStartStop;
        }
    }
}
