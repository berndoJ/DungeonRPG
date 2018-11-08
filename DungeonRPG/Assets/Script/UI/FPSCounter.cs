using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DungeonRPG.UI
{
    /// <summary>
    /// Behavior class which controls the FPS counter text.
    /// </summary>
    /// <remarks>
    /// Copyright (c) 2018 by Johannes Berndorfer (berndoJ)
    /// </remarks>
    public class FPSCounter : MonoBehaviour
    {
        #region Static Fields

        /// <summary>
        /// Input enable / disable value.
        /// </summary>
        public static bool INPUT_ENABLED = true;

        #endregion

        #region Fields

        /// <summary>
        /// The label the text should be displayed to.
        /// </summary>
        public Text FPSCounterLabel;

        /// <summary>
        /// Enable bool for enabling the display of the text.
        /// </summary>
        public bool Enabled;

        #endregion

        #region Behavior Methods

        /// <summary>
        /// Init of this code.
        /// </summary>
        private void Start()
        {
            this.FPSCounterLabel.enabled = false; // Default value is false : the FPS counter is not shown.
            this.StartCoroutine("UpdateFPSCounter");
        }

        /// <summary>
        /// Frame loop.
        /// Here the frame loop is used for inputs.
        /// </summary>
        private void Update()
        {
            if (INPUT_ENABLED && Input.GetButtonDown("FPS Toggle"))
            {
                this.FPSCounterLabel.enabled = !this.FPSCounterLabel.enabled;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Coroutine for displaying the fps on screen
        /// </summary>
        /// <returns></returns>
        private IEnumerator UpdateFPSCounter()
        {
            while (true)
            {
                if (Enabled)
                    this.FPSCounterLabel.text = string.Format("{0:0.0} FPS", 1 / Time.deltaTime);
                else
                    this.FPSCounterLabel.text = string.Empty;
                yield return new WaitForSeconds(0.5F);
            }
        }

        #endregion
    }
}
