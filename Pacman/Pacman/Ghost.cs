using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pacman
{
    class Ghost : IGameObject
    {
        private Rectangle sourceRectangle;
        private Vector2 position;
        private Vector2 origin;
        private Vector2 velocity;
        private float orientation;
        private float speed;
        private List<Rectangle> sourceRectangles;
        private Texture2D texture;
        protected Sequence[] sequences;
        private Sequence sequence;
        private double timeSinceLastFrame;
        private int currentFrame;
        private int totalFrames;

        public float X { get { return position.X; } set { position.X = value; } }
        public float Y { get { return position.Y; } set { position.Y = value; } }

        public Ghost(Texture2D texture, List<Rectangle> sourceRectangles)
        {
            this.texture = texture;
            this.sourceRectangles = sourceRectangles;

            speed = 200;
            sequences = new Sequence[] { new Sequence(name: "NoAnimation", start: 34, count: 1, time: 0), };
            setSequence("NoAnimation");
        }

        public void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();
            UpdateVelocityFromKeyboard(keyboardState);
            UpdatePositionFromVelocity(gameTime);
            UpdateAnimation(gameTime);
            UpdateSequence();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var tint = Color.White;
            var spriteEffect = SpriteEffects.None;
            var layerDepth = 0.0f;
            var scale = 1.0f;
            spriteBatch.Draw(texture, position, sourceRectangle, tint, orientation, origin, scale, spriteEffect, layerDepth);
        }

        private void UpdateSequence()
        {
            if (velocity.X > 0)
                setSequence("MoveRight");
            else if (velocity.X < 0)
                setSequence("MoveLeft");
            else if (velocity.Y > 0)
                setSequence("MoveDown");
            else if (velocity.Y < 0)
                setSequence("MoveUp");
            else if (velocity.Equals(Vector2.Zero))
                setSequence("Still");
        }

        protected void setSequence(string sequenceName)
        {
            if (sequence.name == sequenceName)
                return;

            foreach (var seq in sequences)
            {
                if (seq.name == sequenceName)
                {
                    sequence = seq;
                    totalFrames = sequence.frames.Count;
                    return;
                }
            }
        }

        private void UpdatePositionFromVelocity(GameTime gameTime)
        {
            var time = (float)gameTime.ElapsedGameTime.TotalSeconds;
            position += velocity * time * speed;
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

            velocity = Vector2.Zero;
            foreach (var key in keyDictionary)
                if (keyboardState.IsKeyDown(key.Key))
                    velocity += key.Value;
        }

        private void UpdateAnimation(GameTime gameTime)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.TotalSeconds;
            if (timeSinceLastFrame > SecondsBetweenFrames())
            {
                currentFrame++;
                timeSinceLastFrame = 0;
            }
            if (currentFrame >= totalFrames)
                currentFrame = 0;

            UpdateSourceRectangle(sourceRectangles[sequence.frames[currentFrame]]);
        }

        private double SecondsBetweenFrames()
        {
            return sequence.time / 1000.0 / totalFrames;
        }

        private void UpdateSourceRectangle(Rectangle newSourceRectangle)
        {
            this.sourceRectangle = newSourceRectangle;
            setOriginToCenter();
        }

        private void setOriginToCenter()
        {
            origin.X = sourceRectangle.Width / 2;
            origin.Y = sourceRectangle.Height / 2;
        }
    }

    class Blinky : Ghost
    {
        public Blinky(Texture2D texture, List<Rectangle> textureRectangles)
            : base(texture, textureRectangles)
        {
            sequences = new Sequence[]
            {
                new Sequence(name: "Still", start: 2, count: 1, time: 0),
                new Sequence(name: "MoveUp", start: 0, count: 2, time: 200),
                new Sequence(name: "MoveDown", start: 2, count: 2, time: 200),
                new Sequence(name: "MoveLeft", start: 4, count: 2, time: 200),
                new Sequence(name: "MoveRight", start: 6, count: 2, time: 200),
                new Sequence(name: "Eatable", start: 32, count: 2, time: 200),
                new Sequence(name: "EatableFlashing", frames: new List<int>{32,33,32,33,34,35,34,35}, time: 400),
                new Sequence(name: "EatableFastFlashing", start: 32, count: 4, time: 200),
            };
            setSequence("Still");
        }
    }

    class Clyde : Ghost
    {
        public Clyde(Texture2D texture, List<Rectangle> textureRectangles)
            : base(texture, textureRectangles)
        {
            this.sequences = new Sequence[]
            {
                new Sequence(name: "Still", start: 26, count: 1, time: 0),
                new Sequence(name: "MoveUp", start: 24, count: 2, time: 200),
                new Sequence(name: "MoveDown", start: 26, count: 2, time: 200),
                new Sequence(name: "MoveLeft", start: 28, count: 2, time: 200),
                new Sequence(name: "MoveRight", start: 30, count: 2, time: 200),
                new Sequence(name: "Eatable", start: 32, count: 2, time: 200),
                new Sequence(name: "EatableFlashing", frames: new List<int>{32,33,32,33,34,35,34,35}, time: 400),
                new Sequence(name: "EatableFastFlashing", start: 32, count: 4, time: 200),
            };

            setSequence("Still");
        }
    }

    class Inky : Ghost
    {
        public Inky(Texture2D texture, List<Rectangle> textureRectangles)
            : base(texture, textureRectangles)
        {
            this.sequences = new Sequence[]
            {
                new Sequence(name: "Still", start: 18, count: 1, time: 0),
                new Sequence(name: "MoveUp", start: 16, count: 2, time: 200),
                new Sequence(name: "MoveDown", start: 18, count: 2, time: 200),
                new Sequence(name: "MoveLeft", start: 20, count: 2, time: 200),
                new Sequence(name: "MoveRight", start: 22, count: 2, time: 200),
                new Sequence(name: "Eatable", start: 32, count: 2, time: 200),
                new Sequence(name: "EatableFlashing", frames: new List<int>{32,33,32,33,34,35,34,35}, time: 400),
                new Sequence(name: "EatableFastFlashing", start: 32, count: 4, time: 200),
            };

            setSequence("Still");
        }
    }

    class Pinky : Ghost
    {
        public Pinky(Texture2D texture, List<Rectangle> textureRectangles)
            : base(texture, textureRectangles)
        {
            this.sequences = new Sequence[]
            {
                new Sequence(name: "Still", start: 10, count: 1, time: 0),
                new Sequence(name: "MoveUp", start: 8, count: 2, time: 200),
                new Sequence(name: "MoveDown", start: 10, count: 2, time: 200),
                new Sequence(name: "MoveLeft", start: 12, count: 2, time: 200),
                new Sequence(name: "MoveRight", start: 14, count: 2, time: 200),
                new Sequence(name: "Eatable", start: 32, count: 2, time: 200),
                new Sequence(name: "EatableFlashing", frames: new List<int>{32,33,32,33,34,35,34,35}, time: 400),
                new Sequence(name: "EatableFastFlashing", start: 32, count: 4, time: 200),
            };

            setSequence("Still");
        }
    }
}
