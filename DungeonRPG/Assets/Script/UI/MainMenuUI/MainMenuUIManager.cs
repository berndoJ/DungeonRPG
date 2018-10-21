using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DungeonRPG.UI.MainMenu
{
    public class MainMenuUIManager : MonoBehaviour
    {
        #region Button Click Handlers

        /// <summary>
        /// Gets invoked when the button "New Game" got clicked.
        /// </summary>
        public void OnNewGameButtonClick()
        {
            SceneManager.LoadScene("Level0");
        }

        #endregion
    }
}
