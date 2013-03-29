using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DisplayLibrary;
using Microsoft.Xna.Framework;

namespace Pacman
{
    class Collision : IDisposable
    {
        public delegate void CollisionHandler(Object sender, Object target);
        public event CollisionHandler Collide;

        private static List<GameObject> gameObjects = new List<GameObject>();

        public GameObject GameObject { get; set; }
        private TilePosition previousTilePosition;

        public Collision(GameObject gameObject)
        {
            GameObject = gameObject;

            if (GameObject != null)
            {
                previousTilePosition = GameObject.TilePosition.Copy();
                gameObjects.Add(GameObject);
                Runtime.GameUpdate += CheckCollisions;
            }
        }

        private void CheckCollisions(GameTime gameTime)
        {
            foreach (var otherObject in gameObjects)
            {
                if (otherObject != GameObject)
                {
                    if (otherObject.TilePosition.Value == GameObject.TilePosition.Value)
                    {
                        Collide(this, otherObject);
                    }
                }
            }
        }

        public void Dispose()
        {
            if (GameObject != null)
            {
                previousTilePosition = null;
                gameObjects.Remove(GameObject);
                Runtime.GameUpdate -= CheckCollisions;
            }
        }
    }
}
