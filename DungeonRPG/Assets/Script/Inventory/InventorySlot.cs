using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DungeonRPG.Items;
using DungeonRPG.Event;

namespace DungeonRPG.Inventory
{
    public class InventorySlot
    {
        /// <summary>
        /// The current itemstack in this slot.
        /// </summary>
        public ItemStack CurrentItemStack
        {
            get
            {
                return this.mCurrentItemStack;
            }
            set
            {
                if (this.mCurrentItemStack != null)
                {
                    this.mCurrentItemStack.OnItemCountChanged -= this.OnItemStackItemCountChanged;
                }

                this.mCurrentItemStack = value;

                if (this.mCurrentItemStack != null)
                {
                    this.mCurrentItemStack.OnItemCountChanged += this.OnItemStackItemCountChanged;
                }
            }
        }
        private ItemStack mCurrentItemStack;

        /// <summary>
        /// Boolean that indicates if this slot is empty.
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return this.CurrentItemStack == null;
            }
        }

        /// <summary>
        /// Clears this slot.
        /// </summary>
        public void ClearSlot()
        {
            this.CurrentItemStack = null;
        }

        /// <summary>
        /// Event delegate for <see cref="ItemStack.OnItemCountChanged"/>
        /// </summary>
        private void OnItemStackItemCountChanged(object sender, ItemCountChangedEventArgs e)
        {
            if (e.NewItemCount == 0)
            {
                this.CurrentItemStack = null;
                return;
            }
        }
    }
}
