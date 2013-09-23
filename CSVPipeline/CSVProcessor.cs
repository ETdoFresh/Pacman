using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;

using TInput = System.String;
using TOutput = System.String;

namespace CSVPipeline
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
    [ContentProcessor(DisplayName = "CSV Processor")]
    public class CSVProcessor : ContentProcessor<String, List<Dictionary<String, String>>>
    {
        public override List<Dictionary<String,String>> Process(String input, ContentProcessorContext context)
        {
            var lines = input.Split('\n');

            var newList = new List<Dictionary<String, String>>();
            
            for (var i = 0; i < lines.Length; i++)
            {
                lines[i] = lines[i].Trim();
                newList.Add(new Dictionary<String, String>());
            }

            var headers = lines[0].Split(',');

            for (var i = 1; i < lines.Length; i++)
            {
                var items = lines[i].Split(',');
                for (var j = 0; j < items.Length; j++)
                    newList[i].Add(headers[j], items[j]);
            }

            newList.RemoveAt(0);

            return newList;
        }
    }
}