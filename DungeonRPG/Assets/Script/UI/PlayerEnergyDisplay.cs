using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DungeonRPG.Entities;
using DungeonRPG.Event;

namespace DungeonRPG.UI
{
    /// <summary>
    /// Behavior class which controls the energy display text.
    /// </summary>
    /// <remarks>
    /// Copyright (c) 2018 by Johannes Berndorfer (berndoJ)
    /// </remarks>
    public class PlayerEnergyDisplay : MonoBehaviour
    {
        #region Fields

        /// <summary>
        /// The player of which the energy should be displayed.
        /// </summary>
        public Player Player;

        /// <summary>
        /// The text UI component where the energy should be displayed.
        /// </summary>
        public Text EnergyDisplayText;

        #endregion

        #region Behavior Methods

        /// <summary>
        /// Init of this code.
        /// </summary>
        private void Awake()
        {
            this.Player.OnPlayerEnergyChanged += this.OnPlayerEnergyChanged;
        }

        #endregion

        #region Event Delegates

        /// <summary>
        /// Gets invoked when the energy of the player attached changed.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event args</param>
        private void OnPlayerEnergyChanged(object sender, PlayerEnergyChangedEventArgs e)
        {
            this.EnergyDisplayText.text = string.Format("{0:0}%", e.NewEnergy);
        }

        #endregion
    }
}