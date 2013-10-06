using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pacman.Engine.Display;
using Pacman.Engine.Helpers;

namespace Pacman.Objects
{
    class BonusFruit : SpriteObject
    {
        private Timer _appearTimer;

        public BonusFruit(TileGrid _tileGrid) : base("pacman", 34)
        {
            Random random = new Random();
            _appearTimer = new Timer(random.Next(9000, 10000));
            AddComponent(_appearTimer);
            _appearTimer.ClockReachedLimit += OnAppearTimerLimit;
            Translate(_tileGrid.GetPosition(13.5f, 17));
        }

        private void OnAppearTimerLimit()
        {
            _appearTimer.ClockReachedLimit -= OnAppearTimerLimit;
            RemoveSelf();
        }

        public override void RemoveSelf()
        {
            _appearTimer.ClockReachedLimit -= OnAppearTimerLimit;
            base.RemoveSelf();
        }
    }
}
