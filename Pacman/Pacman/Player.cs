using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pacman
{
    class Player : IGameObject
    {
        private Rectangle sourceRectangle;
        private Vector2 position;
        private Vector2 previousPosition;
        private Vector2 destination;
        private Vector2 origin;
        private Vector2 velocity;
        private float orientation;
        private float speed;
        private List<Rectangle> sourceRectangles;
        private Texture2D texture;
        private Sequence[] sequences;
        private Sequence sequence;
        private double timeSinceLastFrame;
        private int currentFrame;
        private int totalFrames;

        public int X { get { return (int)position.X; } set { previousPosition = position; position.X = value; } }
        public int Y { get { return (int)position.Y; } set { previousPosition = position; position.Y = value; } }
        public Vector2 Position { get { return position; } }
        public Vector2 InputDirection { get; set; }
        public Vector2 Destination { get { return destination; } set { destination = value; } }

        public Player(Texture2D texture, List<Rectangle> sourceRectangles)
        {
            this.texture = texture;
            this.sourceRectangles = sourceRectangles;

            speed = 200;
            sequences = new Sequence[]
            {
                new Sequence(name: "Still", start: 36, count: 1, time: 0),
                new Sequence(name: "Chomp", frames: new List<int>() { 36, 37, 36, 38 }, time: 200),
                new Sequence(name: "Die", start: 38, count: 12, time: 0),
            };
            sequence = sequences[0];
            totalFrames = sequence.frames.Count;
            origin = new Vector2(sourceRectangle.Width / 2, sourceRectangle.Height / 2);
            InputDirection = new Vector2(-1, 0);
        }

        public void Update(GameTime gameTime)
        {
            UpdateIntendedVelocity();
            UpdateVelocity();
            UpdatePositionFromVelocity(gameTime);
            UpdateAnimation(gameTime);
            UpdateSequence();
        }

        private void UpdateIntendedVelocity()
        {
            var keyboardState = Keyboard.GetState();
            var keyDictionary = new Dictionary<Keys, Vector2>
            {
                {Keys.Left, new Vector2(-1, 0)},
                {Keys.Right, new Vector2(1, 0)},
                {Keys.Up, new Vector2(0, -1)},
                {Keys.Down, new Vector2(0, 1)},
            };

            var newVelocity = Vector2.Zero;
            foreach (var key in keyDictionary)
                if (keyboardState.IsKeyDown(key.Key))
                    newVelocity += key.Value;

            if (!newVelocity.Equals(Vector2.Zero))
                InputDirection = newVelocity;
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
            if (!velocity.Equals(Vector2.Zero))
                setSequence("Chomp");
            else
                setSequence("Still");
        }

        private void setSequence(string sequenceName)
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
            previousPosition = position;
            var time = (float)gameTime.ElapsedGameTime.TotalSeconds;
            position += velocity * time * speed;

            var destinationLength = (destination - previousPosition).Length();
            var movementLength = (position - previousPosition).Length();

            if (destinationLength < movementLength)
                position = destination;

            if (!velocity.Equals(Vector2.Zero))
                orientation = (float)Math.Atan2(-velocity.Y, -velocity.X);
            //orientation += Rotation * time * Speed;
        }

        private void UpdateVelocity()
        {
            velocity = destination - position;
            if (!velocity.Equals(Vector2.Zero))
                velocity.Normalize();
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

        internal void collideWithWall()
        {
            setPreviosPosition();
            stopVelocity();
        }

        private void stopVelocity()
        {
            velocity = Vector2.Zero;
        }

        private void setPreviosPosition()
        {
            position = previousPosition;
        }
    }

    public struct Sequence
    {
        public string name;
        public List<int> frames;
        public int time;

        public Sequence(string name, List<int> frames, int time = 1000)
        {
            this.name = name;
            this.frames = frames;
            this.time = time;
        }

        public Sequence(string name, int start = 0, int count = 1, int time = 1000)
        {
            this.name = name;
            this.frames = new List<int>();
            for (int i = start; i < start + count; i++)
                this.frames.Add(i);
            this.time = time;
        }
    }
}
