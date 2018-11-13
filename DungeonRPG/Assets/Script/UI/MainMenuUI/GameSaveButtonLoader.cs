using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using DungeonRPG.FileManagement;

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

        /// <summary>
        /// The private field for the <see cref="LevelUID"/> property.
        /// </summary>
        private string mLevelUID;

        #endregion

        #region Properties

        /// <summary>
        /// The level's uid string which this button corresponds to.
        /// This string should be set when creating the button.
        /// Setting the text updates the button automatically from the saves.
        /// </summary>
        public string LevelUID
        {
            get
            {
                return this.mLevelUID;
            }
            set
            {
                // Get the file path
                string saveFilePath = GameSaveManager.SaveFolderPath + "/" + value + ".sda";
                // Check for existance
                if (!File.Exists(saveFilePath))
                    return;
                try
                {
                    // Get the file info and update the dates.
                    FileInfo saveFileInfo = new FileInfo(GameSaveManager.SaveFolderPath + "/" + value + ".sda");
                    this.SaveTimeCreatedText.text = "Created: " + saveFileInfo.CreationTime.ToShortDateString();
                    this.SaveTimeLastModifiedText.text = "Last accessed: " + saveFileInfo.LastAccessTime.ToShortDateString();
                    // Update the title.
                    this.SaveTitleText.text = value;
                }
                catch (Exception e)
                {
                    Debug.Log("Error while loading FileInfo of save " + saveFilePath + ". Exception: " + e.Message);
                    this.SaveTimeCreatedText.text = "Created: -";
                    this.SaveTimeLastModifiedText.text = "Last accessed: -";
                }
                // Update the private field
                this.mLevelUID = value;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets called when the button to load the game has been clicked.
        /// </summary>
        public void LoadButtonClick()
        {
            GameSaveManager.LoadGame(this.LevelUID);
        }

        #endregion

    }
}
