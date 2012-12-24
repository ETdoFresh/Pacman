using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
namespace Pacman
{
    class Sprite
    {
        private Texture2D texture;
        private List<Rectangle> textureRectangles;
        private Rectangle sourceRectangle = Rectangle.Empty;
        private Rectangle destinationRectangle = Rectangle.Empty;
        private Vector2 position = Vector2.Zero;
        private Vector2 previousPosition = Vector2.Zero;
        private Vector2 velocity = Vector2.Zero;
        protected Vector2 origin = Vector2.Zero;
        private float speed = 300f;
        private float rotation = 0f;
        private AnimationSequence sequence;
        private int currentFrame;
        private int totalFrames;
        private double timeSinceLastFrame;
        //private bool animationPlayedOnce;

        protected List<AnimationSequence> sequences;

        public Vector2 Position
        {
            get { return position; }
            set
            {
                position = value;
                destinationRectangle.X = (int)value.X;
                destinationRectangle.Y = (int)value.Y;
                destinationRectangle.Width = sourceRectangle.Width;
                destinationRectangle.Height = sourceRectangle.Height;
            }
        }
        public Vector2 PreviousPosition { get { return previousPosition; } }
        public Vector2 Velocity { get { return velocity; } set { velocity = value; } }
        public float Speed { get { return speed; } set { speed = value; } }
        public float Rotation { get { return rotation; } set { rotation = value; } }
        public int Width { get { return destinationRectangle.Width; } }
        public int Height { get { return destinationRectangle.Height; } }
        public string Sequence { get { return sequence.name; } }
        public virtual Rectangle BoundingBox { get { return new Rectangle((int)(Position.X - origin.X), (int)(Position.Y - origin.Y), Width, Height); } }

        public Sprite(Texture2D texture, List<Rectangle> textureRectangles)
        {
            this.texture = texture;
            this.textureRectangles = textureRectangles;

            this.sequence = new AnimationSequence(name: "Default", start: 0, count: 1, time: 0);
            this.currentFrame = 0;
            this.totalFrames = sequence.frames.Count;
            this.sourceRectangle = textureRectangles[sequence.frames[0]];
            this.destinationRectangle = textureRectangles[sequence.frames[0]];
        }

        public virtual void Update(GameTime gameTime)
        {
            previousPosition = position;
            var newPosition = position + Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds * Speed;
            Position = newPosition;

            WrapAroundScreen();
            UpdateAnimation(gameTime);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White, rotation, origin, SpriteEffects.None, 0.0f);
        }

        private void UpdateAnimation(GameTime gameTime)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.TotalSeconds;
            if (timeSinceLastFrame > SecondsBetweenFrames())
            {
                currentFrame++;
                timeSinceLastFrame = 0;
            }
            if (currentFrame == totalFrames)
            {
                currentFrame = 0;
                //animationPlayedOnce = true;
            }
            updateFrameDependents();
        }

        private double SecondsBetweenFrames()
        {
            return sequence.time / 1000.0 / totalFrames;
        }

        private void WrapAroundScreen()
        {
            var boundsWidth = 800;
            var boundsHeight = 500;

            if (position.X < 0)
                position.X = boundsWidth;
            else if (position.X > boundsWidth)
                position.X = 0;

            if (position.Y < 0)
                position.Y = boundsHeight;
            else if (position.Y > boundsHeight)
                position.Y = 0;
        }

        protected void setSequence(string sequenceName)
        {
            foreach (var sequence in sequences)
            {
                if (sequenceName == sequence.name)
                {
                    this.sequence = sequence;
                    this.currentFrame = 0;
                    this.totalFrames = sequence.frames.Count;
                    updateFrameDependents();
                    return;
                }
            }
        }

        private void updateFrameDependents()
        {
            sourceRectangle = textureRectangles[sequence.frames[currentFrame]];
            destinationRectangle.Width = sourceRectangle.Width;
            destinationRectangle.Height = sourceRectangle.Height;
            origin.X = destinationRectangle.Width / 2;
            origin.Y = destinationRectangle.Height / 2;
        }
    }
}
