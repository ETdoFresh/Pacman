using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Pacman
{
    class CollisionManager
    {
        private Player player;
        private List<Ghost> ghosts;
        private List<Tile> walls;

        public CollisionManager(Player player, List<Ghost> ghosts, List<Tile> walls)
        {
            this.player = player;
            this.ghosts = ghosts;
            this.walls = walls;
        }

        public void Update(GameTime gameTime)
        {
            CheckCollisions();
        }

        private void CheckCollisions()
        {
            CheckPlayerToGhost();
            CheckPlayerToWalls();
        }

        private void CheckPlayerToWalls()
        {
            for (int i = 0; i < walls.Count; i++)
            {
                var wall = walls[i];
                if (player.BoundingBox.Intersects(wall.BoundingBox))
                {
                    var collidedPosition = player.Position;
                    var previousPosition = player.PreviousPosition;
                    var width = player.BoundingBox.Width;
                    var height = player.BoundingBox.Height;

                    var xCheckBox = new Rectangle ((int)(collidedPosition.X - width / 2), (int)(previousPosition.Y - height / 2), width, height);
                    var yCheckBox = new Rectangle ((int)(previousPosition.X - width / 2), (int)(collidedPosition.Y - height / 2), width, height);

                    if (!xCheckBox.Intersects(wall.BoundingBox))
                        player.Position = new Vector2(collidedPosition.X, previousPosition.Y);
                    else if (!yCheckBox.Intersects(wall.BoundingBox))
                        player.Position = new Vector2(previousPosition.X, collidedPosition.Y);
                }
            }
        }

        private void CheckPlayerToGhost()
        {
            for (int i = 0; i < ghosts.Count; i++)
            {
                var ghost = ghosts[i];
                if (player.BoundingBox.Intersects(ghost.BoundingBox))
                {
                    if (ghost.IsEatable)
                        ghost.die();
                    else
                        player.die();
                }
            }
        }
    }
}
