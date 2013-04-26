using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DisplayLibrary;
using Microsoft.Xna.Framework;

namespace Pacman
{
    class StopWatch : IDisposable
    {
        private float timer = 0;

        public float Second { get { return timer / 1000; } }
        public float Milliseconds { get { return timer; } }

        public StopWatch(Boolean start = false)
        {
            if (start)
                Start();
        }

        private void UpdateTimer(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        }

        public void Start()
        {
            Runtime.GameUpdate -= UpdateTimer;
            Runtime.GameUpdate += UpdateTimer;
        }

        public void Stop()
        {
            Runtime.GameUpdate -= UpdateTimer;
        }

        public void Reset()
        {
            timer = 0;
        }

        public void Dispose()
        {
            Stop();
        }
    }
}
