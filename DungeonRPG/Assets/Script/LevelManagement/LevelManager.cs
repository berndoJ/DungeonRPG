using DungeonRPG.Entities;
using DungeonRPG.FileManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DungeonRPG.LevelManagement
{
    /// <summary>
    /// Management class which manages all the levels of the game. It can load a level,
    /// get the save data of the current level. It is responsible for all level related
    /// management of the game.
    /// </summary>
    /// <remarks>
    /// Coypright (c) 2018 by Johannes Berndorfer (berndoJ)
    /// </remarks>
    public static class LevelManager
    {
        #region Static Fields

        /// <summary>
        /// The registry list of levels for the game.
        /// </summary>
        private static List<GameLevelInfo> sGameLevelRegistryList;

        /// <summary>
        /// The game save info to load on level scene change.
        /// </summary>
        public static GameSaveInfo GameSaveInfoLoadQueue = null;

        #endregion

        #region Static Methods

        /// <summary>
        /// Gets a copied game level registry list.
        /// </summary>
        /// <returns>The copy of the game level registry list.</returns>
        public static List<GameLevelInfo> GetGameLevelRegistryListCopy()
        {
            return new List<GameLevelInfo>(sGameLevelRegistryList);
        }

        /// <summary>
        /// Registers a new level with the given <see cref="GameLevelInfo"/> data structure.
        /// </summary>
        /// <param name="levelInfo">The level info</param>
        /// <returns>The id of the level.</returns>
        public static int RegisterLevel(GameLevelInfo levelInfo)
        {
            sGameLevelRegistryList.Add(levelInfo);
            return sGameLevelRegistryList.Count - 1;
        }

        /// <summary>
        /// Loads a level by the given level id.
        /// </summary>
        /// <param name="levelId">The level id.</param>
        public static void LoadLevel(int levelId)
        {
            if (levelId >= 0 && levelId < sGameLevelRegistryList.Count)
            {
                LoadLevel(sGameLevelRegistryList[levelId].LevelSceneName);
            }
            else return;
        }

        /// <summary>
        /// Loads a level by the given level scene name.
        /// </summary>
        /// <param name="levelSceneName">The level's scene name.</param>
        public static void LoadLevel(string levelSceneName)
        {
            List<GameLevelInfo> foundGLI = sGameLevelRegistryList.Where(gli => gli.LevelSceneName == levelSceneName).ToList();
            if (foundGLI.Count < 1)
                return;
            GameLevelInfo gliToLoad = foundGLI[0];
            LoadLevelInternal(gliToLoad);
        }

        /// <summary>
        /// Internal function for loading a level.
        /// </summary>
        /// <param name="gli">The game level info</param>
        internal static void LoadLevelInternal(GameLevelInfo gli)
        {
            SceneManager.LoadScene(gli.LevelSceneName);
        }

        /// <summary>
        /// Loads a saved level.
        /// </summary>
        /// <param name="gsi">The game save info to load.</param>
        public static void LoadSavedLevel(GameSaveInfo gsi)
        {
            GameSaveInfoLoadQueue = gsi;
            LoadLevel(gsi.CurrentLevelUID);
        }        

        #endregion

        #region Static Constructor & Scene Changed Code

        /// <summary>
        /// Static constructor.
        /// </summary>
        static LevelManager()
        {
            SceneManager.activeSceneChanged += OnSceneChanged;
        }

        /// <summary>
        /// Event delegate for the on scene changed event.
        /// </summary>
        private static void OnSceneChanged(Scene oldScene, Scene newScene)
        {
            if (GameSaveInfoLoadQueue != null)
            {
                if (newScene.name == GameSaveInfoLoadQueue.CurrentLevelUID) // Check if the info to be loaded belongs to the newly loaded scene.
                {
                    // Load all scene parts.
                    Player scenePlayer = GameObject.FindGameObjectsWithTag("Player").Where(go => go.GetComponent<Player>() != null).Select(go => go.GetComponent<Player>()).FirstOrDefault();
                    GameSaveInfoLoadQueue.Player.ApplyToPlayer(ref scenePlayer);
                    GameSaveInfoLoadQueue = null;
                }
            }
        }

        #endregion
    }
}
