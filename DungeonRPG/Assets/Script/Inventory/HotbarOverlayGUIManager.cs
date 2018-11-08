using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DungeonRPG.ItemContainer
{
    /// <summary>
    /// Behavior class which manages the selection overlay on the
    /// hotbar.
    /// </summary>
    /// <remarks>
    /// Copyright (c) 2018 by Johannes Berndorfer (berndoJ)
    /// </remarks>
    public class HotbarOverlayGUIManager : MonoBehaviour
    {
        #region Fields

        /// <summary>
        /// List of all hotbar renderers.
        /// </summary>
        [Tooltip("List of all hotbar renderers.")]
        public HotbarOverlayRenderer[] HotbarOverlayRenderers;

        #endregion

        #region Methods

        /// <summary>
        /// Sets the new main selected hotbar index.
        /// </summary>
        /// <param name="index">The new index.</param>
        public void SetMainSelectedIndex(int index)
        {
            if (index >= 0 && index < this.HotbarOverlayRenderers.Length)
            {
                foreach (HotbarOverlayRenderer renderer in this.HotbarOverlayRenderers)
                    renderer.SetEnableOverlayMain(false);
                this.HotbarOverlayRenderers[index].SetEnableOverlayMain(true);
            }
        }

        /// <summary>
        /// Sets the new secondary selected hotbar index.
        /// </summary>
        /// <param name="index">The new index.</param>
        public void SetSecondarySelectedIndex(int index)
        {
            if (index >= 0 && index < this.HotbarOverlayRenderers.Length)
            {
                foreach (HotbarOverlayRenderer renderer in this.HotbarOverlayRenderers)
                    renderer.SetEnableOverlaySecondary(false);
                this.HotbarOverlayRenderers[index].SetEnableOverlaySecondary(true);
            }
        }

        #endregion
    }
}
