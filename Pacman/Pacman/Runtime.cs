using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pacman
{
    class Runtime
    {
        public delegate void GameUpdateHandler(GameTime gameTime);
        public delegate void GameDrawHandler(SpriteBatch spriteBatch);
        
        static public event GameUpdateHandler GameUpdate;
        static public event GameDrawHandler GameDraw;

        static public void Update(GameTime gameTime)
        {
            if (GameUpdate != null) GameUpdate(gameTime);
        }

        static public void Draw(SpriteBatch spriteBatch)
        {
            if (GameDraw != null) GameDraw(spriteBatch);
        }
    }
}
