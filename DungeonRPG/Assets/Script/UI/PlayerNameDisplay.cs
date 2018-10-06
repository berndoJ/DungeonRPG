using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DungeonRPG.Entity;

namespace DungeonRPG.UI
{
    public class PlayerNameDisplay : MonoBehaviour
    {
        /// <summary>
        /// The player of which the name should be displayed.
        /// </summary>
        public Player Player;

        /// <summary>
        /// The UI text where the name should be dispalyed.
        /// </summary>
        public Text PlayerNameText;

        /// <summary>
        /// Init of this code.
        /// </summary>
        void Start()
        {
            this.PlayerNameText.text = this.Player.PlayerName;
        }
    }
}
