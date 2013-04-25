﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DisplayLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Pacman
{
    class Controller
    {
        public Board Board { get; set; }
        public Pacman Pacman { get; set; }
        public Ghost Blinky { get; set; }
        public Ghost Pinky { get; set; }
        public Ghost Inky { get; set; }
        public Ghost Clyde { get; set; }
        public TileSelector TileSelector { get; set; }
        public DebugInfo DebugInfo { get; set; }
        public Collision collision { get; set; }
        public Score Score { get; set; }

        private const Int32 firstTileIndex = 54;

        public Controller()
        {
            Initialize();
        }

        private void Initialize()
        {
            TileEngine.Initialize("pacman", firstTileIndex);

            Board = new Board();
            Board.Position = new Position(300, 0);
            Board.Group = new GroupObject(Board.Position);
            Board.Tiles = Board.GenerateTiles(firstTileIndex);
            Board.Pellets = Board.GeneratePellets();

            Pacman = new Pacman();
            Pacman.StartPosition = TileEngine.GetPosition(13.5f, 17);
            Pacman.Position = Pacman.StartPosition.Copy();
            Pacman.TilePosition = new TilePosition(Pacman.Position);
            Pacman.Direction = new Direction(Direction.Left);
            Pacman.Rotation = new Rotation();
            Pacman.AnimatedSprite = new AnimatedSprite(filename: "pacman", parent: Board.Group, position: Pacman.Position, rotation: Pacman.Rotation);
            Pacman.AnimatedSprite.AddSequence(name: "chomp", frames: new int[] { 36, 37, 36, 38 }, time: 200);
            Pacman.AnimatedSprite.AddSequence(name: "still", frames: new int[] { 36 });
            Pacman.AnimatedSprite.SetSequence("chomp");
            Pacman.Velocity = new Velocity(Pacman.Position);
            Pacman.Target = new PacmanTarget(Pacman, Pacman.Direction, Board.Tiles, Board.Group);
            Pacman.Steering = new Steering(Pacman, Pacman.Target);
            Pacman.SnapToTarget = new SnapToTarget(Pacman, Pacman.Target, 100);
            Pacman.StartStopAnimation = new StartStopAnimation(Pacman.Velocity, Pacman.AnimatedSprite);
            Pacman.WrapAroundScreen = new WrapAroundScreen(Pacman, Board);
            Pacman.Collision = new Collision(Pacman);

            Blinky = new Ghost();
            Blinky.State = GhostState.Chase;
            Blinky.StartPosition = TileEngine.GetPosition(13.5f, 11);
            Blinky.Position = Blinky.StartPosition.Copy();
            Blinky.TilePosition = new TilePosition(Blinky.Position);
            Blinky.Direction = new Direction(Direction.Left);
            Blinky.Rotation = new Rotation();
            Blinky.AnimatedSprite = new AnimatedSprite(filename: "pacman", parent: Board.Group, position: Blinky.Position);
            Blinky.AnimatedSprite.AddSequence(name: "Up", start: 0, count: 2, time: 150);
            Blinky.AnimatedSprite.AddSequence(name: "Down", start: 2, count: 2, time: 150);
            Blinky.AnimatedSprite.AddSequence(name: "Left", start: 4, count: 2, time: 150);
            Blinky.AnimatedSprite.AddSequence(name: "Right", start: 6, count: 2, time: 150);
            Blinky.AnimatedSprite.SetSequence(name: "Up");
            Blinky.AnimatedTowardDirection = new AnimatedTowardDirection(Blinky.Direction, Blinky.AnimatedSprite);
            Blinky.Velocity = new Velocity(Blinky.Position);
            Blinky.Target = new NextTile(Blinky, Blinky.Direction, Board.Tiles, Board.Group);
            Blinky.Steering = new Steering(Blinky, Blinky.Target);
            Blinky.EndTarget = new BlinkyTarget(Blinky, Pacman, Board.Group);
            Blinky.GetToEndTarget = new GetToEndTarget(Blinky, Board.Tiles);
            Blinky.SnapToTarget = new SnapToTarget(Blinky, Blinky.Target, 150);
            Blinky.WrapAroundScreen = new WrapAroundScreen(Blinky, Board);

            Pinky = new Ghost();
            Pinky.State = GhostState.Chase;
            Pinky.StartPosition = TileEngine.GetPosition(13.5f, 13.5f);
            Pinky.Position = Pinky.StartPosition.Copy();
            Pinky.TilePosition = new TilePosition(Pinky.Position);
            Pinky.Direction = new Direction(Direction.Left);
            Pinky.Rotation = new Rotation();
            Pinky.AnimatedSprite = new AnimatedSprite(filename: "pacman", parent: Board.Group, position: Pinky.Position);
            Pinky.AnimatedSprite.AddSequence(name: "Up", start: 8, count: 2, time: 150);
            Pinky.AnimatedSprite.AddSequence(name: "Down", start: 10, count: 2, time: 150);
            Pinky.AnimatedSprite.AddSequence(name: "Left", start: 12, count: 2, time: 150);
            Pinky.AnimatedSprite.AddSequence(name: "Right", start: 14, count: 2, time: 150);
            Pinky.AnimatedSprite.SetSequence(name: "Up");
            Pinky.AnimatedTowardDirection = new AnimatedTowardDirection(Pinky.Direction, Pinky.AnimatedSprite);
            Pinky.Velocity = new Velocity(Pinky.Position);
            Pinky.Target = new NextTile(Pinky, Pinky.Direction, Board.Tiles, Board.Group);
            Pinky.Steering = new Steering(Pinky, Pinky.Target);
            Pinky.EndTarget = new PinkyTarget(Pinky, Pacman, Board.Group);
            Pinky.GetToEndTarget = new GetToEndTarget(Pinky, Board.Tiles);
            Pinky.SnapToTarget = new SnapToTarget(Pinky, Pinky.Target, 150);
            Pinky.WrapAroundScreen = new WrapAroundScreen(Pinky, Board);

            Inky = new Ghost();
            Inky.State = GhostState.Chase;
            Inky.StartPosition = TileEngine.GetPosition(11.5f, 14.5f);
            Inky.Position = Inky.StartPosition.Copy();
            Inky.TilePosition = new TilePosition(Inky.Position);
            Inky.Direction = new Direction(Direction.Right);
            Inky.Rotation = new Rotation();
            Inky.AnimatedSprite = new AnimatedSprite(filename: "pacman", parent: Board.Group, position: Inky.Position);
            Inky.AnimatedSprite.AddSequence(name: "Up", start: 16, count: 2, time: 150);
            Inky.AnimatedSprite.AddSequence(name: "Down", start: 18, count: 2, time: 150);
            Inky.AnimatedSprite.AddSequence(name: "Left", start: 20, count: 2, time: 150);
            Inky.AnimatedSprite.AddSequence(name: "Right", start: 22, count: 2, time: 150);
            Inky.AnimatedSprite.SetSequence(name: "Up");
            Inky.AnimatedTowardDirection = new AnimatedTowardDirection(Inky.Direction, Inky.AnimatedSprite);
            Inky.Velocity = new Velocity(Inky.Position);
            Inky.Target = new NextTile(Inky, Inky.Direction, Board.Tiles, Board.Group);
            Inky.Steering = new Steering(Inky, Inky.Target);
            Inky.EndTarget = new InkyTarget(Inky, Blinky, Pacman, Board.Group);
            Inky.GetToEndTarget = new GetToEndTarget(Inky, Board.Tiles);
            Inky.SnapToTarget = new SnapToTarget(Inky, Inky.Target, 150);
            Inky.WrapAroundScreen = new WrapAroundScreen(Inky, Board);

            Clyde = new Ghost();
            Clyde.State = GhostState.Chase;
            Clyde.StartPosition = TileEngine.GetPosition(15.5f, 14.5f);
            Clyde.Position = Clyde.StartPosition.Copy();
            Clyde.TilePosition = new TilePosition(Clyde.Position);
            Clyde.Direction = new Direction(Direction.Left);
            Clyde.Rotation = new Rotation();
            Clyde.AnimatedSprite = new AnimatedSprite(filename: "pacman", parent: Board.Group, position: Clyde.Position);
            Clyde.AnimatedSprite.AddSequence(name: "Up", start: 24, count: 2, time: 150);
            Clyde.AnimatedSprite.AddSequence(name: "Down", start: 26, count: 2, time: 150);
            Clyde.AnimatedSprite.AddSequence(name: "Left", start: 28, count: 2, time: 150);
            Clyde.AnimatedSprite.AddSequence(name: "Right", start: 30, count: 2, time: 150);
            Clyde.AnimatedSprite.SetSequence(name: "Up");
            Clyde.AnimatedTowardDirection = new AnimatedTowardDirection(Clyde.Direction, Clyde.AnimatedSprite);
            Clyde.Velocity = new Velocity(Clyde.Position);
            Clyde.Target = new NextTile(Clyde, Clyde.Direction, Board.Tiles, Board.Group);
            Clyde.Steering = new Steering(Clyde, Clyde.Target);
            Clyde.EndTarget = new ClydeTarget(Clyde, Pacman, Board.Group);
            Clyde.GetToEndTarget = new GetToEndTarget(Clyde, Board.Tiles);
            Clyde.SnapToTarget = new SnapToTarget(Clyde, Clyde.Target, 150);
            Clyde.WrapAroundScreen = new WrapAroundScreen(Clyde, Board);
            
            TileSelector = new TileSelector();
            TileSelector.Position = TileEngine.GetPosition(13, 17);
            TileSelector.TilePosition = new TilePosition(TileSelector.Position);
            TileSelector.Rectangle = new RectangleObject(Board.Group, TileSelector.Position, new Dimension(TileEngine.TileWidth, TileEngine.TileHeight));
            TileSelector.Rectangle.Alpha = 0.5f;

            Score = new Score();

            DebugInfo = new DebugInfo();
            DebugInfo.addDebug("Pacman Position: ", Pacman.Position);
            DebugInfo.addDebug("Pacman Tile: ", Pacman.TilePosition);
            DebugInfo.addDebug("Blinky Position: ", Blinky.Position);
            DebugInfo.addDebug("Blinky Tile: ", Blinky.TilePosition);
            DebugInfo.addDebug("Pinky Position: ", Pinky.Position);
            DebugInfo.addDebug("Pinky Tile: ", Pinky.TilePosition);
            DebugInfo.addDebug("Inky Position: ", Inky.Position);
            DebugInfo.addDebug("Inky Tile: ", Inky.TilePosition);
            DebugInfo.addDebug("Clyde Position: ", Clyde.Position);
            DebugInfo.addDebug("Clyde Tile: ", Clyde.TilePosition);
            DebugInfo.addDebug("Tile Selector: ", TileSelector.TilePosition);
            DebugInfo.addDebug("Score: ", Score);

            KeyboardListener.Press += ToggleStates;
            Blinky.ChangeGhostState += OnChangeGhostState;
            Pinky.ChangeGhostState += OnChangeGhostState;
            Inky.ChangeGhostState += OnChangeGhostState;
            Clyde.ChangeGhostState += OnChangeGhostState;
            Collision.Collide += onCollision;
        }

        private void OnChangeGhostState(Ghost ghost, GhostState state)
        {
            if (ghost.EndTarget != null)
                ghost.EndTarget.Dispose();

            if (ghost == Blinky) Blinky.EndTarget = new BlinkyTarget(Blinky, Pacman, Board.Group);
            else if (ghost == Pinky) Pinky.EndTarget = new PinkyTarget(Pinky, Pacman, Board.Group);
            else if (ghost == Inky) Inky.EndTarget = new InkyTarget(Inky, Blinky, Pacman, Board.Group);
            else if (ghost == Clyde) Clyde.EndTarget = new ClydeTarget(Clyde, Pacman, Board.Group);
        }

        private void ToggleStates(Keys key)
        {
            if (key == Keys.Space)
            {
                var state = Blinky.State == GhostState.Chase ? GhostState.Scatter : GhostState.Chase;
                Blinky.State = state;
                Pinky.State = state;
                Inky.State = state;
                Clyde.State = state;
            }
        }

        private void onCollision(Pacman pacman, GameObject gameObject)
        {
            if (Board.Pellets.Contains(gameObject))
            {
                var pellet = gameObject as Pellet;
                
                if (pellet.IsPowerPellet)
                    Score.Value += 50;
                else
                    Score.Value += 10;

                pellet.Dispose();
                Board.Pellets.Remove(pellet);
            }
        }
    }
}
