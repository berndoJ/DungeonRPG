using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;


namespace DungeonRPG.FileManagement
{
    public static class GameSaveManager
    {
        /// <summary>
        /// Saves a given save game info with the given save UID.
        /// </summary>
        /// <param name="saveUID">The save UID to save the save as.</param>
        /// <param name="gameSaveInfo">The game save info.</param>
        public static void SaveGameInfo(string saveUID, GameSaveInfo gameSaveInfo)
        {
            string saveFolderPath = Application.persistentDataPath + "/dat/sda";
            Directory.CreateDirectory(saveFolderPath);
            BinaryFormatter binFormatter = new BinaryFormatter();
            using (FileStream saveFs = new FileStream(Application.persistentDataPath + "/dat/sda/" + saveUID + ".sda", FileMode.Create))
            {
                binFormatter.Serialize(saveFs, gameSaveInfo);
            }
        }

        /// <summary>
        /// Loads a saved game info with the given saveUID.
        /// </summary>
        /// <param name="saveUID">The save UID to load the save of.</param>
        /// <returns>The game save.</returns>
        public static GameSaveInfo LoadGameInfo(string saveUID)
        {
            string savePath = Application.persistentDataPath + "/dat/sda/" + saveUID + ".sda";
            if (!File.Exists(savePath))
            {
                Debug.LogError("Error while loading '" + savePath + "': File does not exist.");
                return null;
            }
            BinaryFormatter binFormatter = new BinaryFormatter();
            GameSaveInfo gameSaveInfo = null;
            using (FileStream loadFs = new FileStream(savePath, FileMode.Open))
            {
                gameSaveInfo = binFormatter.Deserialize(loadFs) as GameSaveInfo;
            }
            return gameSaveInfo;
        }

        /// <summary>
        /// Gets all avialiable save UIDs.
        /// </summary>
        /// <returns>All available save UIDs</returns>
        public static List<string> GetAllSaveUIDs()
        {
            string saveFolderPath = Application.persistentDataPath + "/dat/sda";
            if (!Directory.Exists(saveFolderPath))
            {
                return new List<string>();
            }
            string[] filesInSaveFolder = Directory.GetFiles(Application.persistentDataPath + "/dat/sda");
            List<string> saveUIDs = new List<string>();
            foreach (string saveFilePath in filesInSaveFolder)
            {
                try
                {
                    FileInfo saveFileInfo = new FileInfo(saveFilePath);
                    if (saveFileInfo.Extension == ".sda")
                    {
                        saveUIDs.Add(saveFileInfo.Name);
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogWarning("Exception in GetAllSaveUIDs(): (" + ex.GetType().ToString() + ") " + ex.Message);
                }
            }
            return saveUIDs;
        }
    }
}
