using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman
{
    public class Ghost
    {
        private AnimatedSprite animatedSprite;
        private Kinematic position;
        private Direction direction;
        private int artificialIntelligence;
        private Steering steering;
        private Collision collision;
    
        public Ghost()
        {
            direction = new Direction() { current = Direction.LEFT };
            position = new Kinematic(x: 50, y: 50);

            animatedSprite = new AnimatedSprite(filename: "pacman", kinematic: position);
            animatedSprite.AddSequence(name: "move", frames: new int[] { 0, 1 }, time: 150);
            animatedSprite.SetSequence(name: "move");

            //steering = new Steering(position, direction);
            //steering.MaxSpeed = 0;

            collision = new Collision(position);
        }
    }
}
