using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace TextureAtlasPipeline
{
    /// <summary>
    /// This class will be instantiated by the XNA Framework Content Pipeline
    /// to write the specified data type into binary .xnb format.
    ///
    /// This should be part of a Content Pipeline Extension Library project.
    /// </summary>
    [ContentTypeWriter]
    public class TextureAtlasWriter : ContentTypeWriter<List<Rectangle>>
    {
        protected override void Write(ContentWriter output, List<Rectangle> value)
        {
            output.Write(value.Count);
            foreach (var rect in value)
            {
                output.Write(rect.X);
                output.Write(rect.Y);
                output.Write(rect.Width);
                output.Write(rect.Height);
            }
        }

        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return "PacmanLibrary.Engine.TextureAtlasReader, PacmanLibrary";
        }
    }
}
