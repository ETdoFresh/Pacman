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
        public delegate void CollisionHandler(Object sender, Object target);
        public static event CollisionHandler Collide = delegate { };

        private static List<GameObject> gameObjects = new List<GameObject>();

        public static void AddGameObject(GameObject gameObject)
        {
            if (gameObject != null)
                Collision.gameObjects.Add(gameObject);
        }

        public static void AddGameObjects(IEnumerable<GameObject> gameObjects)
        {
            if (gameObjects != null)
                Collision.gameObjects.AddRange(gameObjects);
        }

        public static void RemoveGameObject(GameObject gameObject)
        {
            if (gameObject != null)
                Collision.gameObjects.Remove(gameObject);
        }

        public Collision(List<GameObject> gameObjects = null)
        {
            if (gameObjects != null)
                Collision.gameObjects.AddRange(gameObjects);

            Runtime.GameUpdate += CheckCollisions;
        }

        public void CheckCollisions(GameTime gameTime)
        {
            for (var i = 0; i < gameObjects.Count; i++)
                for (var j = i + 1; j < gameObjects.Count; j++)
                    if (gameObjects[i].TilePosition.Value == gameObjects[j].TilePosition.Value)
                        Collide(gameObjects[i], gameObjects[j]);
        }

        public void Dispose()
        {
            Runtime.GameUpdate -= CheckCollisions;
        }
    }
}
