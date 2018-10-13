using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace DungeonRPG.Items
{
    /// <summary>
    /// Item base class.
    /// </summary>
    [CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
    public class Item : ScriptableObject
    {
        /// <summary>
        /// The display name of the item.
        /// Default: "New Item"
        /// </summary>
        [SerializeField]
        [Tooltip("The display name of the item.")]
        [Header("Display Name")]
        private string mDisplayName = "New Item";

        /// <summary>
        /// The UniqueID-String of this item.
        /// Default: "new_item"
        /// </summary>
        [SerializeField]
        [Tooltip("The UniqueID-String of this item.")]
        [Header("UID")]
        private string mUIDString = "new_item";

        /// <summary>
        /// The maximum stack size of this item.
        /// Default: 1
        /// </summary>
        [SerializeField]
        [Tooltip("The maximum stack size of this item.")]
        [Header("Max Stack Size")]
        private uint mMaxStackSize = 1;

        /// <summary>
        /// The icon of the item.
        /// </summary>
        [SerializeField]
        [Tooltip("The icon of the item.")]
        [Header("Icon")]
        private Sprite mIcon = null;

        #region ScriptableObject Methods

        /// <summary>
        /// Init of this code.
        /// </summary>
        private void Awake()
        {
            if (this.mMaxStackSize <= 0)
            {
                this.mMaxStackSize = 1;
                Debug.LogError("MaxStackSize is 0!");
            }
        }

        #endregion

        /// <summary>
        /// Retrieves the display name of this item.
        /// </summary>
        public string GetDisplayName()
        {
            return this.mDisplayName;
        }

        /// <summary>
        /// Retrieves the UID string of this item.
        /// </summary>
        public string GetUID()
        {
            return this.mUIDString;
        }

        /// <summary>
        /// Retrieves the maximum stack size of this item.
        /// </summary>
        public uint GetMaxStackSize()
        {
            return this.mMaxStackSize;
        }

        /// <summary>
        /// Retrieves the icon of this item.
        /// </summary>
        public Sprite GetIcon()
        {
            return this.mIcon;
        }

        /// <summary>
        /// Gets executed on main fire when holding this item.
        /// </summary>
        /// <param name="itemStack">The held itemstack.</param>
        public virtual void OnMainFire(ItemStack itemStack) { }

        /// <summary>
        /// Gets executed on secondary fire when holding this item.
        /// </summary>
        /// <param name="itemStack">The held itemstack.</param>
        public virtual void OnSecondaryFire(ItemStack itemStack) { }

        #region Static

        /// <summary>
        /// Buffer list for all currently "known" items to this class.
        /// </summary>
        private static List<Item> CurrentlyKnownItems = null;

        /// <summary>
        /// Retrieves an item by the given UID string.
        /// </summary>
        /// <param name="uidString">The UID string of the item.</param>
        /// <returns>The item / null if it does not exist.</returns>
        public static Item GetItemByUID(string uidString)
        {
            if (CurrentlyKnownItems == null)
                CurrentlyKnownItems = AssetDatabase.FindAssets("t:" + typeof(Item).Name)
                    .Select(guid => AssetDatabase.LoadAssetAtPath<Item>(AssetDatabase.GUIDToAssetPath(guid)))
                    .ToList();
            List<Item> matches = CurrentlyKnownItems.Where(i => i.GetUID() == uidString).ToList();
            return matches.FirstOrDefault();
        }

        #endregion
    }
}
