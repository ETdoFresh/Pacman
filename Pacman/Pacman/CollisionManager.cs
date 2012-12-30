using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Pacman
{
    class CollisionManager
    {
        private Player player;

        public CollisionManager(Player player)
        {
            this.player = player;
        }

        internal void Update(GameTime gameTime)
        {
            var mapLimitX = 800;
            var mapLimitY = 500;
            
            if (player.X > mapLimitX)
                player.X = 0;
            else if (player.X < 0)
                player.X = mapLimitX;

            if (player.Y > mapLimitY)
                player.Y = 0;
            else if (player.Y < 0)
                player.Y = mapLimitY;
        }
    }
}
