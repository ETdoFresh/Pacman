using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pacman
{
    class Animation : Image
    {
        protected List<Rectangle> sourceRectangles;
        protected Sequence sequence;

        public List<Sequence> sequences;

        private int currentFrame;
        private int totalFrames;
        private double timeSinceLastFrame;

        public Animation(Texture2D texture, List<Rectangle> sourceRectangles) : base(texture)
        {
            this.sourceRectangles = sourceRectangles;
            UpdateSourceRectangle(sourceRectangles[0]);

            sequence = new Sequence(name: "Default", start: 0, count: sourceRectangles.Count);
            totalFrames = sequence.frames.Count;
        }

        public override void  Update(GameTime gameTime)
        {
            base.Update(gameTime);
            UpdateAnimation(gameTime);
        }

        public void setSequence(string sequenceName)
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
