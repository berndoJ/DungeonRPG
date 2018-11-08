using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DungeonRPG.UI
{
    /// <summary>
    /// Behavior class which controls the F2 easter egg text.
    /// "Maxi ist cool!"
    /// </summary>
    /// <remarks>
    /// Copyright (c) 2018 by Johannes Berndorfer (berndoJ)
    /// </remarks>
    public class F2EasterEgg : MonoBehaviour
    {
        #region Static Fields

        /// <summary>
        /// Input enable / disable value.
        /// </summary>
        public static bool INPUT_ENABLED = true;

        #endregion

        #region Fields

        /// <summary>
        /// Text of the easter egg.
        /// </summary>
        public Text F2EasterEggText;

        #endregion

        #region Behavior Methods

        /// <summary>
        /// Init of this code.
        /// </summary>
        private void Awake()
        {
            this.F2EasterEggText.enabled = false;
        }

        /// <summary>
        /// Frame loop.
        /// </summary>
        private void Update()
        {
            if (INPUT_ENABLED && Input.GetButtonDown("Whats here"))
            {
                this.F2EasterEggText.enabled = !this.F2EasterEggText.enabled;
            }
        }

        #endregion
    }
}
