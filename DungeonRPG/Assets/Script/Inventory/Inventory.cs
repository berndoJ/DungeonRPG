using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using DungeonRPG.Items;
using DungeonRPG.Entities;

namespace DungeonRPG.Inventory
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
                    this.InventorySlots[i] = new InventorySlot(); 
            }
            // Test
            this.InventorySlots[0].CurrentItemStack = new ItemStack(testItm, testItm.GetMaxStackSize());
            // End Test
        }

        #endregion
    }
}
