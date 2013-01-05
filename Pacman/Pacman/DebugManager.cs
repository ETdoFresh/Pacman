using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Pacman
{
    class DebugManager : IGameObject
    {
        private SpriteFont font;
        private Map map;
        private string output;

        public DebugManager(SpriteFont font, Map map)
        {
            this.font = font;
            this.map = map;
        }

        public void Update(GameTime gameTime)
        {
            var tile = map.GetTileCoordinates(map.Player);
            var tile2 = map.GetTileCoordinates(map.Ghosts[0]);
            output = string.Format("player.Position = {0}, {1}\nplayer.tile = {2}, {3}\nghost[0].tile = {4}, {5}", map.Player.X, map.Player.Y, tile.X, tile.Y, tile2.X, tile2.Y);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, output, Vector2.Zero, Color.White);
        }
    }
}
