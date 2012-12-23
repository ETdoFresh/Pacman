using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman
{
    class AnimationSequence
    {
        public string name;
        public List<int> frames;
        public int time;

        public AnimationSequence(string name, List<int> frames, int time)
        {
            this.name = name;
            this.frames = frames;
            this.time = time;
        }

        public AnimationSequence(string name, int start, int count, int time)
        {
            this.name = name;
            this.time = time;

            this.frames = new List<int>();
            for (int i = start; i < start + count; i++)
                frames.Add(i);
        }
    }
}
