using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace Pacman
{
    public class Target : Kinematic, IDisposable
    {
        private Vector2 offsetFromSource;
        private Kinematic source;

        public Target(int x = 0, int y = 0, int orientation = 0, Kinematic source = null)
            : base(x, y, orientation)
        {
            if (source != null)
            {
                this.source = source;
                KeyboardListener.Press += OnKeyPress;
                Runtime.GameUpdate += OnGameUpdate;
            }
        }

        private void OnGameUpdate(GameTime gameTime)
        {
            Position = source.Position + offsetFromSource;
        }

        private void OnKeyPress(Keys key)
        {
            if (key == Keys.Left) offsetFromSource = new Vector2(-1, 0);
            else if (key == Keys.Right) offsetFromSource = new Vector2(1, 0);
            else if (key == Keys.Up) offsetFromSource = new Vector2(0, -1);
            else if (key == Keys.Down) offsetFromSource = new Vector2(0, 1);
            else offsetFromSource = Vector2.Zero;
        }

        public void Dispose()
        {
            KeyboardListener.Press -= OnKeyPress;
            Runtime.GameUpdate -= OnGameUpdate;
        }
    }
}
