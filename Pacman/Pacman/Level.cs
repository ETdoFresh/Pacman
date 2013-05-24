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
        private List<Dictionary<String, String>> home;
        private List<Dictionary<string, string>> state;

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

        public int pinkyLimit;
        public int inkyLimit;
        public int clydeLimit;
        public int globalPinkyLimit;
        public int globalInkyLimit;
        public int globalClydeLimit;
        public int timerLimit;

        public float Scatter1;
        public float Chase1;
        public float Scatter2;
        public float Chase2;
        public float Scatter3;
        public float Chase3;
        public float Scatter4;
        public float Chase4;

        public Level(int level = 1)
        {
            levels = ContentLoader.Content.Load<List<Dictionary<String, String>>>("levels");
            home = ContentLoader.Content.Load<List<Dictionary<String, String>>>("home");
            state = ContentLoader.Content.Load<List<Dictionary<String, String>>>("state");
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

            pinkyLimit = Convert.ToInt32(home[level]["Pinky Dot Limit"]);
            inkyLimit = Convert.ToInt32(home[level]["Inky Dot Limit"]);
            clydeLimit = Convert.ToInt32(home[level]["Clyde Dot Limit"]);
            globalPinkyLimit = Convert.ToInt32(home[level]["Global Pinky"]);
            globalInkyLimit = Convert.ToInt32(home[level]["Global Inky"]);
            globalClydeLimit = Convert.ToInt32(home[level]["Global Clyde"]);
            timerLimit = Convert.ToInt32(home[level]["Timer Limit"]);

            Scatter1 = (float)Convert.ToDecimal(state[level]["Scatter1"]);
            Chase1 = (float)Convert.ToDecimal(state[level]["Chase1"]);
            Scatter2 = (float)Convert.ToDecimal(state[level]["Scatter2"]);
            Chase2 = (float)Convert.ToDecimal(state[level]["Chase2"]);
            Scatter3 = (float)Convert.ToDecimal(state[level]["Scatter3"]);
            Chase3 = (float)Convert.ToDecimal(state[level]["Chase3"]);
            Scatter4 = (float)Convert.ToDecimal(state[level]["Scatter4"]);
            Chase4 = (float)Convert.ToDecimal(state[level]["Chase4"]);
        }

        public void Next()
        {
            Load(current + 1);
        }
    }
}
