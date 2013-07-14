using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;
using System.Xml.Linq;

namespace TextureAtlasPipeline
{
    /// <summary>
    /// This class will be instantiated by the XNA Framework Content Pipeline
    /// to apply custom processing to content data, converting an object of
    /// type TInput to TOutput. The input and output types may be the same if
    /// the processor wishes to alter data without changing its type.
    ///
    /// This should be part of a Content Pipeline Extension Library project.
    ///
    /// TODO: change the ContentProcessor attribute to specify the correct
    /// display name for this processor.
    /// </summary>
    [ContentProcessor(DisplayName = "TextureAtlas Processor")]
    public class TextureAtlasProcessor : ContentProcessor<XElement, List<Rectangle>>
    {
        public override List<Rectangle> Process(XElement input, ContentProcessorContext context)
        {
            var textureRectangles = new List<Rectangle>();
            var query = from ele in input.Elements("sprite") select ele;
            foreach (var ele in query)
            {
                var name = ele.Attribute("n").Value;
                var x = int.Parse(ele.Attribute("x").Value);
                var y = int.Parse(ele.Attribute("y").Value);
                var width = int.Parse(ele.Attribute("w").Value);
                var height = int.Parse(ele.Attribute("h").Value);
                textureRectangles.Add(new Rectangle(x, y, width, height));
            }
            return textureRectangles;
        }
    }
}