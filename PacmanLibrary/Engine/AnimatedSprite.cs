using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacmanLibrary.Engine
{
    class AnimatedSprite : Sprite
    {
        public int CurrentFrame { get; set; }
        public int TotalFrames { get { return _currentSequence.Frames.Length; } }

        private AnimationSequence _currentSequence;
        private Dictionary<string, AnimationSequence> _sequences;

        private double _timeSinceLastFrame;
        private bool _isPlaying;
        private float _playSpeed;

        public AnimatedSprite(string assetFile, Group parent = null)
            : base(assetFile, 0, parent)
        {
            _sequences = new Dictionary<string, AnimationSequence>();
            _isPlaying = true;
            _playSpeed = 1.0f;
        }

        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            base.LoadContent(Content);

            if (_currentSequence == null)
            {
                // Create an animation with all frames in Sprite with a 1 second playtime
                // This is done in Load since we don't know about rectangles until after content loads
                var frames = new int[_sourceRectangles.Count];
                for (int i = 0; i < _sourceRectangles.Count; i++) frames[i] = i;

                _currentSequence = new AnimationSequence() { Frames = frames, PlayTime = 1000 };
            }
        }

        public override void Update(RenderContext renderContext)
        {
            base.Update(renderContext);

            if (_isPlaying)
            {
                var gameTime = renderContext.GameTime;
                var previousFrame = CurrentFrame;

                _timeSinceLastFrame += gameTime.ElapsedGameTime.TotalSeconds;

                if (_timeSinceLastFrame > SecondsBetweenFrames())
                {
                    CurrentFrame++;
                    _timeSinceLastFrame = 0;
                }
                if (CurrentFrame >= TotalFrames)
                {
                    if (_currentSequence.LoopCount < 0)
                    {
                        CurrentFrame = 0;
                    }
                    else
                    {
                        _currentSequence.LoopCount--;
                        if (_currentSequence.LoopCount == 0)
                        {
                            Pause();
                            CurrentFrame--;
                        }
                        else
                        {
                            CurrentFrame = 0;
                        }
                    }
                }

                if (CurrentFrame != previousFrame)
                {
                    _sourceRectangle = _sourceRectangles[_currentSequence.Frames[CurrentFrame]];
                    //Origin = new Vector2(sourceRectangle.Width / 2, sourceRectangle.Height / 2);
                }
            }
        }

        public void Play()
        {
            _isPlaying = true;
        }

        public void Pause()
        {
            _isPlaying = false;
        }

        private double SecondsBetweenFrames()
        {
            return _currentSequence.PlayTime / 1000.0 / TotalFrames / _playSpeed;
        }

        public void SetSequence(string name, bool resetFrame = false)
        {
            _currentSequence = _sequences[name];

            if (CurrentFrame > TotalFrames || resetFrame)
                CurrentFrame = 0;

            if (_sourceRectangles != null)
                _sourceRectangle = _sourceRectangles[_currentSequence.Frames[CurrentFrame]];
        }

        public void AddSequence(string name, int start, int count = 1, int time = 1000, int loopCount = -1, bool setSequence = true)
        {
            var frames = new int[count];
            for (int i = start; i < start + count; i++) frames[i - start] = i;

            AddSequence(name, frames, time, loopCount, setSequence);
        }

        public void AddSequence(string name, int[] frames, int playTime = 1000, int loopCount = -1, bool setSequence = true)
        {
            _sequences.Remove(name);
            var sequence = new AnimationSequence() { Frames = frames, PlayTime = playTime, LoopCount = loopCount};
            _sequences.Add(name, sequence);

            if (setSequence) SetSequence(name);
        }
    }
}
