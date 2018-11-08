using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


namespace DungeonRPG.Entities
{
    /// <summary>
    /// A behavior class which controls the players energy
    /// regeneration.
    /// </summary>
    /// <remarks>
    /// Copyright (c) 2018 by Johannes Berndorfer (berndoJ)
    /// </remarks>
    public class PlayerRegen : MonoBehaviour
    {
        #region Fields

        /// <summary>
        /// The player.
        /// </summary>
        [Tooltip("The player.")]
        public Player Player;

        /// <summary>
        /// The amount of energy that the player regens per gameloop cycle.
        /// Default: 1F
        /// </summary>
        [Tooltip("The amount of energy that the player regens per gameloop cycle.")]
        public float EnergyRegen = 1F;

        /// <summary>
        /// Value indicating if the player should only regenerate if he is standing still.
        /// Default: true
        /// </summary>
        [Tooltip("Value indicating if the player should only regenerate if he is standing still.")]
        public bool OnlyRegenWhenStanding = true;

        #endregion

        #region Behavior Methods

        /// <summary>
        /// Game loop.
        /// </summary>
        private void FixedUpdate()
        {
            if (Mathf.Abs(this.Player.AI.CurrentMovementSpeed) < 0.01F || !this.OnlyRegenWhenStanding)
            {
                this.Player.Energy += this.EnergyRegen;
            }
        }

        #endregion
    }
}
