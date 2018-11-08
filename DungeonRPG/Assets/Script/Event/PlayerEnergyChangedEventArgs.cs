using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DungeonRPG.Event
{
    /// <summary>
    /// Event argument class for the PlayerEnergyChanged event.
    /// </summary>
    /// <remarks>
    /// Copyright (c) 2018 by Johannes Berndorfer (berndoJ)
    /// </remarks>
    public class PlayerEnergyChangedEventArgs : EventArgs
    {
        #region Properties

        /// <summary>
        /// The new energy of the player.
        /// </summary>
        public float NewEnergy
        {
            get;
            private set;
        }

        /// <summary>
        /// The type of energy change that occured to the player.
        /// </summary>
        public PlayerEnergyChangedType EnergyChangeType
        {
            get;
            private set;
        }

        /// <summary>
        /// The amount of energy change difference.
        /// </summary>
        public float EnergyChangeDelta
        {
            get;
            private set;
        }

        /// <summary>
        /// The old energy of the entity (caluclated from new value and difference)
        /// </summary>
        public float OldEnergy
        {
            get
            {
                return this.NewEnergy - this.EnergyChangeDelta;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new instance of this class.
        /// </summary>
        /// <param name="newEnergy">The new energy of the player.</param>
        /// <param name="energyChangeType">The energy change type.</param>
        /// <param name="energyChangeDelta">The energy change delta.</param>
        public PlayerEnergyChangedEventArgs(float newEnergy, PlayerEnergyChangedType energyChangeType, float energyChangeDelta)
        {
            this.NewEnergy = newEnergy;
            this.EnergyChangeType = energyChangeType;
            this.EnergyChangeDelta = energyChangeDelta;
        }

        #endregion
    }

    /// <summary>
    /// Enum to declare the type of energy change.
    /// </summary>
    /// <remarks>
    /// Coypright (c) 2018 by Johannes Berndorfer (berndoJ)
    /// </remarks>
    public enum PlayerEnergyChangedType
    {
        /// <summary>
        /// The player gained energy.
        /// </summary>
        GAINED_ENERGY,

        /// <summary>
        /// The player lost energy.
        /// </summary>
        LOST_ENERGY
    }
}
