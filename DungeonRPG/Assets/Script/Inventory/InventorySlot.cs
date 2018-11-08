using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DungeonRPG.Items;
using DungeonRPG.Event;
using UnityEngine;

namespace DungeonRPG.ItemContainer
{
    /// <summary>
    /// Class which represents one slot in an inventory.
    /// </summary>
    /// <remarks>
    /// Copyright (c) 2018 by Johannes Berndorfer (berndoJ)
    /// </remarks>
    public class InventorySlot
    {
        #region Properties

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

                this.mSlotChangedCallbackFunc(this);
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

        #endregion

        #region Fields

        /// <summary>
        /// The parent inventory of this slot.
        /// </summary>
        private Action<InventorySlot> mSlotChangedCallbackFunc;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new instance of this class.
        /// </summary>
        /// <param name="callbackFunc">The parent inventory of this slot.</param>
        public InventorySlot(Action<InventorySlot> slotChangedCallbackFunc)
        {
            this.mSlotChangedCallbackFunc = slotChangedCallbackFunc;
        }

        #endregion

        #region Methods

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
            }
            this.mSlotChangedCallbackFunc(this);
        }

        #endregion
    }
}
