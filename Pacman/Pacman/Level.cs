using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DisplayLibrary;

namespace Pacman
{
    class Level
    {
        private List<Dictionary<String, String>> levels;

        public int current;
        public string bonusSymbol;
        public int bonusScore;
        public float pacmanSpeed;
        public float pacmanEatSpeed;
        public float ghostSpeed;
        public float ghostTunnelSpeed;
        public int elroy1Dots;
        public float elroy1Speed;
        public int elroy2Dots;
        public float elroy2Speed;
        public float frightPacmanSpeed;
        public float frightPacmanEatSpeed;
        public float frightGhostSpeed;
        public int frightTime;
        public int numFlashes;

        public Level(int level = 1)
        {
            levels = ContentLoader.Content.Load<List<Dictionary<String, String>>>("levels");
            Load(level);
        }

        public void Load(int level)
        {
            level = level - 1;
            current = Convert.ToInt32(levels[level]["Level"]);
            bonusSymbol = levels[level]["Bonus Symbol"];
            bonusScore = Convert.ToInt32(levels[level]["Bonus Points"]);
            pacmanSpeed = (float)Convert.ToDecimal(levels[level]["Pac-Man Speed"].Replace("%", "")) / 100;
            pacmanEatSpeed = (float)Convert.ToDecimal(levels[level]["Pac-Man Dots Speed"].Replace("%", "")) / 100;
            ghostSpeed = (float)Convert.ToDecimal(levels[level]["Ghost Speed"].Replace("%", "")) / 100;
            ghostTunnelSpeed = (float)Convert.ToDecimal(levels[level]["Ghost Tunnel Speed"].Replace("%", "")) / 100;
            elroy1Dots = Convert.ToInt32(levels[level]["Elroy 1 Dots Left"]);
            elroy1Speed = (float)Convert.ToDecimal(levels[level]["Elroy 1 Speed"].Replace("%", "")) / 100;
            elroy2Dots = Convert.ToInt32(levels[level]["Elroy 2 Dots Left"]);
            elroy2Speed = (float)Convert.ToDecimal(levels[level]["Elroy 2 Speed"].Replace("%", "")) / 100;
            frightPacmanSpeed = (float)Convert.ToDecimal(levels[level]["Fright. Pac-Man Speed"].Replace("%", "")) / 100;
            frightPacmanEatSpeed = (float)Convert.ToDecimal(levels[level]["Fright Pac-Man Dots Speed"].Replace("%", "")) / 100;
            frightGhostSpeed = (float)Convert.ToDecimal(levels[level]["Fright Ghost Speed"].Replace("%", "")) / 100;
            frightTime = Convert.ToInt32(levels[level]["Fright. Time (in sec.)"]);
            numFlashes = Convert.ToInt32(levels[level]["# of Flashes"]);
        }

        public void Next()
        {
            Load(current + 1);
        }
    }
}
