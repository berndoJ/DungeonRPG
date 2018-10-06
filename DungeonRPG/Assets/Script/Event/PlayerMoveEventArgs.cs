using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DungeonRPG.Event
{
    public class PlayerMoveEventArgs : EventArgs
    {
        /// <summary>
        /// The movement speed at the time the event was called.
        /// </summary>
        public float CurrentMovementSpeed
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates a new instance of this class.
        /// </summary>
        /// <param name="currentMovementSpeed">The current movement speed of the player (at the time this event was called)</param>
        public PlayerMoveEventArgs(float currentMovementSpeed)
        {
            this.CurrentMovementSpeed = currentMovementSpeed;
        }
    }
}
