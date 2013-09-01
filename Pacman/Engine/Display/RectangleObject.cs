using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pacman.Engine.Display
{
    class RectangleObject : DisplayObject
    {
        static List<Texture2D> _previousTextures;

        Rectangle _rectangle;
        Texture2D _texture;

        static RectangleObject()
        {
            _previousTextures = new List<Texture2D>();
        }

        public RectangleObject(float width, float height)
            : base()
        {
            _rectangle = new Rectangle();
            Width = width;
            Height = height;
        }

        public override void LoadContent()
        {
            base.LoadContent();
            if (_previousTextures != null)
            {
                foreach (var texture in _previousTextures)
                {
                    if (texture.Width == Width && texture.Height == Height)
                    {
                        _texture = texture;
                        break;
                    }
                }
            }
            if (_texture == null)
            {
                _texture = new Texture2D(SpriteBatch.GraphicsDevice, (int)Width, (int)Height);
                _previousTextures.Add(_texture);
            }

            Color[] color = new Color[(int)(Width * Height)];
            for (int i = 0; i < color.Length; i++) color[i] = Color.White;
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
            SpriteBatch.Draw(_texture, _rectangle, null, Tint * Alpha, ContentOrientation, Origin, SpriteEffects.None, 0);
        }
    }
}
