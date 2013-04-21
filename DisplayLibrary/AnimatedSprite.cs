using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DisplayLibrary
{
    public class AnimatedSprite : Sprite
    {
        private Dictionary<String, Sequence> sequences = new Dictionary<string, Sequence>();
        private Sequence sequence = new Sequence() { frames = new int[] { 0 } };
        private double timeSinceLastFrame;

        public int CurrentFrame { get; set; }
        public int TotalFrames { get { return sequence.frames.Length; } }

        public delegate void EndSequenceHandler();
        public event EndSequenceHandler EndSequence = delegate { };

        public AnimatedSprite(String filename, Position position = null, Rotation rotation = null, Scale scale = null, GroupObject parent = null)
            : base(filename, 0, position, rotation, scale, parent)
        {
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
            {
                CurrentFrame = 0;
                EndSequence();
            }

            if (CurrentFrame != previousFrame)
            {
                sourceRectangle = sourceRectangles[sequence.frames[CurrentFrame]];
                origin = new Vector2(sourceRectangle.Width / 2, sourceRectangle.Height / 2);
            }
        }

        public void Play()
        {
            Runtime.GameUpdate -= Update;
            Runtime.GameUpdate += Update;
        }

        public void Pause()
        {
            Runtime.GameUpdate -= Update;
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

            sourceRectangle = sourceRectangles[sequence.frames[CurrentFrame]];
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
