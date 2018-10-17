using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace DungeonRPG.ItemContainer
{
    public class HotbarOverlayRenderer : MonoBehaviour
    {
        /// <summary>
        /// Prmary / main image of the solt overlay.
        /// </summary>
        [Header("Overlay Images")]
        [Tooltip("Prmary / main image of the solt overlay.")]
        public Image MainOverlayImage;

        /// <summary>
        /// Secondary image of the solt overlay.
        /// </summary>
        [Tooltip("Secondary image of the solt overlay.")]
        public Image SecondaryOverlayImage;

        /// <summary>
        /// Enables / disables the main overlay image.
        /// </summary>
        /// <param name="value">The new value</param>
        public void SetEnableOverlayMain(bool value)
        {
            this.MainOverlayImage.enabled = value;
        }

        /// <summary>
        /// Enables / disables the secondary overlay image.
        /// </summary>
        /// <param name="value">The new value.</param>
        public void SetEnableOverlaySecondary(bool value)
        {
            this.SecondaryOverlayImage.enabled = value;
        }
    }
}
