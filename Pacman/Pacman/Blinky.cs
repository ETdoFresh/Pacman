using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Pacman
{
    class Blinky
    {
        private Texture2D texture;
        private List<Rectangle> textureRectangles;
        private Rectangle sourceRectangle = Rectangle.Empty;
        private Rectangle destinationRectangle = Rectangle.Empty;
        private Vector2 position = Vector2.Zero;
        private Vector2 velocity = Vector2.Zero;
        private Vector2 origin = Vector2.Zero;
        private float speed = 300f;
        private float rotation = 0f;
        private AnimationSequence sequence;
        private int currentFrame;
        private int totalFrames;
        private double timeSinceLastFrame;
        private bool animationPlayedOnce;
        private List<AnimationSequence> sequences = new List<AnimationSequence>()
        {
            new AnimationSequence(name: "MoveUp", start: 0, count: 2, time: 200),
            new AnimationSequence(name: "MoveDown", start: 2, count: 2, time: 200),
            new AnimationSequence(name: "MoveLeft", start: 4, count: 2, time: 200),
            new AnimationSequence(name: "MoveRight", start: 6, count: 2, time: 200),
        };

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
        public Vector2 Velocity { get { return velocity; } set { velocity = value; } }
        public float Speed { get { return speed; } set { speed = value; } }

        public Blinky(Texture2D texture, List<Rectangle> textureRectangles)
        {
            this.texture = texture;
            this.textureRectangles = textureRectangles;

            this.sequence = sequences[0];
            this.currentFrame = 0;
            this.totalFrames = sequence.frames.Count;
            this.sourceRectangle = textureRectangles[sequence.frames[0]];
            this.destinationRectangle = textureRectangles[sequence.frames[0]];
            origin.X = destinationRectangle.Width / 2;
            origin.Y = destinationRectangle.Height / 2;
        }
        public void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();
            UpdateVelocityFromKeyboard(keyboardState);

            var newPosition = position + Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds * Speed;
            Position = newPosition;

            WrapAroundScreen();
            UpdateAnimation(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
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
                animationPlayedOnce = true;
            }
            updateSourceRectangle();
        }

        private double SecondsBetweenFrames()
        {
            return sequence.time / 1000.0 / totalFrames;
        }

        private void WrapAroundScreen()
        {
            var boundsWidth = 800;
            var boundsHeight = 450;

            if (position.X < 0)
                position.X = boundsWidth;
            else if (position.X > boundsWidth)
                position.X = 0;

            if (position.Y < 0)
                position.Y = boundsHeight;
            else if (position.Y > boundsHeight)
                position.Y = 0;
        }

        private void UpdateVelocityFromKeyboard(KeyboardState keyboardState)
        {
            var keyDictionary = new Dictionary<Keys, Vector2>
            {
                {Keys.Left, new Vector2(-1, 0)},
                {Keys.Right, new Vector2(1, 0)},
                {Keys.Up, new Vector2(0, -1)},
                {Keys.Down, new Vector2(0, 1)},
            };

            var keySequence = new Dictionary<Keys, string>
            {
                {Keys.Left, "MoveLeft"},
                {Keys.Right, "MoveRight"},
                {Keys.Up, "MoveUp"},
                {Keys.Down, "MoveDown"},
            };

            var velocity = Velocity;
            foreach (var key in keyDictionary)
            {
                if (keyboardState.IsKeyDown(key.Key))
                {
                    velocity = key.Value;
                    setSequence(keySequence[key.Key]);
                }
            }
            Velocity = velocity;
        }

        private void setSequence(string sequenceName)
        {
            foreach (var sequence in sequences)
            {
                if (sequenceName == sequence.name)
                {
                    this.currentFrame = 0;
                    this.sequence = sequence;
                    return;
                }
            }
        }

        private void updateSourceRectangle()
        {
            sourceRectangle = textureRectangles[sequence.frames[currentFrame]];
        }
    }
}
