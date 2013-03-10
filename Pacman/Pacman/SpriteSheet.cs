using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Pacman
{
    class SpriteSheet
    {
        internal static Texture2D GetTexture(string filename)
        {
            return Content.Load<Texture2D>(filename);
        }

        internal static List<Rectangle> GetRectangles(string filename)
        {
            return Content.Load<List<Rectangle>>(filename + "xml");
        }

        internal static void Initialize(ContentManager Content)
        {
            SpriteSheet.Content = Content;
        }

        public static ContentManager Content { get; set; }
    }
}
