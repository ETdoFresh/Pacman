using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DisplayLibrary
{
    public delegate void UpdateHandler(GameTime gameTime);
    public delegate void DrawHandler(SpriteBatch spriteBatch);

    public class Runtime
    {
        static public event UpdateHandler GameUpdate;
        static public event DrawHandler GameDraw;

        static public void Update(GameTime gameTime)
        {
            if (GameUpdate != null)
                GameUpdate(gameTime);
        }

        static public void Draw(SpriteBatch spriteBatch)
        {
            if (GameDraw != null)
                GameDraw(spriteBatch);
        }
    }
}
