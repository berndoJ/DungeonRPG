using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DungeonRPG.Event
{
    public class ItemCountChangedEventArgs : EventArgs
    {
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
    }
}
