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
        public override Rectangle BoundingBox { get { return new Rectangle((int)(Position.X - 5), (int)(Position.Y - 5), 10, 10); } }

        public Player(Texture2D texture, List<Rectangle> textureRectangles)
            : base(texture, textureRectangles)
        {
            this.sequences = new List<AnimationSequence>()
            {
                new AnimationSequence(name: "Still", start: 36, count: 1, time: 0),
                new AnimationSequence(name: "Chomp", frames: new List<int>() { 36, 37, 36, 38 }, time: 200)
            };

            setSequence("Still");
        }

        public override void Update(GameTime gameTime)
        {
            string sequenceName;

            if (Velocity == Vector2.Zero)
                sequenceName = "Still";
            else
            {
                sequenceName = "Chomp";
                Rotation = (float)Math.Atan2(-Velocity.Y, -Velocity.X);
            }

            if (Sequence != sequenceName)
                setSequence(sequenceName);

   
            base.Update(gameTime);
        }

        public void die()
        {
            Velocity = Vector2.Zero;
            Position = new Vector2(Width, Height);
        }
    }
}
