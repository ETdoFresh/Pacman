using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Pacman.Engine.Display
{
    class GameObject
    {
        static public ContentManager Content { get; set; }
        static public SpriteBatch SpriteBatch { get; set; }

        public string Name { get; protected set; }
        public bool IsInitialized { get; protected set; }
        public bool Enabled { get; set; }
        public bool Visible { get; set; }
        public GroupObject Parent { get; set; }

        public GameObject()
        {
            Enabled = true;
            Visible = true;
            Debug.WriteLine("Game Object Constructed | " + this);

            if (Stage.Instance.IsInitialized)
                Initialize();
        }

        public virtual void Initialize() 
        {
            IsInitialized = true;
            Debug.WriteLine("Game Object Initalized | " + this);
            LoadContent();
        }

        public virtual void LoadContent() 
        {
            Debug.WriteLine("Game Object Content Loaded | " + this);
        }

        public virtual void UnloadContent() 
        {
            Debug.WriteLine("Game Object Content Unloaded | " + this);
        }

        public virtual void Update(GameTime gameTime) { }
        public virtual void Draw(GameTime gameTime) { }
    }
}
