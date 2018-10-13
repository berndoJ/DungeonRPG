using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace DungeonRPG.Inventory
{
    public class InventorySlotRenderer : MonoBehaviour
    {
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
                this.ItemDisplayNameText.text = slot.CurrentItemStack.ItemCount.ToString();
            }
            else
            {
                this.ItemIconImage.enabled = false;
                this.ItemDisplayNameText.enabled = false;
            }
        }
    }
}
