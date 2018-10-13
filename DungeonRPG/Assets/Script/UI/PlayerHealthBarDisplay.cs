using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DungeonRPG.Entities;
using DungeonRPG.Event;

namespace DungeonRPG.UI
{
    public class PlayerHealthBarDisplay : MonoBehaviour
    {
        /// <summary>
        /// The player of which the health should be displayed.
        /// </summary>
        public Player Player;

        /// <summary>
        /// The image to fill according to the health of the player.
        /// </summary>
        public Image HealthBarImage;

        /// <summary>
        /// Init of this code.
        /// </summary>
        private void Awake()
        {
            this.Player.OnEntityHealthChanged += this.OnEntityHealthChanged;
        }

        /// <summary>
        /// Gets invoked when the health of the player attached changed.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event args</param>
        private void OnEntityHealthChanged(object sender, EntityHealthChangedEventArgs e)
        {
            this.HealthBarImage.fillAmount = (float)e.NewHealth / this.Player.MaxHealth;
        }
    }
}
