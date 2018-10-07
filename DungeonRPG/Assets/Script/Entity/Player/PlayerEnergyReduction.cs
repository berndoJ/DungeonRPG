using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using DungeonRPG.Event;

namespace DungeonRPG.Entity
{
    public class PlayerEnergyReduction : MonoBehaviour
    {
        /// <summary>
        /// The player.
        /// </summary>
        [Tooltip("The player.")]
        public Player Player;

        /// <summary>
        /// The loss of energy when the player moves (constant loss).
        /// Default: 0.1F
        /// </summary>
        [Tooltip("The loss of energy when the player moves (constant loss).")]
        public float MovementEnergyLoss = 0.1F;

        /// <summary>
        /// The loss of energy when the player jumps.
        /// Default: 5F
        /// </summary>
        [Tooltip("The loss of energy when the player jumps.")]
        public float JumpEnergyLoss = 5F;

        /// <summary>
        /// Init of this code.
        /// </summary>
        private void Awake()
        {
            this.Player.AI.OnPlayerMove += this.OnPlayerMove;
            this.Player.AI.OnPlayerJump += this.OnPlayerJump;
        }

        /// <summary>
        /// Event delegate for <see cref="PlayerAI.OnPlayerMove"/>
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void OnPlayerMove(object sender, PlayerMoveEventArgs e)
        {
            this.Player.Energy -= this.MovementEnergyLoss * Mathf.Abs(e.CurrentMovementSpeed);
        }

        /// <summary>
        /// Event delegate for <see cref="PlayerAI.OnPlayerJump"/>
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void OnPlayerJump(object sender, PlayerJumpEventArgs e)
        {
            this.Player.Energy -= this.JumpEnergyLoss;
        }
    }
}
