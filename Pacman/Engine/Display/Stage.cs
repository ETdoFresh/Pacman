using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;
using Pacman.Engine.Helpers;

namespace Pacman.Engine.Display
{
    class Stage : DrawableGameComponent
    {
        static Stage _instance;
        static List<SceneObject> _loadedScenes;
        static List<SceneObject> _activeScenes;
        static List<SceneObject> _scenesToUpdate;

        public Stage(Game game)
            : base(game)
        {
            game.Components.Add(this);
            Debug.WriteLine("Stage Constructed | " + this);
        }

        public override void Initialize()
        {
            DisplayObject.Content = StageGame.Content;
            IsInitialized = true;
            Debug.WriteLine("Stage Initialized | " + this);
            base.Initialize();
        }

        protected override void LoadContent()
        {

            DisplayObject.SpriteBatch = new SpriteBatch(MainGraphicsDevice);
            Debug.WriteLine("Stage Content Loaded | " + this);
            base.LoadContent();

            foreach (var scene in _loadedScenes) scene.Initialize();
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            InputHelper.Update();

            // Have a seperate array of scenes to update in case an active scene is added or removed
            _scenesToUpdate.Clear();
            foreach (var scene in _activeScenes)
                _scenesToUpdate.Add(scene);

            // pop scenes out until all scenes are updated
            while (_scenesToUpdate.Count > 0)
            {
                var scene = _scenesToUpdate[_scenesToUpdate.Count - 1];
                _scenesToUpdate.RemoveAt(_scenesToUpdate.Count - 1);
                if (scene.Enabled)
                    scene.Update(gameTime);
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (var scene in _activeScenes)
                if (scene.Visible)
                    scene.Draw(gameTime);

            base.Draw(gameTime);
        }

        public Game StageGame { get { return Game; } }
        public GraphicsDevice StageGraphicsDevice { get { return GraphicsDevice; } }

        static public Stage Create(Game game)
        {
            if (_instance == null)
            {
                _instance = new Stage(game);
                _loadedScenes = new List<SceneObject>();
                _activeScenes = new List<SceneObject>();
                _scenesToUpdate = new List<SceneObject>();
            }
            return _instance;
        }

        static public SceneObject LoadScene(SceneObject scene)
        {
            if (!_loadedScenes.Contains(scene))
                _loadedScenes.Add(scene);

            if (IsInitialized && !scene.IsInitialized)
                scene.Initialize();

            return scene;
        }

        static public SceneObject UnloadScene(SceneObject scene)
        {
            _activeScenes.Remove(scene);
            _loadedScenes.Remove(scene);
            return scene;
        }

        static public SceneObject GotoScene(string name)
        {
            var scene = GetSceneByName(name);

            if (scene != null)
                GotoScene(scene);

            return scene;
        }

        static public SceneObject GotoScene(SceneObject scene) { return GotoScene(scene, true); }
        static public SceneObject GotoScene(SceneObject scene, bool suspendOtherScenes)
        {
            LoadScene(scene);

            if (suspendOtherScenes)
                while (_activeScenes.Count > 0)
                    SuspendScene(_activeScenes[0]);

            if (!_activeScenes.Contains(scene))
                _activeScenes.Add(scene);

            return scene;
        }

        static public SceneObject SuspendScene(SceneObject scene)
        {
            _activeScenes.Remove(scene);
            return scene;
        }

        static public SceneObject GetSceneByName(string name)
        {
            foreach (var scene in _loadedScenes)
                if (scene.Name == name)
                    return scene;

            return null;
        }

        static public bool IsInitialized { get; private set; }
        static public Game MainGame { get { return _instance.StageGame; } }
        static public GraphicsDevice MainGraphicsDevice { get { return _instance.StageGraphicsDevice; } }
    }
}
