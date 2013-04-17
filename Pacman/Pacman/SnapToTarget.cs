using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DisplayLibrary;
using Microsoft.Xna.Framework;

namespace Pacman
{
    class SnapToTarget : IDisposable
    {
        private GameObject source;
        private GameObject target;
        private float maxSpeed;

        public SnapToTarget(GameObject source, GameObject target, float maxSpeed)
        {
            this.source = source;
            this.target = target;
            this.maxSpeed = maxSpeed;

            Runtime.GameUpdate += UpdateSnap;
        }

        private void UpdateSnap(GameTime gameTime)
        {
            var distance = (source.Position.Value - target.Position.Value).Length();
            var littleExtraSnapDistanceFactor = 1.5;
            if (distance < maxSpeed * gameTime.ElapsedGameTime.TotalSeconds * littleExtraSnapDistanceFactor)
            {
                source.Position.Value = target.Position.Value;
                source.Velocity.Value = Vector2.Zero;
            }
        }

        public void Dispose()
        {
            Runtime.GameUpdate -= UpdateSnap;
        }
    }
}
