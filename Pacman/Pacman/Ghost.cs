using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Pacman
{
    class Ghost : Sprite
    {
        protected bool isEatable;
        protected KeyboardState previousKeyboardState;
        protected KeyboardState currentKeyboardState;
        private double timeSinceEatable;
        private double eatableDuration;

        public bool IsEatable { get { return isEatable; } }
        public Rectangle BoundingBox { get { return new Rectangle((int)X - Width / 2, (int)Y - Height / 2, Width, Height); } }

        public Ghost(Texture2D texture, List<Rectangle> sourceRectangles)
            : base(texture, sourceRectangles)
        {
            sequence = new Sequence(name: "Default", start: 0, count: 2, time: 200);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            currentKeyboardState = Keyboard.GetState();
            UpdateSequenceFromKeyboard();
            previousKeyboardState = currentKeyboardState;

            if (isEatable)
                checkIfEatableExpired(gameTime);
        }

        private void checkIfEatableExpired(GameTime gameTime)
        {
            timeSinceEatable += gameTime.ElapsedGameTime.TotalSeconds;
            if (timeSinceEatable >= eatableDuration)
                isEatable = false;
        }

        private void UpdateSequenceFromKeyboard()
        {
            if (currentKeyboardState.IsKeyDown(Keys.Space) && !previousKeyboardState.IsKeyDown(Keys.Space))
                isNowEatable(duration: 5);

            string sequenceName;
            if (isEatable)
            {
                if (eatableDuration - timeSinceEatable < 2)
                    sequenceName = "EatableFastFlashing";
                else if (eatableDuration - timeSinceEatable < 4)
                    sequenceName = "EatableFlashing";
                else
                    sequenceName = "Eatable";
            }
            else
            {
                if (Velocity.X > 0)
                    sequenceName = "MoveRight";
                else if (Velocity.X < 0)
                    sequenceName = "MoveLeft";
                else if (Velocity.Y > 0)
                    sequenceName = "MoveDown";
                else if (Velocity.Y < 0)
                    sequenceName = "MoveUp";
                else
                    sequenceName = "Still";
            }

            setSequence(sequenceName);
        }

        private void isNowEatable(double duration)
        {
            isEatable = true;
            timeSinceEatable = 0;
            eatableDuration = duration;
            setSequence("Eatable");
        }

        public void die()
        {
        }
    }
}
