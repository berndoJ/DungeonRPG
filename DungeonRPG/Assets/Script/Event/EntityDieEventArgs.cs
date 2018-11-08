using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DungeonRPG.Event
{
    /// <summary>
    /// Event argument class for the EntityDie event.
    /// </summary>
    /// <remarks>
    /// Copyright (c) 2018 by Johannes Berndorfer (berndoJ)
    /// </remarks>
    public class EntityDieEventArgs : EventArgs
    {
        #region Constructor 

        /// <summary>
        /// Creates a new instance of this class.
        /// </summary>
        public EntityDieEventArgs() { }

        #endregion
    }
}
