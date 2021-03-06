﻿using DungeonRPG.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DungeonRPG.ItemContainer
{
    /// <summary>
    /// Behavior class which manages the logic and abstract rendering of
    /// item containers. (container of an inventory GUI which holds items)
    /// </summary>
    /// <remarks>
    /// Copyright (c) 2018 by Johannes Berndorfer (berndoJ)
    /// </remarks>
    public class InventoryItemContainerHandler : MonoBehaviour, IDragHandler, IEndDragHandler, IDropHandler
    {
        #region Constants

        /// <summary>
        /// The distance to the nearest inventoryslot beyond which the item gets dropped.
        /// </summary>
        public const float DRAG_DROP_REMOVE_DIST = 75F;

        #endregion

        #region Fields

        /// <summary>
        /// The slot renderer which corresponds with this item container handler.
        /// </summary>
        [Tooltip("The slot renderer which corresponds with this item container handler.")]
        public InventorySlotRenderer SlotRenderer;

        /// <summary>
        /// Helper bool for <see cref="OnDisable"/>
        /// </summary>
        private bool mExecOnDisable = false;

        /// <summary>
        /// Boolean value which indicates if the container is currently dragged.
        /// </summary>
        private bool mDragging = false;

        #endregion

        #region Interface Implementation

        /// <summary>
        /// Gets executed while the container is dragged.
        /// </summary>
        public void OnDrag(PointerEventData eventData)
        {
            this.transform.position = Input.mousePosition;
            this.mDragging = true;
        }

        /// <summary>
        /// Gets executed when the drag ended.
        /// </summary>
        public void OnEndDrag(PointerEventData eventData)
        {
            this.transform.localPosition = Vector3.zero;
            this.mDragging = false;
        }

        /// <summary>
        /// Gets called when disabled.
        /// </summary>
        private void OnDisable()
        {
            if (!this.mExecOnDisable)
            {
                this.mExecOnDisable = true;
                if (this.mDragging)
                    this.DropItemSubFunc();
            }
        }

        /// <summary>
        /// Gets called when enabled.
        /// </summary>
        private void OnEnable()
        {
            if (this.mExecOnDisable)
            {
                this.mExecOnDisable = false;
            }
        }

        /// <summary>
        /// Gets executed when the item got dropped.
        /// </summary>
        public void OnDrop(PointerEventData eventData)
        {
            Vector3 dragPosition = Input.mousePosition;
            GameObject closestInventorySlot = null;
            float minInventorySlotDist = Mathf.Infinity;
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
            if (minInventorySlotDist > DRAG_DROP_REMOVE_DIST)
            {
                this.DropItemSubFunc();
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

        /// <summary>
        /// Sub function for dropping item (subcode function for <see cref="OnDrop(PointerEventData)"/> and <see cref="OnDisable"/>.
        /// </summary>
        private void DropItemSubFunc()
        {
            // Drop the item
            ItemStack dropItemStack = this.SlotRenderer.CorrespondingInventory.InventorySlots[this.SlotRenderer.CorrespondingInventorySlotID].CurrentItemStack;
            this.SlotRenderer.CorrespondingInventory.InventorySlots[this.SlotRenderer.CorrespondingInventorySlotID].CurrentItemStack = null;
            Debug.Log("Dropping itemstack: " + dropItemStack);
            Vector3 dropPos = Vector3.zero;
            Transform playerTransform = GameObject.FindGameObjectsWithTag("Player").Select(o => o.transform).FirstOrDefault();
            if (playerTransform == null)
            {
                Debug.Log("No player found to drop the itemstack at!");
                return;
            }
            dropPos = playerTransform.position;
            if (dropItemStack == null)
                return;
            dropItemStack.CreateEntity(dropPos);
        }

        #endregion
    }
}
