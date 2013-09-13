using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pacman.Engine.Display;
using Microsoft.Xna.Framework;

namespace Pacman.Objects
{
    class Timer : GameObject
    {
        private float time;
        private float limit;
        
        public delegate void ClockReachedLimitHandler();
        public event ClockReachedLimitHandler ClockReachedLimit = delegate { };

        public Timer(float limit = 0)
        {
            this.limit = limit;
            Start();
        }

        public void Stop()
        {
            Enabled = false;
        }

        public void Start()
        {
            Enabled = true;
        }

        public void Reset()
        {
            time = 0;
            Enabled = true;
        }

        public void Reset(float newLimit)
        {
            limit = newLimit;
            Reset();
        }

        public override void  Update(GameTime gameTime)
        {
            if (Enabled)
            {
                base.Update(gameTime);
                time += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                if (limit > 0 && time >= limit)
                {
                    Enabled = false;
                    ClockReachedLimit();
                }
            }
        }
    }
}
