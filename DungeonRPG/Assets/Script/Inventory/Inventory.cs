using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using DungeonRPG.Items;
using DungeonRPG.Entities;
using DungeonRPG.Event;

namespace DungeonRPG.ItemContainer
{
    public class Inventory : MonoBehaviour
    {
        /// <summary>
        /// The entity this inventory belongs to.
        /// </summary>
        [Tooltip("The entity this inventory belongs to.")]
        public Entity InventoryHolder;

        /// <summary>
        /// The size of the inventory in slots.
        /// </summary>
        [Tooltip("The size of the inventory in slots.")]
        public uint InventorySize;

        /// <summary>
        /// All inventory slots.
        /// </summary>
        public InventorySlot[] InventorySlots
        {
            get;
            private set;
        }

        #region Events

        /// <summary>
        /// Gets invoked when the inventory changed.
        /// </summary>
        public event EventHandler<InventoryChangedEventArgs> OnInventoryChanged;

        #endregion

        public Item testItm;

        #region Behavior Methods

        /// <summary>
        /// The init of this code.
        /// </summary>
        private void Awake()
        {
            if (this.InventoryHolder == null)
                Debug.LogError("InventoryHolder is null!");
            if (this.InventorySize <= 0)
                Debug.LogError("InventorySize is 0!");
            else
            {
                this.InventorySlots = new InventorySlot[(int)this.InventorySize];
                for (int i = 0; i < this.InventorySize; i++)
                    this.InventorySlots[i] = new InventorySlot((slot) => this.InvChangedCallbackFunc(slot)); 
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds an item to the inventory. If the item could not be added, the leftover is returned.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <param name="count">The cound of items to add.</param>
        public uint AddItem(Item item, uint count)
        {
            if (count == 0) return 0;
            foreach (InventorySlot slot in this.InventorySlots)
            {
                if (!slot.IsEmpty)
                {
                    if (slot.CurrentItemStack.Item == item)
                    {
                        uint availableSlotSpace = item.GetMaxStackSize() - slot.CurrentItemStack.ItemCount;
                        if (count <= availableSlotSpace)
                        {
                            slot.CurrentItemStack.ItemCount += count;
                            count = 0;
                        }
                        else
                        {
                            slot.CurrentItemStack.ItemCount = item.GetMaxStackSize();
                            count -= availableSlotSpace;
                        }
                    }
                }
                else
                {
                    if (count <= item.GetMaxStackSize())
                    {
                        slot.CurrentItemStack = new ItemStack(item, count);
                        count = 0;
                    }
                    else
                    {
                        slot.CurrentItemStack = new ItemStack(item, item.GetMaxStackSize());
                        count -= item.GetMaxStackSize();
                    }
                }
                if (count == 0)
                    break;
            }
            return count;
        }

        #endregion

        #region Callback Methods

        /// <summary>
        /// Callback function for slots to register a change in the inventory.
        /// </summary>
        /// <param name="senderSlot">The slot that got changed.</param>
        private void InvChangedCallbackFunc(InventorySlot senderSlot)
        {
            if (this.OnInventoryChanged != null)
                this.OnInventoryChanged(this, new InventoryChangedEventArgs(senderSlot));
        }

        #endregion
    }
}
