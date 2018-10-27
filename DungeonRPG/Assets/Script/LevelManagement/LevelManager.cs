using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

using DungeonRPG.Entities;
using DungeonRPG.FileManagement;
using System.Collections;

namespace DungeonRPG.LevelManagement
{
    //public class LevelManager : MonoBehaviour
    //{
    //    /// <summary>
    //    /// Static helper field.
    //    /// </summary>
    //    private static GameSaveInfo GameSaveInfoToLoad = null;
        
    //    /// <summary>
    //    /// Gets the current save info of the level.
    //    /// </summary>
    //    /// <returns>The current save info.</returns>
    //    public GameSaveInfo GetSaveInfo()
    //    {
    //        string currentSceneUID = SceneManager.GetActiveScene().name;
    //        //List<Entity> sceneEntities = GameObject.FindGameObjectsWithTag("Entity").Where(go => go.GetComponent<Entity>() != null).Select(go => go.GetComponent<Entity>()).ToList();
    //        Player scenePlayer = GameObject.FindGameObjectsWithTag("Player").Where(go => go.GetComponent<Player>() != null).Select(go => go.GetComponent<Player>()).FirstOrDefault();
    //        if (scenePlayer == null)
    //        {
    //            Debug.LogError("GetSaveInfo(): Did not find a player object.");
    //            return null;
    //        }
    //        SerializedPlayer scenePlayerSer = SerializedPlayer.NewFromPlayer(scenePlayer);
    //        return new GameSaveInfo(currentSceneUID, scenePlayerSer);
    //    }

    //    /// <summary>
    //    /// Loads from a save info.
    //    /// </summary>
    //    /// <param name="gameSaveInfo">The save info to load from.</param>
    //    public void LoadGameSaveInfo(GameSaveInfo gameSaveInfo)
    //    {
    //        GameSaveInfoToLoad = gameSaveInfo;
    //        SceneManager.LoadScene(gameSaveInfo.CurrentLevelUID);
    //    }

    //    /// <summary>
    //    /// Handler for the activeSceneChanged event in SceneManager.
    //    /// </summary>
    //    /// <param name="current">The current scene</param>
    //    /// <param name="next">The next scene</param>
    //    private void OnActiveSceneChanged(Scene current, Scene next)
    //    {
    //        if (GameSaveInfoToLoad != null)
    //        {
    //            Player scenePlayer = GameObject.FindGameObjectsWithTag("Player").Where(go => go.GetComponent<Player>() != null).Select(go => go.GetComponent<Player>()).FirstOrDefault();
    //            GameSaveInfoToLoad.Player.ApplyToPlayer(ref scenePlayer);
    //            GameSaveInfoToLoad = null;
    //        }
    //    }

    //    /// <summary>
    //    /// Init of this code.
    //    /// </summary>
    //    private void Awake()
    //    {
    //        SceneManager.activeSceneChanged += OnActiveSceneChanged;
    //    }

    //    private void Update()
    //    {
    //        if (Input.GetButtonDown("Whats here"))
    //        {
    //            GameSaveManager.SaveGameInfo("tst_save", this.GetSaveInfo());
    //        }

    //        if (Input.GetButtonDown("FPS Toggle"))
    //        {
    //            this.LoadGameSaveInfo(GameSaveManager.LoadGameInfo("tst_save"));
    //        }
    //    }
    //}
}
