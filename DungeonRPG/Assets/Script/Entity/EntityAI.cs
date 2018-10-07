using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG.Entity
{
    /// <summary>
    /// Entity AI base class
    /// </summary>
    public class EntityAI : MonoBehaviour
    {
        #region Property Fields

        /// <summary>
        /// The rigidbody of the entity.
        /// </summary>
        [Tooltip("The rigidbody of the entity.")]
        public Rigidbody2D EntityRigidbody;

        #endregion

        #region Properties

        /// <summary>
        /// Coefficient for controlling movement speed.
        /// </summary>
        public float MovementSpeedCoefficient
        {
            get;
            set;
        }

        /// <summary>
        /// Value for controlling if the player can move.
        /// </summary>
        public bool CanMove
        {
            get;
            set;
        }

        /// <summary>
        /// Coefficient for controlling jump force.
        /// </summary>
        public float JumpForceCoefficient
        {
            get;
            set;
        }

        /// <summary>
        /// Value for controlling if the player can jump.
        /// </summary>
        public bool CanJump
        {
            get;
            set;
        }

        /// <summary>
        /// Value indicating if the player is currently on the ground.
        /// </summary>
        public bool IsOnGround
        {
            get;
            protected set;
        }

        /// <summary>
        /// The looking direction of the entity.
        /// </summary>
        public EntityLookingDirection LookingDirection
        {
            get;
            protected set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets the rigidbody transform position.
        /// </summary>
        /// <param name="position">The new position.</param>
        public void SetTransformPosition(Vector2 position)
        {
            this.EntityRigidbody.position = position;
        }

        #endregion
    }
}
