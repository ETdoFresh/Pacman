using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Pacman.Engine.Display
{
    /// <summary>
    /// Same as SpriteObject, but displays a tile/sprite as an animation
    /// </summary>
    class AnimatedSpriteObject : SpriteObject
    {
        public int CurrentFrame { get; set; }
        public int TotalFrames { get { return _currentSequence.Frames.Length; } }

        AnimationSequence _currentSequence;
        Dictionary<string, AnimationSequence> _sequences;
        double _timeOfAnimationLoop;
        bool _isPlaying;
        float _playSpeed;

        public AnimatedSpriteObject(string assetFile)
            : base(assetFile, 0)
        {
            _sequences = new Dictionary<string, AnimationSequence>();
            _isPlaying = true;
            _playSpeed = 1.0f;
        }

        /// <summary>
        /// Loads the sprite sheet and updates properties.
        /// If sequence not set, entire spritesheet is loaded as animation
        /// </summary>
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

        /// <summary>
        /// Increments to next frame, updates properties, and setup to be drawn
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (_isPlaying)
            {
                var previousFrame = CurrentFrame;

                _timeOfAnimationLoop += gameTime.ElapsedGameTime.TotalSeconds;

                CurrentFrame = (int)(Math.Floor(_timeOfAnimationLoop / SecondsBetweenFrames()));
                if (CurrentFrame >= TotalFrames)
                {
                    _timeOfAnimationLoop = 0;
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

        /// <summary>
        /// Play Animation
        /// </summary>
        public void Play()
        {
            _isPlaying = true;
        }

        /// <summary>
        /// Pause Animation
        /// </summary>
        public void Pause()
        {
            _isPlaying = false;
        }

        /// <summary>
        /// Computes number of seconds between frames
        /// </summary>
        /// <returns>Seconds between frames</returns>
        private double SecondsBetweenFrames()
        {
            return _currentSequence.PlayTime / 1000.0 / TotalFrames / _playSpeed;
        }

        /// <summary>
        /// Sets/Changes animation sequence.
        /// </summary>
        /// <param name="name">Animation sequence name</param>
        /// <param name="resetFrame">Reset current frame to zero?</param>
        public void SetSequence(string name, bool resetFrame)
        {
            _currentSequence = _sequences[name];

            if (CurrentFrame > TotalFrames || resetFrame)
                CurrentFrame = 0;

            if (_sourceRectangles != null)
                _sourceRectangle = _sourceRectangles[_currentSequence.Frames[CurrentFrame]];
        }
        public void SetSequence(string name) { SetSequence(name, false); }

        /// <summary>
        /// Add a sequence to AnimateSpriteObject.
        /// </summary>
        /// <param name="name">Animation sequence name</param>
        /// <param name="start">Starting index in spritesheet</param>
        /// <param name="count">Number of frames</param>
        /// <param name="time">Milliseconds until animation loops/ends</param>
        /// <param name="loopCount">Number of loops until animation stops (-1 = infinite looping)</param>
        /// <param name="setSequence">Set sequence automatically</param>
        public void AddSequence(string name, int start, int count, int time, int loopCount, bool setSequence)
        {
            var frames = new int[count];
            for (int i = start; i < start + count; i++) frames[i - start] = i;

            AddSequence(name, frames, time, loopCount, setSequence);
        }
        public void AddSequence(string name, int start, int count, int time, int loopCount) { AddSequence(name, start, count, time, loopCount, true); }
        public void AddSequence(string name, int start, int count, int time) { AddSequence(name, start, count, time, -1, true); }
        public void AddSequence(string name, int start, int count) { AddSequence(name, start, count, 1000, -1, true); }

        /// <summary>
        /// Add a sequence to AnimateSpriteObject.
        /// </summary>
        /// <param name="name">Animation sequence name</param>
        /// <param name="frames">Frame of indexes that makeup the animation</param>
        /// <param name="playTime">Milliseconds until animation loops/ends</param>
        /// <param name="loopCount">Number of loops until animation stops (-1 = infinite looping)</param>
        /// <param name="setSequence">Set sequence automatically</param>
        public void AddSequence(string name, int[] frames, int playTime, int loopCount, bool setSequence)
        {
            _sequences.Remove(name);
            var sequence = new AnimationSequence() { Frames = frames, PlayTime = playTime, LoopCount = loopCount };
            _sequences.Add(name, sequence);

            if (setSequence) SetSequence(name);
        }
        public void AddSequence(string name, int[] frames, int playTime, int loopCount) { AddSequence(name, frames, playTime, loopCount, true); }
        public void AddSequence(string name, int[] frames, int playTime) { AddSequence(name, frames, playTime, -1, true); }
        public void AddSequence(string name, int[] frames) { AddSequence(name, frames, 1000, -1, true); }
    }
}
