using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DisplayLibrary;
using Microsoft.Xna.Framework;

namespace Pacman
{
    class Clock : IDisposable
    {
        private float time;
        private float limit;
        
        public delegate void ClockReachedLimitHandler();
        public event ClockReachedLimitHandler ClockReachedLimit = delegate { };

        public Clock(float limit = 0)
        {
            Runtime.GameUpdate += OnGameUpdate;
            this.limit = limit;
        }

        public void Reset()
        {
            time = 0;
        }

        private void OnGameUpdate(GameTime gameTime)
        {
            time += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            
            if (limit > 0 && time >= limit)
                ClockReachedLimit();
        }

        public void Dispose()
        {
            Runtime.GameUpdate -= OnGameUpdate;
        }
    }
}
