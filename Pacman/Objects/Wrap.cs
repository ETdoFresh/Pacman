using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pacman.Engine.Display;
using Microsoft.Xna.Framework;
using Pacman.Engine.Helpers;

namespace Pacman.Objects
{
    class Wrap : GameObject
    {
        Position _position;
        int _left;
        int _top;
        float _right;
        float _bottom;

        public Wrap(Position position, int left, int top, float right, float bottom)
        {
            _position = position;
            _left = left;
            _top = top;
            _right = right;
            _bottom = bottom;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
            if (_position.X < _left)
                _position.X = _right - 1;
            else if (_position.X >= _right)
                _position.X = 0;

            if (_position.Y < _top)
                _position.Y = _bottom - 1;
            else if (_position.Y >= _bottom)
                _position.Y = 0;
        }
    }
}
