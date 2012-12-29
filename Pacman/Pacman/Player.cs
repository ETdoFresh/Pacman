using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Pacman
{
    class Player : Sprite
    {
        public Player(Texture2D texture, List<Rectangle> textureRectangles)
            : base(texture, textureRectangles)
        {
            this.sequences = new List<Sequence>()
            {
                new Sequence(name: "Still", start: 36, count: 1, time: 0),
                new Sequence(name: "Chomp", frames: new List<int>() { 36, 37, 36, 38 }, time: 200)
            };

            setSequence("Still");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            updateDirection();

            string sequenceName;
            if (Velocity == Vector2.Zero)
                sequenceName = "Still";
            else
            {
                sequenceName = "Chomp";
                Orientation = (float)Math.Atan2(-Velocity.Y, -Velocity.X);
            }
            setSequence(sequenceName);
        }

        public void die()
        {
            Velocity = Vector2.Zero;
            Position = new Vector2(Width, Height);
        }
    }
}
