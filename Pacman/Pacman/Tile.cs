using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Pacman
{
    class Tile : IGameObject, IStatic
    {
        public static ContentManager Content;

        private Vector2 position;
        private Rectangle destinationRectangle;
        private Texture2D texture;
        private List<Rectangle> textureRectangles;
        private List<Rectangle> sourceRectangles = new List<Rectangle>();
        private List<float> orientations = new List<float>();
        private Vector2 origin;

        public Tile()
        {
            texture = Content.Load<Texture2D>("pacman");
            textureRectangles = TexturePacker.GetTextureRectangles(Content.RootDirectory + "\\pacman.xml");
            origin = new Vector2(15 / 2, 15 / 2);
            IsPassable = true;
        }

        public Tile(Vector2 position)
            : this()
        {
            this.position = position;
            destinationRectangle = new Rectangle((int)position.X, (int)position.Y, 15, 15);
        }

        public void Insert(int index, float orientation = 0)
        {
            sourceRectangles.Add(textureRectangles[index]);
            orientations.Add(orientation);
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (var i = 0; i < sourceRectangles.Count; i++)
                spriteBatch.Draw(texture, destinationRectangle, sourceRectangles[i], Color.White, orientations[i], origin, SpriteEffects.None, 0);
        }

        public Vector2 Position { get { return position; } }
        public float Orientation { get { return 0; } }
        public bool IsPassable { get; set; }
    }
}
