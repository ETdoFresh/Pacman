﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Pacman.Engine
{
    /// <summary>
    /// This class will be instantiated by the XNA Framework Content
    /// Pipeline to read the specified data type from binary .xnb format.
    /// 
    /// Unlike the other Content Pipeline support classes, this should
    /// be a part of your main game project, and not the Content Pipeline
    /// Extension Library project.
    /// 
    /// This class takes Generic XML Spritesheet definition from TexturePacker
    /// and extracts a List of Rectangles from it.
    /// </summary>
    public class TextureAtlasReader : ContentTypeReader<List<Rectangle>>
    {
        protected override List<Rectangle> Read(ContentReader input, List<Rectangle> existingInstance)
        {
            var rectangles = new List<Rectangle>();
            var count = input.ReadInt32();
            for (var i = 0; i < count; i++)
            {
                var rect = new Rectangle(input.ReadInt32(), input.ReadInt32(), input.ReadInt32(), input.ReadInt32());
                rectangles.Add(rect);
            }
            return rectangles;
        }
    }
}