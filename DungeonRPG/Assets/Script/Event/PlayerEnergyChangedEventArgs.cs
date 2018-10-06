using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DungeonRPG.Event
{
    public class PlayerEnergyChangedEventArgs : EventArgs
    {
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
    }

    /// <summary>
    /// Enum to declare the type of energy change.
    /// </summary>
    public enum PlayerEnergyChangedType
    {
        GAINED_ENERGY,
        LOST_ENERGY
    }
}
