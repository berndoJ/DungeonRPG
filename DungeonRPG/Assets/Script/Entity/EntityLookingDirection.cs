using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DungeonRPG.Entities
{
    /// <summary>
    /// Enum for specifying the direction the player looks in.
    /// </summary>
    /// <remarks>
    /// Copyright (c) 2018 by Johannes Berndorfer (berndoJ)
    /// </remarks>
    public enum EntityLookingDirection
    {
        /// <summary>
        /// The player is looking to the right of the screen. (Positive X)
        /// </summary>
        RIGHT = 0x0000,

        /// <summary>
        /// The player is looking to the right of the screen. (Positive X)
        /// </summary>
        POSITIVE_X = 0x0000,

        /// <summary>
        /// The player is looking to the left of the screen. (Negative X)
        /// </summary>
        LEFT = 0x0001,

        /// <summary>
        /// The player is looking to the left of the screen. (Negative X)
        /// </summary>
        NEGATIVE_X = 0x0001
    }
}
