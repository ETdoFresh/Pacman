using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DisplayLibrary;

namespace Pacman
{
    class Pellet : GameObject
    {
        public Boolean IsPowerPellet { get; set; }
        public Sprite Sprite { get; set; }
        public Collision Collision { get; set; }

        public Pellet(float x = 0, float y = 0, GroupObject displayParent = null, Boolean isPowerPellet = false)
        {
            Position = new Position(x, y);
            TilePosition = new TilePosition(Position);
            IsPowerPellet = isPowerPellet;
            Collision = new Collision(this);

            if (isPowerPellet)
                Sprite = new Sprite("pacman", 59, Position, parent: displayParent);
            else
                Sprite = new Sprite("pacman", 60, Position, parent: displayParent);

        }

        public override void Dispose()
        {
            Collision.Dispose();
            Sprite.Dispose();
            TilePosition.Dispose();
            base.Dispose();
        }
    }
}
