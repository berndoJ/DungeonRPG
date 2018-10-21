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

        /// <summary>
        /// Gets a serialized version of the inventory.
        /// </summary>
        /// <returns>-</returns>
        public string GetSerialized()
        {
            string serializedString = string.Empty;
            foreach (InventorySlot invSlot in this.InventorySlots)
            {
                string itemCount = invSlot.IsEmpty ? "null" : invSlot.CurrentItemStack.ItemCount.ToString();
                string itemUID = invSlot.IsEmpty ? "null" : invSlot.CurrentItemStack.Item.GetUID();
                if (serializedString != string.Empty)
                    serializedString += ":";
                serializedString += (itemCount + "," + itemUID);
            }
            return serializedString;
        }

        /// <summary>
        /// Loads a serialized version of the inventory.
        /// </summary>
        /// <param name="serializedString">The serialized version of the inventory.</param>
        public void LoadFromSerialized(string serializedString)
        {
            List<List<string>> invSlotValues = serializedString.Split(':').Select(s => s.Split(',').ToList()).ToList();
            if (invSlotValues.Count != this.InventorySize)
            {
                Debug.LogError("Tried loading inventory from serialized: Inventory slot count not matching! (" + serializedString + ")");
                return;
            }
            int i = 0;
            foreach (List<string> invSlotValue in invSlotValues)
            {
                if (invSlotValue.Count != 2)
                {
                    Debug.LogError("Tried loading inventory from serialized: Slot formatting error! (" + serializedString + ")");
                    return;
                }
                uint itemCount = 0;
                if (!uint.TryParse(invSlotValue[0], out itemCount))
                {
                    if (invSlotValue[0] == "null")
                    {
                        this.InventorySlots[i].CurrentItemStack = null;
                        continue;
                    }
                    Debug.LogError("Tried loading inventory from serialized: Slot item count formatting error! (" + serializedString + ")");
                    return;
                }
                string itemUID = invSlotValue[1];
                Item item = Item.GetItemByUID(itemUID);
                if (item == null)
                {
                    Debug.LogError("Tried loading inventory from serialized: Slot item uid does not exist! (" + serializedString + ")");
                    return;
                }
                this.InventorySlots[i].CurrentItemStack = new ItemStack(item, itemCount);
            }
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
