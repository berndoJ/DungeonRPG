using DungeonRPG.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DungeonRPG.Items
{
    /// <summary>
    /// A class that represents a collection of same items. (One item, many counts of it: A stack)
    /// The maximum count of items one stack can hold is defined by the item's max stack size.
    /// </summary>
    /// <remarks>
    /// Copyright (c) 2018 by Johannes Berndorfer (berndoJ)
    /// </remarks>
    public class ItemStack
    {
        #region Properties

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

        #endregion

        #region Events

        /// <summary>
        /// Gets invoked when the amount of items in this itemstack changed.
        /// </summary>
        public event EventHandler<ItemCountChangedEventArgs> OnItemCountChanged;

        #endregion

        #region Constructor

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

        #endregion

        #region Methods

        /// <summary>
        /// Creates a dropped entity of this itemstack on the given vector position.
        /// </summary>
        /// <param name="position">The position to drop the itemstack at.</param>
        public void CreateEntity(Vector3 position)
        {
            this.Item.CreateEntity(this.ItemCount, position);
        }

        #endregion

        #region Override Methods

        /// <summary>
        /// Converts the object given to a string.
        /// </summary>
        /// <returns>The string representation of the object.</returns>
        public override string ToString()
        {
            if (this.Item == null) return "null";
            if (this.ItemCount == 0) return "ItemStack(" + this.Item.ToString() + ",empty)";
            return "ItemStack(" + this.Item.ToString() + ", " + this.ItemCount + ")";
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Swaps two itemstacks.
        /// </summary>
        /// <param name="stack1">The first itemstack.</param>
        /// <param name="stack2">The second itemstack.</param>
        public static void SwapItemStacks(ref ItemStack stack1, ref ItemStack stack2)
        {
            ItemStack temp = stack1;
            stack1 = stack2;
            stack2 = temp;
        }

        #endregion
    }
}
