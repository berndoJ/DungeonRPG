using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DungeonRPG.UI
{
    public class ObjectActiveToggle : MonoBehaviour
    {
        /// <summary>
        /// The target of the toggle.
        /// </summary>
        [Tooltip("The target of the toggle.")]
        public GameObject Target;

        /// <summary>
        /// The button that should toggle the activity of the target game object.
        /// </summary>
        [Tooltip("The button that should toggle the activity of the target game object.")]
        public string ToggleButton;

        /// <summary>
        /// The toggle state of the target.
        /// </summary>
        [Tooltip("The toggle state of the target.")]
        public bool Toggled;

        /// <summary>
        /// Init of this code.
        /// </summary>
        private void Awake()
        {
            this.Target.SetActive(this.Toggled);
        }

        /// <summary>
        /// Frame loop.
        /// </summary>
        private void Update()
        {
            if (Input.GetButtonDown(this.ToggleButton))
            {
                this.Toggled = !this.Toggled;
                this.Target.SetActive(this.Toggled);
            }
        }
    }
}
