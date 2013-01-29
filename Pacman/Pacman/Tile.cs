using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using DisplayEngine;

namespace PacmanGame
{
    class Tile : GroupObject, IGameObject, IStatic
    {
        private Rectangle destinationRectangle;

        public bool IsPassable { get; set; }

        public Tile() : base()
        {
            origin = new Vector2(15 / 2, 15 / 2);
            IsPassable = true;
        }

        public Tile(Vector2 position)
            : this()
        {
            Position = position;
            destinationRectangle = new Rectangle((int)position.X, (int)position.Y, 15, 15);
        }

        public void InsertSprite(int tileIndex, float rotation)
        {
            var newSprite = display.NewSprite("pacman");
            newSprite.SetFrame(tileIndex);
            newSprite.Rotation = rotation;
            Insert(newSprite);
        }
    }
}
