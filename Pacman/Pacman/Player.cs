using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Pacman.DisplayObject;

namespace Pacman
{
    class Player : SpriteObject
    {
        private Vector2 previousPosition;
        private Vector2 destination;

        public Vector2 Velocity { get; set; }
        public float Speed { get; set; }
        public bool IsDead { get; set; }

        public Player(GroupObject parent = null)
            : base(display.RetrieveTexture("pacman"), display.RetrieveSourceRectangles("pacman"))
        {
            if (parent == null)
                parent = display.Stage;
            parent.Insert(this);

            Speed = 200;
            addSequence("Still", 36, 1, 0);
            addSequence("Chomp", new List<int> { 36, 38, 36, 37 }, 200);
            setSequence("Chomp");
            Play();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (!IsDead)
            {
                UpdateVelocityFromKeyboard();
                UpdatePositionFromVelocity(gameTime);
                UpdateSequence();
            }
        }

        private void UpdateVelocityFromKeyboard()
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
                Velocity = newVelocity;
        }

        private void UpdatePositionFromVelocity(GameTime gameTime)
        {
            previousPosition = Position;
            var time = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position += Velocity * time * Speed;

            if (!Velocity.Equals(Vector2.Zero))
                Orientation = (float)Math.Atan2(-Velocity.Y, -Velocity.X);
            //orientation += Rotation * time * Speed;
        }

        private void UpdateSequence()
        {
            if (!Velocity.Equals(Vector2.Zero))
                setSequence("Chomp");
            else
                setSequence("Still");
        }
    }
}
