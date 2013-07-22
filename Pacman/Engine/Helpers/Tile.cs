using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pacman.Engine.Display;
using Microsoft.Xna.Framework;

namespace Pacman.Engine.Helpers
{
    class Tile : GroupObject
    {
        static Random _random = new Random();
        
        RectangleObject _tile;
        bool _isPassable;

        public float  Left { get; set; }
        public float Right { get; set; }
        public float Top { get; set; }
        public float Bottom { get; set; }
        public bool IsPassable { get { return _isPassable; } set { if (_tile != null) { _tile.Tint = Color.Red; _isPassable = value; } } }

        public Tile(int width, int height) : this(width, height, true) { }
        public Tile(int width, int height, bool drawRectangle)
        {

            Width = width;
            Height = height;

            Left = 0;
            Top = 0;
            Right = width;
            Bottom = height;

            IsPassable = true;

            if (drawRectangle)
            {
                _tile = new RectangleObject(width, height);
                _tile.Tint = new Color(0, _random.Next(0, 255), _random.Next(0, 255));
                _tile.Alpha = 0.3f;
                AddChild(_tile);
            }
        }

        public override void Translate(float x, float y)
        {
            base.Translate(x, y);
            Left = x - Origin.X;
            Right = x + Origin.X;
            Top = y - Origin.Y;
            Bottom = y + Origin.Y;
        }
    }
}
