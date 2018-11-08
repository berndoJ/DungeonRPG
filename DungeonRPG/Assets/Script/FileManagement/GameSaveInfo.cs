using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using DungeonRPG.Entities;
using DungeonRPG.LevelManagement;

namespace DungeonRPG.FileManagement
{
    /// <summary>
    /// A data class which holds the necessary information to store
    /// the current game state in a file (to load it later on).
    /// </summary>
    /// <remarks>
    /// Copyright (c) 2018 by Johannes Berndorfer (berndoJ)
    /// </remarks>
    [Serializable]
    public class GameSaveInfo
    {
        #region Fields

        /// <summary>
        /// The current level the player is in.
        /// </summary>
        public readonly string CurrentLevelUID;

        /// <summary>
        /// The player of the scene.
        /// </summary>
        public readonly SerializedPlayer Player;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new instance of this class.
        /// </summary>
        /// <param name="currentLevelUID">The current level uid.</param>
        /// <param name="player">The serialized current player.</param>
        public GameSaveInfo(string currentLevelUID, SerializedPlayer player)
        {
            this.CurrentLevelUID = currentLevelUID;
            this.Player = player;
        }

        #endregion
    }
}
