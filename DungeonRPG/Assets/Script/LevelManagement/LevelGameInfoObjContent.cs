using DungeonRPG.LevelManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DungeonRPG.LevelManagement
{
    /// <summary>
    /// Behavior class which is attached to the level game info obj.
    /// </summary>
    /// <remarks>
    /// Copyright (c) 2018 by Johannes Berndorfer (berndoJ)
    /// </remarks>
    public class LevelGameInfoObjContent : MonoBehaviour
    {
        #region Properties

        /// <summary>
        /// The name of the scene of this leve.
        /// </summary>
        public string LevelSceneName;

        #endregion
    }
}
