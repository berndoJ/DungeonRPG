using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DungeonRPG.Inventory;

namespace DungeonRPG.Event
{
    public class InventoryChangedEventArgs : EventArgs
    {
        /// <summary>
        /// The original source slot of the event.
        /// </summary>
        public InventorySlot OriginalSourceSlot
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates a new instance of this class.
        /// </summary>
        /// <param name="originalSourceSlot">The original source slot of the event.</param>
        public InventoryChangedEventArgs(InventorySlot originalSourceSlot)
        {
            this.OriginalSourceSlot = originalSourceSlot;
        }
    }
}
