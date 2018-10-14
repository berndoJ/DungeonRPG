using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using DungeonRPG.InputUtils;

namespace DungeonRPG.UI
{
    public class InventoryToggle : MonoBehaviour
    {
        /// <summary>
        /// Input enable / disable value.
        /// </summary>
        public static bool INPUT_ENABLED = true;

        /// <summary>
        /// The inventory object to toggle.
        /// </summary>
        [Header("Inventory Elements")]
        [Tooltip("The inventory object to toggle.")]
        public GameObject InventoryOpenObj;

        #region Behavior Methods

        /// <summary>
        /// Init of this code.
        /// </summary>
        private void Awake()
        {
            this.InventoryOpenObj.SetActive(false);
        }

        /// <summary>
        /// Frame loop.
        /// </summary>
        private void Update()
        {
            if (INPUT_ENABLED && Input.GetButtonDown("Inventory Toggle"))
            {
                this.InventoryOpenObj.SetActive(!this.InventoryOpenObj.activeInHierarchy);
                if (this.InventoryOpenObj.activeInHierarchy)
                {
                    InputManager.DisableInputState(GameInputClass.PLAYER_AI);
                }
                else
                {
                    InputManager.EnableInputState(GameInputClass.PLAYER_AI);
                }
            }
        }

        #endregion
    }
}
