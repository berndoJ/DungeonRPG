using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DungeonRPG.Entity;
using DungeonRPG.Event;

namespace DungeonRPG.UI
{
    public class PlayerEnergyDisplay : MonoBehaviour
    {
        /// <summary>
        /// The player of which the energy should be displayed.
        /// </summary>
        public Player Player;

        /// <summary>
        /// The text UI component where the energy should be displayed.
        /// </summary>
        public Text EnergyDisplayText;

        /// <summary>
        /// Init of this code.
        /// </summary>
        private void Awake()
        {
            this.Player.OnPlayerEnergyChanged += this.OnPlayerEnergyChanged;
        }

        /// <summary>
        /// Gets invoked when the energy of the player attached changed.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event args</param>
        private void OnPlayerEnergyChanged(object sender, PlayerEnergyChangedEventArgs e)
        {
            this.EnergyDisplayText.text = string.Format("{0:0}%", e.NewEnergy);
        }
    }

}