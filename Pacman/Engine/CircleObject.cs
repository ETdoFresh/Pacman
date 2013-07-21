using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pacman.Engine
{
    class CircleObject : DisplayObject
    {
        Rectangle _rectangle;
        Texture2D _texture;

        public CircleObject(float radius)
            : base()
        {
            _rectangle = new Rectangle();
            Width = radius * 2;
            Height = radius * 2;
        }

        public override void LoadContent()
        {
            base.LoadContent();
            _texture = new Texture2D(SpriteBatch.GraphicsDevice, (int)Width, (int)Height);

            var radius = Width / 2;

            Color[] color = new Color[(int)(Width * Height)];
            for (int i = 0; i < color.Length; i++)
            {
                int x = (i + 1) % (int)Width;
                int y = (i + 1) / (int)Width;
                Vector2 distance = new Vector2(Math.Abs(radius - x), Math.Abs(radius - y));
                if (Math.Sqrt((distance.X * distance.X) + (distance.Y * distance.Y)) > radius)
                    color[i] = Color.Black * 0;
                else
                    color[i] = Color.White;
            }
            _texture.SetData(color);

            Origin = new Vector2(Width / 2, Height / 2);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            var width = (int)(Width * ContentScale);
            var height = (int)(Height * ContentScale);
            _rectangle = new Rectangle((int)ContentPosition.X, (int)ContentPosition.Y, width, height);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Draw(_texture, _rectangle, null, Tint * Alpha, ContentRotation, Origin, SpriteEffects.None, 0);
        }
    }
}
