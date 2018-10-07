using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using DungeonRPG.Event;

namespace DungeonRPG.Entity
{
    /// <summary>
    /// This class controlls all the movements done by the player.
    /// </summary>
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
        /// Init of this code on 1st frame.
        /// </summary>
        private void Start()
        {
            this.Health = this.MaxHealth;
            this.Energy = this.MaxEnergy;
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
