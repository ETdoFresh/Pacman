using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pacman.Engine.Display;

namespace Pacman.Objects
{
    class Score : DisplayObject
    {
        int _prevScore;
        int _score;
        TextObject _scoreText;
        int _numberOfGhostsEaten;
        int[] _ghostPoints = new int[4] { 200, 400, 800, 1600 };

        public Score()
        {
            _prevScore = -1;
            _score = 0;
            _scoreText = new TextObject("");
            AddComponent(_scoreText);
        }

        public void Add(int points)
        {
            _score += points;
        }

        public void ResetNumberOfGhostsEaten()
        {
            _numberOfGhostsEaten = 0;
        }

        public int AddForEatingGhost()
        {
            int points = _ghostPoints[_numberOfGhostsEaten];
            Add(points);

            if (_numberOfGhostsEaten < _ghostPoints.Length)
                _numberOfGhostsEaten++;

            return points;
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (_prevScore != _score)
            {
                _prevScore = _score;
                _scoreText.Text = string.Format("1UP: {0}", _score);
            }

            base.Update(gameTime);

            _scoreText.Translate(_scoreText.Width / 2, Stage.GameGraphicsDevice.Viewport.Height - _scoreText.Height / 2);
        }
    }
}
