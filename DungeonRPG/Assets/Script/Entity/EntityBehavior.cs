using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DungeonRPG.Entity
{
    /// <summary>
    /// Base class for entities.
    /// </summary>
    public class EntityBehavior : MonoBehaviour
    {
        #region Property Fields

        /// <summary>
        /// Maximum health of the entity.
        /// Default: 20
        /// </summary>
        public int MaxHealth = 20;

        /// <summary>
        /// Base attack damage of the entity.
        /// Default: 5
        /// </summary>
        public int BaseDamage = 5;

        #endregion

        #region Events



        #endregion

        /// <summary>
        /// The current health of the entity.
        /// </summary>
        public int Health
        {
            get
            {
                return this.mHealth;
            }
            set
            {
                this.mHealth = value;
            }
        }
        private int mHealth;
    }
}


