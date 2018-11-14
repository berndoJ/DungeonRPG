using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DungeonRPG.LevelManagement
{
    /// <summary>
    /// A data structure which represents the info needed for a level
    /// in the game.
    /// </summary>
    /// <remarks>
    /// Copyright (c) 2018 by Johannes Berndorfer (berndoJ)
    /// </remarks>
    public struct GameLevelInfo
    {
        /// <summary>
        /// The name of the scene this level corresponds to.
        /// </summary>
        public string LevelSceneName;

        /// <summary>
        /// The display name of the name.
        /// </summary>
        public string LevelDisplayName;

        /// <summary>
        /// The difficulty of the level. Range: 0 - 10
        /// </summary>
        public int Difficulty;

        /// <summary>
        /// The level type of this level.
        /// </summary>
        public GameLevelType LevelType;

        /// <summary>
        /// Constructor of this data structure.
        /// </summary>
        /// <param name="levelSceneName">The scene name of this level.</param>
        /// <param name="levelDisplayName">The level's display name.</param>
        /// <param name="difficulty">The difficulty of the level. (Range 0 - 10)</param>
        /// <param name="levelType">The level type.</param>
        public GameLevelInfo(string levelSceneName, string levelDisplayName, int difficulty, GameLevelType levelType)
        {
            this.LevelSceneName = levelSceneName;
            this.LevelDisplayName = levelDisplayName;
            this.Difficulty = difficulty;
            this.LevelType = levelType;
        }
    }
}
