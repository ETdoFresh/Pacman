using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DisplayLibrary;
using Microsoft.Xna.Framework;

namespace Pacman
{
    class DebugInfo : IDisposable
    {
        public TextObject text = new TextObject("ABC!");

        private List<String> labels = new List<string>();
        private List<Type> types = new List<Type>();
        private List<Object> objects = new List<object>();

        public DebugInfo()
        {
            Runtime.GameUpdate += Update;
        }

        private void Update(GameTime gameTime)
        {
            var updatedString = "";
            for (var i = 0; i < labels.Count; i++)
            {
                updatedString += labels[i];

                if (types[i] == typeof(Position))
                    updatedString += "X: " + ((Position)objects[i]).X + " Y: " + ((Position)objects[i]).Y + "\n";
                else if (types[i] == typeof(TilePosition))
                    updatedString += "X: " + ((TilePosition)objects[i]).X + " Y: " + ((TilePosition)objects[i]).Y + "\n";
                else if (types[i] == typeof(Dimension))
                    updatedString += "Width: " + ((Dimension)objects[i]).Width + " Height: " + ((Dimension)objects[i]).Height + "\n";
                else
                    updatedString += "\n";

            }
            text.Text = updatedString;
            text.Position.X = text.Dimension.Width / 2;
            text.Position.Y = text.Dimension.Height / 2;
        }

        public void addDebug(String label, Object position)
        {
            labels.Add(label);
            types.Add(position.GetType());
            objects.Add(position);
        }

        public void Dispose()
        {
            text.Dispose();
            labels = null;
            types = null;
            objects = null;
            Runtime.GameUpdate -= Update;
        }
    }
}
