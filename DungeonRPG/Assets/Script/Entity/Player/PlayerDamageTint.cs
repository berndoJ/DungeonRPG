using DungeonRPG.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG.Entities
{
    public class PlayerDamageTint : MonoBehaviour
    {
        /// <summary>
        /// The sprite renderer of the player.
        /// </summary>
        public SpriteRenderer PlayerRenderer;

        /// <summary>
        /// The player.
        /// </summary>
        public Player Player;

        /// <summary>
        /// The color of the player to be tinted to.
        /// </summary>
        public Color TintColor = new Color(255F, 144F, 144F);

        /// <summary>
        /// Bool indicating if damage tint should be shown.
        /// </summary>
        private bool mShowTint
        {
            get { return this.m_mShowTint; }
            set
            {
                this.m_mShowTint = value;
                if (!this.mIsShowingTint)
                    this.StartCoroutine("ShowDamageTint");
            }
        }
        private bool m_mShowTint;
        private bool mIsShowingTint;

        /// <summary>
        /// Init of this code.
        /// </summary>
        public void Start()
        {
            this.Player.OnEntityHealthChanged += this.OnEntityHealthChanged;
        }

        /// <summary>
        /// Gets invoked when the player's health changes.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event args.</param>
        public void OnEntityHealthChanged(object sender, EntityHealthChangedEventArgs e)
        {
            if (e.HealthChangeType == EntityHealthChangedType.LOST_HEALTH)
            {
                this.mShowTint = true;
            }
        }

        /// <summary>
        /// Coroutine showing the damage tint.
        /// </summary>
        /// <returns>-</returns>
        public IEnumerator ShowDamageTint()
        {
            this.mIsShowingTint = true;
            do
            {
                this.PlayerRenderer.color = this.TintColor;
                this.mShowTint = false;
                yield return new WaitForSeconds(0.2F);
            } while (this.mShowTint);
            this.PlayerRenderer.color = Color.white;
            this.mIsShowingTint = false;
        }

    }
}
