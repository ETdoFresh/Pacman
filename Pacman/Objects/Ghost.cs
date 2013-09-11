﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pacman.Engine.Display;
using Pacman.Engine.Helpers;
using Microsoft.Xna.Framework;

namespace Pacman.Objects
{
    abstract class Ghost : DisplayObject, ISteer
    {
        private GhostState _ghostState;

        protected TileGrid _tileGrid;
        protected PacmanObject _pacman;

        public Ghost(TileGrid tileGrid, PacmanObject pacmanObject)
        {
            _tileGrid = tileGrid;
            _pacman = pacmanObject;
        }

        protected virtual void Create()
        {
            SetAnimations();
            SetTransforms();
            SetProperties();
            SetUpdaters();
            ResetProperties();
            _tileGrid.AddComponent(this);
        }

        protected void SetAnimations()
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
            Speed = new Speed(225);
            Direction = new Direction(Direction.LEFT);
        }

        protected virtual void SetUpdaters()
        {
            Velocity = new Velocity() { Position = Position, Speed = Speed };
            Rotation = new Rotation() { Orientation = Orientation };
            Steering = new Steering(this, ImmediateTarget as ISteer);
            Wrap = new Wrap(Position, 0, 0, _tileGrid.Width, _tileGrid.Height);
            SnapToTarget = new SnapToTarget(this, Velocity, ImmediateTarget);
            ShiftEyesToDirection = new ShiftEyesToDirection(this);
            AddComponent(Velocity);
            AddComponent(Steering);
            AddComponent(Wrap);
            AddComponent(SnapToTarget);
            AddComponent(ShiftEyesToDirection);
        }

        public virtual void ResetProperties() 
        {
            Body.Visible = true;
            Eyes.Visible = true;
            Pupils.Visible = true;
            Eyes.ChangeIndex(20);
            Pupils.ChangeIndex(25);
            Pupils.Tint = new Color(60, 87, 167);
            ShiftEyesToDirection.Enabled = true;
            Speed.Factor = 1;
        }

        public void ChangeState(GhostState.GhostStates ghostState)
        {
            _ghostState = GhostState.Change(ghostState);
            _ghostState.SetProperties(this);
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
            if (LeaveHome != null) LeaveHome.RemoveSelf();
            base.RemoveSelf();
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
        public LeaveHome LeaveHome { get; set; }
    }
}
