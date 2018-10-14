using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DungeonRPG.UI
{
    public class F2EasterEgg : MonoBehaviour
    {
        /// <summary>
        /// Input enable / disable value.
        /// </summary>
        public static bool INPUT_ENABLED = true;

        /// <summary>
        /// Text of the easter egg.
        /// </summary>
        public Text F2EasterEggText;

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
    }
}
