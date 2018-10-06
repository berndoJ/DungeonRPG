using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DungeonRPG.Event
{
    public class EntityHealthChangedEventArgs : EventArgs
    {
        /// <summary>
        /// The new health of the player.
        /// </summary>
        public int NewHealth
        {
            get;
            private set;
        }

        /// <summary>
        /// The type of health change that occured to the player. (Also includes death type)
        /// </summary>
        public EntityHealthChangedType HealthChangeType
        {
            get;
            private set;
        }

        /// <summary>
        /// The amount of health change difference.
        /// </summary>
        public int HealthChangeDelta
        {
            get;
            private set;
        }

        /// <summary>
        /// The old health of the entity (caluclated from new value and difference)
        /// </summary>
        public int OldHealth
        {
            get
            {
                return this.NewHealth - this.HealthChangeDelta;
            }
        }

        /// <summary>
        /// Creates a new instance of this event args class.
        /// </summary>
        /// <param name="newHealth">The new health of the entity.</param>
        /// <param name="healthChangeType">The type of health change.</param>
        /// <param name="healthChangeDelta">The delta of health change.</param>
        public EntityHealthChangedEventArgs(int newHealth, EntityHealthChangedType healthChangeType, int healthChangeDelta)
        {
            this.NewHealth = newHealth;
            this.HealthChangeType = healthChangeType;
            this.HealthChangeDelta = healthChangeDelta;
        }
    }

    /// <summary>
    /// Enum to decalre the type of health change.
    /// </summary>
    public enum EntityHealthChangedType
    {
        GAINED_HEALTH,
        LOST_HEALTH,
        DIED
    }
}
