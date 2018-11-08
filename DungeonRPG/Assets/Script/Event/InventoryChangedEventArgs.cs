using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DungeonRPG.ItemContainer;

namespace DungeonRPG.Event
{
    /// <summary>
    /// Event argument class for the InventoryChanged event.
    /// </summary>
    /// <remarks>
    /// Copyright (c) 2018 by Johannes Berndorfer (berndoJ)
    /// </remarks>
    public class InventoryChangedEventArgs : EventArgs
    {
        #region Properties

        /// <summary>
        /// The original source slot of the event.
        /// </summary>
        public InventorySlot OriginalSourceSlot
        {
            get;
            private set;
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new instance of this class.
        /// </summary>
        /// <param name="originalSourceSlot">The original source slot of the event.</param>
        public InventoryChangedEventArgs(InventorySlot originalSourceSlot)
        {
            this.OriginalSourceSlot = originalSourceSlot;
        }

        #endregion
    }
}
