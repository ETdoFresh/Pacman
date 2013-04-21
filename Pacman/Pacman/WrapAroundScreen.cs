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
        private GameObject gameObject;
        private Board board;

        public WrapAroundScreen(GameObject gameObject, Board board)
        {
            this.gameObject = gameObject;
            this.board = board;

            Runtime.GameUpdate += WrapPosition;
        }

        private void WrapPosition(GameTime gameTime)
        {
            var position = gameObject.Position;
            var width = board.Width;
            var height = board.Height;

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
