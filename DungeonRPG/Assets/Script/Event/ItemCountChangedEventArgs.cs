using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DungeonRPG.Event
{
    /// <summary>
    /// Event argument class for the ItemCountChanged event.
    /// </summary>
    /// <remarks>
    /// Copyright (c) 2018 by Johannes Berndorfer (berndoJ)
    /// </remarks>
    public class ItemCountChangedEventArgs : EventArgs
    {
        #region Properties

        /// <summary>
        /// The new item count.
        /// </summary>
        public uint NewItemCount
        {
            get;
            private set;
        }

        /// <summary>
        /// The old item count.
        /// </summary>
        public uint OldItemCount
        {
            get;
            private set;
        }

        /// <summary>
        /// The item count delta.
        /// </summary>
        public int ItemCountDelta
        {
            get
            {
                return (int)this.NewItemCount - (int)this.OldItemCount;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new instnace of this class.
        /// </summary>
        /// <param name="newItemCount">The new item count.</param>
        /// <param name="oldItemCount">The old item count.</param>
        public ItemCountChangedEventArgs(uint newItemCount, uint oldItemCount)
        {
            this.NewItemCount = newItemCount;
            this.OldItemCount = oldItemCount;
        }

        #endregion
    }
}
