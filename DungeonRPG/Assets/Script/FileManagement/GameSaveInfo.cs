using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using DungeonRPG.Entities;
using DungeonRPG.LevelManagement;

namespace DungeonRPG.FileManagement
{
    [Serializable]
    public class GameSaveInfo
    {
        /// <summary>
        /// The current level the player is in.
        /// </summary>
        public readonly string CurrentLevelUID;

        /// <summary>
        /// The player of the scene.
        /// </summary>
        public readonly SerializedPlayer Player;
        
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
    }
}
