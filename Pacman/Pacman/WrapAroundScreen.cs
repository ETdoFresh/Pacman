using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DisplayLibrary;
using Microsoft.Xna.Framework;

namespace Pacman
{
    class WrapAroundScreen : IDisposable
    {
        private float width;
        private float height;
        private Position position;

        public WrapAroundScreen(float width, float height, DisplayLibrary.Position position)
        {
            this.width = width;
            this.height = height;
            this.position = position;

            Runtime.GameUpdate += WrapPosition;
        }

        private void WrapPosition(GameTime gameTime)
        {
            if (position.X < 0)
                position.X = width - 1;
            else if (position.X >= width)
                position.X = 0;
            else if (position.Y < 0)
                position.Y = height - 1;
            else if (position.Y >= height)
                position.Y = 0;
        }

        public void Dispose()
        {
            Runtime.GameUpdate -= WrapPosition;
        }
    }
}
