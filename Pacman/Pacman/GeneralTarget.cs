using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DisplayLibrary;
using Microsoft.Xna.Framework;

namespace Pacman
{
    class GeneralTarget : GameObject
    {
        private RectangleObject rectangle;

        public GeneralTarget(GroupObject displayParent, Position position = null)
        {
            Position = position == null ? new Position() : position;
            TilePosition = new TilePosition(Position);

            rectangle = new RectangleObject(displayParent, Position, TileEngine.GetDimension());
            rectangle.Color = Color.White;
            rectangle.Alpha = 0.4f;
        }

        public override void Dispose()
        {
            TilePosition.Dispose();
            rectangle.Dispose();
            base.Dispose();
        }
    }
}
