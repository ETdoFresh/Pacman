using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pacman
{
    class Map : IGameObject
    {
        private static byte[,] outerWallData = new byte[,]
        {
            { 4, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3 },
            { 4, 3, 3, 3, 3, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 3, 3, 3, 3, 4 },
            { 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 5, 3, 3, 0, 0, 3, 3, 5, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0 },
            { 3, 3, 3, 3, 3, 5, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 5, 3, 3, 3, 3, 3 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 3, 3, 3, 3, 3, 5, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 5, 3, 3, 3, 3, 3 },
            { 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 5, 3, 3, 3, 3, 3, 3, 5, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0 },
            { 4, 3, 3, 3, 3, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 3, 3, 3, 3, 4 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3 },
            { 4, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4 },
        };

        private static byte[,] outerWallOrientation = new byte[,]
        {
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 3, 2, 2, 2, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 2, 2, 2, 2, 2 },
            { 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 1, 2, 2, 0, 0, 2, 2, 2, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 2, 2, 2, 2, 2, 2, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 1, 2, 2, 2, 2, 2 },
            { 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 },
        };

        private static byte[,] innerWallData = new byte[,]
        {
            { 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 1, 0, 2, 1, 1, 2, 0, 2, 1, 1, 1, 2, 0, 1, 1, 0, 2, 1, 1, 1, 2, 0, 2, 1, 1, 2, 0, 1 },
            { 1, 0, 1, 0, 0, 1, 0, 1, 0, 0, 0, 1, 0, 1, 1, 0, 1, 0, 0, 0, 1, 0, 1, 0, 0, 1, 0, 1 },
            { 1, 0, 2, 1, 1, 2, 0, 2, 1, 1, 1, 2, 0, 2, 2, 0, 2, 1, 1, 1, 2, 0, 2, 1, 1, 2, 0, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 1, 0, 2, 1, 1, 2, 0, 2, 2, 0, 2, 1, 1, 1, 1, 1, 1, 2, 0, 2, 2, 0, 2, 1, 1, 2, 0, 1 },
            { 1, 0, 2, 1, 1, 2, 0, 1, 1, 0, 2, 1, 1, 2, 2, 1, 1, 2, 0, 1, 1, 0, 2, 1, 1, 2, 0, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 1 },
            { 2, 1, 1, 1, 1, 2, 0, 1, 2, 1, 1, 2, 0, 1, 1, 0, 2, 1, 1, 2, 1, 0, 2, 1, 1, 1, 1, 2 },
            { 0, 0, 0, 0, 0, 1, 0, 1, 2, 1, 1, 2, 0, 2, 2, 0, 2, 1, 1, 2, 1, 0, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 1, 0, 1, 1, 0, 2, 1, 1, 0, 0, 1, 1, 2, 0, 1, 1, 0, 1, 0, 0, 0, 0, 0 },
            { 1, 1, 1, 1, 1, 2, 0, 2, 2, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 2, 2, 0, 2, 1, 1, 1, 1, 1 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 1, 1, 1, 1, 1, 2, 0, 2, 2, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 2, 2, 0, 2, 1, 1, 1, 1, 1 },
            { 0, 0, 0, 0, 0, 1, 0, 1, 1, 0, 2, 1, 1, 1, 1, 1, 1, 2, 0, 1, 1, 0, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 1, 0, 1, 1, 0, 2, 1, 1, 1, 1, 1, 1, 2, 0, 1, 1, 0, 1, 0, 0, 0, 0, 0 },
            { 2, 1, 1, 1, 1, 2, 0, 2, 2, 0, 2, 1, 1, 2, 2, 1, 1, 2, 0, 2, 2, 0, 2, 1, 1, 1, 1, 2 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 1, 0, 2, 1, 1, 2, 0, 2, 1, 1, 1, 2, 0, 1, 1, 0, 2, 1, 1, 1, 2, 0, 2, 1, 1, 2, 0, 1 },
            { 1, 0, 2, 1, 2, 1, 0, 2, 1, 1, 1, 2, 0, 2, 2, 0, 2, 1, 1, 1, 2, 0, 1, 2, 1, 2, 0, 1 },
            { 1, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 1 },
            { 2, 1, 2, 0, 1, 1, 0, 2, 2, 0, 2, 1, 1, 1, 1, 1, 1, 2, 0, 2, 2, 0, 1, 1, 0, 2, 1, 2 },
            { 2, 1, 2, 0, 2, 2, 0, 1, 1, 0, 2, 1, 1, 2, 2, 1, 1, 2, 0, 1, 1, 0, 2, 2, 0, 2, 1, 2 },
            { 1, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 1 },
            { 1, 0, 2, 1, 1, 1, 1, 2, 2, 1, 1, 2, 0, 1, 1, 0, 2, 1, 1, 2, 2, 1, 1, 1, 1, 2, 0, 1 },
            { 1, 0, 2, 1, 1, 1, 1, 1, 1, 1, 1, 2, 0, 2, 2, 0, 2, 1, 1, 1, 1, 1, 1, 1, 1, 2, 0, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2 },
        };

        private static byte[,] innerWallOrientation = new byte[,]
        {
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 1 },
            { 1, 0, 1, 0, 0, 1, 0, 1, 0, 0, 0, 1, 0, 1, 1, 0, 1, 0, 0, 0, 1, 0, 1, 0, 0, 1, 0, 1 },
            { 1, 0, 3, 0, 0, 2, 0, 3, 0, 0, 0, 2, 0, 3, 2, 0, 3, 0, 0, 0, 2, 0, 3, 0, 0, 2, 0, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 1, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 1, 0, 1 },
            { 1, 0, 3, 0, 0, 2, 0, 1, 1, 0, 3, 0, 0, 1, 0, 0, 0, 2, 0, 1, 1, 0, 3, 0, 0, 2, 0, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 1 },
            { 3, 0, 0, 0, 0, 1, 0, 1, 3, 0, 0, 1, 0, 1, 1, 0, 0, 0, 0, 2, 1, 0, 0, 0, 0, 0, 0, 2 },
            { 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 2, 0, 3, 2, 0, 3, 0, 0, 1, 1, 0, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 1, 0, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 2, 0, 3, 2, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 3, 2, 0, 3, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 1, 0, 1, 1, 0, 3, 0, 0, 0, 0, 0, 0, 2, 0, 1, 1, 0, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 1, 0, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 2, 0, 3, 2, 0, 3, 0, 0, 1, 0, 0, 0, 2, 0, 3, 2, 0, 3, 0, 0, 0, 0, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 1 },
            { 1, 0, 3, 0, 1, 1, 0, 3, 0, 0, 0, 2, 0, 3, 2, 0, 3, 0, 0, 0, 2, 0, 1, 0, 0, 2, 0, 1 },
            { 1, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 1 },
            { 3, 0, 1, 0, 1, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 1, 1, 0, 0, 0, 2 },
            { 0, 0, 2, 0, 3, 2, 0, 1, 1, 0, 3, 0, 0, 1, 0, 0, 0, 2, 0, 1, 1, 0, 3, 2, 0, 3, 0, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 2, 3, 0, 0, 1, 0, 1, 1, 0, 0, 0, 0, 2, 3, 0, 0, 0, 0, 1, 0, 1 },
            { 1, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 3, 2, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2 },
        };

        private Player player;
        private List<Ghost> ghosts = new List<Ghost>();
        private MapCell[,] mapCells;

        public Player Player { get { return player; } }
        public List<Ghost> Ghosts { get { return ghosts; } }
        public List<IGameObject> Objects { get; set; }

        public int TileWidth { get; private set; }
        public int TileHeight { get; private set; }
        public int MapWidth { get; private set; }
        public int MapHeight { get; private set; }
        public int Width { get { return MapWidth * TileWidth; } }
        public int Height { get { return MapHeight * TileHeight; } }

        public Map(Texture2D texture, List<Rectangle> textureRectangles)
        {
            MapWidth = innerWallData.GetLength(1);
            MapHeight = innerWallData.GetLength(0);

            var testTile = new Tile(texture, textureRectangles, 54);
            TileWidth = testTile.Width;
            TileHeight = testTile.Height;

            mapCells = new MapCell[MapWidth, MapHeight];
            for (int row = 0; row < MapHeight; row++)
            {
                for (int column = 0; column < MapWidth; column++)
                {
                    var mapCell = new MapCell(column, row);
                    mapCells[column, row] = mapCell;

                    if (innerWallData[row, column] > 0)
                    {
                        var textureIndex = innerWallData[row, column] + 53;
                        var orientation = innerWallOrientation[row, column] * MathHelper.ToRadians(90);
                        var newTile = new Tile(texture, textureRectangles, textureIndex, orientation);
                        newTile.X = newTile.Width * column + newTile.Width / 2;
                        newTile.Y = newTile.Height * row + newTile.Width / 2;
                        mapCell.addLayer(newTile);
                        mapCell.IsPassable = false;
                    }
                    else
                    {
                        mapCell.IsPassable = true;
                    }

                    if (outerWallData[row, column] > 0)
                    {
                        var textureIndex = outerWallData[row, column] + 53;
                        var orientation = outerWallOrientation[row, column] * MathHelper.ToRadians(90);
                        var newTile = new Tile(texture, textureRectangles, textureIndex, orientation);
                        newTile.X = newTile.Width * column + newTile.Width / 2;
                        newTile.Y = newTile.Height * row + newTile.Width / 2;
                        mapCell.addLayer(newTile);
                    }
                }
            }

            player = new Player(texture, textureRectangles);
            ghosts.Add(new Blinky(texture, textureRectangles));
            //ghosts.Add(new Pinky(texture, textureRectangles));
            //ghosts.Add(new Inky(texture, textureRectangles));
            //ghosts.Add(new Clyde(texture, textureRectangles));

            var objects = new List<IGameObject> { player };
            foreach (var ghost in ghosts) objects.Add(ghost);
            Objects = objects;

            placePlayerAtStartingPoint();
            placeBlinkyAtStartingPoint();
        }

        private void placeBlinkyAtStartingPoint()
        {
            ghosts[0].X = TileWidth * 14;
            ghosts[0].Y = (int)(TileHeight * 11.5);
        }

        private void placePlayerAtStartingPoint()
        {
            player.X = TileWidth * 14;
            player.Y = (int)(TileHeight * 23.5);
        }

        public void Update(GameTime gameTime)
        {
            foreach (var obj in Objects) obj.Update(gameTime);
            foreach (var cell in mapCells) cell.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var obj in Objects) obj.Draw(spriteBatch);
            foreach (var cell in mapCells) cell.Draw(spriteBatch);
        }

        public Vector2 GetTileCoordinates(Vector2 worldCoordinates)
        {
            var x = (float)Math.Floor(worldCoordinates.X / TileWidth);
            var y = (float)Math.Floor(worldCoordinates.Y / TileHeight);
            return new Vector2(x, y);
        }

        public Vector2 GetTileCoordinates(Player player) { return GetTileCoordinates(new Vector2(player.X, player.Y)); }
        public Vector2 GetTileCoordinates(Ghost ghost) { return GetTileCoordinates(new Vector2(ghost.X, ghost.Y)); }

        public Vector2 GetTileCoordinates(MapCell mapCell) 
        {
            for (int x = 0; x < MapWidth; x++)
                for (int y = 0; y < MapHeight; y++)
                    if (mapCells[x, y] == mapCell)
                        return new Vector2(x, y);
            return new Vector2(-1, -1);
        }

        public Vector2 GetWorldCoordinates(Vector2 tileCoordinates)
        {
            var x = tileCoordinates.X * TileWidth + TileWidth / 2;
            var y = tileCoordinates.Y * TileHeight + TileHeight / 2;
            return new Vector2(x, y);
        }

        public Vector2 GetWorldCoordinates(MapCell mapCell)
        {
            var tileCoordinates = GetTileCoordinates(mapCell);
            return GetWorldCoordinates(tileCoordinates);
        }

        public Vector2 GetWorldCoordinates(Player player) { return GetWorldCoordinates(GetTileCoordinates(player)); }

        public bool IsPassable(Vector2 tileCoordinates)
        {
            if (isValidTileCoordinates(tileCoordinates))
                return mapCells[(int)tileCoordinates.X, (int)tileCoordinates.Y].IsPassable;

            return false;
        }

        private bool isValidTileCoordinates(Vector2 tileCoordinates)
        {
            if (0 <= tileCoordinates.X && tileCoordinates.X < MapWidth)
                if (0 <= tileCoordinates.Y && tileCoordinates.Y < MapHeight)
                    return true;
            
            return false;
        }

        internal MapCell getCurrentMapCell(Ghost ghost)
        {
            var tileCoordinates = GetTileCoordinates(ghost);
            return mapCells[(int)tileCoordinates.X, (int)tileCoordinates.Y];
        }

        internal MapCell getNextMapCell(Ghost ghost)
        {
            var currentMapCell = GetTileCoordinates(ghost);
            var x = currentMapCell.X + ghost.InputDirection.X;
            var y = currentMapCell.Y + ghost.InputDirection.Y;
            if (0 <= x && x < MapWidth && 0 <= y && y < MapHeight)
                return mapCells[(int)x, (int)y];
            else
                return null;
        }
    }
}
