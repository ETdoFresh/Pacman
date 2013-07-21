using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Pacman.Engine.Display
{
    class AnimatedSpriteObject : SpriteObject
    {
        public int CurrentFrame { get; set; }
        public int TotalFrames { get { return _currentSequence.Frames.Length; } }

        private AnimationSequence _currentSequence;
        private Dictionary<string, AnimationSequence> _sequences;

        private double _timeSinceLastFrame;
        private bool _isPlaying;
        private float _playSpeed;

        public AnimatedSpriteObject(string assetFile)
            : base(assetFile, 0)
        {
            _sequences = new Dictionary<string, AnimationSequence>();
            _isPlaying = true;
            _playSpeed = 1.0f;
        }

        public override void LoadContent()
        {
            base.LoadContent();

            if (_currentSequence == null)
            {
                // Create an animation with all frames in Sprite with a 1 second playtime
                // This is done in Load since we don't know about rectangles until after content loads
                var frames = new int[_sourceRectangles.Count];
                for (int i = 0; i < _sourceRectangles.Count; i++) frames[i] = i;

                _currentSequence = new AnimationSequence() { Frames = frames, PlayTime = 1000 };
            }
            else
            {
                _sourceRectangle = _sourceRectangles[_currentSequence.Frames[CurrentFrame]];
                Width = _sourceRectangle.Width;
                Height = _sourceRectangle.Height;
                Origin = new Vector2(Width / 2, Height / 2);
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (_isPlaying)
            {
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

                if (_sourceRectangle != _sourceRectangles[_currentSequence.Frames[CurrentFrame]])
                {
                    _sourceRectangle = _sourceRectangles[_currentSequence.Frames[CurrentFrame]];
                    Width = _sourceRectangle.Width;
                    Height = _sourceRectangle.Height;
                    Origin = new Vector2(Width / 2, Height / 2);
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

        public void SetSequence(string name) { SetSequence(name, false); }
        public void SetSequence(string name, bool resetFrame)
        {
            _currentSequence = _sequences[name];

            if (CurrentFrame > TotalFrames || resetFrame)
                CurrentFrame = 0;

            if (_sourceRectangles != null)
                _sourceRectangle = _sourceRectangles[_currentSequence.Frames[CurrentFrame]];
        }

        public void AddSequence(string name, int start, int count) { AddSequence(name, start, count, 1000, -1, true); }
        public void AddSequence(string name, int start, int count, int time) { AddSequence(name, start, count, time, -1, true); }
        public void AddSequence(string name, int start, int count, int time, int loopCount) { AddSequence(name, start, count, time, loopCount, true); }
        public void AddSequence(string name, int start, int count, int time, int loopCount, bool setSequence)
        {
            var frames = new int[count];
            for (int i = start; i < start + count; i++) frames[i - start] = i;

            AddSequence(name, frames, time, loopCount, setSequence);
        }

        public void AddSequence(string name, int[] frames) { AddSequence(name, frames, 1000, -1, true); }
        public void AddSequence(string name, int[] frames, int playTime) { AddSequence(name, frames, playTime, -1, true); }
        public void AddSequence(string name, int[] frames, int playTime, int loopCount) { AddSequence(name, frames, playTime, loopCount, true); }
        public void AddSequence(string name, int[] frames, int playTime, int loopCount, bool setSequence)
        {
            _sequences.Remove(name);
            var sequence = new AnimationSequence() { Frames = frames, PlayTime = playTime, LoopCount = loopCount };
            _sequences.Add(name, sequence);

            if (setSequence) SetSequence(name);
        }
    }
}
