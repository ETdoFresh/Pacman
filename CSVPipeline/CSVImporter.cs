using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;

// TODO: replace this with the type you want to import.
using TImport = System.String;
using System.IO;

namespace CSVPipeline
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
    [ContentImporter(".csv", DisplayName = "CSV Importer", DefaultProcessor = "CSV Processor")]
    public class CSVImporter : ContentImporter<String>
    {
        public override String Import(string filename, ContentImporterContext context)
        {
            String data = "";
            using (StreamReader sr = new StreamReader(filename))
            {
                data = sr.ReadToEnd();
            }
            return data;
        }
    }
}
