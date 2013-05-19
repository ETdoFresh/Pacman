using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DisplayLibrary;

namespace Pacman
{
    class Level
    {
        public List<Dictionary<String, String>> Levels { get; set; }

        public Level()
        {
            var test = ContentLoader.Content.Load<List<Dictionary<String, String>>>("levels");
            System.Console.WriteLine(test[0]["Bonus Symbol"]);
        }
    }
}
