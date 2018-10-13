using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using DungeonRPG.Event;

namespace DungeonRPG.Inventory
{
    public class InventoryGUIManager : MonoBehaviour
    {
        /// <summary>
        /// The inventory to render.
        /// </summary>
        [Tooltip("The inventory to render.")]
        public Inventory InventoryToRender;

        /// <summary>
        /// The slot renderers to redner the inventory to.
        /// </summary>
        [Tooltip("The slot renderers to redner the inventory to.")]
        public List<InventorySlotRenderer> SlotRenderers;

        /// <summary>
        /// The transform of the inventory slot panel.
        /// </summary>
        [Tooltip("The transform of the inventory slot panel.")]
        public Transform InventorySlotPanelTransform;

        #region Behavior Methods

        /// <summary>
        /// Init of this code.
        /// </summary>
        private void Awake()
        {
            this.InventoryToRender.OnInventoryChanged += this.OnInventoryChanged;
        }

        /// <summary>
        /// Init on first frame.
        /// </summary>
        private void Start()
        {
            this.UpdateInventoryView();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Updates the inventory view and re-draws it.
        /// </summary>
        public void UpdateInventoryView()
        {
            for (int i = 0; i < this.SlotRenderers.Count; i++)
            {
                if (i < this.InventoryToRender.InventorySlots.Length) // Make sure not to get out of inventory slot array bounds.
                {
                    this.SlotRenderers[i].RenderNewInventorySlot(this.InventoryToRender.InventorySlots[i]);
                    this.SlotRenderers[i].InventorySlotPanelTransform = this.InventorySlotPanelTransform;
                    this.SlotRenderers[i].CorrespondingInventory = this.InventoryToRender;
                    this.SlotRenderers[i].CorrespondingInventorySlotID = i;
                }
            }
        }

        #endregion

        #region Event Delegates

        /// <summary>
        /// Event delegate for <see cref="Inventory.OnInventoryChanged"/>
        /// </summary>
        private void OnInventoryChanged(object sender, InventoryChangedEventArgs e)
        {
            this.UpdateInventoryView();
        }

        #endregion
    }
}
