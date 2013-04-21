using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DisplayLibrary;

namespace Pacman
{
    class Ghost : GameObject
    {
        public AnimatedSprite AnimatedSprite { get; set; }
        public GhostTarget Target { get; set; }
     
        public Ghost(GroupObject displayParent = null)
        {
        }

    }
}
