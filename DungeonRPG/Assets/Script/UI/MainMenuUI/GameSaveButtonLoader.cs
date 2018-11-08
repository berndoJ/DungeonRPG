using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace DungeonRPG.UI.MainMenu
{
    /// <summary>
    /// Behavior class which gets attached to the button which is used
    /// to load a save game in the main menu.
    /// </summary>
    /// <remarks>
    /// Copyright (c) 2018 by Johannes Berndorfer (berndoJ)
    /// </remarks>
    public class GameSaveButtonLoader : MonoBehaviour
    {
        #region Fields

        /// <summary>
        /// The text object which displays the game save title.
        /// </summary>
        public Text SaveTitleText;

        /// <summary>
        /// The text object which displays the time the game save was last modified.
        /// </summary>
        public Text SaveTimeLastModifiedText;

        /// <summary>
        /// The text object which displays the time the game save was created.
        /// </summary>
        public Text SaveTimeCreatedText;

        #endregion

        #region Methods

        /// <summary>
        /// Gets called when the button to load the game has been clicked.
        /// </summary>
        public void LoadButtonClick()
        {

        }

        #endregion

    }
}
