using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace DungeonRPG.ItemContainer
{
    /// <summary>
    /// Behavior class which is responsible for the abstract GUI rendering of an
    /// inventory slot.
    /// </summary>
    /// <remarks>
    /// Copyright (c) 2018 by Johannes Berndorfer (berndoJ)
    /// </remarks>
    public class InventorySlotRenderer : MonoBehaviour
    {
        #region Fields

        /// <summary>
        /// The image object that displays the item's icon.
        /// </summary>
        [Tooltip("The image object that displays the item's icon.")]
        public Image ItemIconImage;

        /// <summary>
        /// The text object that displays the item's count.
        /// </summary>
        [Tooltip("The text object that displays the item's count.")]
        public Text ItemDisplayNameText;

        /// <summary>
        /// The parent inventory.
        /// </summary>
        [NonSerialized]
        public Inventory CorrespondingInventory;

        /// <summary>
        /// The slot ID in the parent inventory.
        /// </summary>
        [NonSerialized]
        public int CorrespondingInventorySlotID;

        #endregion

        #region Methods

        /// <summary>
        /// Renders a new inventory slot.
        /// </summary>
        /// <param name="slot">The inventory slot to render.</param>
        public void RenderNewInventorySlot(InventorySlot slot)
        {
            if (!slot.IsEmpty)
            {
                this.ItemIconImage.enabled = true;
                this.ItemIconImage.sprite = slot.CurrentItemStack.Item.GetIcon();
                this.ItemDisplayNameText.enabled = true;
                if (slot.CurrentItemStack.ItemCount > 1)
                    this.ItemDisplayNameText.text = slot.CurrentItemStack.ItemCount.ToString();
                else
                    this.ItemDisplayNameText.text = string.Empty;
            }
            else
            {
                this.ItemIconImage.enabled = false;
                this.ItemDisplayNameText.enabled = false;
            }
        }

        #endregion
    }
}
