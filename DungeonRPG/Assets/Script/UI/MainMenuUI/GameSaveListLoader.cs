using DungeonRPG.FileManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.IO;

namespace DungeonRPG.UI.MainMenu
{
    /// <summary>
    /// Behavior class which is responsible for loading all available saves into the list
    /// of saves in the main menu of the game.
    /// </summary>
    /// <author>
    /// Copyright (c) 2018 by Johannes Berndorfer (berndoJ)
    /// </author>
    public class GameSaveListLoader : MonoBehaviour
    {
        #region Fields

        /// <summary>
        /// The object to which all the buttons / list items will get added to. (as children)
        /// </summary>
        [Tooltip("The object to which all the buttons / list items will get added to. (as children)")]
        public GameObject ListParentObj;

        /// <summary>
        /// The prefab object for a list item representing the game save. (Button)
        /// </summary>
        [Tooltip("The prefab object for a list item representing the game save. (Button)")]
        public GameObject LevelSaveItemPrefab;

        #endregion

        #region Behavior Methods

        /// <summary>
        /// Init of this code.
        /// </summary>
        private void Awake()
        {
            this.ReloadGameSaveList();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Reloads the list of available game saves to load.
        /// </summary>
        public void ReloadGameSaveList()
        {
            // Clear all children of the parent object.
            foreach (Transform childTransform in this.ListParentObj.GetComponentInChildren<Transform>())
                GameObject.Destroy(childTransform.gameObject);
            // Get the new children
            string[] saveFolderContentFiles = Directory.GetFiles(GameSaveManager.SaveFolderPath);
            List<string> saveIDs = new List<string>();
            foreach (string potSaveFile in saveFolderContentFiles)
            {
                try
                {
                    FileInfo fileInfo = new FileInfo(potSaveFile);
                    if (fileInfo.Extension == ".sda")
                        saveIDs.Add(fileInfo.Name.Substring(0, fileInfo.Name.Length - fileInfo.Extension.Length));
                }
                catch (Exception e)
                {
                    Debug.Log("An error occured while reloading the game save list in GameSaveListLoader.cs: " + e.Message);
                    return;
                }
            }
            // Create new list objects.
            foreach (string saveID in saveIDs)
            {
                GameObject createdListItemObj = Instantiate(this.LevelSaveItemPrefab, this.ListParentObj.transform);
                createdListItemObj.GetComponent<GameSaveButtonLoader>().LevelUID = saveID;
            }
        }

        #endregion
    }
}
