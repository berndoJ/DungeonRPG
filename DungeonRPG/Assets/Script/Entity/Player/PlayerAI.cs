﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using DungeonRPG.Event;

namespace DungeonRPG.Entity
{
    public class PlayerAI : EntityAI
    {
        #region Constants

        /// <summary>
        /// Radius used to check for ground around the ground check transform object.
        /// </summary>
        private readonly float GROUND_CHECK_RADIUS = 0.2F;

        /// <summary>
        /// Coefficient used to tune the movement speed by default.
        /// </summary>
        private readonly float MOVEMENT_X_COEFFICIENT = 27F;

        /// <summary>
        /// Coefficient used to tune the jump force.
        /// </summary>
        private readonly float JUMP_COEFFICIENT = 115F;

        #endregion

        #region Property Fields

        /// <summary>
        /// The normal movement speed of the player.
        /// Default: 10
        /// </summary>
        [Tooltip("The normal movement speed of the player.")]
        public float MovementSpeed = 10F;

        /// <summary>
        /// The height level below which the player dies.
        /// Default: -10F
        /// </summary>
        [Tooltip("The height level below which the player dies.")]
        public float DeathHeightLevel = -10F;

        /// <summary>
        /// The jump force of the player.
        /// Default: 5F
        /// </summary>
        [Tooltip("The jump force of the player.")]
        public float JumpForce = 5F;

        /// <summary>
        /// Coefficient for controlling the smoothness of the player's movement.
        /// Default: 0.1F
        /// </summary>
        [Tooltip("Coefficient for controlling the smoothness of the player's movement.")]
        [Range(0F, 0.3F)]
        public float MovementSmoothing = 0.1F;

        /// <summary>
        /// The transform / object used to check for ground. This should be an empty object located near the feet of the player.
        /// </summary>
        [Tooltip("The transform / object used to check for ground. This should be an empty object located near the feet of the player.")]
        public Transform GroundCheckTransform;

        /// <summary>
        /// The layer mask to determine what is ground for the character.
        /// </summary>
        [Tooltip("The layer mask to determine what is ground for the character.")]
        public LayerMask GroundLayerMask;

        /// <summary>
        /// The rigidbody of the player.
        /// </summary>
        [Tooltip("The rigidbody of the player.")]
        public Rigidbody2D PlayerRigidbody;

        /// <summary>
        /// The animator of the player.
        /// </summary>
        [Tooltip("The animator of the player.")]
        public Animator PlayerAnimator;

        /// <summary>
        /// The player this AI belongs to.
        /// </summary>
        [Tooltip("The player this AI belongs to.")]
        public Player Player;

        #endregion

        #region Events

        /// <summary>
        /// Gets invoked when the player jumps.
        /// </summary>
        public event EventHandler<PlayerJumpEventArgs> OnPlayerJump;

        /// <summary>
        /// Gets invoked when the player moves.
        /// </summary>
        public event EventHandler<PlayerMoveEventArgs> OnPlayerMove;

        /// <summary>
        /// Gets invoked when the player lands on ground.
        /// </summary>
        public event EventHandler<PlayerLandEventArgs> OnPlayerLand;

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
            private set;
        }

        /// <summary>
        /// The looking direction of the player.
        /// </summary>
        public PlayerLookingDirection LookingDirection
        {
            get;
            private set;
        }

        #endregion

        #region Members

        /// <summary>
        /// The input for horizontal movement.
        /// </summary>
        private float mInputHorizontalMovement;

        /// <summary>
        /// The input for jump button.
        /// </summary>
        private bool mInputButtonJump;

        /// <summary>
        /// The current velocity of the player.
        /// Used to smoothen the movement of the player.
        /// </summary>
        private Vector3 mCurrentPlayerVelocity = Vector3.zero;

        #endregion

        #region Behavior Methods

        /// <summary>
        /// Init of this code.
        /// </summary>
        private void Awake()
        {
            // Set defaults
            this.MovementSpeedCoefficient = 1F;
            this.CanMove = true;
            this.JumpForceCoefficient = 1F;
            this.CanJump = true;
            // Register all event delegates.
            this.OnPlayerJump += this.OnPlayerJumpInvoke;
            this.OnPlayerLand += this.OnPlayerLandInvoke;
            this.OnPlayerMove += this.OnPlayerMoveInvoke;
        }

        /// <summary>
        /// Game loop.
        /// </summary>
        private void FixedUpdate()
        {
            // Update the player's physics and movement.
            this.UpdatePlayerPhysics();
        }

        /// <summary>
        /// Frame loop.
        /// </summary>
        private void Update()
        {
            // Update the input fields.
            this.UpdateInputFields();
            // Update the IsOnGround property.
            this.UpdateIsOnGround();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Routine for updating the input field members.
        /// Shall be called in the frame loop.
        /// </summary>
        private void UpdateInputFields()
        {
            // Update horizontal movement
            this.mInputHorizontalMovement = Input.GetAxisRaw("Horizontal");
            // Update jump
            if (Input.GetButtonDown("Jump"))
                this.mInputButtonJump = true;

            if (Input.GetButtonDown("Fire1"))
                this.Player.Health -= 5;
        }

        /// <summary>
        /// Updates the player's physics and movement.
        /// Shall be called in the game loop.
        /// </summary>
        private void UpdatePlayerPhysics()
        {
            if (this.CanMove)
            {
                // Calculate the horizontal movement of the player.
                float xMovement = this.mInputHorizontalMovement;
                xMovement *= (this.MovementSpeed * this.MovementSpeedCoefficient * Time.deltaTime);
                // Apply movement to the rigidbody of the player.
                Vector3 targetVelocity = new Vector2(xMovement * this.MOVEMENT_X_COEFFICIENT, this.PlayerRigidbody.velocity.y);
                this.PlayerRigidbody.velocity = Vector3.SmoothDamp(this.PlayerRigidbody.velocity, targetVelocity, ref this.mCurrentPlayerVelocity, this.MovementSmoothing);
                // Flip the player if necessary
                if (xMovement > 0 && this.LookingDirection == PlayerLookingDirection.NEGATIVE_X)
                    this.FlipPlayerLookingDirection();
                else if (xMovement < 0 && this.LookingDirection == PlayerLookingDirection.POSITIVE_X)
                    this.FlipPlayerLookingDirection();
                // Invoke OnPlayerMove
                if (this.OnPlayerMove != null)
                    this.OnPlayerMove(this, new PlayerMoveEventArgs(xMovement));
            }
            // Check if the player jumps
            if (this.CanJump && this.IsOnGround && this.mInputButtonJump)
            {
                this.mInputButtonJump = false;
                this.PlayerRigidbody.AddForce(new Vector2(0F, this.JumpForce * this.JUMP_COEFFICIENT));
                // Invoke OnJumpEvent
                if (this.OnPlayerJump != null)
                    this.OnPlayerJump(this, new PlayerJumpEventArgs());
            }
            // Check for falling death
            if (this.PlayerRigidbody.position.y < this.DeathHeightLevel)
                this.Player.Kill();
        }

        /// <summary>
        /// Updates the property <see cref="IsOnGround"/>
        /// </summary>
        private void UpdateIsOnGround()
        {
            bool prevIsOnGround = this.IsOnGround;
            this.IsOnGround = false;
            Collider2D[] interferingColliders = Physics2D.OverlapCircleAll(this.GroundCheckTransform.position, this.GROUND_CHECK_RADIUS, this.GroundLayerMask);
            foreach (Collider2D collider in interferingColliders)
                if (collider.gameObject != this.gameObject)
                {
                    this.IsOnGround = true;
                }
            if (!prevIsOnGround && this.IsOnGround && this.OnPlayerLand != null) // Player landed
                this.OnPlayerLand(this, new PlayerLandEventArgs());
        }

        /// <summary>
        /// Flips the player's looking direction.
        /// </summary>
        private void FlipPlayerLookingDirection()
        {
            switch (this.LookingDirection)
            {
                case PlayerLookingDirection.NEGATIVE_X:
                    this.LookingDirection = PlayerLookingDirection.POSITIVE_X;
                    break;
                case PlayerLookingDirection.POSITIVE_X:
                    this.LookingDirection = PlayerLookingDirection.NEGATIVE_X;
                    break;
                default:
                    this.LookingDirection = PlayerLookingDirection.POSITIVE_X;
                    break;
            }
            Vector3 scale = this.transform.localScale;
            scale.x *= -1;
            this.transform.localScale = scale;
        }

        #endregion

        #region Event Delegates

        /// <summary>
        /// Event delegate for <see cref="OnPlayerJump"/>
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void OnPlayerJumpInvoke(object sender, PlayerJumpEventArgs e)
        {
            // Update the animator.
            this.PlayerAnimator.SetBool("IsJumping", true);
        }

        /// <summary>
        /// Event delegate for <see cref="OnPlayerLand"/>
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void OnPlayerLandInvoke(object sender, PlayerLandEventArgs e)
        {
            // Update the animator.
            this.PlayerAnimator.SetBool("IsJumping", false);
        }

        /// <summary>
        /// Event delegate for <see cref="OnPlayerMove"/>
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void OnPlayerMoveInvoke(object sender, PlayerMoveEventArgs e)
        {
            // Update the animator.
            this.PlayerAnimator.SetFloat("PlayerSpeed", Mathf.Abs(e.CurrentMovementSpeed));
        }

        #endregion
    }
}
