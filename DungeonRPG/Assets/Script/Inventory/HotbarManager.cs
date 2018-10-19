using DungeonRPG.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DungeonRPG.ItemContainer
{
    public class HotbarManager : MonoBehaviour
    {
        #region Static Fields

        /// <summary>
        /// Input enable / disable value.
        /// </summary>
        public static bool INPUT_ENABLED = true;

        #endregion

        /// <summary>
        /// Count of hotbar slots.
        /// </summary>
        [SerializeField]
        [Tooltip("Count of hotbar slots.")]
        private int mHotbarSlots;

        /// <summary>
        /// Count of hotbar slots.
        /// </summary>
        [SerializeField]
        [Tooltip("The hotbar overlay GUI manager.")]
        private HotbarOverlayGUIManager mOverlayManager;

        /// <summary>
        /// The inventory of which the overlay belongs to.
        /// </summary>
        [Tooltip("The inventory of which the overlay belongs to.")]
        public Inventory OverlayInventory;

        /// <summary>
        /// The offset of the hotbar in the inventory.
        /// </summary>
        [Tooltip("The offset of the hotbar in the inventory.")]
        public int HotbarInvIndexOffset;

        /// <summary>
        /// The main selected hotbar index.
        /// </summary>
        public int MainHotbarIndex
        {
            get
            {
                return this.mMainHotbarIndex;
            }
            set
            {
                if (value < 0)
                    value = this.mHotbarSlots - 1;
                if (value >= this.mHotbarSlots)
                    value = 0;
                this.mMainHotbarIndex = value;
                this.mOverlayManager.SetMainSelectedIndex(value);
            }
        }
        private int mMainHotbarIndex;

        /// <summary>
        /// The secondary selected hotbar index.
        /// </summary>
        public int SecondaryHotbarIndex
        {
            get
            {
                return this.mSecondaryHotbarIndex;
            }
            set
            {
                if (value < 0)
                    value = this.mHotbarSlots - 1;
                if (value >= this.mHotbarSlots)
                    value = 0;
                this.mSecondaryHotbarIndex = value;
                this.mOverlayManager.SetSecondarySelectedIndex(value);
            }
        }
        private int mSecondaryHotbarIndex;

        #region Behavior Methods

        /// <summary>
        /// Init of this code.
        /// </summary>
        private void Awake()
        {
            this.MainHotbarIndex = 0;
            this.SecondaryHotbarIndex = 0;
        }

        /// <summary>
        /// Frame loop.
        /// </summary>
        private void Update()
        {
            if (INPUT_ENABLED)
            {
                float scrollAxis = Input.GetAxis("Mouse ScrollWheel");
                bool ctrlPressed = Input.GetKey(KeyCode.LeftControl);
                if (scrollAxis > 0F) // Scroll up.
                {
                    if (!ctrlPressed)
                        this.MainHotbarIndex--;
                    else
                        this.SecondaryHotbarIndex--;
                }
                else if (scrollAxis < 0F) // Scroll down.
                {
                    if (!ctrlPressed)
                        this.MainHotbarIndex++;
                    else
                        this.SecondaryHotbarIndex++;
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the main held itemstack.
        /// </summary>
        /// <returns>The main held itemstack.</returns>
        public ItemStack GetMainHeldItemStack()
        {
            return null;
        }

        /// <summary>
        /// Gets the secondary held itemstack.
        /// </summary>
        /// <returns>The secondary held itemstack.</returns>
        public ItemStack GetSecondaryHeldItemStack()
        {
            return null;
        }

        #endregion
    }
}
