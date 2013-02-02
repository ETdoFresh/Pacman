using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using DisplayEngine;

namespace PacmanGame
{
    class Ghost : SpriteObject, IGameObject, IStatic
    {
        private KinematicSeek kinematicSeek;

        public IStatic Target { get { return kinematicSeek.target; } set { kinematicSeek.target = value; } }
        public float Speed { get { return kinematicSeek.maxSpeed; } set { kinematicSeek.maxSpeed = value; } }

        public Tile TargetTile { get; set; }
        public Tile ScatterTargetTile { get; set; }
        public Tile NextTile { get; set; }
        public Tile NextNextTile { get; set; }
        public bool IsSteering { get; set; }
        public GhostState State { get; set; }

        public RectangleObject debugRectangle;

        public Ghost(GhostState state)
            : base(display.RetrieveTexture("pacman"), display.RetrieveSourceRectangles("pacman"))
        {
            debugRectangle = display.NewRect(0, 0, 15, 15);
            kinematicSeek = new KinematicSeek() { character = this, target = this, maxSpeed = 150 };

            State = state;
            State.Initialize(this);
            origin.X = Width / 2;
            origin.Y = Height / 2;
            IsSteering = true;
        }

        public Ghost() : this(new BlinkyScatter()) { }

        public override void Update(GameTime gameTime)
        {
            SteerToTarget(gameTime);
            
            if (debugRectangle != null && TargetTile != null)
                debugRectangle.Position = TargetTile.Position;

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        private void SteerToTarget(GameTime gameTime)
        {
            if (IsSteering && Target != null && this.Position != Target.Position)
            {
                var steering = kinematicSeek.GetSteering();
                var time = (float)gameTime.ElapsedGameTime.TotalSeconds;

                var deltaPosition = steering.linear * time;
                var distanceToTarget = (Target.Position - Position).LengthSquared();
                var distanceBySpeed = deltaPosition.LengthSquared();

                if (distanceToTarget < distanceBySpeed)
                    Position = Target.Position;
                else
                    Position += deltaPosition;

                Rotation += steering.angular * time;
            }
        }

        public virtual void UpdateGhostTiles(Map map, Pacman pacman)
        {
            State.UpdateGhostTiles(map, pacman);
            if (TargetTile == null) return;
            var ghostTile = map.GetTileFromPosition(Position);

            if (NextTile == null && NextNextTile == null)
            {
                NextTile = map.GetTileFromDirection(ghostTile, new Vector2(-1, 0));
                NextNextTile = map.GetTileFromDirection(ghostTile, new Vector2(-2, 0));
            }
            else if (Position == NextTile.Position)
            {
                NextTile = NextNextTile;
                Tile[] checkTiles = new Tile[4] 
                {
                    map.GetTileFromDirection(NextTile, new Vector2(-1, 0)),
                    map.GetTileFromDirection(NextTile, new Vector2(1, 0)),
                    map.GetTileFromDirection(NextTile, new Vector2(0, -1)),
                    map.GetTileFromDirection(NextTile, new Vector2(0, 1))
                };

                Tile nextNextTile = null;
                float distance = 0;
                for (int i = 0; i < checkTiles.Length; i++)
                {
                    if (checkTiles[i] != null && checkTiles[i] != ghostTile && checkTiles[i].IsPassable)
                    {
                        if (nextNextTile != null)
                        {
                            var newDistance = (TargetTile.Position - checkTiles[i].Position).LengthSquared();
                            if (newDistance < distance)
                            {
                                distance = newDistance;
                                nextNextTile = checkTiles[i];
                            }
                        }
                        else
                        {
                            nextNextTile = checkTiles[i];
                            distance = (TargetTile.Position - nextNextTile.Position).LengthSquared();
                        }
                    }
                }
                NextNextTile = nextNextTile;
            }
            Target = NextTile;
        }
    }

   
}
