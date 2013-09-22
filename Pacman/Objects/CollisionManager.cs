using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pacman.Engine.Display;
using Microsoft.Xna.Framework;

namespace Pacman.Objects
{
    class CollisionManager : GameObject
    {
        public delegate void CollisionHandler(PacmanObject pacman, Ghost ghost);
        public event CollisionHandler Collision = delegate { };

        private PacmanObject _pacman;
        private Ghost _blinky;
        private Ghost _pinky;
        private Ghost _inky;
        private Ghost _clyde;
        private Ghost[] _ghostArray;

        public CollisionManager(PacmanObject pacman, Ghost blinky, Ghost pinky, Ghost inky, Ghost clyde)
        {
            _pacman = pacman;
            _blinky = blinky;
            _pinky = pinky;
            _inky = inky;
            _clyde = clyde;
            _ghostArray = new Ghost[4] { blinky, pinky, inky, clyde };
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            foreach (Ghost ghost in _ghostArray)
            {
                if (_pacman.TilePosition.Vector == ghost.TilePosition.Vector)
                {
                    Collision(_pacman, ghost);
                }
            }
        }
    }
}
