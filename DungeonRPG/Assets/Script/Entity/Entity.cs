using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using DungeonRPG.Event;

namespace DungeonRPG.Entity
{
    /// <summary>
    /// Base class for entities.
    /// </summary>
    public class Entity : MonoBehaviour
    {
        #region Property Fields

        /// <summary>
        /// Maximum health of the entity.
        /// Default: 20
        /// </summary>
        public int MaxHealth = 20;

        #endregion

        #region Events

        /// <summary>
        /// Event that gets invoked when the entities health changed.
        /// Also gets invoked on death.
        /// </summary>
        public event EventHandler<EntityHealthChangedEventArgs> OnEntityHealthChanged;

        /// <summary>
        /// Event that gets invoked when the entity dies.
        /// </summary>
        public event EventHandler<EntityDieEventArgs> OnEntityDie;

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
                // Calculate the difference of health.
                int deltaHealth = value - this.mHealth;
                EntityHealthChangedType changeType = 0;
                // Set the type of change.
                if (deltaHealth > 0) changeType = EntityHealthChangedType.GAINED_HEALTH;
                else if (deltaHealth < 0) changeType = EntityHealthChangedType.LOST_HEALTH;
                // Clamp to min health and check for death.
                if (value <= 0)
                {
                    changeType = EntityHealthChangedType.DIED;
                    value = 0;
                }
                // Clamp to max health.
                if (value > this.MaxHealth) value = this.MaxHealth;
                // Set the value.
                this.mHealth = value;
                // Invoke events.
                if (deltaHealth != 0 && this.OnEntityHealthChanged != null)
                    this.OnEntityHealthChanged(this, new EntityHealthChangedEventArgs(value, changeType, deltaHealth));
                if (changeType == EntityHealthChangedType.DIED && this.OnEntityDie != null)
                    this.OnEntityDie(this, new EntityDieEventArgs());
            }
        }
        private int mHealth;

        #region Methods

        /// <summary>
        /// Kills the entity.
        /// </summary>
        public void Kill()
        {
            this.Health = 0;
        }

        /// <summary>
        /// Damages the entity in a generic way (damage not dealt by entity)
        /// </summary>
        /// <param name="damage">The damage to deal to the entities health.</param>
        public void DamageGeneric(int damage)
        {
            this.Health -= damage;
        }

        /// <summary>
        /// Damages the entity. (damage dealt by another entity)
        /// </summary>
        /// <param name="additionalDamage">The damage to deal to the entities health.</param>
        /// <param name="damager">The entity that dealth the damage.</param>
        public void Damage(int damage, Entity damager)
        {
            this.DamageGeneric(damage);
        }

        /// <summary>
        /// Teleports the entity to the given position
        /// </summary>
        /// <param name="position">The position to teleport the entitiy to</param>
        public void TeleportTo(Vector3 position)
        {

        }

        #endregion
    }
}


