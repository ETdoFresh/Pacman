using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Pacman.DisplayObject;

namespace Pacman
{
    class Ghost: SpriteObject
    {
        public Vector2 Velocity { get; set; }
        public float Speed { get; set; }
        public bool IsDead { get; set; }

        public Ghost(GroupObject parent = null)
            : base(display.RetrieveTexture("pacman"), display.RetrieveSourceRectangles("pacman"))
        {
            if (parent == null)
                parent = display.Stage;
            parent.Insert(this);

            Speed = 200;
            addSequence("Still", 4, 1, 0);
            setSequence("Still");

            var animationTime = 150;
            addSequence("Up", 0, 2, animationTime);
            addSequence("Down", 2, 2, animationTime);
            addSequence("Left", 4, 2, animationTime);
            addSequence("Right", 6, 2, animationTime);
            Play();
        }

        public override void Update(GameTime gameTime)
        {
            if (!IsDead)
            {
                UpdateVelocityFromKeyboard();
                UpdatePositionFromVelocity(gameTime);
                UpdateSequence();
            }
            base.Update(gameTime);
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
            
            Velocity = newVelocity;
        }

        private void UpdatePositionFromVelocity(GameTime gameTime)
        {
            var time = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position += Velocity * time * Speed;
        }

        private void UpdateSequence()
        {
            Play();
            if (Velocity.X < 0)
                setSequence("Left");
            else if (Velocity.X > 0)
                setSequence("Right");
            else if (Velocity.Y < 0)
                setSequence("Up");
            else if (Velocity.Y > 0)
                setSequence("Down");
            else
                Pause();
        }
    }
}
