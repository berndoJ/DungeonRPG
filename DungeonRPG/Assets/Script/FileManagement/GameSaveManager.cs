using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

using DungeonRPG.Entities;
using DungeonRPG.LevelManagement;

namespace DungeonRPG.FileManagement
{
    /// <summary>
    /// A static game management class which contains helper and management
    /// functions used to save and load a game save.
    /// </summary>
    /// <remarks>
    /// Coypright (c) 2018 by Johannes Berndorfer (berndoJ)
    /// </remarks>
    public static class GameSaveManager
    {
        #region Constants

        /// <summary>
        /// The initial level id.
        /// </summary>
        public static readonly string InitialLevelId = "Level0";

        /// <summary>
        /// The default path to save the games in.
        /// </summary>
        public static readonly string SaveFolderPath = Application.persistentDataPath + "/dat/sda";

        #endregion

        #region Static Fields

        /// <summary>
        /// The uid of the current game
        /// </summary>
        public static string CurrentGameUID;

        /// <summary>
        /// The save info to load on scene change.
        /// </summary>
        private static GameSaveInfo GameSaveInfoToLoadOnSceneChange = null;

        /// <summary>
        /// Value indicating if the game should be saved on the next scene change.
        /// </summary>
        private static bool SaveGameOnSceneChange = false;

        #endregion

        #region Static Methods

        /// <summary>
        /// Saves a given save game info with the given save UID.
        /// </summary>
        /// <param name="saveUID">The save UID to save the save as.</param>
        /// <param name="gameSaveInfo">The game save info.</param>
        public static void SaveGameInfo(string saveUID, GameSaveInfo gameSaveInfo)
        {;
            Directory.CreateDirectory(SaveFolderPath);
            BinaryFormatter binFormatter = new BinaryFormatter();
            using (FileStream saveFs = new FileStream(SaveFolderPath + "/" + saveUID + ".sda", FileMode.Create))
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
            string savePath = SaveFolderPath + "/" + saveUID + ".sda";
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
            string saveFolderPath = SaveFolderPath;
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
                        saveUIDs.Add(saveFileInfo.Name.Substring(0, saveFileInfo.Name.Length - saveFileInfo.Extension.Length));
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogWarning("Exception in GetAllSaveUIDs(): (" + ex.GetType().ToString() + ") " + ex.Message);
                }
            }
            return saveUIDs;
        }

        /// <summary>
        /// Saves the current game.
        /// </summary>
        public static void SaveCurrentGame()
        {
            if (CurrentGameUID != string.Empty)
            {
                GameSaveInfo gameSaveInfo = GetGameSaveInfoFromCurrent();
                SaveGameInfo(CurrentGameUID, gameSaveInfo);
            }
        }

        /// <summary>
        /// Loads a saved game with the given saveUID.
        /// </summary>
        /// <param name="saveUID"></param>
        public static void LoadGame(string saveUID)
        {
            CurrentGameUID = saveUID;
            GameSaveInfoToLoadOnSceneChange = LoadGameInfo(saveUID);
            SceneManager.LoadScene(GameSaveInfoToLoadOnSceneChange.CurrentLevelUID);
        }

        /// <summary>
        /// Loads a newly created game and saves the new game on start.
        /// </summary>
        /// <param name="newSaveUID">The new save uid.</param>
        public static void LoadNewGame(string newSaveUID)
        {
            CurrentGameUID = newSaveUID;
            SaveGameOnSceneChange = true;
            SceneManager.LoadScene(InitialLevelId);
        }

        /// <summary>
        /// Gets the game save info from the currently loaded scene.
        /// </summary>
        /// <returns>The game save info.</returns>
        public static GameSaveInfo GetGameSaveInfoFromCurrent()
        {
            string currentSceneUID = SceneManager.GetActiveScene().name;
            //List<Entity> sceneEntities = GameObject.FindGameObjectsWithTag("Entity").Where(go => go.GetComponent<Entity>() != null).Select(go => go.GetComponent<Entity>()).ToList();
            Player scenePlayer = GameObject.FindGameObjectsWithTag("Player").Where(go => go.GetComponent<Player>() != null).Select(go => go.GetComponent<Player>()).FirstOrDefault();
            if (scenePlayer == null)
            {
                Debug.LogError("GetSaveInfo(): Did not find a player object.");
                return null;
            }
            SerializedPlayer scenePlayerSer = SerializedPlayer.NewFromPlayer(scenePlayer);
            return new GameSaveInfo(currentSceneUID, scenePlayerSer);
        }

        #endregion

        #region Static Constructor

        /// <summary>
        /// Static constructor of this class.
        /// </summary>
        static GameSaveManager()
        {
            SceneManager.activeSceneChanged += OnActiveSceneChanged;
        }

        #endregion

        #region Static Event Delegates

        /// <summary>
        /// Gets triggered when the active scene got changed.
        /// </summary>
        /// <param name="oldScene">The old scene.</param>
        /// <param name="newScene">The new scene.</param>
        private static void OnActiveSceneChanged(Scene oldScene, Scene newScene)
        {
            if (GameSaveInfoToLoadOnSceneChange != null)
            {
                if (newScene.name == GameSaveInfoToLoadOnSceneChange.CurrentLevelUID) // Check if the info to be loaded belongs to the newly loaded scene.
                {
                    // Load all scene parts.
                    Player scenePlayer = GameObject.FindGameObjectsWithTag("Player").Where(go => go.GetComponent<Player>() != null).Select(go => go.GetComponent<Player>()).FirstOrDefault();
                    GameSaveInfoToLoadOnSceneChange.Player.ApplyToPlayer(ref scenePlayer);
                    GameSaveInfoToLoadOnSceneChange = null;
                }
            }
            if (SaveGameOnSceneChange)
            {
                SaveCurrentGame();
            }
        }

        #endregion
    }
}
