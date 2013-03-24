using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DisplayLibrary
{
    public class AnimatedSprite : DisplayObject
    {
        private List<Rectangle> sourceRectangles;
        private Dictionary<String, Sequence> sequences = new Dictionary<string, Sequence>();
        private Sequence sequence;
        private double timeSinceLastFrame;

        public int CurrentFrame { get; set; }
        public int TotalFrames { get { return sequence.frames.Length; } }

        public AnimatedSprite(String filename, Position position = null, Rotation rotation = null, Scale scale = null, GroupObject parent = null)
            : base(parent, position, rotation, scale)
        {
            texture = SpriteSheet.GetTexture(filename);
            sourceRectangles = SpriteSheet.GetRectangles(filename);

            AddSequence("Default", 0, sourceRectangles.Count, time: 5000);
            SetSequence("Default");
            sourceRectangle = sourceRectangles[sequence.frames[0]];

            Runtime.GameUpdate += Update;
        }

        private void Update(GameTime gameTime)
        {
            var previousFrame = CurrentFrame;
            timeSinceLastFrame += gameTime.ElapsedGameTime.TotalSeconds;

            if (timeSinceLastFrame > SecondsBetweenFrames())
            {
                CurrentFrame++;
                timeSinceLastFrame = 0;
            }
            if (CurrentFrame >= TotalFrames)
                CurrentFrame = 0;

            if (CurrentFrame != previousFrame)
            {
                sourceRectangle = sourceRectangles[sequence.frames[CurrentFrame]];
                origin = new Vector2(sourceRectangle.Width / 2, sourceRectangle.Height / 2);
            }
        }

        private double SecondsBetweenFrames()
        {
            return sequence.time / 1000.0 / TotalFrames;
        }

        public void SetSequence(string name)
        {
            sequence = sequences[name];

            if (CurrentFrame > TotalFrames)
                CurrentFrame = 0;
        }

        public void AddSequence(string name, int start, int count = 1, int time = 1000, int loopCount = 0)
        {
            var frames = new int[count];
            for (int i = start; i < start + count; i++)
                frames[i - start] = i;
            AddSequence(name, frames, time, loopCount);
        }

        public void AddSequence(string name, int[] frames, int time = 1000, int loopCount = 0)
        {
            sequences.Remove(name);
            var sequence = new Sequence() { frames = frames, time = time, loopCount = loopCount };
            sequences.Add(name, sequence);
        }

        public override void Dispose()
        {
            Runtime.GameUpdate -= Update;
            base.Dispose();
        }
    }
}
