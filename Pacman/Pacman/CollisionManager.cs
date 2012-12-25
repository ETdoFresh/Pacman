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
        private List<Ghost> ghosts;
        private MapManager mapManager;

        public CollisionManager(Player player, List<Ghost> ghosts, MapManager mapManager)
        {
            this.player = player;
            this.ghosts = ghosts;
            this.mapManager = mapManager;
        }
        public void Update(GameTime gameTime)
        {
            checkPlayerWallCollision();
        }

        private void checkPlayerWallCollision()
        {
        }
    }
}
