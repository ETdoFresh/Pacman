using Microsoft.Xna.Framework;

namespace Pacman.Objects
{
    class Blinky : Ghost
    {
        private Target _target;

        public MoveTowardsPacman MoveTowardsPacman { get; set; }

        public Blinky()
        {
            _body.Tint = Color.Red;

            _target = new Target();
            _target.Tint = Color.Red;
        }
    }
}
