using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Pacman
{
    public class Pacman
    {

        public AnimatedSprite animatedSprite;
        public Direction direction;
        public Kinematic kinematic;
        public Target keyboardTarget;
        public Steering seekKeyboardTarget;
        public Collision collision;

        public Pacman()
        {
            kinematic = new Kinematic(x: 50, y: 50);

            animatedSprite = new AnimatedSprite(filename: "pacman", kinematic: kinematic);
            animatedSprite.AddSequence(name: "still", frames: new int[] { 36 });
            animatedSprite.AddSequence(name: "chomp", frames: new int[] { 36, 37, 36, 38 }, time: 200);
            animatedSprite.SetSequence(name: "chomp");

            keyboardTarget = new Target(source: kinematic);

            seekKeyboardTarget = new Steering(kinematic, keyboardTarget);

            collision = new Collision(kinematic);
        }
    }
}
