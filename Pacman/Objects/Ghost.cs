﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pacman.Engine.Display;
using Pacman.Engine.Helpers;
using Microsoft.Xna.Framework;
using Pacman.Scenes;

namespace Pacman.Objects
{
    abstract class Ghost : DisplayObject, ISteer
    {
        private GhostState _ghostState;
        private GhostState.GhostStates _levelState;
        TunnelChecker _tunnelHandler;
        Timer _flashTimer;
        bool _flashBlue;

        protected TileGrid _tileGrid;
        protected PacmanObject _pacman;

        public Ghost(TileGrid tileGrid, PacmanObject pacman)
        {
            _tileGrid = tileGrid;
            _pacman = pacman;
        }

        protected virtual void Create()
        {
            SetAnimations();
            SetTransforms();
            SetProperties();
            SetUpdaters();
            SetupFlashTimer();
            ChangeState(GhostState.HOME);
            _tileGrid.AddComponent(this);
        }

        private void SetupFlashTimer()
        {
            _flashTimer = new Timer(166);
            _flashTimer.Stop();
            _flashTimer.ClockReachedLimit += OnFlashTimerReached;
            AddComponent(_flashTimer);
        }

        protected virtual void SetAnimations()
        {
            Body = new AnimatedSpriteObject("pacman");
            Eyes = new SpriteObject("pacman", 20);
            Pupils = new SpriteObject("pacman", 25);
            Body.AddSequence("BodyFloat", 8, 8, 250);
            AddComponent(Body);
            AddComponent(Eyes);
            AddComponent(Pupils);
        }

        protected virtual void SetTransforms()
        {
            TilePosition = new TilePosition(Position, _tileGrid.TileWidth, _tileGrid.TileHeight);
            Orientation = new Orientation(0);
            Scale = new Scale(1);
            AddComponent(TilePosition);
        }

        protected virtual void SetProperties()
        {
            Speed = new Speed(LevelScene.maxSpeed);
            Direction = new Direction(Direction.LEFT);
            Target = new Target();
            ImmediateTarget = new Target();
            ImmediateTarget.ChangeState(Target.IMMEDIATE, _tileGrid, this);
            _tileGrid.AddComponent(Target);
            _tileGrid.AddComponent(ImmediateTarget);
        }

        protected virtual void SetUpdaters()
        {
            Velocity = new Velocity() { Position = Position, Speed = Speed };
            Rotation = new Rotation() { Orientation = Orientation };
            Steering = new Steering(this, ImmediateTarget as ISteer);
            Wrap = new Wrap(Position, 0, 0, _tileGrid.Width, _tileGrid.Height);
            SnapToTarget = new SnapToTarget(this, Velocity, ImmediateTarget);
            ShiftEyesToDirection = new ShiftEyesToDirection(this);
            _tunnelHandler = new TunnelChecker(TilePosition);
            _tunnelHandler.TunnelStart += OnTunnelStart;
            _tunnelHandler.TunnelEnd += OnTunnelEnd;
            AddComponent(Velocity);
            AddComponent(Steering);
            AddComponent(Wrap);
            AddComponent(SnapToTarget);
            AddComponent(ShiftEyesToDirection);
            AddComponent(_tunnelHandler);
        }

        public virtual void ChangeState(GhostState.GhostStates ghostState)
        {
            ResetProperties();
            _ghostState = GhostState.Change(ghostState, this);
        }

        public void ReverseDirection()
        {
            if (ImmediateTarget != null)
                (ImmediateTarget.CurrentType as Target.ImmediateType).ReverseDirection();
        }

        protected virtual void ResetProperties()
        {
            DisableAllComponents();
            HideAllComponents();
            TilePosition.Enabled = true;
            Wrap.Enabled = true;
            Body.Enabled = true;
            Eyes.Enabled = true;
            Pupils.Enabled = true;
            Body.Visible = true;
            Eyes.Visible = true;
            Pupils.Visible = true;
            Eyes.Tint = Color.White;
            Pupils.Tint = new Color(60, 87, 167);
            Speed.Factor = 0.75f;
            _tunnelHandler.Enabled = true;
        }

        public virtual void OnHomeState()
        {
            Target.ChangeState(Target.FIXED);
            Eyes.ChangeIndex(20);
            Pupils.ChangeIndex(25);
        }

        public virtual void OnLeavingHomeState()
        {
            Direction.Value = Direction.RIGHT;
            ImmediateTarget.ChangeState(Target.FIXED);
            ImmediateTarget.Translate(_tileGrid.GetPosition(13.5f, 14.01f));
            Velocity.Enabled = true;
            Steering.Enabled = true;
            ShiftEyesToDirection.Enabled = true;
            ShiftEyesToDirection.SetEyesByDirection();
            SnapToTarget.Enabled = true;
            Steering.OnArriveAtTarget += OnLeaveHomeFirstArrive;
            Speed.Factor = 0.4f;
        }

        private void OnLeaveHomeFirstArrive()
        {
            Direction.Value = Direction.UP;
            Steering.OnArriveAtTarget -= OnLeaveHomeFirstArrive;
            ImmediateTarget.Translate(_tileGrid.GetPosition(13.5f, 11f));
            Steering.OnArriveAtTarget += OnLeaveHomeSecondArrive;
        }

        private void OnLeaveHomeSecondArrive()
        {
            Steering.OnArriveAtTarget -= OnLeaveHomeSecondArrive;
            ImmediateTarget.ChangeState(Target.IMMEDIATE, _tileGrid, this);
            ChangeState(_levelState);
        }

        public void SetLevelState(GhostState.GhostStates ghostState)
        {
            _levelState = ghostState;
            if (_ghostState.CurrentState == GhostState.CHASE || _ghostState.CurrentState == GhostState.SCATTER)
            {
                ReverseDirection();
                ChangeState(_levelState);
            }
        }

        public virtual void OnChaseState()
        {
            Velocity.Enabled = true;
            Steering.Enabled = true;
            ShiftEyesToDirection.Enabled = true;
            ShiftEyesToDirection.SetEyesByDirection();
            SnapToTarget.Enabled = true;
        }

        public virtual void OnScatterState()
        {
            Target.ChangeState(Target.FIXED);
            Velocity.Enabled = true;
            Steering.Enabled = true;
            ShiftEyesToDirection.Enabled = true;
            ShiftEyesToDirection.SetEyesByDirection();
            SnapToTarget.Enabled = true;
        }

        public virtual void OnFrightenedState()
        {
            Target.ChangeState(Target.FRIGHTENED, _tileGrid);
            Body.Tint = new Color(60, 87, 167);
            Eyes.Tint = new Color(255, 207, 50);
            Eyes.ChangeIndex(28);
            Pupils.Visible = false;
            Velocity.Enabled = true;
            Steering.Enabled = true;
            SnapToTarget.Enabled = true;
            ShiftEyesToDirection.Enabled = false;
            Speed.Factor = 0.5f;
        }

        public virtual void OnFrightenedFlashingState()
        {
            Target.ChangeState(Target.FRIGHTENED, _tileGrid);
            Body.Tint = Color.White;
            Eyes.Tint = Color.Red;
            Eyes.ChangeIndex(28);
            Pupils.Visible = false;
            Velocity.Enabled = true;
            Steering.Enabled = true;
            SnapToTarget.Enabled = true;
            ShiftEyesToDirection.Enabled = false;
            Speed.Factor = 0.5f;
            _flashTimer.Reset();
            _flashBlue = true;
        }

        private void OnFlashTimerReached()
        {
            if (CurrentState == GhostState.FRIGHTENEDFLASHING)
            {
                if (_flashBlue)
                {
                    Body.Tint = new Color(60, 87, 167);
                    Eyes.Tint = new Color(255, 207, 50);
                }
                else
                {
                    Body.Tint = Color.White;
                    Eyes.Tint = Color.Red;
                }
                _flashBlue = !_flashBlue;
                _flashTimer.Reset();
            }
        }

        public virtual void OnEyesState()
        {
            Target.ChangeState(Target.FIXED);
            Body.Visible = false;
            Speed.Factor = 1.2f;
        }

        public virtual void OnTunnelStart()
        {
            Speed.Factor = 0.40f;
        }

        public virtual void OnTunnelEnd()
        {
            if (CurrentState == GhostState.CHASE || CurrentState == GhostState.SCATTER)
                Speed.Factor = 0.75f;
            else if (CurrentState == GhostState.FRIGHTENED || CurrentState == GhostState.FRIGHTENEDFLASHING)
                Speed.Factor = 0.5f;
        }

        public override void RemoveSelf()
        {
            if (Body != null) Body.RemoveSelf();
            if (Eyes != null) Eyes.RemoveSelf();
            if (Pupils != null) Pupils.RemoveSelf();
            if (Velocity != null) Velocity.RemoveSelf();
            if (Rotation != null) Rotation.RemoveSelf();
            if (Target != null) Target.RemoveSelf();
            if (Steering != null) Steering.RemoveSelf();
            if (Wrap != null) Wrap.RemoveSelf();
            if (SnapToTarget != null) SnapToTarget.RemoveSelf();
            if (TilePosition != null) TilePosition.RemoveSelf();
            if (ImmediateTarget != null) ImmediateTarget.RemoveSelf();
            if (ShiftEyesToDirection != null) ShiftEyesToDirection.RemoveSelf();
            base.RemoveSelf();
        }

        public void AddDotCounter(int limit)
        {
            if (DotCounter != null) DotCounter.RemoveSelf();
            DotCounter = new DotCounter(limit, this);
            AddComponent(DotCounter);
        }

        public AnimatedSpriteObject Body { get; set; }
        public SpriteObject Eyes { get; set; }
        public SpriteObject Pupils { get; set; }
        public Speed Speed { get; set; }
        public Velocity Velocity { get; set; }
        public Rotation Rotation { get; set; }
        public Direction Direction { get; set; }
        public Target Target { get; set; }
        public Steering Steering { get; set; }
        public Wrap Wrap { get; set; }
        public SnapToTarget SnapToTarget { get; set; }
        public TilePosition TilePosition { get; set; }
        public Target ImmediateTarget { get; set; }
        public ShiftEyesToDirection ShiftEyesToDirection { get; set; }
        public GhostState.GhostStates CurrentState { get { return _ghostState.CurrentState; } }
        public DotCounter DotCounter { get; set; }
    }
}
