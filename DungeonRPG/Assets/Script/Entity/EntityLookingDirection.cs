using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DungeonRPG.Entities
{
    /// <summary>
    /// Enum for specifying the direction the player looks in.
    /// </summary>
    public enum EntityLookingDirection
    {
        RIGHT = 0x0000,
        POSITIVE_X = 0x0000,
        LEFT = 0x0001,
        NEGATIVE_X = 0x0001
    }
}
