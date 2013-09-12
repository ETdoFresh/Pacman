using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pacman.Engine.Display;
using Microsoft.Xna.Framework;
using Pacman.Objects;

namespace Pacman.Engine.Helpers
{
    class DebugHelper : DisplayObject
    {
        TextObject _textObject;
        List<string> _labels;
        List<object> _objects;

        public DebugHelper()
        {
            _textObject = new TextObject("");
            AddComponent(_textObject);

            _labels = new List<string>();
            _objects = new List<object>();
        }

        public void AddLine(string label, object obj)
        {
            _labels.Add(label);
            _objects.Add(obj);
        }

        public override void Update(GameTime gameTime)
        {
            if (Enabled)
            {
                base.Update(gameTime);
                var output = "";

                for (var i = 0; i < _labels.Count; i++)
                {
                    output += _labels[i] + " ";

                    if (_objects[i].GetType() == typeof(Position))
                        output += (_objects[i] as Position).Value;
                    else if (_objects[i].GetType() == typeof(TilePosition))
                        output += (_objects[i] as TilePosition).Vector;
                    else if (_objects[i].GetType() == typeof(Rotation))
                        output += (_objects[i] as Rotation).Value;
                    else if (_objects[i].GetType() == typeof(Orientation))
                        output += (_objects[i] as Orientation).Value;
                    else if (_objects[i].GetType() == typeof(Direction))
                        output += (_objects[i] as Direction).Value;
                    else if (_objects[i].GetType() == typeof(Velocity))
                        output += (_objects[i] as Velocity).Speed.Value;

                    output += "\n";
                }

                _textObject.Text = output;
                _textObject.Translate(_textObject.Width / 2, _textObject.Height / 2);
            }
        }
    }
}
