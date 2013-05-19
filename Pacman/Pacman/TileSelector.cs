using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DisplayLibrary;
using Microsoft.Xna.Framework.Input;

namespace Pacman
{
    class TileSelector : GameObject
    {
        public RectangleObject Rectangle { get; set; }

        public TileSelector(GroupObject displayParent = null)
        {
            KeyboardListener.Press += UpdateRectangle;
        }

        private void UpdateRectangle(Keys key)
        {
            if (key == Keys.Up)
                Position.Y -= TileEngine.TileHeight;
            else if (key == Keys.Down)
                Position.Y += TileEngine.TileHeight;
            else if (key == Keys.Left)
                Position.X -= TileEngine.TileWidth;
            else if (key == Keys.Right)
                Position.X += TileEngine.TileWidth;
        }

        public override void Dispose()
        {
            if (Rectangle != null) Rectangle.Dispose();
            KeyboardListener.Press -= UpdateRectangle;
            base.Dispose();
        }
    }
}
