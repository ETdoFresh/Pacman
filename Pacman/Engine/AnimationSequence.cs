using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman.Engine
{
    class AnimationSequence
    {
        public int[] Frames { get; set; } // An array of frames to play
        public int PlayTime { get; set; } // In milliseconds
        public int LoopCount { get; set; } // How many times to loop (-1 = forever)

        public AnimationSequence()
        {
            PlayTime = 1000;
            LoopCount = -1;
        }
    }
}
