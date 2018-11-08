using DungeonRPG.Entities;
using DungeonRPG.ItemContainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DungeonRPG.LevelManagement
{
    /// <summary>
    /// A data structure which holds information (serializable) about the player to save. (Used in saving
    /// the current game)
    /// </summary>
    /// <remarks>
    /// Copyright (c) 2018 by Johannes Berndorfer (berndoJ)
    /// </remarks>
    [Serializable]
    public struct SerializedPlayer
    {
        #region Fields

        /// <summary>
        /// The position of the player.
        /// </summary>
        public Vector3Serializable PlayerPosition;

        /// <summary>
        /// The health of the player.
        /// </summary>
        public int PlayerHealth;

        /// <summary>
        /// Player god mode value.
        /// </summary>
        public bool PlayerGodMode;

        /// <summary>
        /// The player's name.
        /// </summary>
        public string PlayerName;

        /// <summary>
        /// The energy of the player.
        /// </summary>
        public float PlayerEnergy;

        /// <summary>
        /// The inventory of the player.
        /// </summary>
        public string PlayerInventorySer;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new value of this struct.
        /// </summary>
        /// <param name="playerPosition">The position of the player.</param>
        /// <param name="playerHealth">The health of the player.</param>
        /// <param name="playerGodMode">Player god mode value.</param>
        /// <param name="playerName">The player's name.</param>
        /// <param name="playerEnergy">The energy of the player.</param>
        /// <param name="playerInventory">The inventory of the player.</param>
        public SerializedPlayer(Vector3 playerPosition, int playerHealth, bool playerGodMode, string playerName, float playerEnergy, Inventory playerInventory)
        {
            this.PlayerPosition = new Vector3Serializable(playerPosition);
            this.PlayerHealth = playerHealth;
            this.PlayerGodMode = playerGodMode;
            this.PlayerName = playerName;
            this.PlayerEnergy = playerEnergy;
            this.PlayerInventorySer = playerInventory.GetSerialized();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Applies all values of this struct to a player object.
        /// </summary>
        /// <param name="player">The reference pass of a player object.</param>
        public void ApplyToPlayer(ref Player player)
        {
            player.transform.position = this.PlayerPosition.GetVector3();
            player.Health = this.PlayerHealth;
            player.GodMode = this.PlayerGodMode;
            player.PlayerName = this.PlayerName;
            player.Energy = this.PlayerEnergy;
            player.PlayerInventory.LoadFromSerialized(this.PlayerInventorySer);
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Creates a new value of this struct by the given player.
        /// </summary>
        /// <param name="p">The player.</param>
        /// <returns>The new serialized player value.</returns>
        public static SerializedPlayer NewFromPlayer(Player p)
        {
            return new SerializedPlayer(p.transform.position, p.Health, p.GodMode, p.PlayerName, p.Energy, p.PlayerInventory);
        }

        #endregion
    }
}
