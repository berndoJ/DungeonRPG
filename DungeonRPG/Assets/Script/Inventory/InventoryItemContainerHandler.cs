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
            RectTransform invRectTransform = this.SlotRenderer.InventorySlotPanelTransform as RectTransform;
            if (!RectTransformUtility.RectangleContainsScreenPoint(invRectTransform, Input.mousePosition))
            {
                Debug.Log("REMOVE");
                // Remove the item.
                this.SlotRenderer.CorrespondingInventory.InventorySlots[this.SlotRenderer.CorrespondingInventorySlotID].ClearSlot();
            }
        }

        #endregion
    }
}
