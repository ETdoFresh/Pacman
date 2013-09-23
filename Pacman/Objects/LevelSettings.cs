using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pacman.Engine.Display;

namespace Pacman.Objects
{
    class LevelSettings
    {
        private int _currentLevel;
        private List<Dictionary<string, string>> _homeSettings;
        private List<Dictionary<string, string>> _levelSettings;
        private List<Dictionary<string, string>> _stateSettings;

        public LevelSettings(int level)
        {
            _currentLevel = level - 1;
            _homeSettings = Stage.GameContent.Load<List<Dictionary<String, String>>>("home");
            _levelSettings = Stage.GameContent.Load<List<Dictionary<String, String>>>("levels");
            _stateSettings = Stage.GameContent.Load<List<Dictionary<String, String>>>("state");
        }

        private float PercentToFloat(string percent)
        {
            string temp = percent.Replace('%', ' ').Trim();
            return Convert.ToSingle(temp) / 100;
        }

        public int CurrentLevel { get { return _currentLevel + 1; } }
        public string BonusSymbol { get { return _levelSettings[_currentLevel]["Bonus Symbol"]; } }
        public int BonusPoint { get { return Convert.ToInt32(_levelSettings[_currentLevel]["Bonus Points"]); } }
        public float PacmanSpeed { get { return PercentToFloat(_levelSettings[_currentLevel]["Pac-Man Speed"]); } }
        public float PacmanDotsSpeed { get { return PercentToFloat(_levelSettings[_currentLevel]["Pac-Man Dots Speed"]);}}
        public float GhostSpeed { get { return PercentToFloat(_levelSettings[_currentLevel]["Ghost Speed"]);}}
        public float GhostTunnelSpeed { get { return PercentToFloat(_levelSettings[_currentLevel]["Ghost Tunnel Speed"]);}}
        public int Elroy1DotsLeft { get { return Convert.ToInt32(_levelSettings[_currentLevel]["Elroy 1 Dots Left"]); } }
        public float Elroy1Speed { get { return PercentToFloat(_levelSettings[_currentLevel]["Elroy 1 Speed"]);}}
        public int Elroy2DotsLeft { get { return Convert.ToInt32(_levelSettings[_currentLevel]["Elroy 2 Dots Left"]); } }
        public float Elroy2Speed { get { return PercentToFloat(_levelSettings[_currentLevel]["Elroy 2 Speed"]);}}
        public float FrightPacmanSpeed { get { return PercentToFloat(_levelSettings[_currentLevel]["Fright. Pac-Man Speed"]);}}
        public float FrightPacmanDotsSpeed { get { return PercentToFloat(_levelSettings[_currentLevel]["Fright Pac-Man Dots Speed"]);}}
        public float FrightGhostSpeed { get { return PercentToFloat(_levelSettings[_currentLevel]["Fright Ghost Speed"]);}}
        public int FrightTime { get { return Convert.ToInt32(_levelSettings[_currentLevel]["Fright. Time (in sec.)"]); } }
        public int NumberOfFlashes { get { return Convert.ToInt32(_levelSettings[_currentLevel]["# of Flashes"]); } }

        public int PinkyDotLimit { get { return Convert.ToInt32(_homeSettings[_currentLevel]["Pinky Dot Limit"]); } }
        public int InkyDotLimit { get { return Convert.ToInt32(_homeSettings[_currentLevel]["Inky Dot Limit"]); } }
        public int ClydeDotLimit { get { return Convert.ToInt32(_homeSettings[_currentLevel]["Clyde Dot Limit"]); } }
        public int GlobalPinky { get { return Convert.ToInt32(_homeSettings[_currentLevel]["Global Pinky"]); } }
        public int GlobalInky { get { return Convert.ToInt32(_homeSettings[_currentLevel]["Global Inky"]); } }
        public int GlobalClyde { get { return Convert.ToInt32(_homeSettings[_currentLevel]["Global Clyde"]); } }
        public int TimerLimit { get { return Convert.ToInt32(_homeSettings[_currentLevel]["Timer Limit"]); } }

        public int Scatter1 { get { return Convert.ToInt32(_stateSettings[_currentLevel]["Scatter1"]); } }
        public int Chase1 { get { return Convert.ToInt32(_stateSettings[_currentLevel]["Chase1"]); } }
        public int Scatter2 { get { return Convert.ToInt32(_stateSettings[_currentLevel]["Scatter2"]); } }
        public int Chase2 { get { return Convert.ToInt32(_stateSettings[_currentLevel]["Chase2"]); } }
        public int Scatter3 { get { return Convert.ToInt32(_stateSettings[_currentLevel]["Scatter3"]); } }
        public int Chase3 { get { return Convert.ToInt32(_stateSettings[_currentLevel]["Chase3"]); } }
        public float Scatter4 { get { return Convert.ToSingle(_stateSettings[_currentLevel]["Scatter4"]); } }
        public int Chase4 { get { return Convert.ToInt32(_stateSettings[_currentLevel]["Chase4"]); } }
    }
}
