using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Pacman.Objects
{
    class GhostState
    {
        public enum GhostStates { Home, LeavingHome, Chase, Scatter, Frightened, Eyes }
        static public GhostStates HOME { get { return GhostStates.Home; } }
        static public GhostStates LEAVINGHOME { get { return GhostStates.LeavingHome; } }
        static public GhostStates CHASE { get { return GhostStates.Chase; } }
        static public GhostStates SCATTER { get { return GhostStates.Scatter; } }
        static public GhostStates FRIGHTENED { get { return GhostStates.Frightened; } }
        static public GhostStates EYES { get { return GhostStates.Eyes; } }

        private GhostState(GhostStates ghostState) { }

        static public GhostState Change(GhostStates ghostState)
        {
            switch (ghostState)
            {
                case GhostStates.Home:
                    return new Home();
                case GhostStates.LeavingHome:
                    return new LeavingHome();
                case GhostStates.Chase:
                    return new Chase();
                case GhostStates.Scatter:
                    return new Scatter();
                case GhostStates.Frightened:
                    return new Frightened();
                case GhostStates.Eyes:
                    return new Eyes();
                default:
                    throw new Exception("Ghost State not valid");
            }
        }

        public virtual void SetProperties(Ghost ghost)
        {
            ghost.DisableAllComponents();
            ghost.HideAllComponents();

            ghost.TilePosition.Enabled = true;
            ghost.Wrap.Enabled = true;
            ghost.Body.Enabled = true;
            ghost.Eyes.Enabled = true;
            ghost.Pupils.Enabled = true;

            ghost.Body.Visible = true;
            ghost.Eyes.Visible = true;
            ghost.Pupils.Visible = true;

            ghost.Pupils.Tint = new Color(60, 87, 167);

            ghost.Speed.Factor = 1;
        }

        private class Home : GhostState
        {
            public Home() : base(HOME) { }

            public override void SetProperties(Ghost ghost)
            {
                base.SetProperties(ghost);
                ghost.Eyes.ChangeIndex(20);
                ghost.Pupils.ChangeIndex(25);
            }
        }

        private class LeavingHome : GhostState
        {
            public LeavingHome() : base(LEAVINGHOME) { }
        }

        private class Chase : GhostState
        {
            public Chase() : base(CHASE) { }

            public override void SetProperties(Ghost ghost)
            {
                base.SetProperties(ghost);
                ghost.Velocity.Enabled = true;
                ghost.Steering.Enabled = true;
                ghost.ShiftEyesToDirection.Enabled = true;
                ghost.Target.Enabled = true;
                ghost.Target.Visible = true;
                ghost.ImmediateTarget.Enabled = true;
                ghost.ImmediateTarget.Visible = true;
                ghost.SnapToTarget.Enabled = true;
            }
        }

        private class Scatter : GhostState
        {
            public Scatter() : base(SCATTER) { }
        }

        private class Frightened : GhostState
        {
            public Frightened() : base(FRIGHTENED) { }

            public override void SetProperties(Ghost ghost)
            {
                base.SetProperties(ghost);
                ghost.Body.Tint = new Color(60, 87, 167);
                ghost.Eyes.Tint = new Color(255, 207, 50);
                ghost.Eyes.ChangeIndex(28);
                ghost.Pupils.Visible = false;
                ghost.ShiftEyesToDirection.Enabled = false;
                ghost.Speed.Factor = 0.7f;
            }
        }

        private class Eyes : GhostState
        {
            public Eyes() : base(EYES) { }

            public override void SetProperties(Ghost ghost)
            {
                base.SetProperties(ghost);
                ghost.Body.Visible = false;
                ghost.Speed.Factor = 1.2f;
            }
        }
    }
}
