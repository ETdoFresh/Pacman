using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman.Engine.Display
{
    /// <summary>
    /// Scenes are DisplayObjects specifically geared for the Stage.
    /// More functionality to come.
    /// </summary>
    class SceneObject : DisplayObject
    {
        public SceneObject(string name) : base()
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
