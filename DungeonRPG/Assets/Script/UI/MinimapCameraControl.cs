﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace DungeonRPG.UI
{
    /// <summary>
    /// Behavior class which controls the minimap camera. (Movement and abstract rendering)
    /// </summary>
    /// <remarks>
    /// Copyright (c) 2018 by Johannes Berndorfer (berndoJ)
    /// </remarks>
    public class MinimapCameraControl : MonoBehaviour
    {
        #region Static Fields

        /// <summary>
        /// Input enable / disable value.
        /// </summary>
        public static bool INPUT_ENABLED = true;

        #endregion

        #region Fields

        /// <summary>
        /// The minimap camera.
        /// </summary>
        [Tooltip("The minimap camera.")]
        public Camera MinimapCamera;

        /// <summary>
        /// The render object of the minimap.
        /// </summary>
        [Tooltip("The render object of the minimap.")]
        public GameObject MinimapRenderObject;

        /// <summary>
        /// The step size of zooming the camera.
        /// Default: 0.5F
        /// </summary>
        [Tooltip("The step size of zooming the camera.")]
        public float StepSize = 0.5F;

        /// <summary>
        /// The upper zoom border of the camera.
        /// </summary>
        [Tooltip("The upper zoom border of the camera.")]
        public float UpperZoomBorder = 15F;

        /// <summary>
        /// The lower zoom border of the camera.
        /// </summary>
        [Tooltip("The lower zoom border of the camera.")]
        public float LowerZoomBorder = 3F;

        #endregion

        #region Behavior Methods

        /// <summary>
        /// Frame loop.
        /// </summary>
        private void Update()
        {
            if (INPUT_ENABLED)
            {
                // Minimap zoom
                if (Input.GetButtonDown("Minimap Zoom In"))
                    this.MinimapCamera.orthographicSize -= this.StepSize;
                if (Input.GetButtonDown("Minimap Zoom Out"))
                    this.MinimapCamera.orthographicSize += this.StepSize;
                if (this.MinimapCamera.orthographicSize > this.UpperZoomBorder)
                    this.MinimapCamera.orthographicSize = this.UpperZoomBorder;
                if (this.MinimapCamera.orthographicSize < this.LowerZoomBorder)
                    this.MinimapCamera.orthographicSize = this.LowerZoomBorder;
                // Minimap toggle
                if (Input.GetButtonDown("Minimap Toggle"))
                {
                    this.MinimapRenderObject.SetActive(!this.MinimapRenderObject.activeSelf);
                    this.MinimapCamera.enabled = !this.MinimapCamera.enabled;
                }
            }
        }

        #endregion
    }
}
