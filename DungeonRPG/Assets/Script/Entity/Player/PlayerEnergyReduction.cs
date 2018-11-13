using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using DungeonRPG.Event;

namespace DungeonRPG.Entities
{
    /// <summary>
    /// Behavior class handling the energy reduction when the player moves.
    /// </summary>
    /// <remarks>
    /// Copyright (c) 2018 by Johannes Berndorfer (berndoJ)
    /// </remarks>
    public class PlayerEnergyReduction : MonoBehaviour
    {
        #region Fields

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
        /// The thershold below which the player runs slowly.
        /// Default: 10F
        /// </summary>
        [Tooltip("The thershold below which the player runs slowly.")]
        public float SlowRunEnergyThreshold = 10F;

        /// <summary>
        /// The factor which gets multiplied with the players speed when he should run slowly.
        /// Default: 0.5F
        /// </summary>
        [Tooltip("The factor which gets multiplied with the players speed when he should run slowly.")]
        public float SlowRunCoefficient = 0.5F;

        /// <summary>
        /// The threshold below which the player cannot jump anymore.
        /// Default: 15F
        /// </summary>
        [Tooltip("The threshold below which the player cannot jump anymore.")]
        public float NoJumpEnergyThreshold = 15F;

        #endregion

        #region Properties

        /// <summary>
        /// Boolean indicating if the speed of the player is currently reduced by this script.
        /// </summary>
        public bool IsSpeedReduced
        {
            get;
            private set;
        }

        #endregion

        #region Behavior Methods

        /// <summary>
        /// Init of this code.
        /// </summary>
        private void Awake()
        {
            this.IsSpeedReduced = false;
            this.Player.AI.OnPlayerMove += this.OnPlayerMove;
            this.Player.AI.OnPlayerJump += this.OnPlayerJump;
        }

        /// <summary>
        /// Game loop.
        /// </summary>
        private void FixedUpdate()
        {
            // Slow run
            if (this.Player.Energy < this.SlowRunEnergyThreshold && this.Player.AI.IsOnGround && !this.IsSpeedReduced)
            {
                this.IsSpeedReduced = true;
                this.Player.AI.MovementSpeedCoefficient *= this.SlowRunCoefficient;
            }
            else if (this.Player.Energy >= this.SlowRunEnergyThreshold && this.IsSpeedReduced)
            {
                this.IsSpeedReduced = false;
                this.Player.AI.MovementSpeedCoefficient /= this.SlowRunCoefficient;
            }
            // No jump
            if (this.Player.Energy < this.NoJumpEnergyThreshold)
            {
                this.Player.AI.CanJump = false;
            }
            else
            {
                this.Player.AI.CanJump = true;
            }
        }

        #endregion

        #region Event Delegates

        /// <summary>
        /// Event delegate for <see cref="PlayerAI.OnPlayerMove"/>
        /// </summary>
        /// <param name="sender">-</param>
        /// <param name="e">-</param>
        private void OnPlayerMove(object sender, PlayerMoveEventArgs e)
        {
            if (this.Player.AI.FlyMode) return;
            this.Player.Energy -= this.MovementEnergyLoss * Mathf.Abs(e.CurrentMovementSpeed);
        }

        /// <summary>
        /// Event delegate for <see cref="PlayerAI.OnPlayerJump"/>
        /// </summary>
        /// <param name="sender">-</param>
        /// <param name="e">-</param>
        private void OnPlayerJump(object sender, PlayerJumpEventArgs e)
        {
            if (this.Player.AI.FlyMode) return;
            this.Player.Energy -= this.JumpEnergyLoss;
        }

        #endregion
    }
}
