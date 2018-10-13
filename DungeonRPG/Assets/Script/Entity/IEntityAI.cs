using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG.Entities
{
    /// <summary>
    /// Entity AI base class
    /// </summary>
    public interface IEntityAI
    {
        /// <summary>
        /// Coefficient for controlling movement speed.
        /// </summary>
        float MovementSpeedCoefficient { get; set; }

        /// <summary>
        /// Boolean for controlling if the player can move.
        /// </summary>
        bool CanMove { get; set; }

        /// <summary>
        /// Coefficient for controlling jump force.
        /// </summary>
        float JumpForceCoefficient { get; set; }

        /// <summary>
        /// Boolean for controlling if the player can jump.
        /// </summary>
        bool CanJump { get; set; }

        /// <summary>
        /// Value indicating if the player is currently on the ground.
        /// </summary>
        bool IsOnGround { get; }

        /// <summary>
        /// The looking direction of the entity.
        /// </summary>
        EntityLookingDirection LookingDirection { get; }

        /// <summary>
        /// The current movement speed of the entity.
        /// </summary>
        float CurrentMovementSpeed { get; }

        /// <summary>
        /// Gets the entity's rigidbody.
        /// </summary>
        /// <returns>The rigidbody</returns>
        Rigidbody2D GetEntityRigidbody();        
    }
}
