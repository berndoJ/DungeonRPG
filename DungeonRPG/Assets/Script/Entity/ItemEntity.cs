using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using DungeonRPG.Items;
using DungeonRPG.ItemContainer;

namespace DungeonRPG.Entities
{
    public class ItemEntity : MonoBehaviour
    {
        /// <summary>
        /// The sprite renderer of the light sprite. Needed to make the item "glow".
        /// </summary>
        [Tooltip("The sprite renderer of the light sprite. Needed to make the item \"glow\".")]
        public SpriteRenderer LightSpriteRenderer;

        /// <summary>
        /// The count of items this entity contains.
        /// </summary>
        public uint ItemCount
        {
            get;
            set;
        }

        /// <summary>
        /// The item corresponding to this entity.
        /// </summary>
        [SerializeField]
        [Tooltip("The item corresponding to this entity.")]
        [Header("Corresponding Item")]
        private Item mCorrespondingItem;

        /// <summary>
        /// Boolean value for enabling the blink.
        /// </summary>
        private bool mBlinkActive = false;

        #region Behavior Methods

        /// <summary>
        /// Init of this code.
        /// </summary>
        private void Awake()
        {
            this.StartCoroutine(this.SpriteBlinkCoFunc());
        }

        /// <summary>
        /// Gets called when the mouse enters the boundings of this object.
        /// </summary>
        private void OnMouseEnter()
        {
            this.mBlinkActive = true;
        }

        /// <summary>
        /// Gets called when the mouse exits the boundings of this object.
        /// </summary>
        private void OnMouseExit()
        {
            this.mBlinkActive = false;
        }

        /// <summary>
        /// Gets called when the mouse got pressed while being over this object.
        /// </summary>
        private void OnMouseDown()
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            if (players.Length < 1)
            {
                Debug.LogError("Could not find a player.");
                return;
            }
            Inventory playerInventory = players[0].GetComponent<Inventory>();
            if (playerInventory == null)
            {
                Debug.LogError("The player found has no inventory!");
                return;
            }
            this.ItemCount = playerInventory.AddItem(this.mCorrespondingItem, this.ItemCount);
            if (this.ItemCount == 0)
                Destroy(this.gameObject);
        }

        #endregion

        #region Coroutines

        /// <summary>
        /// Sprite blinking coroutine.
        /// </summary>
        /// <returns>The enumerator for corouties.</returns>
        private IEnumerator SpriteBlinkCoFunc()
        {
            while (true)
            {
                bool dir = true;
                float currentAlpha = 0F;
                while (this.mBlinkActive)
                {
                    if (dir)
                        currentAlpha += 0.05F;
                    else
                        currentAlpha -= 0.05F;
                    if (currentAlpha <= 0F)
                    {
                        dir = true;
                        currentAlpha = 0F;
                    }
                    if (currentAlpha >= 1F)
                    {
                        dir = false;
                        currentAlpha = 1F;
                    }
                    Color spriteColor = this.LightSpriteRenderer.color;
                    spriteColor.a = currentAlpha;
                    this.LightSpriteRenderer.color = spriteColor;
                    yield return new WaitForFixedUpdate();
                }
                Color endColor = this.LightSpriteRenderer.color;
                endColor.a = 0F;
                this.LightSpriteRenderer.color = endColor;
                yield return new WaitUntil(() => this.mBlinkActive == true);
            }
        }

        #endregion
    }
}
