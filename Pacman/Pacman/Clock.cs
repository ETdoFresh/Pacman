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
            this.limit = limit;
            Play();
        }

        public void Pause()
        {
            Runtime.GameUpdate -= OnGameUpdate;
        }

        public void Play()
        {
            Runtime.GameUpdate -= OnGameUpdate;
            Runtime.GameUpdate += OnGameUpdate;
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
            ClockReachedLimit = null;
            Runtime.GameUpdate -= OnGameUpdate;
        }
    }
}
