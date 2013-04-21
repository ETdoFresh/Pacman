using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DisplayLibrary;
using Microsoft.Xna.Framework;

namespace Pacman
{
    class Controller
    {
        private const Int32 firstTileIndex = 54;

        public Controller()
        {
            Initialize();
        }

        private void Initialize()
        {
            TileEngine.Initialize("pacman", firstTileIndex);
            collision = new Collision();

            Board = new Board();
            Board.Position = new Position(300, 0);
            Board.Group = new GroupObject(Board.Position);
            Board.Tiles = Board.GenerateTiles(firstTileIndex);

            Pacman = new Pacman();
            Pacman.Position = TileEngine.GetPosition(13.5f, 17);
            Pacman.TilePosition = new TilePosition(Pacman.Position);
            Pacman.Rotation = new Rotation();
            Pacman.AnimatedSprite = new AnimatedSprite(filename: "pacman", parent: Board.Group, position: Pacman.Position, rotation: Pacman.Rotation);
            Pacman.AnimatedSprite.AddSequence(name: "chomp", frames: new int[] { 36, 37, 36, 38 }, time: 200);
            Pacman.AnimatedSprite.AddSequence(name: "still", frames: new int[] { 36 });
            Pacman.AnimatedSprite.SetSequence("chomp");
            Pacman.Velocity = new Velocity(Pacman.Position);
            Pacman.Target = new PacmanTarget(Pacman, Board.Tiles, Board.Group);
            Pacman.Steering = new Steering(Pacman, Pacman.Target);
            Pacman.SnapToTarget = new SnapToTarget(Pacman, Pacman.Target, 100);
            Pacman.StartStopAnimation = new StartStopAnimation(Pacman.Velocity, Pacman.AnimatedSprite);
            Pacman.WrapAroundScreen = new WrapAroundScreen(Pacman, Board);

            Blinky = new Ghost();
            Blinky.Position = TileEngine.GetPosition(13.5f, 11);
            Blinky.TilePosition = new TilePosition(Blinky.Position);
            Blinky.Rotation = new Rotation();
            Blinky.AnimatedSprite = new AnimatedSprite(filename: "pacman", parent: Board.Group, position: Blinky.Position);
            Blinky.AnimatedSprite.AddSequence(name: "move", frames: new int[] { 0, 1 }, time: 150);
            Blinky.AnimatedSprite.SetSequence(name: "move");
            Blinky.Velocity = new Velocity(Blinky.Position);
            Blinky.Target = new BlinkyTarget(Blinky, Pacman, Board.Group);

            Pinky = new Ghost();
            Pinky.Position = TileEngine.GetPosition(13.5f, 13.5f);
            Pinky.TilePosition = new TilePosition(Pinky.Position);
            Pinky.Rotation = new Rotation();
            Pinky.AnimatedSprite = new AnimatedSprite(filename: "pacman", parent: Board.Group, position: Pinky.Position);
            Pinky.AnimatedSprite.AddSequence(name: "move", start: 8, count: 2, time: 150);
            Pinky.AnimatedSprite.SetSequence(name: "move");
            Pinky.Target = new PinkyTarget(Blinky, Pacman, Board.Group);

            Inky = new Ghost();
            Inky.Position = TileEngine.GetPosition(11.5f, 14.5f);
            Inky.TilePosition = new TilePosition(Inky.Position);
            Inky.Rotation = new Rotation();
            Inky.AnimatedSprite = new AnimatedSprite(filename: "pacman", parent: Board.Group, position: Inky.Position);
            Inky.AnimatedSprite.AddSequence(name: "move", start: 16, count: 2, time: 150);
            Inky.AnimatedSprite.SetSequence(name: "move");

            Clyde = new Ghost();
            Clyde.Position = TileEngine.GetPosition(15.5f, 14.5f);
            Clyde.TilePosition = new TilePosition(Clyde.Position);
            Clyde.Rotation = new Rotation();
            Clyde.AnimatedSprite = new AnimatedSprite(filename: "pacman", parent: Board.Group, position: Clyde.Position);
            Clyde.AnimatedSprite.AddSequence(name: "move", start: 24, count: 2, time: 150);
            Clyde.AnimatedSprite.SetSequence(name: "move");

            TileSelector = new TileSelector();
            TileSelector.Position = TileEngine.GetPosition(13.5f, 11);
            TileSelector.TilePosition = new TilePosition(TileSelector.Position);
            TileSelector.Rectangle = new RectangleObject(Board.Group, TileSelector.Position, new Dimension(TileEngine.TileWidth, TileEngine.TileHeight));
            TileSelector.Rectangle.Alpha = 0.5f;

            DebugInfo = new DebugInfo();
            DebugInfo.addDebug("Pacman Position: ", Pacman.Position);
            DebugInfo.addDebug("Pacman Tile: ", Pacman.TilePosition);
            DebugInfo.addDebug("Blinky Position: ", Blinky.Position);
            DebugInfo.addDebug("Tile Selector: ", TileSelector.TilePosition);
        }

        public Board Board { get; set; }
        public Pacman Pacman { get; set; }
        public Ghost Blinky { get; set; }
        public Ghost Pinky { get; set; }
        public Ghost Inky { get; set; }
        public Ghost Clyde { get; set; }
        public TileSelector TileSelector { get; set; }
        public DebugInfo DebugInfo { get; set; }

        public Collision collision { get; set; }
    }
}
