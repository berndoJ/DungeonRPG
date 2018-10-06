using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DungeonRPG.Entity
{
    /// <summary>
    /// Enum for specifying the direction the player looks in.
    /// </summary>
    public enum PlayerLookingDirection
    {
        RIGHT = 0x0000,
        POSITIVE_X = 0x0000,
        LEFT = 0x0001,
        NEGATIVE_X = 0x0001
    }
}
