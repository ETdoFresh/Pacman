using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pacman.DisplayObject
{
    class SpriteObject : DisplayObject
    {
        private SequenceData sequenceData;
        private ImageSheet imageSheet;
        private Dictionary<string, SequenceData> sequences = new Dictionary<string, SequenceData>();
        private double timeSinceLastFrame;

        public int CurrentFrame { get; private set; }
        public bool IsPlaying { get; private set; }
        public int TotalFrames { get { return sequenceData.frames.Count; } }
        public string Sequence { get; private set; }
        public float TimeScale { get; set; }

        public SpriteObject(Texture2D texture, List<Rectangle> rectangles)
        {
            imageSheet = new ImageSheet() { texture = texture, sourceRectangles = rectangles };

            addSequence("Default", 0, 1);
            setSequence("Default");
            UpdateProperties();
        }

        public void Pause()
        {
            IsPlaying = false;
        }

        public void Play()
        {
            IsPlaying = true;
        }

        public void setFrame(int frameIndex)
        {
            CurrentFrame = frameIndex;
        }

        public void setSequence(string sequenceName)
        {
            if (sequences.ContainsKey(sequenceName))
            {
                Sequence = sequenceName;
                sequenceData = sequences[Sequence];
            }
        }

        public void addSequence(string sequenceName, List<int> frames, int time = 1000, int loopCount = 0)
        {
            sequences.Remove(sequenceName);
            var sequenceData = new SequenceData() { frames = frames, time = time, loopCount = loopCount };
            sequences.Add(sequenceName, sequenceData);
        }

        public void addSequence(string sequenceName, int start, int count, int time = 1000, int loopCount = 0)
        {
            var frames = new List<int>();
            for (int i = start; i < start + count; i++)
                frames.Add(i);
            addSequence(sequenceName, frames, time, loopCount);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            var sourceRectangleIndex = sequenceData.frames[CurrentFrame];
            var sourceRectangle = imageSheet.sourceRectangles[sourceRectangleIndex];
            spriteBatch.Draw(
                texture: imageSheet.texture,
                position: new Vector2(ContentX, ContentY),
                sourceRectangle: sourceRectangle,
                color: Color.White,
                rotation: ContentOrientation,
                origin: new Vector2(XOrigin, YOrigin),
                scale: new Vector2(ContentXScale, ContentYScale),
                effects: SpriteEffects.None,
                layerDepth: 0f);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (IsPlaying)
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
                CurrentFrame = 0;

            UpdateProperties();
        }

        private void UpdateProperties()
        {
            var sourceRectangleIndex = sequenceData.frames[CurrentFrame];
            var sourceRectangle = imageSheet.sourceRectangles[sourceRectangleIndex];
            Width = sourceRectangle.Width;
            Height = sourceRectangle.Height;
            XOrigin = Width / 2;
            YOrigin = Height / 2;
        }

        private double SecondsBetweenFrames()
        {
            return sequenceData.time / 1000.0 / TotalFrames;
        }

    }

    class SequenceData
    {
        public List<int> frames;
        public int time;
        public int loopCount;
    }
}
