using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

using DungeonRPG.FileManagement;

namespace DungeonRPG.UI.MainMenu
{
    /// <summary>
    /// Behavior class which manages the NewGameMenu GUI.
    /// </summary>
    /// <remarks>
    /// Copyright (c) 2018 by Johannes Berndorfer (berndoJ)
    /// </remarks>
    public class NewGameMenuUIManager : MonoBehaviour
    {
        #region Fields

        /// <summary>
        /// The input field of the name.
        /// </summary>
        [Tooltip("The input field of the name.")]
        public InputField GameNameInputField;

        #endregion

        #region Button Click Handlers

        /// <summary>
        /// Gets invoked when the create game button gets clicked.
        /// </summary>
        public void CreateGameButtonClick()
        {
            string gameName = this.GameNameInputField.text;
            if (!LevelNameTextValidator.FullyVerifyGameName(gameName))
                return;
            Debug.Log("Creating new Game: " + gameName);
            GameSaveManager.LoadNewGame(gameName);
        }

        #endregion
    }
}
