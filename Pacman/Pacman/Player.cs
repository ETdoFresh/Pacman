using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DisplayEngine;

namespace PacmanGame
{
    class Player
    {
        public int Score { get; set; }

        internal void OnPelletEaten(object sender, EventArgs e)
        {
            Pellet pellet = (Pellet)sender;
            Score += pellet.Score;
        }
    }
}
