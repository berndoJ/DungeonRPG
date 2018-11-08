﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DungeonRPG.Entities;
using DungeonRPG.Event;

namespace DungeonRPG.UI
{
    /// <summary>
    /// Behavior class which controls the health bar display.
    /// </summary>
    /// <remarks>
    /// Copyright (c) 2018 by Johannes Berndorfer (berndoJ)
    /// </remarks>
    public class PlayerHealthBarDisplay : MonoBehaviour
    {
        #region Fields

        /// <summary>
        /// The player of which the health should be displayed.
        /// </summary>
        public Player Player;

        /// <summary>
        /// The image to fill according to the health of the player.
        /// </summary>
        public Image HealthBarImage;

        #endregion

        #region Behavior Methods

        /// <summary>
        /// Init of this code.
        /// </summary>
        private void Awake()
        {
            this.Player.OnEntityHealthChanged += this.OnEntityHealthChanged;
        }

        #endregion

        #region Event Delegates

        /// <summary>
        /// Gets invoked when the health of the player attached changed.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event args</param>
        private void OnEntityHealthChanged(object sender, EntityHealthChangedEventArgs e)
        {
            this.HealthBarImage.fillAmount = (float)e.NewHealth / this.Player.MaxHealth;
        }

        #endregion
    }
}
