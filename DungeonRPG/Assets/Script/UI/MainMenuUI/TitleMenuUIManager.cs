using DungeonRPG.CommandEnvironment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DungeonRPG.UI.MainMenu
{
    /// <summary>
    /// Behvaior class which manages the TitleMenu GUI.
    /// </summary>
    /// <remarks>
    /// Copyright (c) 2018 by Johannes Berndorfer (berndoJ)
    /// </remarks>
    public class TitleMenuUIManager : MonoBehaviour
    {
        #region Button Click Handlers
        
        /// <summary>
        /// Gets invoked when the quit button has been clicked.
        /// </summary>
        public void QuitButtonClick()
        {
            Debug.Log("Quitting game.");
            Application.Quit();
        }

        #endregion
    }
}
