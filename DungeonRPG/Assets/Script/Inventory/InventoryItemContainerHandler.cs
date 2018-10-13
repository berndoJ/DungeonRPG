using DungeonRPG.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DungeonRPG.Inventory
{
    public class InventoryItemContainerHandler : MonoBehaviour, IDragHandler, IEndDragHandler, IDropHandler
    {
        #region Constants

        /// <summary>
        /// The distance to the nearest inventoryslot beyond which the item gets dropped.
        /// </summary>
        public const float DRAG_DROP_REMOVE_DIST = 75F;

        #endregion

        #region Public Fields

        /// <summary>
        /// The slot renderer which corresponds with this item container handler.
        /// </summary>
        [Tooltip("The slot renderer which corresponds with this item container handler.")]
        public InventorySlotRenderer SlotRenderer;
        
        #endregion

        #region Interface Implementation

        /// <summary>
        /// Gets executed while the container is dragged.
        /// </summary>
        public void OnDrag(PointerEventData eventData)
        {
            this.transform.position = Input.mousePosition;
        }

        /// <summary>
        /// Gets executed when the drag ended.
        /// </summary>
        public void OnEndDrag(PointerEventData eventData)
        {
            this.transform.localPosition = Vector3.zero;
        }

        /// <summary>
        /// Gets executed when the item got dropped.
        /// </summary>
        public void OnDrop(PointerEventData eventData)
        {
            Vector3 dragPosition = Input.mousePosition;
            GameObject closestInventorySlot = null;
            float minInventorySlotDist = Mathf.Infinity;
            Debug.Log("FOUND " + GameObject.FindGameObjectsWithTag("InventorySlot").Length);
            foreach (GameObject invSlotObj in GameObject.FindGameObjectsWithTag("InventorySlot"))
            {
                float slotDist = Vector2.Distance(invSlotObj.transform.position, dragPosition);
                if (slotDist < minInventorySlotDist)
                {
                    minInventorySlotDist = slotDist;
                    closestInventorySlot = invSlotObj;
                }
            }
            if (closestInventorySlot == null)
                return;
            if (minInventorySlotDist > DRAG_DROP_REMOVE_DIST && false)
            {
                // TODO: Drop the item
                ItemStack dropItemStack = this.SlotRenderer.CorrespondingInventory.InventorySlots[this.SlotRenderer.CorrespondingInventorySlotID].CurrentItemStack;
                this.SlotRenderer.CorrespondingInventory.InventorySlots[this.SlotRenderer.CorrespondingInventorySlotID].CurrentItemStack = null;
                Debug.Log("Dropping itemstack: " + dropItemStack);
            }
            else
            {
                InventorySlotRenderer destSlotRenderer = closestInventorySlot.GetComponent<InventorySlotRenderer>();
                if (destSlotRenderer == null)
                    return;
                ItemStack srcItemStack = this.SlotRenderer.CorrespondingInventory.InventorySlots[this.SlotRenderer.CorrespondingInventorySlotID].CurrentItemStack;
                ItemStack destItemStack = destSlotRenderer.CorrespondingInventory.InventorySlots[destSlotRenderer.CorrespondingInventorySlotID].CurrentItemStack;
                ItemStack.SwapItemStacks(ref srcItemStack, ref destItemStack);
                this.SlotRenderer.CorrespondingInventory.InventorySlots[this.SlotRenderer.CorrespondingInventorySlotID].CurrentItemStack = srcItemStack;
                destSlotRenderer.CorrespondingInventory.InventorySlots[destSlotRenderer.CorrespondingInventorySlotID].CurrentItemStack = destItemStack;
            }
        }

        #endregion
    }
}
