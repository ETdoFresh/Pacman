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
        TileGrid _tileGrid;
        Objects.Pacman _pacman;
        DisplayObject _mouse;
        TilePosition _mouseTilePosition;
        Random _random;
        DebugHelper _debugHelper;
        Ghost _blinky, _pinky, _inky, _clyde;
        List<Pellet> _pellets;

        public LevelScene()
            : base("Level")
        {
            _random = new Random();
            _tileGrid = new TileGrid(outerWallData.GetLength(1), outerWallData.GetLength(0), 32, 32);
            AddChild(_tileGrid);

            SetupBoard();
            GeneratePellets();

            _pacman = new Objects.Pacman();
            _pacman.Translate(_tileGrid.GetPosition(14.5f, 23f));
            _pacman.TilePosition = new TilePosition(_pacman.Position, _tileGrid.TileWidth, _tileGrid.TileHeight);
            _pacman.AnimatedSprite = new AnimatedSpriteObject("pacman");
            _pacman.AnimatedSprite.AddSequence("Chomp", new[] { 0, 1, 2, 3, 4, 5, 6, 7, 6, 5, 4, 3, 2, 1 }, 200);
            _pacman.AnimatedSprite.AddSequence("Die", 0, 11, 1000);
            _pacman.AnimatedSprite.SetSequence("Chomp");
            _pacman.AnimatedSprite.Tint = Color.Yellow;
            _pacman.Target = new Target.Pacman();
            _pacman.Speed = new Speed(225);
            _pacman.Velocity = new Velocity() { Position = _pacman.Position, Speed = _pacman.Speed };
            _pacman.Rotation = new Rotation() { Orientation = _pacman.Orientation };
            _pacman.DesiredDirection = new Direction(Direction.LEFT);
            _pacman.PreviousDirection = new Direction(Direction.LEFT);
            _pacman.Steering = new Steering(_pacman, _pacman.Target as ISteer);
            _pacman.Wrap = new Wrap(_pacman.Position, 0, 0, _tileGrid.Width, _tileGrid.Height);
            _pacman.SnapToTarget = new SnapToTarget(_pacman, _pacman.Velocity, _pacman.Target);
            _pacman.PlayerMovement = new PlayerMovement(_pacman, _pacman.Target, _tileGrid);
            _pacman.PelletEater = new PelletEater(_pacman, _pellets, _tileGrid);
            _pacman.AddChild(_pacman.TilePosition);
            _pacman.AddChild(_pacman.AnimatedSprite);
            _pacman.AddChild(_pacman.Velocity);
            _pacman.AddChild(_pacman.Rotation);
            _pacman.AddChild(_pacman.Steering);
            _pacman.AddChild(_pacman.Wrap);
            _pacman.AddChild(_pacman.SnapToTarget);
            _pacman.AddChild(_pacman.PlayerMovement);
            _pacman.AddChild(_pacman.PelletEater);
            _tileGrid.AddChild(_pacman.Target);
            _tileGrid.AddChild(_pacman);

            _blinky = new Blinky();
            _blinky.Translate(_tileGrid.GetPosition(13.5f, 11f));
            _blinky.TilePosition = new TilePosition(_blinky.Position, _tileGrid.TileWidth, _tileGrid.TileHeight);
            _blinky.Direction = new Direction(Direction.LEFT);
            _blinky.Body = new AnimatedSpriteObject("pacman");
            _blinky.Body.AddSequence("BodyFloat", 8, 8, 250);
            _blinky.Body.Tint = Color.Red;
            _blinky.Eyes = new SpriteObject("pacman", 20);
            _blinky.Pupils = new SpriteObject("pacman", 25);
            _blinky.Pupils.Tint = new Color(60, 87, 167);
            _blinky.Target = new Target.Blinky(_pacman);
            _blinky.ImmediateTarget = new Target.Immediate(_blinky, _tileGrid);
            _blinky.Speed = new Speed(225);
            _blinky.Velocity = new Velocity() { Position = _blinky.Position, Speed = _blinky.Speed };
            _blinky.Rotation = new Rotation() { Orientation = _blinky.Orientation };
            _blinky.Steering = new Steering(_blinky, _blinky.ImmediateTarget as ISteer);
            _blinky.Wrap = new Wrap(_blinky.Position, 0, 0, _tileGrid.Width, _tileGrid.Height);
            _blinky.SnapToTarget = new SnapToTarget(_blinky, _blinky.Velocity, _blinky.ImmediateTarget);
            _blinky.ShiftEyesToDirection = new ShiftEyesToDirection(_blinky);
            _blinky.AddChild(_blinky.TilePosition);
            _blinky.AddChild(_blinky.Body);
            _blinky.AddChild(_blinky.Eyes);
            _blinky.AddChild(_blinky.Pupils);
            _blinky.AddChild(_blinky.Velocity);
            _blinky.AddChild(_blinky.Steering);
            _blinky.AddChild(_blinky.Wrap);
            _blinky.AddChild(_blinky.SnapToTarget);
            _blinky.AddChild(_blinky.ShiftEyesToDirection);
            _tileGrid.AddChild(_blinky);
            _tileGrid.AddChild(_blinky.Target);
            _tileGrid.AddChild(_blinky.ImmediateTarget);
            _blinky.StateChange += OnGhostStateChange;

            _pinky = new Pinky();
            _pinky.Translate(_tileGrid.GetPosition(11.5f, 14f));
            _pinky.TilePosition = new TilePosition(_pinky.Position, _tileGrid.TileWidth, _tileGrid.TileHeight);
            _pinky.Direction = new Direction(Direction.LEFT);
            _pinky.Body = new AnimatedSpriteObject("pacman");
            _pinky.Body.AddSequence("BodyFloat", 8, 8, 250);
            _pinky.Body.Tint = Color.Pink;
            _pinky.Eyes = new SpriteObject("pacman", 20);
            _pinky.Pupils = new SpriteObject("pacman", 25);
            _pinky.Pupils.Tint = new Color(60, 87, 167);
            _pinky.Target = new Target.Pinky(_pacman);
            _pinky.ImmediateTarget = new Target.Immediate(_pinky, _tileGrid);
            _pinky.Speed = new Speed(225);
            _pinky.Velocity = new Velocity() { Position = _pinky.Position, Speed = _pinky.Speed };
            _pinky.Rotation = new Rotation() { Orientation = _pinky.Orientation };
            _pinky.AddChild(_pinky.TilePosition);
            _pinky.AddChild(_pinky.Body);
            _pinky.AddChild(_pinky.Eyes);
            _pinky.AddChild(_pinky.Pupils);
            _tileGrid.AddChild(_pinky);
            _tileGrid.AddChild(_pinky.Target);
            _tileGrid.AddChild(_pinky.ImmediateTarget);

            _inky = new Inky();
            _inky.Translate(_tileGrid.GetPosition(13.5f, 14f));
            _inky.Body = new AnimatedSpriteObject("pacman");
            _inky.Body.AddSequence("BodyFloat", 8, 8, 250);
            _inky.Body.Tint = Color.Cyan;
            _inky.Eyes = new SpriteObject("pacman", 20);
            _inky.Pupils = new SpriteObject("pacman", 25);
            _inky.Pupils.Tint = new Color(60, 87, 167);
            _inky.Target = new Target.Inky(_pacman, _blinky);
            _inky.Speed = new Speed(225);
            _inky.Velocity = new Velocity() { Position = _inky.Position, Speed = _inky.Speed };
            _inky.Rotation = new Rotation() { Orientation = _inky.Orientation };
            _inky.Direction = new Direction(Direction.LEFT);
            _inky.AddChild(_inky.Body);
            _inky.AddChild(_inky.Eyes);
            _inky.AddChild(_inky.Pupils);
            _tileGrid.AddChild(_inky);
            _tileGrid.AddChild(_inky.Target);

            _clyde = new Clyde();
            _clyde.Translate(_tileGrid.GetPosition(15.5f, 14f));
            _clyde.Body = new AnimatedSpriteObject("pacman");
            _clyde.Body.AddSequence("BodyFloat", 8, 8, 250);
            _clyde.Body.Tint = Color.Orange;
            _clyde.Eyes = new SpriteObject("pacman", 20);
            _clyde.Pupils = new SpriteObject("pacman", 25);
            _clyde.Pupils.Tint = new Color(60, 87, 167);
            _clyde.Target = new Target.Clyde(_pacman, _clyde);
            _clyde.Speed = new Speed(225);
            _clyde.Velocity = new Velocity() { Position = _clyde.Position, Speed = _clyde.Speed };
            _clyde.Rotation = new Rotation() { Orientation = _clyde.Orientation };
            _clyde.Direction = new Direction(Direction.LEFT);
            _clyde.AddChild(_clyde.Body);
            _clyde.AddChild(_clyde.Eyes);
            _clyde.AddChild(_clyde.Pupils);
            _tileGrid.AddChild(_clyde);
            _tileGrid.AddChild(_clyde.Target);

            _mouse = new CircleObject(15 / 2);
            _mouse.Translate(400, 25);
            _mouse.Alpha = 0.75f;
            _mouseTilePosition = new TilePosition(_mouse.Position, _tileGrid.TileWidth, _tileGrid.TileHeight);
            AddChild(_mouseTilePosition);
            _tileGrid.AddChild(_mouse);

            _debugHelper = new DebugHelper();
            _debugHelper.AddLine("Self Position: ", Position);
            _debugHelper.AddLine("TileGrid Position", _tileGrid.Position);
            _debugHelper.AddLine("Pacman Position: ", _pacman.Position);
            _debugHelper.AddLine("Pacman Tile Position: ", _pacman.TilePosition);
            _debugHelper.AddLine("Pacman Target Position: ", _pacman.Target.Position);
            _debugHelper.AddLine("Pacman Orientation: ", _pacman.Orientation);
            _debugHelper.AddLine("Pacman Rotation: ", _pacman.Rotation);
            _debugHelper.AddLine("Pacman Target Orientation: ", _pacman.Target.Orientation);
            _debugHelper.AddLine("Blinky Tile Position: ", _blinky.TilePosition);
            _debugHelper.AddLine("Blinky Target Position: ", _blinky.Target.Position);
            _debugHelper.AddLine("Blinky Immediate Target Position: ", _blinky.ImmediateTarget.Position);
            _debugHelper.AddLine("Blinky Direction: ", _blinky.Direction);
            _debugHelper.AddLine("Mouse Position: ", _mouse.Position);
            _debugHelper.AddLine("Mouse Tile Position: ", _mouseTilePosition);
            _debugHelper.AddLine("Mouse Cursor Position", InputHelper.MousePosition);
            AddChild(_debugHelper);
        }

        public override void LoadContent()
        {
            base.LoadContent();
            var displayWidth = Stage.GraphicsDevice.Viewport.Width;
            var displayHeight = Stage.GraphicsDevice.Viewport.Height;

            var scaleFactor = Math.Min(displayWidth / _tileGrid.ContentWidth, displayHeight / _tileGrid.ContentHeight);
            _tileGrid.Resize(scaleFactor);

            var x = Stage.GraphicsDevice.Viewport.Width / 2 - _tileGrid.ContentWidth / 2;
            var y = Stage.GraphicsDevice.Viewport.Height / 2 - _tileGrid.ContentHeight / 2;
            _tileGrid.Translate(x, y);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (InputHelper.IsPressed(Keys.Space) || InputHelper.IsPressed(Keys.Escape))
            {
                Stage.GotoScene("Menu");
            }

            // Move _mouse object locally on mouse press and hold
            if (InputHelper.GetInputState(MouseButton.Left) == InputState.Pressed || InputHelper.GetInputState(MouseButton.Left) == InputState.Hold)
                _mouse.Translate((InputHelper.MouseX - _tileGrid.ContentPosition.X) / _tileGrid.ContentScale, (InputHelper.MouseY - _tileGrid.ContentPosition.Y) / _tileGrid.ContentScale);
            // When released, snap _mouse object into tile
            else if (InputHelper.GetInputState(MouseButton.Left) == InputState.Released)
                _mouse.Translate(_mouseTilePosition.X * _mouseTilePosition.TileWidth + _mouseTilePosition.TileWidth / 2,
                    _mouseTilePosition.Y * _mouseTilePosition.TileHeight + _mouseTilePosition.TileHeight / 2);
        }

        private void SetupBoard()
        {
            const int indexOffset = 28;
            var board = new GroupObject();
            AddChild(board);

            for (var row = 0; row < outerWallData.GetLength(0); row++)
            {
                for (var column = 0; column < outerWallData.GetLength(1); column++)
                {
                    if (outerWallData[row, column] > 0)
                    {
                        var outerTile = new SpriteObject("pacman", outerWallData[row, column] + indexOffset);
                        outerTile.Rotate(outerWallOrientation[row, column] * 90);
                        outerTile.Tint = new Color(60, 87, 167);
                        _tileGrid.Data[column, row].AddChild(outerTile);
                        _tileGrid.Data[column, row].IsPassable = false;
                    }

                    if (innerWallData[row, column] > 0)
                    {
                        var innerTile = new SpriteObject("pacman", innerWallData[row, column] + indexOffset);
                        innerTile.Rotate(innerWallOrientation[row, column] * 90);
                        innerTile.Tint = new Color(60, 87, 167);
                        _tileGrid.Data[column, row].AddChild(innerTile);
                        _tileGrid.Data[column, row].IsPassable = false;
                    }
                }
            }
        }

        private void GeneratePellets()
        {
            _pellets = new List<Pellet>();
            for (var row = 0; row < pelletsData.GetLength(1); row++)
            {
                for (var column = 0; column < pelletsData.GetLength(0); column++)
                {
                    if (pelletsData[column, row] == 1)
                    {
                        var pellet = new Pellet();
                        pellet.Translate(_tileGrid.GetPosition(row, column));
                        pellet.TilePosition = new TilePosition(pellet.Position, _tileGrid.TileWidth, _tileGrid.TileHeight);
                        pellet.SpriteObject = new SpriteObject("pacman", 26);
                        pellet.AddChild(pellet.TilePosition);
                        pellet.AddChild(pellet.SpriteObject);
                        _tileGrid.AddChild(pellet);
                        _pellets.Add(pellet);
                    }
                    else if (pelletsData[column, row] == 2)
                    {
                        var pellet = new Pellet();
                        pellet.Translate(_tileGrid.GetPosition(row, column));
                        pellet.TilePosition = new TilePosition(pellet.Position, _tileGrid.TileWidth, _tileGrid.TileHeight);
                        pellet.SpriteObject = new SpriteObject("pacman", 27);
                        pellet.AddChild(pellet.TilePosition);
                        pellet.AddChild(pellet.SpriteObject);
                        _tileGrid.AddChild(pellet);
                        _pellets.Add(pellet);
                    }
                }
            }
        }

        private void OnGhostStateChange(Ghost ghost)
        {
            if (ghost.TilePosition == null)
                ghost.TilePosition = new TilePosition(ghost.Position, _tileGrid.TileWidth, _tileGrid.TileHeight);
            if (ghost.Direction == null)
                ghost.Direction = new Direction(Direction.LEFT);
            if (ghost.Body == null)
            {
                ghost.Body = new AnimatedSpriteObject("pacman");
                ghost.Body.AddSequence("BodyFloat", 8, 8, 250);
            }


            if (ghost.State == Ghost.States.HOME)
            {
                if (ghost == _blinky)
                    ghost.State = Ghost.States.SCATTER;
            }
            else if (ghost.State == Ghost.States.LEAVEHOME)
            {
                ghost.LeaveHome = new LeaveHome(ghost);
            }
        }

        private static byte[,] outerWallData = new byte[,]
        {
            { 4, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3 },
            { 4, 3, 3, 3, 3, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 3, 3, 3, 3, 4 },
            { 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 5, 3, 3, 0, 0, 3, 3, 5, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0 },
            { 3, 3, 3, 3, 3, 5, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 5, 3, 3, 3, 3, 3 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 3, 3, 3, 3, 3, 5, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 5, 3, 3, 3, 3, 3 },
            { 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 5, 3, 3, 3, 3, 3, 3, 5, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0 },
            { 4, 3, 3, 3, 3, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 3, 3, 3, 3, 4 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3 },
            { 4, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4 },
        };

        private static byte[,] outerWallOrientation = new byte[,]
        {
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 3, 2, 2, 2, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 2, 2, 2, 2, 2 },
            { 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 1, 2, 2, 0, 0, 2, 2, 2, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 2, 2, 2, 2, 2, 2, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 1, 2, 2, 2, 2, 2 },
            { 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 },
        };

        private static byte[,] innerWallData = new byte[,]
        {
            { 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 1, 0, 2, 1, 1, 2, 0, 2, 1, 1, 1, 2, 0, 1, 1, 0, 2, 1, 1, 1, 2, 0, 2, 1, 1, 2, 0, 1 },
            { 1, 0, 1, 0, 0, 1, 0, 1, 0, 0, 0, 1, 0, 1, 1, 0, 1, 0, 0, 0, 1, 0, 1, 0, 0, 1, 0, 1 },
            { 1, 0, 2, 1, 1, 2, 0, 2, 1, 1, 1, 2, 0, 2, 2, 0, 2, 1, 1, 1, 2, 0, 2, 1, 1, 2, 0, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 1, 0, 2, 1, 1, 2, 0, 2, 2, 0, 2, 1, 1, 1, 1, 1, 1, 2, 0, 2, 2, 0, 2, 1, 1, 2, 0, 1 },
            { 1, 0, 2, 1, 1, 2, 0, 1, 1, 0, 2, 1, 1, 2, 2, 1, 1, 2, 0, 1, 1, 0, 2, 1, 1, 2, 0, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 1 },
            { 2, 1, 1, 1, 1, 2, 0, 1, 2, 1, 1, 2, 0, 1, 1, 0, 2, 1, 1, 2, 1, 0, 2, 1, 1, 1, 1, 2 },
            { 0, 0, 0, 0, 0, 1, 0, 1, 2, 1, 1, 2, 0, 2, 2, 0, 2, 1, 1, 2, 1, 0, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 1, 0, 1, 1, 0, 2, 1, 1, 0, 0, 1, 1, 2, 0, 1, 1, 0, 1, 0, 0, 0, 0, 0 },
            { 1, 1, 1, 1, 1, 2, 0, 2, 2, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 2, 2, 0, 2, 1, 1, 1, 1, 1 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 1, 1, 1, 1, 1, 2, 0, 2, 2, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 2, 2, 0, 2, 1, 1, 1, 1, 1 },
            { 0, 0, 0, 0, 0, 1, 0, 1, 1, 0, 2, 1, 1, 1, 1, 1, 1, 2, 0, 1, 1, 0, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 1, 0, 1, 1, 0, 2, 1, 1, 1, 1, 1, 1, 2, 0, 1, 1, 0, 1, 0, 0, 0, 0, 0 },
            { 2, 1, 1, 1, 1, 2, 0, 2, 2, 0, 2, 1, 1, 2, 2, 1, 1, 2, 0, 2, 2, 0, 2, 1, 1, 1, 1, 2 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 1, 0, 2, 1, 1, 2, 0, 2, 1, 1, 1, 2, 0, 1, 1, 0, 2, 1, 1, 1, 2, 0, 2, 1, 1, 2, 0, 1 },
            { 1, 0, 2, 1, 2, 1, 0, 2, 1, 1, 1, 2, 0, 2, 2, 0, 2, 1, 1, 1, 2, 0, 1, 2, 1, 2, 0, 1 },
            { 1, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 1 },
            { 2, 1, 2, 0, 1, 1, 0, 2, 2, 0, 2, 1, 1, 1, 1, 1, 1, 2, 0, 2, 2, 0, 1, 1, 0, 2, 1, 2 },
            { 2, 1, 2, 0, 2, 2, 0, 1, 1, 0, 2, 1, 1, 2, 2, 1, 1, 2, 0, 1, 1, 0, 2, 2, 0, 2, 1, 2 },
            { 1, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 1 },
            { 1, 0, 2, 1, 1, 1, 1, 2, 2, 1, 1, 2, 0, 1, 1, 0, 2, 1, 1, 2, 2, 1, 1, 1, 1, 2, 0, 1 },
            { 1, 0, 2, 1, 1, 1, 1, 1, 1, 1, 1, 2, 0, 2, 2, 0, 2, 1, 1, 1, 1, 1, 1, 1, 1, 2, 0, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2 },
        };

        private static byte[,] innerWallOrientation = new byte[,]
        {
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 1 },
            { 1, 0, 1, 0, 0, 1, 0, 1, 0, 0, 0, 1, 0, 1, 1, 0, 1, 0, 0, 0, 1, 0, 1, 0, 0, 1, 0, 1 },
            { 1, 0, 3, 0, 0, 2, 0, 3, 0, 0, 0, 2, 0, 3, 2, 0, 3, 0, 0, 0, 2, 0, 3, 0, 0, 2, 0, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 1, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 1, 0, 1 },
            { 1, 0, 3, 0, 0, 2, 0, 1, 1, 0, 3, 0, 0, 1, 0, 0, 0, 2, 0, 1, 1, 0, 3, 0, 0, 2, 0, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 1 },
            { 3, 0, 0, 0, 0, 1, 0, 1, 3, 0, 0, 1, 0, 1, 1, 0, 0, 0, 0, 2, 1, 0, 0, 0, 0, 0, 0, 2 },
            { 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 2, 0, 3, 2, 0, 3, 0, 0, 1, 1, 0, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 1, 0, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 2, 0, 3, 2, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 3, 2, 0, 3, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 1, 0, 1, 1, 0, 3, 0, 0, 0, 0, 0, 0, 2, 0, 1, 1, 0, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 1, 0, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 2, 0, 3, 2, 0, 3, 0, 0, 1, 0, 0, 0, 2, 0, 3, 2, 0, 3, 0, 0, 0, 0, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 1 },
            { 1, 0, 3, 0, 1, 1, 0, 3, 0, 0, 0, 2, 0, 3, 2, 0, 3, 0, 0, 0, 2, 0, 1, 0, 0, 2, 0, 1 },
            { 1, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 1 },
            { 3, 0, 1, 0, 1, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 1, 1, 0, 0, 0, 2 },
            { 0, 0, 2, 0, 3, 2, 0, 1, 1, 0, 3, 0, 0, 1, 0, 0, 0, 2, 0, 1, 1, 0, 3, 2, 0, 3, 0, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 2, 3, 0, 0, 1, 0, 1, 1, 0, 0, 0, 0, 2, 3, 0, 0, 0, 0, 1, 0, 1 },
            { 1, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 3, 2, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2 },
        };

        private static byte[,] pelletsData = new byte[,]
        {
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0 },
            { 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0 },
            { 0, 2, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 2, 0 },
            { 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0 },
            { 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0 },
            { 0, 1, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 1, 0 },
            { 0, 1, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 1, 0 },
            { 0, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 0 },
            { 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0 },
            { 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0 },
            { 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0 },
            { 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0 },
            { 0, 2, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 2, 0 },
            { 0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0 },
            { 0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0 },
            { 0, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 0 },
            { 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0 },
            { 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0 },
            { 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
        };
    }
}
