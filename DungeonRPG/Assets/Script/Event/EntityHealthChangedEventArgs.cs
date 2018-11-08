using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DungeonRPG.Event
{
    /// <summary>
    /// Event argument class for the EntityHealthChanged event.
    /// </summary>
    /// <remarks>
    /// Copyright (c) 2018 by Johannes Berndorfer (berndoJ)
    /// </remarks>
    public class EntityHealthChangedEventArgs : EventArgs
    {
        #region Properties

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

        #endregion

        #region Constructor

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

        #endregion
    }

    /// <summary>
    /// Enum to decalre the type of health change.
    /// </summary>
    /// <remarks>
    /// Copyright (c) 2018 by Johannes Berndorfer (berndoJ)
    /// </remarks>
    public enum EntityHealthChangedType
    {
        /// <summary>
        /// The entity gained health.
        /// </summary>
        GAINED_HEALTH,

        /// <summary>
        /// The entity lost health.
        /// </summary>
        LOST_HEALTH,

        /// <summary>
        /// The entity died.
        /// </summary>
        DIED
    }
}
