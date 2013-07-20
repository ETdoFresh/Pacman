using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman.Engine
{
    class SceneObject : GroupObject
    {
        protected Stage Stage { get { return Stage.Instance; } }

        public SceneObject(string name) : base()
        {
            Name = name;
        }
    }
}
