using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pacman.Engine;
using Pacman.Engine.Helpers;
using Pacman.Objects;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Pacman.Engine.Display;
using System.Diagnostics;

namespace Pacman.Scenes
{
    class LevelScene : SceneObject
    {
        static public int tileWidth = 32;
        static public int tileHeight = 32;
        static public float maxSpeed = 225;

        TileGrid _tileGrid;
        PacmanObject _pacman;
        DisplayObject _mouse;
        TilePosition _mouseTilePosition;
        Random _random;
        DebugHelper _debugHelper;
        Ghost _blinky, _pinky, _inky, _clyde;
        Ghost[] _ghostArray;
        Pellets _pellets;
        GhostState.GhostStates _levelState;
        Timer _levelStateTimer;
        int _stateTimerIteration;
        Timer _ghostHomeTimer;
        PelletEater _pelletEater;
        Timer _frightenedTimer;

        public LevelScene()
            : base("Level")
        {
            LoadLevel();
        }

        public override void LoadContent()
        {
            base.LoadContent();
            var displayWidth = Stage.GameGraphicsDevice.Viewport.Width;
            var displayHeight = Stage.GameGraphicsDevice.Viewport.Height;

            var scaleFactor = Math.Min(displayWidth / _tileGrid.ContentWidth, displayHeight / _tileGrid.ContentHeight);
            _tileGrid.Resize(scaleFactor);

            var x = Stage.GameGraphicsDevice.Viewport.Width / 2 - _tileGrid.ContentWidth / 2;
            var y = Stage.GameGraphicsDevice.Viewport.Height / 2 - _tileGrid.ContentHeight / 2;
            _tileGrid.Translate(x, y);
        }

        public override void Update(GameTime gameTime)
        {
            if (Enabled)
            {
                base.Update(gameTime);
                if (InputHelper.IsPressed(Keys.Space) || InputHelper.IsPressed(Keys.Escape))
                    Stage.GotoScene("Menu");
                else if (InputHelper.IsPressed(Keys.R))
                    RestartLevel();

                if (_mouse != null)
                {
                    // Move _mouse object locally on mouse press and hold
                    if (InputHelper.GetInputState(MouseButton.Left) == InputState.Pressed || InputHelper.GetInputState(MouseButton.Left) == InputState.Hold)
                        _mouse.Translate((InputHelper.MouseX - _tileGrid.ContentPosition.X) / _tileGrid.ContentScale, (InputHelper.MouseY - _tileGrid.ContentPosition.Y) / _tileGrid.ContentScale);
                    // When released, snap _mouse object into tile
                    else if (InputHelper.GetInputState(MouseButton.Left) == InputState.Released)
                        _mouse.Translate(_mouseTilePosition.X * _mouseTilePosition.TileWidth + _mouseTilePosition.TileWidth / 2,
                            _mouseTilePosition.Y * _mouseTilePosition.TileHeight + _mouseTilePosition.TileHeight / 2);
                }
            }
        }

        private void LoadLevel()
        {
            _random = new Random();
            _tileGrid = new TileGrid(LevelData.outerWallData.GetLength(1), LevelData.outerWallData.GetLength(0), tileWidth, tileHeight);
            AddComponent(_tileGrid);

            SetupBoard();
            GeneratePellets();

            _pacman = PacmanObject.Create(_tileGrid);
            _blinky = Blinky.Create(_tileGrid, _pacman);
            _pinky = Pinky.Create(_tileGrid, _pacman);
            _inky = Inky.Create(_tileGrid, _pacman, _blinky);
            _clyde = Clyde.Create(_tileGrid, _pacman);
            _ghostArray = new Ghost[4] { _blinky, _pinky, _inky, _clyde };

            _pelletEater = new PelletEater(_pacman, _pellets, _tileGrid);
            _pelletEater.PowerPelletEaten += OnPowerPelletEaten;
            AddComponent(_pelletEater);

            _levelState = GhostState.SCATTER;
            _blinky.ChangeState(_levelState);
            foreach (Ghost ghost in _ghostArray) ghost.SetLevelState(_levelState);

            _levelStateTimer = new Timer(7 * 1000);
            _levelStateTimer.ClockReachedLimit += OnStateTimerLimitReached;
            AddComponent(_levelStateTimer);

            _ghostHomeTimer = new Timer(4 * 1000);
            _ghostHomeTimer.ClockReachedLimit += OnHomeTimerLimitReached;
            AddComponent(_ghostHomeTimer);

            _pacman.Speed.Factor = 0.8f;
            foreach (Ghost ghost in _ghostArray) ghost.Speed.Factor = 0.75f;

            _mouse = new CircleObject(15 / 2);
            _mouse.Translate(400, 25);
            _mouse.Alpha = 0.75f;
            _mouseTilePosition = new TilePosition(_mouse.Position, _tileGrid.TileWidth, _tileGrid.TileHeight);
            AddComponent(_mouseTilePosition);
            _tileGrid.AddComponent(_mouse);

            _debugHelper = new DebugHelper();
            _debugHelper.AddLine("Self Position: ", Position);
            _debugHelper.AddLine("TileGrid Position", _tileGrid.Position);
            _debugHelper.AddLine("Pacman Position: ", _pacman.Position);
            _debugHelper.AddLine("Pacman Tile Position: ", _pacman.TilePosition);
            _debugHelper.AddLine("Pacman Orientation: ", _pacman.Orientation);
            _debugHelper.AddLine("Pacman Rotation: ", _pacman.Rotation);
            _debugHelper.AddLine("Pacman Velocity: ", _pacman.Velocity);
            _debugHelper.AddLine("Blinky Tile Position: ", _blinky.TilePosition);
            _debugHelper.AddLine("Blinky Target Position: ", _blinky.Target.Position);
            _debugHelper.AddLine("Blinky Immediate Target Position: ", _blinky.ImmediateTarget.Position);
            _debugHelper.AddLine("Blinky Direction: ", _blinky.Direction);
            _debugHelper.AddLine("Mouse Position: ", _mouse.Position);
            _debugHelper.AddLine("Mouse Tile Position: ", _mouseTilePosition);
            _debugHelper.AddLine("Mouse Cursor Position", InputHelper.MousePosition);
            AddComponent(_debugHelper);
        }

        private void RemoveAllItems()
        {
            while (NumComponents > 0)
                this[0].RemoveSelf();

            _tileGrid = null;
            _pacman = null;
            _mouse = null;
            _mouseTilePosition = null;
            _random = null;
            _debugHelper = null;
            _blinky = null;
            _pinky = null;
            _inky = null;
            _clyde = null;
            _ghostArray = null;
            _pellets = null;
            _levelStateTimer = null;
            _ghostHomeTimer = null;
        }

        private void RestartLevel()
        {
            RemoveAllItems();
            LoadLevel();
            LoadContent();
        }

        private void SetupBoard()
        {
            byte[,] outerWallData = LevelData.outerWallData;
            byte[,] outerWallOrientation = LevelData.outerWallOrientation;
            byte[,] innerWallData = LevelData.innerWallData;
            byte[,] innerWallOrientation = LevelData.innerWallOrientation;
            const int indexOffset = 28;
            var board = new DisplayObject();
            AddComponent(board);

            for (var row = 0; row < outerWallData.GetLength(0); row++)
            {
                for (var column = 0; column < outerWallData.GetLength(1); column++)
                {
                    if (outerWallData[row, column] > 0)
                    {
                        var outerTile = new SpriteObject("pacman", outerWallData[row, column] + indexOffset);
                        outerTile.Rotate(outerWallOrientation[row, column] * 90);
                        outerTile.Tint = new Color(60, 87, 167);
                        _tileGrid.Data[column, row].AddComponent(outerTile);
                        _tileGrid.Data[column, row].IsPassable = false;
                    }

                    if (innerWallData[row, column] > 0)
                    {
                        var innerTile = new SpriteObject("pacman", innerWallData[row, column] + indexOffset);
                        innerTile.Rotate(innerWallOrientation[row, column] * 90);
                        innerTile.Tint = new Color(60, 87, 167);
                        _tileGrid.Data[column, row].AddComponent(innerTile);
                        _tileGrid.Data[column, row].IsPassable = false;
                    }
                }
            }
        }

        private void GeneratePellets()
        {
            byte[,] pelletsData = LevelData.pelletsData;
            _pellets = new Pellets(_tileGrid);
            _tileGrid.AddComponent(_pellets);
            for (var row = 0; row < pelletsData.GetLength(1); row++)
            {
                for (var column = 0; column < pelletsData.GetLength(0); column++)
                {
                    if (pelletsData[column, row] == 1)
                    {
                        var pellet = _pellets.AddPellet();
                        pellet.Translate(_tileGrid.GetPosition(row, column));
                        pellet.TilePosition.Update(null);
                    }
                    else if (pelletsData[column, row] == 2)
                    {
                        var pellet = _pellets.AddPowerPellet();
                        pellet.Translate(_tileGrid.GetPosition(row, column));
                        pellet.TilePosition.Update(null);
                    }
                }
            }
        }

        private void OnStateTimerLimitReached()
        {
            _stateTimerIteration++;
            switch (_stateTimerIteration)
            {
                case 1:
                    _levelState = GhostState.CHASE;
                    _levelStateTimer.Reset(20 * 1000);
                    break;
                case 2:
                    _levelState = GhostState.SCATTER;
                    _levelStateTimer.Reset(7 * 1000);
                    break;
                case 3:
                    _levelState = GhostState.CHASE;
                    _levelStateTimer.Reset(20 * 1000);
                    break;
                case 4:
                    _levelState = GhostState.SCATTER;
                    _levelStateTimer.Reset(5 * 1000);
                    break;
                case 5:
                    _levelState = GhostState.CHASE;
                    _levelStateTimer.Reset(20 * 1000);
                    break;
                case 6:
                    _levelState = GhostState.SCATTER;
                    _levelStateTimer.Reset(5 * 1000);
                    break;
                case 7:
                    _levelState = GhostState.CHASE;
                    _levelStateTimer.Stop();
                    break;
                default:
                    throw new Exception("Level Timer is not supposed to reach this iteration");
            }
            foreach (Ghost ghost in _ghostArray) ghost.SetLevelState(_levelState);
        }

        private void OnHomeTimerLimitReached()
        {
            if (_pinky.CurrentState == GhostState.HOME)
            {
                _pinky.ChangeState(GhostState.LEAVINGHOME);
                _ghostHomeTimer.Reset();
            }
            else if (_inky.CurrentState == GhostState.HOME)
            {
                _inky.ChangeState(GhostState.LEAVINGHOME);
                _ghostHomeTimer.Reset();
            }
            else if (_clyde.CurrentState == GhostState.HOME)
            {
                _clyde.ChangeState(GhostState.LEAVINGHOME);
                _ghostHomeTimer.Reset();
            }
            else
            {
                _ghostHomeTimer.ClockReachedLimit -= OnHomeTimerLimitReached;
                _ghostHomeTimer.Stop();
            }
        }

        private void OnPowerPelletEaten()
        {
            foreach (Ghost ghost in _ghostArray)
                if (ghost.CurrentState == GhostState.CHASE || ghost.CurrentState == GhostState.SCATTER)
                    ghost.ChangeState(GhostState.FRIGHTENED);

            _frightenedTimer = new Timer(6 * 1000 - 5 * 166 * 2);
            _frightenedTimer.ClockReachedLimit += OnFrightenedTimerReached;
            AddComponent(_frightenedTimer);
        }

        private void OnFrightenedTimerReached()
        {
            foreach (Ghost ghost in _ghostArray)
                if (ghost.CurrentState == GhostState.FRIGHTENED)
                    ghost.ChangeState(GhostState.FRIGHTENEDFLASHING);

            _frightenedTimer.Reset(5 * 166 * 2);
            _frightenedTimer.ClockReachedLimit -= OnFrightenedTimerReached;
            _frightenedTimer.ClockReachedLimit += OnFrightenedFlashingTimerReached;
        }

        private void OnFrightenedFlashingTimerReached()
        {
            _frightenedTimer.RemoveSelf();
            foreach (Ghost ghost in _ghostArray)
            {
                if (ghost.CurrentState == GhostState.FRIGHTENEDFLASHING)
                {
                    ghost.ChangeState(_levelState);
                }
            }
        }
    }
}
