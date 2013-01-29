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
        public Tile TargetTile { get; set; }
        public Tile ScatterTargetTile { get; set; }
        public Tile NextTile { get; set; }
        public Tile NextNextTile { get; set; }
        public bool IsSteering { get; set; }

        protected Texture2D debugRectangle;
        protected Color debugRectangleColor;

        public Ghost(): base (display.RetrieveTexture("pacman"), display.RetrieveSourceRectangles("pacman"))
        {
            SetFrame(0);
            origin.X = Width / 2;
            origin.Y = Height / 2;
            kinematicSeek = new KinematicSeek() { character = this, target = this, maxSpeed = 150 };
            IsSteering = true;

            debugRectangle = new Texture2D(Game1.graphics.GraphicsDevice, 15, 15);
            Color[] data = new Color[15 * 15];
            for (int i = 0; i < data.Length; i++) data[i] = Color.White;
            debugRectangle.SetData(data);
            debugRectangleColor = Color.White;
        }

        public override void Update(GameTime gameTime)
        {
            SteerToTarget(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (debugRectangle != null && TargetTile != null)
                spriteBatch.Draw(debugRectangle, TargetTile.Position, debugRectangleColor);
        }

        private void SteerToTarget(GameTime gameTime)
        {
            if (IsSteering && this.Position != Target.Position)
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

    class Blinky : Ghost
    {
        public Blinky()
            : base()
        {
            SetFrame(0);
            debugRectangleColor = Color.Red * 0.5f;
        }

        public override void UpdateGhostTiles(Map map, Pacman pacman)
        {
            TargetTile = map.GetTileFromPosition(pacman.Position);
            base.UpdateGhostTiles(map, pacman);
        }
    }

    class Pinky : Ghost
    {
        public Pinky()
            : base()
        {
            SetFrame(8);
            debugRectangleColor = Color.Pink * 0.5f;
        }

        public override void UpdateGhostTiles(Map map, Pacman pacman)
        {
            var pacmanTile = map.GetTileFromPosition(pacman.Position);
            TargetTile = map.GetTileFromDirectionClamped(pacmanTile, pacman.Direction * 4);
            base.UpdateGhostTiles(map, pacman);
        }
    }

    class Inky : Ghost
    {
        public IStatic Blinky { get; set; }

        public Inky()
            : base()
        {
            SetFrame(16);

            debugRectangleColor = Color.Cyan * 0.5f;
        }

        public override void UpdateGhostTiles(Map map, Pacman pacman)
        {
            var pacmanTile = map.GetTileFromPosition(pacman.Position);
            TargetTile = map.GetTileFromDirectionClamped(pacmanTile, pacman.Direction * 2);
            if (Blinky != null)
            {
                var difference = Blinky.Position - TargetTile.Position;
                difference *= 2;
                var targetPosition = Blinky.Position + difference;
                targetPosition.X = MathHelper.Clamp(targetPosition.X, 0, map.TileWidth * map.MapWidth - 1);
                targetPosition.Y = MathHelper.Clamp(targetPosition.Y, 0, map.TileHeight * map.MapHeight - 1);
                TargetTile = map.GetTileFromPosition(targetPosition);
            }
            base.UpdateGhostTiles(map, pacman);
        }
    }

    class Clyde : Ghost
    {
        public Clyde()
            : base()
        {
            SetFrame(24);

            debugRectangleColor = Color.Orange * 0.5f;
        }

        public override void UpdateGhostTiles(Map map, Pacman pacman)
        {
            var pacmanTile = map.GetTileFromPosition(pacman.Position);
            TargetTile = pacmanTile;
            var distance = (Position - pacman.Position).Length();
            if (distance < 15 * 8)
                TargetTile = map.GetTileFromDirectionClamped(pacmanTile, new Vector2(-100, 100));
            base.UpdateGhostTiles(map, pacman);
        }
    }
}
