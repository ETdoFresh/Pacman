using System;
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
        public Ghost ActiveGhost { get; set; }
        public Clock ActiveTimer { get; set; }
        public Level Level { get; set; }
        public GhostState LevelState { get; set; }
        public Clock StateTimer { get; set; }
        public int StateTimerCount { get; set; }
        public Clock FrightenedTimer { get; set; }

        private const Int32 firstTileIndex = 54;
        private const float maxSpeed = 150;

        public Controller()
        {
            Initialize();
        }

        private void Initialize()
        {
            TileEngine.Initialize("pacman", firstTileIndex);

            Level = new Level();

            Board = new Board();
            Board.Position = new Position(300, 0);
            Board.Group = new GroupObject(Board.Position);
            Board.Tiles = Board.GenerateTiles(firstTileIndex);
            Board.Pellets = Board.GeneratePellets();

            Pacman = new Pacman();
            Pacman.Speed = new Speed(maxSpeed, Level.pacmanSpeed);
            Pacman.StartPosition = TileEngine.GetPosition(13.5f, 23);
            Pacman.Position = Pacman.StartPosition.Copy();
            Pacman.TilePosition = new TilePosition(Pacman.Position);
            Pacman.ChangeSpeeds = new PacmanChangeSpeeds(Pacman.Speed, Pacman.TilePosition, Level.pacmanSpeed);
            Pacman.Collision = new Collision(Pacman);
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

            Blinky = new Ghost();
            Blinky.State = GhostState.Scatter;
            Blinky.Speed = new Speed(maxSpeed, Level.ghostSpeed);
            Blinky.StartPosition = TileEngine.GetPosition(13.5f, 11);
            Blinky.Position = Blinky.StartPosition.Copy();
            Blinky.TilePosition = new TilePosition(Blinky.Position);
            Blinky.Direction = new Direction(Direction.Left);
            Blinky.Rotation = new Rotation();
            Blinky.TunnelSpeed = new TunnelSpeed(Blinky.Speed, Blinky.TilePosition, Level.ghostTunnelSpeed);
            Blinky.AnimatedSprite = new AnimatedSprite(filename: "pacman", parent: Board.Group, position: Blinky.Position);
            Blinky.AnimatedSprite.AddSequence(name: "Up", start: 0, count: 2, time: 150);
            Blinky.AnimatedSprite.AddSequence(name: "Down", start: 2, count: 2, time: 150);
            Blinky.AnimatedSprite.AddSequence(name: "Left", start: 4, count: 2, time: 150);
            Blinky.AnimatedSprite.AddSequence(name: "Right", start: 6, count: 2, time: 150);
            Blinky.AnimatedSprite.AddSequence(name: "Frightened", start: 32, count: 2, time: 150);
            Blinky.AnimatedSprite.SetSequence(name: "Up");
            Blinky.AnimatedTowardDirection = new AnimatedTowardDirection(Blinky.Direction, Blinky.AnimatedSprite);
            Blinky.Velocity = new Velocity(Blinky.Position);
            Blinky.Target = new GeneralTarget(Board.Group);
            Blinky.Steering = new Steering(Blinky, Blinky.Target);
            Blinky.EndTarget = new BlinkyTarget(Blinky, Pacman, Board.Group);
            Blinky.GetToEndTarget = new GetToEndTarget(Blinky, Board.Tiles);
            Blinky.SnapToTarget = new SnapToTarget(Blinky, Blinky.Target, 150);
            Blinky.WrapAroundScreen = new WrapAroundScreen(Blinky, Board);

            Pinky = new Ghost();
            Pinky.State = GhostState.Home;
            Pinky.Speed = new Speed(maxSpeed, Level.ghostTunnelSpeed);
            Pinky.StartPosition = TileEngine.GetPosition(13.5f, 13.5f);
            Pinky.StartPosition2 = TileEngine.GetPosition(13.5f, 14.5f);
            Pinky.Position = Pinky.StartPosition.Copy();
            Pinky.TilePosition = new TilePosition(Pinky.Position);
            Pinky.Direction = new Direction(Direction.Left);
            Pinky.Rotation = new Rotation();
            Pinky.TunnelSpeed = new TunnelSpeed(Pinky.Speed, Pinky.TilePosition, Level.ghostTunnelSpeed);
            Pinky.AnimatedSprite = new AnimatedSprite(filename: "pacman", parent: Board.Group, position: Pinky.Position);
            Pinky.AnimatedSprite.AddSequence(name: "Up", start: 8, count: 2, time: 150);
            Pinky.AnimatedSprite.AddSequence(name: "Down", start: 10, count: 2, time: 150);
            Pinky.AnimatedSprite.AddSequence(name: "Left", start: 12, count: 2, time: 150);
            Pinky.AnimatedSprite.AddSequence(name: "Right", start: 14, count: 2, time: 150);
            Pinky.AnimatedSprite.AddSequence(name: "Frightened", start: 32, count: 2, time: 150);
            Pinky.AnimatedSprite.SetSequence(name: "Up");
            Pinky.AnimatedTowardDirection = new AnimatedTowardDirection(Pinky.Direction, Pinky.AnimatedSprite);
            Pinky.Velocity = new Velocity(Pinky.Position);
            Pinky.Target = new GeneralTarget(Board.Group);
            Pinky.Steering = new Steering(Pinky, Pinky.Target);
            Pinky.EndTarget = new PinkyTarget(Pinky, Pacman, Board.Group);
            Pinky.BounceInHome = new BounceInHome(Pinky);
            Pinky.SnapToTarget = new SnapToTarget(Pinky, Pinky.Target, 150);
            Pinky.WrapAroundScreen = new WrapAroundScreen(Pinky, Board);
            Pinky.DotCounter = new DotCounter(Level.pinkyLimit);

            Inky = new Ghost();
            Inky.State = GhostState.Home;
            Inky.Speed = new Speed(maxSpeed, Level.ghostTunnelSpeed);
            Inky.StartPosition = TileEngine.GetPosition(11.5f, 14.5f);
            Inky.StartPosition2 = TileEngine.GetPosition(11.5f, 13.5f);
            Inky.Position = Inky.StartPosition.Copy();
            Inky.TilePosition = new TilePosition(Inky.Position);
            Inky.Direction = new Direction(Direction.Right);
            Inky.Rotation = new Rotation();
            Inky.TunnelSpeed = new TunnelSpeed(Inky.Speed, Inky.TilePosition, Level.ghostTunnelSpeed);
            Inky.AnimatedSprite = new AnimatedSprite(filename: "pacman", parent: Board.Group, position: Inky.Position);
            Inky.AnimatedSprite.AddSequence(name: "Up", start: 16, count: 2, time: 150);
            Inky.AnimatedSprite.AddSequence(name: "Down", start: 18, count: 2, time: 150);
            Inky.AnimatedSprite.AddSequence(name: "Left", start: 20, count: 2, time: 150);
            Inky.AnimatedSprite.AddSequence(name: "Right", start: 22, count: 2, time: 150);
            Inky.AnimatedSprite.AddSequence(name: "Frightened", start: 32, count: 2, time: 150);
            Inky.AnimatedSprite.SetSequence(name: "Up");
            Inky.AnimatedTowardDirection = new AnimatedTowardDirection(Inky.Direction, Inky.AnimatedSprite);
            Inky.Velocity = new Velocity(Inky.Position);
            Inky.Target = new GeneralTarget(Board.Group);
            Inky.Steering = new Steering(Inky, Inky.Target);
            Inky.EndTarget = new InkyTarget(Inky, Blinky, Pacman, Board.Group);
            Inky.BounceInHome = new BounceInHome(Inky);
            Inky.SnapToTarget = new SnapToTarget(Inky, Inky.Target, 150);
            Inky.WrapAroundScreen = new WrapAroundScreen(Inky, Board);
            Inky.DotCounter = new DotCounter(Level.inkyLimit);

            Clyde = new Ghost();
            Clyde.State = GhostState.Home;
            Clyde.Speed = new Speed(maxSpeed, Level.ghostTunnelSpeed);
            Clyde.StartPosition = TileEngine.GetPosition(15.5f, 14.5f);
            Clyde.StartPosition2 = TileEngine.GetPosition(15.5f, 13.5f);
            Clyde.Position = Clyde.StartPosition.Copy();
            Clyde.TilePosition = new TilePosition(Clyde.Position);
            Clyde.Direction = new Direction(Direction.Left);
            Clyde.Rotation = new Rotation();
            Clyde.TunnelSpeed = new TunnelSpeed(Clyde.Speed, Clyde.TilePosition, Level.ghostTunnelSpeed);
            Clyde.AnimatedSprite = new AnimatedSprite(filename: "pacman", parent: Board.Group, position: Clyde.Position);
            Clyde.AnimatedSprite.AddSequence(name: "Up", start: 24, count: 2, time: 150);
            Clyde.AnimatedSprite.AddSequence(name: "Down", start: 26, count: 2, time: 150);
            Clyde.AnimatedSprite.AddSequence(name: "Left", start: 28, count: 2, time: 150);
            Clyde.AnimatedSprite.AddSequence(name: "Right", start: 30, count: 2, time: 150);
            Clyde.AnimatedSprite.AddSequence(name: "Frightened", start: 32, count: 2, time: 150);
            Clyde.AnimatedSprite.SetSequence(name: "Up");
            Clyde.AnimatedTowardDirection = new AnimatedTowardDirection(Clyde.Direction, Clyde.AnimatedSprite);
            Clyde.Velocity = new Velocity(Clyde.Position);
            Clyde.Target = new GeneralTarget(Board.Group);
            Clyde.Steering = new Steering(Clyde, Clyde.Target);
            Clyde.EndTarget = new ClydeTarget(Clyde, Pacman, Board.Group);
            Clyde.BounceInHome = new BounceInHome(Clyde);
            Clyde.SnapToTarget = new SnapToTarget(Clyde, Clyde.Target, 150);
            Clyde.WrapAroundScreen = new WrapAroundScreen(Clyde, Board);
            Clyde.DotCounter = new DotCounter(Level.clydeLimit);

            TileSelector = new TileSelector();
            TileSelector.Position = TileEngine.GetPosition(13, 17);
            TileSelector.TilePosition = new TilePosition(TileSelector.Position);
            TileSelector.Rectangle = new RectangleObject(Board.Group, TileSelector.Position, new Dimension(TileEngine.TileWidth, TileEngine.TileHeight));
            TileSelector.Rectangle.Alpha = 0.5f;

            Score = new Score();

            DebugInfo = new DebugInfo();
            DebugInfo.addDebug("Pacman Position: ", Pacman.Position);
            DebugInfo.addDebug("Pacman Tile: ", Pacman.TilePosition);
            DebugInfo.addDebug("Pacman Speed: ", Pacman.Speed);
            DebugInfo.addDebug("Blinky Position: ", Blinky.Position);
            DebugInfo.addDebug("Blinky Tile: ", Blinky.TilePosition);
            DebugInfo.addDebug("Blinky State: ", Blinky);
            DebugInfo.addDebug("Blinky Speed: ", Blinky.Speed);
            DebugInfo.addDebug("Pinky Position: ", Pinky.Position);
            DebugInfo.addDebug("Pinky Tile: ", Pinky.TilePosition);
            DebugInfo.addDebug("Pinky State: ", Pinky);
            DebugInfo.addDebug("Pinky Speed: ", Pinky.Speed);
            DebugInfo.addDebug("Inky Position: ", Inky.Position);
            DebugInfo.addDebug("Inky Tile: ", Inky.TilePosition);
            DebugInfo.addDebug("Inky State: ", Inky);
            DebugInfo.addDebug("Inky Speed: ", Inky.Speed);
            DebugInfo.addDebug("Clyde Position: ", Clyde.Position);
            DebugInfo.addDebug("Clyde Tile: ", Clyde.TilePosition);
            DebugInfo.addDebug("Clyde State: ", Clyde);
            DebugInfo.addDebug("Clyde Speed: ", Clyde.Speed);
            DebugInfo.addDebug("Tile Selector: ", TileSelector.TilePosition);
            DebugInfo.addDebug("Score: ", Score);

            KeyboardListener.Press += ToggleStates;
            Blinky.ChangeGhostState += OnChangeGhostState;
            Pinky.ChangeGhostState += OnChangeGhostState;
            Inky.ChangeGhostState += OnChangeGhostState;
            Clyde.ChangeGhostState += OnChangeGhostState;
            Collision.Collide += onCollision;

            ActiveGhost = Pinky;
            Pinky.DotCounter.LimitReached += LeaveHome;
            Inky.DotCounter.LimitReached += LeaveHome;
            Clyde.DotCounter.LimitReached += LeaveHome;
            ActiveTimer = new Clock(4000);
            ActiveTimer.ClockReachedLimit += LeaveHome;

            LevelState = GhostState.Scatter;
            StateTimer = new Clock(Level.Scatter1 * 1000);
            StateTimer.ClockReachedLimit += OnStateTimer;
        }

        private void OnStateTimer()
        {
            if (StateTimerCount == 0)
            {
                StateTimer.Reset(Level.Chase1 * 1000);
                LevelState = GhostState.Chase;
            }
            else if (StateTimerCount == 1)
            {
                StateTimer.Reset(Level.Scatter2 * 1000);
                LevelState = GhostState.Scatter;
            }
            else if (StateTimerCount == 2)
            {
                StateTimer.Reset(Level.Chase2 * 1000);
                LevelState = GhostState.Chase;
            }
            else if (StateTimerCount == 3)
            {
                StateTimer.Reset(Level.Scatter3 * 1000);
                LevelState = GhostState.Scatter;
            }
            else if (StateTimerCount == 4)
            {
                StateTimer.Reset(Level.Chase3 * 1000);
                LevelState = GhostState.Chase;
            }
            else if (StateTimerCount == 5)
            {
                StateTimer.Reset(Level.Scatter4 * 1000);
                LevelState = GhostState.Scatter;
            }
            else
            {
                StateTimer.Stop();
                LevelState = GhostState.Chase;
            }

            if (Blinky.State == GhostState.Chase || Blinky.State == GhostState.Scatter)
                Blinky.State = LevelState;
            if (Pinky.State == GhostState.Chase || Pinky.State == GhostState.Scatter)
                Pinky.State = LevelState;
            if (Inky.State == GhostState.Chase || Inky.State == GhostState.Scatter)
                Inky.State = LevelState;
            if (Clyde.State == GhostState.Chase || Clyde.State == GhostState.Scatter)
                Clyde.State = LevelState;

            StateTimerCount++;
        }

        private void LeaveHome()
        {
            if (ActiveTimer != null)
                ActiveTimer.Reset();

            if (ActiveGhost == Pinky)
            {
                Pinky.State = GhostState.LeaveHome;
                Pinky.DotCounter.Dispose();
                ActiveGhost = Inky;
            }
            else if (ActiveGhost == Inky)
            {
                Inky.State = GhostState.LeaveHome;
                Inky.DotCounter.Dispose();
                ActiveGhost = Clyde;
            }
            else if (ActiveGhost == Clyde)
            {
                Clyde.State = GhostState.LeaveHome;
                Clyde.DotCounter.Dispose();
                ActiveGhost = null;
                ActiveTimer.Dispose();
            }

        }

        private void OnChangeGhostState(Ghost ghost, GhostState state)
        {
            if (ghost.EndTarget != null)
                ghost.EndTarget.Dispose();

            if (ghost == Blinky) Blinky.EndTarget = new BlinkyTarget(Blinky, Pacman, Board.Group);
            else if (ghost == Pinky) Pinky.EndTarget = new PinkyTarget(Pinky, Pacman, Board.Group);
            else if (ghost == Inky) Inky.EndTarget = new InkyTarget(Inky, Blinky, Pacman, Board.Group);
            else if (ghost == Clyde) Clyde.EndTarget = new ClydeTarget(Clyde, Pacman, Board.Group);

            if (state == GhostState.LeaveHome)
            {
                if (ghost.BounceInHome != null) ghost.BounceInHome.Dispose();
                ghost.BounceInHome = null;
                if (ghost.LeaveHome == null) ghost.LeaveHome = new LeaveHome(ghost);
                ghost.LeaveHome.FinishLeaving += OnFinishLeavingHome;
            }
            else if (state == GhostState.Scatter || state == GhostState.Chase)
            {
                if (ghost.LeaveHome != null) ghost.LeaveHome.Dispose();
                ghost.LeaveHome = null;
                if (ghost.GetToEndTarget == null)
                {
                    ghost.Direction.Value = Direction.Left;
                    ghost.GetToEndTarget = new GetToEndTarget(ghost, Board.Tiles);
                    ghost.GetToEndTarget.CalculateNextMoves();
                }
                else
                {
                    ghost.Direction.Reverse();
                    ghost.GetToEndTarget.Dispose();
                    ghost.GetToEndTarget = new GetToEndTarget(ghost, Board.Tiles);
                    ghost.GetToEndTarget.CalculateNextMoves();
                }
            }
            else if (state == GhostState.Frightened)
            {
                ghost.AnimatedTowardDirection.Dispose();
                ghost.Direction.Reverse();
                ghost.GetToEndTarget.Dispose();
                ghost.GetToEndTarget = new GetToEndTarget(ghost, Board.Tiles);
                ghost.GetToEndTarget.CalculateNextMoves();
                ghost.AnimatedSprite.SetSequence("Frightened");
            }
        }

        private void OnFinishLeavingHome(Ghost ghost)
        {
            ghost.Speed.Factor = Level.ghostSpeed;
            ghost.State = LevelState;
        }

        private void ToggleStates(Keys key)
        {
            if (key == Keys.Space)
            {
                setNextState(Blinky);
                setNextState(Pinky);
                setNextState(Inky);
                setNextState(Clyde);
            }
            else if (key == Keys.R)
            {
                Dispose();
                Initialize();
            }
        }

        private void Dispose()
        {
            KeyboardListener.Press -= ToggleStates;
            Collision.Collide -= onCollision;

            Board.Dispose();
            Pacman.Dispose();
            Blinky.Dispose();
            Pinky.Dispose();
            Inky.Dispose();
            Clyde.Dispose();
            TileSelector.Dispose();
            DebugInfo.Dispose();
            ActiveTimer.Dispose();
            StateTimer.Dispose();
            StateTimerCount = 0;
        }

        private void setNextState(Ghost ghost)
        {
            if (ghost.State == GhostState.Home)
                ghost.State = GhostState.LeaveHome;
        }

        private void onCollision(Pacman pacman, GameObject gameObject)
        {
            if (Board.Pellets.Contains(gameObject))
            {
                var pellet = gameObject as Pellet;
                pacman.Speed.Factor = Level.pacmanEatSpeed;

                if (pellet.IsPowerPellet)
                {
                    if (FrightenedTimer == null) FrightenedTimer = new Clock();
                    FrightenedTimer.Reset(Level.frightTime);

                    List<Ghost> ghosts = new List<Ghost>() { Blinky, Pinky, Inky, Clyde };
                    foreach (var ghost in ghosts)
                        if (ghost.State != GhostState.Home && ghost.State != GhostState.LeaveHome)
                            ghost.State = GhostState.Frightened;

                    Score.Value += 50;
                }
                else
                {
                    Score.Value += 10;
                    if (ActiveGhost != null) ActiveGhost.DotCounter.Value++;
                }

                pellet.Dispose();
                Board.Pellets.Remove(pellet);
            }
        }
    }
}
