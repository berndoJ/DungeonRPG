using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using DungeonRPG.Event;
using DungeonRPG.ItemContainer;

namespace DungeonRPG.Entities
{
    /// <summary>
    /// This class is the base behavior class for the player.
    /// It handles all tasks handled by an entity as well as
    /// extra tasks proposed by the player.
    /// </summary>
    /// <remarks>
    /// Copyright (c) 2018 by Johannes Berndorfer (berndoJ)
    /// 
    /// Functional parts of this class (w.o. parent classes):
    /// - Player energy handling
    /// - (Temp) Handles player death and reload of scene.
    /// </remarks>
    public class Player : Entity
    {
        #region Property Fields

        /// <summary>
        /// The name of the player.
        /// Default: "Player"
        /// </summary>
        [Tooltip("The name of the player.")]
        public string PlayerName = "Player";

        /// <summary>
        /// The maximum energy level of the player.
        /// Default: 100F, default the energy level is a percentage.
        /// </summary>
        [Tooltip("The maximum energy level of the player.")]
        public float MaxEnergy = 100F;

        /// <summary>
        /// The AI of the player.
        /// </summary>
        [Tooltip("The AI of the player.")]
        public new PlayerAI AI;

        /// <summary>
        /// The player's inventory.
        /// </summary>
        [Tooltip("The player's inventory.")]
        public Inventory PlayerInventory;

        #endregion

        #region Events

        /// <summary>
        /// Gets invoked when the energy of the player changed.
        /// </summary>
        public event EventHandler<Event.PlayerEnergyChangedEventArgs> OnPlayerEnergyChanged;

        #endregion

        #region Properties

        /// <summary>
        /// The energy of the player.
        /// </summary>
        public float Energy
        {
            get
            {
                return this.mEnergy;
            }
            set
            {
                if (value < 0)
                    value = 0;
                if (value > 100)
                    value = this.MaxEnergy;
                float deltaEnergy = value - this.mEnergy;
                if (this.GodMode && deltaEnergy < 0)
                    return;
                this.mEnergy = value;
                PlayerEnergyChangedType changeType = 0;
                if (deltaEnergy > 0)
                    changeType = PlayerEnergyChangedType.GAINED_ENERGY;
                else if (deltaEnergy < 0)
                    changeType = PlayerEnergyChangedType.LOST_ENERGY;
                else return;
                if (this.OnPlayerEnergyChanged != null)
                    this.OnPlayerEnergyChanged(this, new Event.PlayerEnergyChangedEventArgs(value, changeType, deltaEnergy));
            }
        }
        private float mEnergy;

        #endregion

        #region Behavior Methods

        /// <summary>
        /// Init of this code.
        /// </summary>
        private void Awake()
        {
            this.OnEntityDie += this.OnPlayerDie;
        }

        /// <summary>
        /// Init on the frist frame.
        /// </summary>
        private void Start()
        {
            if (this.Health == 0)
            {
                this.Health = this.MaxHealth;
            }
            if (this.Energy == 0)
            {
                this.Energy = this.MaxEnergy;
            }
        }

        #endregion

        #region Event Delegates

        /// <summary>
        /// Event delegate of <see cref="Entity.OnEntityDie"/>
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void OnPlayerDie(object sender, EntityDieEventArgs e)
        {
            SceneManager.LoadScene("Level0");
        }

        #endregion
    }
}
