using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;
using Pacman.Engine.Helpers;
using Microsoft.Xna.Framework.Content;

namespace Pacman.Engine.Display
{
    /// <summary>
    /// Stage is a XNA Game Component that does exactly as PacmanGame does.
    /// It is made into it's own object so that custom code stands out.
    /// </summary>
    class Stage : DrawableGameComponent
    {
        static Stage _instance;
        static List<SceneObject> _loadedScenes;
        static List<SceneObject> _activeScenes;
        static List<SceneObject> _scenesToUpdate;
        static Game _game;
        static SpriteBatch _spriteBatch;

        private Stage(Game game)
            : base(game)
        {
            _game = game;
            game.Components.Add(this);
            Debug.WriteLine("Stage Constructed | " + this);
        }

        /// <summary>
        /// Initializes Stage. LoadContent is loaded from base.Initialize().
        /// </summary>
        public override void Initialize()
        {
            IsInitialized = true;
            Debug.WriteLine("Stage Initialized | " + this);
            base.Initialize();
        }

        /// <summary>
        /// Load spriteBatch and all loaded scenes.
        /// </summary>
        protected override void LoadContent()
        {

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Debug.WriteLine("Stage Content Loaded | " + this);
            base.LoadContent();

            foreach (var scene in _loadedScenes) scene.Initialize();
        }

        /// <summary>
        /// Does nothing yet.
        /// </summary>
        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        /// <summary>
        /// Updates all active, enabled scenes, which in turn updates all the scenes' children.
        /// Input is also updated.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            if (Enabled)
            {
                InputHelper.Update();

                // Have a seperate array of scenes to update in case an active scene is added or removed
                _scenesToUpdate.Clear();
                _scenesToUpdate.AddRange(_activeScenes);

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
        }

        /// <summary>
        /// Draws all active, visible scenes, which in turn draws all the scenes' children.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            foreach (var scene in _activeScenes)
                if (scene.Visible)
                    scene.Draw(gameTime);

            base.Draw(gameTime);
        }

        /// <summary>
        /// Factory method to create the one instance of Stage.
        /// </summary>
        /// <param name="game">XNA parent game object</param>
        /// <returns>Returns the Stage</returns>
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

        /// <summary>
        /// Loads scene (but does not start scene).
        /// </summary>
        /// <param name="scene">The scene to load</param>
        /// <returns>The scene that was loaded</returns>
        static public SceneObject LoadScene(SceneObject scene)
        {
            if (!_loadedScenes.Contains(scene))
                _loadedScenes.Add(scene);

            if (IsInitialized && !scene.IsInitialized)
                scene.Initialize();

            return scene;
        }

        /// <summary>
        /// Stops and removes scene.
        /// </summary>
        /// <param name="scene">The scene to unload</param>
        /// <returns>The scene that was unloaded</returns>
        static public SceneObject UnloadScene(SceneObject scene)
        {
            _activeScenes.Remove(scene);
            _loadedScenes.Remove(scene);
            return scene;
        }

        /// <summary>
        /// Loads and starts scene.
        /// </summary>
        /// <param name="name">Name of scene</param>
        /// <returns>Scene object of name</returns>
        static public SceneObject GotoScene(string name)
        {
            var scene = GetSceneByName(name);

            if (scene != null)
                GotoScene(scene);

            return scene;
        }

        /// <summary>
        /// Loads scene, start scene, and suspends all other scenes.
        /// </summary>
        /// <param name="scene">Scene to start</param>
        /// <returns>Sceneto be started</returns>
        static public SceneObject GotoScene(SceneObject scene) { return GotoScene(scene, true); }

        /// <summary>
        /// Loads and starts scene.
        /// </summary>
        /// <param name="scene">Scene to start</param>
        /// <param name="suspendOtherScenes">Should other scenes be suspended</param>
        /// <returns>Scene to be started</returns>
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

        /// <summary>
        /// Suspends a scene, but remains loaded.
        /// </summary>
        /// <param name="scene">Scene to suspend</param>
        /// <returns>Scene that was suspended</returns>
        static public SceneObject SuspendScene(SceneObject scene)
        {
            _activeScenes.Remove(scene);
            return scene;
        }

        /// <summary>
        /// Check if a loaded scene has specified name.
        /// </summary>
        /// <param name="name">Name of scene</param>
        /// <returns>Scene with name</returns>
        static public SceneObject GetSceneByName(string name)
        {
            foreach (var scene in _loadedScenes)
                if (scene.Name == name)
                    return scene;

            return null;
        }

        static public bool IsInitialized { get; private set; }
        static public ContentManager GameContent { get { return _game.Content; } }
        static public SpriteBatch SpriteBatch { get { return _spriteBatch; } }
        static public Game GameObject { get { return _game; } }
        static public GraphicsDevice GameGraphicsDevice { get { return _game.GraphicsDevice; } }
    }
}
