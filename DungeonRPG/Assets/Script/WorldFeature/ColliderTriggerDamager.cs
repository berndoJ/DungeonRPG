using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DungeonRPG.Entities;

namespace DungeonRPG.WorldFeature
{
    /// <summary>
    /// Generic behavior class which can be applied to a game object which contains a
    /// collider in trigger mode. It deals damage to the player if the player triggers
    /// the collider. The damage can be defined.
    /// </summary>
    /// <remarks>
    /// Copyright (c) 2018 by Johannes Berndorfer (berndoJ)
    /// </remarks>
    public class ColliderTriggerDamager : MonoBehaviour
    {
        #region Fields

        /// <summary>
        /// The collider of the player.
        /// </summary>
        public Collider2D PlayerCollider;

        /// <summary>
        /// The player.
        /// </summary>
        public Player Player;

        /// <summary>
        /// The damage to be dealt to the player upon entering the collider.
        /// </summary>
        public int EnterDamageDealt;

        /// <summary>
        /// The damage to be dealt to the player when staying in the collider.
        /// </summary>
        public int ContinousDamageDealt;

        /// <summary>
        /// The damage to be dealt to the player upon exiting the collider.
        /// </summary>
        public int ExitDamageDealt;

        #endregion

        #region Behavior Methods

        /// <summary>
        /// Event that gets triggered when some rigidbody enters the collider.
        /// </summary>
        /// <param name="triggerCollider">The collider of the object that entered the collider and triggered it.</param>
        private void OnTriggerEnter2D(Collider2D triggerCollider)
        {
            if (triggerCollider == this.PlayerCollider)
            {
                this.Player.DamageGeneric(this.EnterDamageDealt);
            }
        }

        /// <summary>
        /// Event that gets triggered when some rigidbody is within the collider.
        /// </summary>
        /// <param name="triggerCollider">The collider of the object that entered the collider and triggered it.</param>
        private void OnTriggerStay2D(Collider2D triggerCollider)
        {
            if (triggerCollider == this.PlayerCollider)
            {
                this.Player.DamageGeneric(this.ContinousDamageDealt);
            }
        }

        /// <summary>
        /// Event that gets triggered when some rigidbody exits the collider.
        /// </summary>
        /// <param name="triggerCollider">The collider of the object that entered the collider and triggered it.</param>
        private void OnTriggerExit2D(Collider2D triggerCollider)
        {
            if (triggerCollider == this.PlayerCollider)
            {
                this.Player.DamageGeneric(this.ExitDamageDealt);
            }
        }

        #endregion
    }
}