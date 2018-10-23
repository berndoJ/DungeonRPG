using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace DungeonRPG.UI.MainMenu
{
    public class NewGameMenuUIManager : MonoBehaviour
    {
        /// <summary>
        /// The input field of the name.
        /// </summary>
        [Tooltip("The input field of the name.")]
        public Text GameNameInputField;

        #region Button Click Handlers

        public void CreateGameButtonClick()
        {
            string gameName = this.GameNameInputField.text;
            if (!LevelNameTextValidator.FullyVerifyGameName(gameName))
                return;

        }

        #endregion
    }
}
