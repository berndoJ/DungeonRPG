using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


namespace DungeonRPG.Entity
{
    public class PlayerRegen : MonoBehaviour
    {
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
    }
}
