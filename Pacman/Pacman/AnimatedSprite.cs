using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Pacman
{
    public class AnimatedSprite : IDisposable
    {
        private string name;
        private Dictionary<string, SequenceData> sequences = new Dictionary<string, SequenceData>();
        private Kinematic kinematic;
        private Texture2D texture;
        private List<Rectangle> rectangles;

        public event EventHandler EndSequence;
        private double timeSinceLastFrame;
        private Vector2 origin;

        public AnimatedSprite(string filename, Kinematic kinematic)
        {
            this.kinematic = kinematic;

            texture = SpriteSheet.GetTexture(filename);
            rectangles = SpriteSheet.GetRectangles(filename);

            Runtime.GameUpdate += OnGameUpdate;
            Runtime.GameDraw += OnGameDraw;

            AddSequence("Default", 0, rectangles.Count);
            SetSequence("Default");
        }

        void OnGameDraw(SpriteBatch spriteBatch)
        {
            var index = Sequence.frames[CurrentFrame];
            var rectangle = rectangles[index];

            spriteBatch.Draw(
                texture: texture, position: kinematic.Position,
                sourceRectangle: rectangle, color: Color.White,
                rotation: kinematic.Orienation, origin: origin,
                scale: Vector2.One, effects: SpriteEffects.None, layerDepth: 0);
        }

        void OnGameUpdate(GameTime gameTime)
        {
            UpdateAnimation(gameTime);
        }

        private void UpdateAnimation(GameTime gameTime)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.TotalSeconds;
            if (timeSinceLastFrame > SecondsBetweenFrames())
            {
                CurrentFrame++;
                timeSinceLastFrame = 0;
            }
            if (CurrentFrame >= TotalFrames)
            {
                CurrentFrame = 0;
                if (EndSequence != null)
                    EndSequence(this, EventArgs.Empty);
            }
        }

        private double SecondsBetweenFrames()
        {
            return Sequence.time / 1000.0 / TotalFrames;
        }

        public void SetSequence(string name)
        {
            if (sequences.ContainsKey(name))
            {
                Sequence = sequences[name];

                var firstRectangle = rectangles[Sequence.frames[0]];
                origin = new Vector2(firstRectangle.Width / 2, firstRectangle.Height / 2);
            }
        }

        internal void AddSequence(string name, int start, int count = 1, int time = 1000, int loopCount = 0)
        {
            var frames = new int[count];
            for (int i = start; i < start + count; i++)
                frames[i - start] = i;
            AddSequence(name, frames, time, loopCount);
        }

        internal void AddSequence(string name, int[] frames, int time = 1000, int loopCount = 0)
        {
            sequences.Remove(name);
            var sequenceData = new SequenceData() { frames = frames, time = time, loopCount = loopCount };
            sequences.Add(name, sequenceData);
        }

        private class SequenceData
        {
            public int[] frames;
            public int time;
            public int loopCount;
        }

        private SequenceData Sequence { get; set; }
        public int CurrentFrame { get; set; }
        public int TotalFrames { get { return Sequence.frames.Length; } }

        public void Dispose()
        {
            Runtime.GameUpdate -= OnGameUpdate;
            Runtime.GameDraw -= OnGameDraw;
        }
    }
}
