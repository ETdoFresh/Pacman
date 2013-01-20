using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Xml.Linq;

namespace PacmanGame
{
    class TexturePacker
    {
        public static List<Rectangle> GetTextureRectangles(string filename)
        {
            var textureRectangles = new List<Rectangle>();
            var doc = XElement.Load(filename);
            var query = from ele in doc.Elements("sprite") select ele;
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
