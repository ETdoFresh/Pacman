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
            DisplayParent = displayParent;

            Position = TileEngine.GetPosition(tileX: 0, tileY: 0);
            TilePosition = new TilePosition(Position);

            Rectangle = new RectangleObject(position: Position, dimension: new Dimension(TileEngine.TileWidth, TileEngine.TileHeight), parent: DisplayParent);
            Rectangle.Alpha = 0.5f;
            disposables.Add(Rectangle);

            KeyboardListener.Press += UpdateRectangle;
        }

        private void UpdateRectangle(Keys key)
        {
            if (key == Keys.Up)
                Rectangle.Position.Y -= TileEngine.TileHeight;
            else if (key == Keys.Down)
                Rectangle.Position.Y += TileEngine.TileHeight;
            else if (key == Keys.Left)
                Rectangle.Position.X -= TileEngine.TileWidth;
            else if (key == Keys.Right)
                Rectangle.Position.X += TileEngine.TileWidth;
        }

        public override void Dispose()
        {
            KeyboardListener.Press -= UpdateRectangle;
            base.Dispose();
        }
    }
}
