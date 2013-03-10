using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using System.Xml.Linq;

namespace TextureAtlasPipeline
{
    /// <summary>
    /// This class will be instantiated by the XNA Framework Content Pipeline
    /// to import a file from disk into the specified type, TImport.
    /// 
    /// This should be part of a Content Pipeline Extension Library project.
    /// 
    /// TODO: change the ContentImporter attribute to specify the correct file
    /// extension, display name, and default processor for this importer.
    /// </summary>
    [ContentImporter(".xml", DisplayName = "TextureAtlas Importer", DefaultProcessor = "TextureAtlas Processor")]
    public class ContentImporter1 : ContentImporter<XElement>
    {
        public override XElement Import(string filename, ContentImporterContext context)
        {
            return XElement.Load(filename);
        }
    }
}
