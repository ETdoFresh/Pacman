using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman.Engine.Display
{
    class SceneObject : DisplayObject
    {
        public SceneObject(string name) : base()
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
