using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pacman.Engine.Display;
using Pacman.Engine.Helpers;
using Microsoft.Xna.Framework;

namespace Pacman.Objects
{
    class BonusFruit : SpriteObject
    {
        Timer _appearTimer;
        TilePosition _tilePosition;
        PacmanObject _pacman;

        public delegate void EatenHandler();
        public event EatenHandler Eaten = delegate { };

        public BonusFruit(TileGrid tileGrid, PacmanObject pacman) : base("pacman", 34)
        {
            Random random = new Random();
            _appearTimer = new Timer(random.Next(9000, 10000));
            _appearTimer.ClockReachedLimit += OnAppearTimerLimit;
            AddComponent(_appearTimer);
            _tilePosition = new TilePosition(Position, tileGrid.TileWidth, tileGrid.TileHeight);
            AddComponent(_tilePosition);
            _pacman = pacman;
            Translate(tileGrid.GetPosition(13.5f, 17));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (_pacman.TilePosition.Vector == _tilePosition.Vector)
                Eaten();
        }

        private void OnAppearTimerLimit()
        {
            _appearTimer.ClockReachedLimit -= OnAppearTimerLimit;
            RemoveSelf();
        }

        public override void RemoveSelf()
        {
            Eaten = null;
            base.RemoveSelf();
        }
    }
}
