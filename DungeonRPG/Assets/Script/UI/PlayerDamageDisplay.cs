using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DungeonRPG.Entities;
using DungeonRPG.Event;

namespace DungeonRPG.UI
{
    /// <summary>
    /// Behavior class which controls the player damage display text.
    /// </summary>
    /// <remarks>
    /// Copyright (c) 2018 by Johannes Berndorfer (berndoJ)
    /// </remarks>
    public class PlayerDamageDisplay : MonoBehaviour
    {
        #region Fields

        /// <summary>
        /// The player object reference.
        /// </summary>
        public Player Player;

        /// <summary>
        /// The text object to display the text on.
        /// </summary>
        public Text DamageDisplayText;

        #endregion

        #region Properties

        /// <summary>
        /// Integer containing the damage amount to be displayed on screen.
        /// </summary>
        private int mDamageToDisplay
        {
            get { return this.m_mDamageToDisplay; }
            set
            {
                this.m_mDamageToDisplay = value;
                if (!this.DamageDisplayText.enabled)
                {
                    this.StartCoroutine("DisplayDamageCoroutine");
                }
            }
        }
        private int m_mDamageToDisplay;

        #endregion

        #region Behavior Methods

        /// <summary>
        /// Init of this code.
        /// </summary>
        private void Awake()
        {
            this.Player.OnEntityHealthChanged += this.OnEntityHealthChanged;
            this.DamageDisplayText.enabled = false;
        }

        #endregion

        #region Event Delegates

        /// <summary>
        /// Gets invoked when the player takes damage.
        /// </summary>
        /// <param name="e">The event arguments</param>
        public void OnEntityHealthChanged(object sender, EntityHealthChangedEventArgs e)
        {
            if (e.HealthChangeType == EntityHealthChangedType.LOST_HEALTH)
            {
                this.mDamageToDisplay += Mathf.Abs(e.HealthChangeDelta);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Coroutine that handles the display and stacking of the damage values.
        /// </summary>
        /// <returns>-</returns>
        public IEnumerator DisplayDamageCoroutine()
        {
            this.DamageDisplayText.enabled = true;
            int damageDisplayed = this.mDamageToDisplay;
            this.DisplayDamage(damageDisplayed);
            int counter = 0;
            while (true)
            {
                if (counter == 0)
                {
                    this.DamageDisplayText.CrossFadeAlpha(0F, 0.5F, false);
                }
                counter++;
                if (this.mDamageToDisplay != damageDisplayed)
                {
                    counter = 0;
                    damageDisplayed = this.mDamageToDisplay;
                    this.DisplayDamage(damageDisplayed);
                }
                if (counter >= 5)
                {
                    break;
                }
                yield return new WaitForSeconds(0.1F);
            }
            this.mDamageToDisplay = 0;
            this.DamageDisplayText.enabled = false;
        }

        /// <summary>
        /// Displays the damage given on screen (text)
        /// </summary>
        /// <param name="damage">The damage amount (positive) to display.</param>
        public void DisplayDamage(int damage)
        {
            this.DamageDisplayText.CrossFadeAlpha(1F, 0F, false);
            this.DamageDisplayText.text = string.Format("-{0}", damage);
        }

        #endregion
    }
}