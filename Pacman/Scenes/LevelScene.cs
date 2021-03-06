﻿using System;
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
        static public float maxSpeed = 300;

        TileGrid _tileGrid;
        PacmanObject _pacman;
        DisplayObject _mouse;
        TilePosition _mouseTilePosition;
        Random _random;
        DebugHelper _debugHelper;
        Ghost _blinky, _pinky, _inky, _clyde;
        Ghost[] _ghostArray;
        Pellets _pellets;
        AIState.State _levelState;
        Timer _levelStateTimer;
        int _stateTimerIteration;
        Timer _ghostHomeTimer;
        PelletEater _pelletEater;
        Timer _frightenedTimer;
        DotCounter _activeDotCounter;
        DotCounter _bonusFruitCounter;
        GlobalDotCounter _globalDotCounter;
        CollisionManager _collisionManager;
        Timer _pauseTimer;
        Ghost _eatenGhost;
        LevelSettings _levelSettings;
        BonusFruit _bonusFruit;
        Score _score;
        TextObject _ghostScoreText;

        public LevelScene()
            : base("Level")
        {
            _levelSettings = new LevelSettings(1);
            StartLevel();
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
                if (InputHelper.IsPressed(Keys.Q) || InputHelper.IsPressed(Keys.Escape))
                    Stage.GotoScene("Menu");
                else if (InputHelper.IsPressed(Keys.R))
                    RestartLevel();
                else if (InputHelper.IsPressed(Keys.Space))
                    _tileGrid.Enabled = !_tileGrid.Enabled;

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

        private void StartLevel()
        {
            _random = new Random();
            _tileGrid = new TileGrid(LevelData.outerWallData.GetLength(1), LevelData.outerWallData.GetLength(0), tileWidth, tileHeight);
            AddComponent(_tileGrid);

            SetupBoard();
            GeneratePellets();

            _score = new Score();
            AddComponent(_score);

            ShowReadyText();
            ShowPlayerOneText();

            PerformWithDelay(2000, LoadLevel);
        }

        private void LoadLevel()
        {
            _pacman = PacmanObject.Create(_tileGrid);
            _blinky = Blinky.Create(_tileGrid, _pacman);
            _pinky = Pinky.Create(_tileGrid, _pacman);
            _inky = Inky.Create(_tileGrid, _pacman, _blinky);
            _clyde = Clyde.Create(_tileGrid, _pacman);
            _ghostArray = new Ghost[4] { _blinky, _pinky, _inky, _clyde };

            _pelletEater = new PelletEater(_pacman, _pellets, _tileGrid);
            _pelletEater.PelletEaten += OnPelletEaten;
            _pelletEater.PowerPelletEaten += OnPowerPelletEaten;
            AddComponent(_pelletEater);

            _stateTimerIteration = 0;
            _levelState = AIState.SCATTER;
            _blinky.ChangeState(_levelState);
            _blinky.ChangeState(GhostState.NORMAL);
            foreach (Ghost ghost in _ghostArray) ghost.SetLevelState(_levelState);

            // Set blinky to right initial direction (which is left)
            _blinky.ImmediateTarget.Update(null);
            _blinky.ReverseDirection();

            _levelStateTimer = new Timer(_levelSettings.Scatter1 * 1000);
            _levelStateTimer.ClockReachedLimit += OnStateTimerLimitReached;
            _tileGrid.AddComponent(_levelStateTimer);

            _pinky.AddDotCounter(_levelSettings.PinkyDotLimit);
            _inky.AddDotCounter(_levelSettings.InkyDotLimit);
            _clyde.AddDotCounter(_levelSettings.ClydeDotLimit);
            _pinky.DotCounter.GhostDotLimitReached += OnDotLimitReached;
            _inky.DotCounter.GhostDotLimitReached += OnDotLimitReached;
            _clyde.DotCounter.GhostDotLimitReached += OnDotLimitReached;
            _activeDotCounter = _pinky.DotCounter;

            foreach (Ghost ghost in _ghostArray) ghost.GhostArriveHome += OnGhostArriveHome;

            _bonusFruitCounter = new DotCounter(70, null);
            _bonusFruitCounter.DotLimitReached += OnFirstBonusFruit;
            _tileGrid.AddComponent(_bonusFruitCounter);

            //_globalDotCounter = new GlobalDotCounter(7, 17, 32, _pinky, _inky, _clyde);
            //_globalDotCounter.DotLimitReached += OnDotLimitReached;

            _ghostHomeTimer = new Timer(_levelSettings.TimerLimit * 1000);
            _ghostHomeTimer.ClockReachedLimit += OnHomeTimerLimitReached;
            _tileGrid.AddComponent(_ghostHomeTimer);

            _pacman.Speed.Factor = _levelSettings.PacmanSpeed;
            foreach (Ghost ghost in _ghostArray) ghost.Speed.Factor = _levelSettings.GhostSpeed;

            _collisionManager = new CollisionManager(_pacman, _blinky, _pinky, _inky, _clyde);
            _collisionManager.Collision += OnCollision;
            _tileGrid.AddComponent(_collisionManager);

            _mouse = new CircleObject(15 / 2);
            _mouse.Translate(400, 25);
            _mouse.Alpha = 0.75f;
            _mouseTilePosition = new TilePosition(_mouse.Position, _tileGrid.TileWidth, _tileGrid.TileHeight);
            AddComponent(_mouseTilePosition);
            _tileGrid.AddComponent(_mouse);

            _debugHelper = new DebugHelper();
            _debugHelper.AddLine("Mouse Position: ", _mouse.Position);
            _debugHelper.AddLine("Mouse Tile Position: ", _mouseTilePosition);
            _debugHelper.AddLine("Mouse Cursor Position", InputHelper.MousePosition);
            //AddComponent(_debugHelper);

            Pause();
            PerformWithDelay(2000, Resume);
        }

        private void Pause()
        {
            _tileGrid.Enabled = false;
        }

        private void Resume()
        {
            _tileGrid.Enabled = true;
        }

        private void ShowPlayerOneText()
        {
            var playerText = new TextObject("PLAYER ONE");
            playerText.Translate(_tileGrid.GetPosition(13.5f, 11));
            playerText.Tint = Color.Cyan;
            playerText.Resize(3);
            _tileGrid.AddComponent(playerText);
            PerformWithDelay(2000, playerText.RemoveSelf);
        }

        private void ShowReadyText()
        {
            var readyText = new TextObject("READY!");
            readyText.Translate(_tileGrid.GetPosition(13.5f, 17));
            readyText.Tint = Color.Yellow;
            readyText.Resize(3);
            _tileGrid.AddComponent(readyText);
            PerformWithDelay(4000, readyText.RemoveSelf);
        }

        private void PerformWithDelay(int delay, Action action)
        {
            Timer timer = new Timer(delay);
            AddComponent(timer);
            timer.ClockReachedLimit += new Timer.ClockReachedLimitHandler(action);
            timer.ClockReachedLimit += timer.RemoveSelf;
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
            StartLevel();
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
            _tileGrid.Data[13, 12].IsPassable = false;
            _tileGrid.Data[14, 12].IsPassable = false;
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
                    _levelState = AIState.CHASE;
                    _levelStateTimer.Reset(_levelSettings.Chase1 * 1000);
                    break;
                case 2:
                    _levelState = AIState.SCATTER;
                    _levelStateTimer.Reset(_levelSettings.Scatter2 * 1000);
                    break;
                case 3:
                    _levelState = AIState.CHASE;
                    _levelStateTimer.Reset(_levelSettings.Chase2 * 1000);
                    break;
                case 4:
                    _levelState = AIState.SCATTER;
                    _levelStateTimer.Reset(_levelSettings.Scatter3 * 1000);
                    break;
                case 5:
                    _levelState = AIState.CHASE;
                    _levelStateTimer.Reset(_levelSettings.Chase3 * 1000);
                    break;
                case 6:
                    _levelState = AIState.SCATTER;
                    _levelStateTimer.Reset(_levelSettings.Scatter4 * 1000);
                    break;
                case 7:
                    _levelState = AIState.CHASE;
                    _levelStateTimer.Stop();
                    break;
                default:
                    throw new Exception("Level Timer is not supposed to reach this iteration");
            }
            foreach (Ghost ghost in _ghostArray) ghost.SetLevelState(_levelState);
        }

        private void OnHomeTimerLimitReached()
        {
            if (_pinky.CurrentState == AIState.HOME)
            {
                _pinky.ChangeState(AIState.LEAVINGHOME);
                _ghostHomeTimer.Reset();
            }
            else if (_inky.CurrentState == AIState.HOME)
            {
                _inky.ChangeState(AIState.LEAVINGHOME);
                _ghostHomeTimer.Reset();
            }
            else if (_clyde.CurrentState == AIState.HOME)
            {
                _clyde.ChangeState(AIState.LEAVINGHOME);
                _ghostHomeTimer.Reset();
            }
            else
            {
                _ghostHomeTimer.Reset();
            }

            SetActiveDotCounter();
        }

        private void OnPelletEaten()
        {
            _score.Add(10);
            if (_activeDotCounter != null)
                _activeDotCounter.AddDot();
            else if (_globalDotCounter != null)
                _globalDotCounter.AddDot();

            if (_ghostHomeTimer != null)
                _ghostHomeTimer.Reset();

            _bonusFruitCounter.AddDot();
        }

        private void OnPowerPelletEaten()
        {
            _score.Add(50);
            _score.ResetNumberOfGhostsEaten();

            if (_activeDotCounter != null)
                _activeDotCounter.AddDot();
            else if (_globalDotCounter != null)
                _globalDotCounter.AddDot();

            if (_ghostHomeTimer != null)
                _ghostHomeTimer.Reset();

            _bonusFruitCounter.AddDot();

            foreach (Ghost ghost in _ghostArray)
            {
                if (ghost.CurrentGhostState != GhostState.EYES)
                {
                    ghost.ChangeState(GhostState.FRIGHTENED);
                    ghost.ReverseDirection();

                    if (ghost.CurrentState != AIState.HOME && ghost.CurrentState != AIState.LEAVINGHOME)
                        ghost.ChangeState(AIState.WANDER);
                }
            }

            if (_frightenedTimer != null)
                _frightenedTimer.RemoveSelf();
            _frightenedTimer = new Timer(_levelSettings.FrightTime * 1000 - _levelSettings.NumberOfFlashes * 166 * 2);
            _frightenedTimer.ClockReachedLimit += OnFrightenedTimerReached;
            _tileGrid.AddComponent(_frightenedTimer);
        }

        private void OnFrightenedTimerReached()
        {
            foreach (Ghost ghost in _ghostArray)
                if (ghost.CurrentGhostState == GhostState.FRIGHTENED)
                    ghost.ChangeState(GhostState.FLASHINGFRIGHTENED);

            _frightenedTimer.Reset(_levelSettings.NumberOfFlashes * 166 * 2);
            _frightenedTimer.ClockReachedLimit -= OnFrightenedTimerReached;
            _frightenedTimer.ClockReachedLimit += OnFrightenedFlashingTimerReached;
        }

        private void OnFrightenedFlashingTimerReached()
        {
            _frightenedTimer.RemoveSelf();
            foreach (Ghost ghost in _ghostArray)
            {
                if (ghost.CurrentGhostState == GhostState.FLASHINGFRIGHTENED)
                {
                    if (ghost.CurrentState == AIState.HOME)
                        ghost.ChangeState(GhostState.HOME);
                    else
                        ghost.ChangeState(GhostState.NORMAL);
                }
                if (ghost.CurrentState == AIState.WANDER)
                {
                    ghost.ChangeState(_levelState);
                }
            }
        }

        private void OnDotLimitReached(Ghost ghost)
        {
            if (ghost.CurrentState == AIState.HOME)
                ghost.ChangeState(AIState.LEAVINGHOME);

            if (ghost == _pinky)
                _activeDotCounter = _inky.DotCounter;
            else if (ghost == _inky)
                _activeDotCounter = _clyde.DotCounter;
            else
                _activeDotCounter = null;
        }

        private void OnCollision(PacmanObject pacman, Ghost ghost)
        {
            if (ghost.IsFrightened)
            {
                int ghostScore = _score.AddForEatingGhost();
                _ghostScoreText = new TextObject(ghostScore.ToString(), "Font/PacmanFont2");
                _ghostScoreText.Resize(3.25f);
                _ghostScoreText.Translate(ghost.Position);
                _ghostScoreText.Tint = Color.Cyan;
                _tileGrid.AddComponent(_ghostScoreText);
                _ghostScoreText.Update(null);
                ghost.ChangeState(GhostState.EYES);
                ghost.ChangeState(AIState.EYES);
                _eatenGhost = ghost;
                _eatenGhost.Visible = false;
                _pacman.Visible = false;
                _tileGrid.Enabled = false;

                if (_pauseTimer == null)
                    _pauseTimer = new Timer();
                _pauseTimer.Reset(1000);
                _pauseTimer.ClockReachedLimit += OnFrightenedEatenResume;
                AddComponent(_pauseTimer);
            }
            else if (ghost.CurrentGhostState == GhostState.NORMAL)
            {
                _pacman.Die();
                _blinky.RemoveSelf();
                _pinky.RemoveSelf();
                _inky.RemoveSelf();
                _clyde.RemoveSelf();
                PerformWithDelay(3000, RestartLevel);
            }
        }

        private void OnFrightenedEatenResume()
        {
            _ghostScoreText.RemoveSelf();
            _tileGrid.Enabled = true;
            _eatenGhost.Visible = true;
            _pacman.Visible = true;
            _pauseTimer.ClockReachedLimit -= OnFrightenedEatenResume;
        }

        private void OnGhostArriveHome(Ghost ghost)
        {
            if (_globalDotCounter == null)
            {
                if (ghost is Blinky)
                    ghost.ChangeState(AIState.LEAVINGHOME);
                else if (ghost.DotCounter.IsDotLimitReached())
                    ghost.ChangeState(AIState.LEAVINGHOME);
                else
                    SetActiveDotCounter();
            }
        }

        private void SetActiveDotCounter()
        {
            if (_pinky.CurrentState == AIState.HOME)
                _activeDotCounter = _pinky.DotCounter;
            else if (_inky.CurrentState == AIState.HOME)
                _activeDotCounter = _inky.DotCounter;
            else if (_clyde.CurrentState == AIState.HOME)
                _activeDotCounter = _inky.DotCounter;
            else
                _activeDotCounter = null;
        }

        private void OnFirstBonusFruit()
        {
            _bonusFruit = new BonusFruit(_tileGrid, _pacman);
            _bonusFruit.Eaten += OnEatBonusFruit;
            _tileGrid.AddComponent(_bonusFruit);
            _bonusFruitCounter.SetNewLimit(170);
            _bonusFruitCounter.DotLimitReached -= OnFirstBonusFruit;
            _bonusFruitCounter.DotLimitReached += OnSecondBonusFruit;
        }

        private void OnSecondBonusFruit()
        {
            _bonusFruit = new BonusFruit(_tileGrid, _pacman);
            _bonusFruit.Eaten += OnEatBonusFruit;
            _tileGrid.AddComponent(_bonusFruit);
            _bonusFruitCounter.SetNewLimit(1000);
            _bonusFruitCounter.DotLimitReached -= OnSecondBonusFruit;
        }

        private void OnEatBonusFruit()
        {
            _score.Add(_levelSettings.BonusPoint);
            var bonusScoreText = new TextObject(_levelSettings.BonusPoint.ToString(), "Font/PacmanFont2");
            bonusScoreText.Resize(3.25f);
            bonusScoreText.Translate(_bonusFruit.Position);
            bonusScoreText.Tint = Color.LightPink;
            _tileGrid.AddComponent(bonusScoreText);
            PerformWithDelay(3000, bonusScoreText.RemoveSelf);
            _bonusFruit.RemoveSelf();
        }
    }
}
