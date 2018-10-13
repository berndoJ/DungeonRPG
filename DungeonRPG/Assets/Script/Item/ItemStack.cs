using DungeonRPG.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DungeonRPG.Items
{
    public class ItemStack
    {
        /// <summary>
        /// The count of items this itemstack currently holds.
        /// </summary>
        public uint ItemCount
        {
            get
            {
                return this.mItemCount;
            }
            set
            {
                uint oldItemCount = this.mItemCount;
                this.mItemCount = value;
                if (this.OnItemCountChanged != null)
                    this.OnItemCountChanged(this, new ItemCountChangedEventArgs(value, oldItemCount));
            }
        }
        private uint mItemCount;

        /// <summary>
        /// The item of this itemstack.
        /// </summary>
        public Item Item
        {
            get;
            private set;
        }

        #region Events

        /// <summary>
        /// Gets invoked when the amount of items in this itemstack changed.
        /// </summary>
        public event EventHandler<ItemCountChangedEventArgs> OnItemCountChanged;

        #endregion

        /// <summary>
        /// Creates a new instance of this class.
        /// </summary>
        /// <param name="item">The item of this itemstack.</param>
        public ItemStack(Item item)
        {
            this.ItemCount = 1;
            this.Item = item;
        }

        /// <summary>
        /// Creates a new instance of this class.
        /// </summary>
        /// <param name="item">The item of this itemstack.</param>
        /// <param name="itemCount">The count of items this itemstack currently holds.</param>
        public ItemStack(Item item, uint itemCount)
        {
            this.ItemCount = itemCount;
            this.Item = item;
        }
    }
}
