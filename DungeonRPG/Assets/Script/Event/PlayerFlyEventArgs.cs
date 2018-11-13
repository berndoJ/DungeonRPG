using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DungeonRPG.Event
{
    /// <summary>
    /// Event argument class for the PlayerFly event.
    /// </summary>
    /// <remarks>
    /// Copyright (c) 2018 by Johannes Berndorfer (berndoJ)
    /// </remarks>
    public class PlayerFlyEventArgs : EventArgs
    {
        #region Properties

        /// <summary>
        /// The current fly speed (x-Axis)
        /// </summary>
        public float CurrentFlySpeedX
        {
            get;
            private set;
        }

        /// <summary>
        /// The current fly speed (y-Axis)
        /// </summary>
        public float CurrentFlySpeedY
        {
            get;
            private set;
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new instance of this class.
        /// </summary>
        /// <param name="currentFlySpeedX">The current fly speed (x-Axis)</param>
        /// <param name="currentFlySpeedY">The current fly speed (y-Axis)</param>
        public PlayerFlyEventArgs(float currentFlySpeedX, float currentFlySpeedY)
        {
            this.CurrentFlySpeedX = currentFlySpeedX;
            this.CurrentFlySpeedY = currentFlySpeedY;
        }

        #endregion
    }
}
