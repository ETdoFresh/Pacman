﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace DisplayLibrary
{
    public class SpriteSheet
    {
        public static Dictionary<String, Texture2D> textures = new Dictionary<String, Texture2D>();
        public static Dictionary<String, List<Rectangle>> rectangles = new Dictionary<string, List<Rectangle>>();

        public static Texture2D GetTexture(string filename)
        {
            if (!textures.ContainsKey(filename))
                textures.Add(filename, Content.Load<Texture2D>(filename));
                
            return textures[filename];
        }

        public static List<Rectangle> GetRectangles(string filename)
        {
            if (!rectangles.ContainsKey(filename))
                rectangles.Add(filename, Content.Load<List<Rectangle>>(filename + "xml"));

            return rectangles[filename];
        }

        public static void Initialize(ContentManager Content)
        {
            SpriteSheet.Content = Content;
        }

        public static ContentManager Content { get; set; }
    }
}
