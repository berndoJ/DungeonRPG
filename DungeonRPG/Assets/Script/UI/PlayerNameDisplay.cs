using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DungeonRPG.Entities;

namespace DungeonRPG.UI
{
    /// <summary>
    /// Behavior class which controls the name display text.
    /// </summary>
    /// <remarks>
    /// Copyright (c) 2018 by Johannes Berndorfer (berndoJ)
    /// </remarks>
    public class PlayerNameDisplay : MonoBehaviour
    {
        #region Fields

        /// <summary>
        /// The player of which the name should be displayed.
        /// </summary>
        public Player Player;

        /// <summary>
        /// The UI text where the name should be dispalyed.
        /// </summary>
        public Text PlayerNameText;

        #endregion

        #region Behavior Methods

        /// <summary>
        /// Init of this code.
        /// </summary>
        void Start()
        {
            this.PlayerNameText.text = this.Player.PlayerName;
        }

        #endregion
    }
}
