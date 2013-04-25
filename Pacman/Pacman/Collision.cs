using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DisplayLibrary;
using Microsoft.Xna.Framework;
using System.Collections;

namespace Pacman
{
    class Collision : IDisposable
    {
        public delegate void CollisionHandler(Pacman pacman, GameObject gameObject);
        public static event CollisionHandler Collide = delegate { };

        private static Pacman pacman;
        private static List<GameObject> gameObjects = new List<GameObject>();
        private GameObject gameObject;

        public Collision(Pacman pacman)
        {
            Collision.pacman = pacman;
            gameObject = pacman;
            Runtime.GameUpdate += CheckCollisions;
        }

        public Collision(GameObject gameObject)
        {
            Collision.gameObjects.Add(gameObject);
            this.gameObject = gameObject;
        }
    
        public void CheckCollisions(GameTime gameTime)
        {
            for (var i = gameObjects.Count - 1; i >= 0; i--)
            {
                var gameObject = gameObjects[i];
                if (pacman.TilePosition.Value == gameObject.TilePosition.Value)
                {
                    Collide(pacman, gameObject);
                }
            }
        }

        public void Dispose()
        {
            if (pacman == gameObject)
            {
                pacman = null;
                Runtime.GameUpdate -= CheckCollisions;
            }
            else
            {
                gameObjects.Remove(gameObject);
            }
        }
    }
}
