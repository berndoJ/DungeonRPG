using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Behavior for the player allowing it to move...
/// </summary>
public class PlayerBehavior : MonoBehaviour
{
    #region Constants

    private readonly float MOVE_SPEED_COEFF = 1F;

    #endregion

    #region General Variables

    /// <summary>
    /// The movement speed of the player. Default: 10
    /// </summary>
    public int MovementSpeed = 10;

    /// <summary>
    /// The maximum health that a player can have. Default: 100
    /// </summary>
    public int MaxHealth = 100;

    /// <summary>
    /// The name of the player. Default: "David", named after the creator of the game.
    /// </summary>
    public string PlayerName = "David";

    /// <summary>
    /// The character controller script.
    /// </summary>
    public CharacterController2D CharacterController;

    /// <summary>
    /// The animator of the player.
    /// </summary>
    public Animator PlayerAnimator;

    /// <summary>
    /// The y level under which the player dies (of falling) Default: -10
    /// </summary>
    public int FallDeathLevel = -10;

    /// <summary>
    /// The regenerated energy per gameloop tick.
    /// </summary>
    public float EnergyRegen = 0.15F;

    /// <summary>
    /// The amount of energy the player looses if he moves.
    /// </summary>
    public float PlayerMoveEnergyLoss = 0.16F;

    /// <summary>
    /// The loss of energy (in percent, 0F to 100F) the player gets when jumping.
    /// </summary>
    public float PlayerJumpEnergyLoss = 5F;

    #endregion

    #region Members

    /// <summary>
    /// The current health of the player.
    /// </summary>
    public int Health
    {
        get
        {
            return this.mHealth;
        }
        set
        {
            if (value <= 0)
            {
                if (this.OnPlayerDie != null)
                    this.OnPlayerDie(this, EventArgs.Empty);
                // Die code
                SceneManager.LoadScene("Level0");
            }
            if (value > this.MaxHealth) return;
            this.mHealth = value;
            if (value > 0 && this.OnPlayerHealthChanged != null)
                this.OnPlayerHealthChanged(this, new PlayerHealthChangedEventArgs(this.mHealth));
        }
    }
    private int mHealth;

    /// <summary>
    /// The energy of the player.
    /// </summary>
    public float Energy
    {
        get
        {
            return this.mEnergy;
        }
        set
        {
            if (value < 0) value = 0;
            if (value > 100) value = 100F;
            this.mEnergy = value;
            if (this.OnPlayerEnergyChanged != null)
                this.OnPlayerEnergyChanged(this, new PlayerEnergyChangedEventArgs(this.mEnergy));
        }
    }
    private float mEnergy;

    #endregion

    #region Input Members

    private float mInputXMovement;
    private bool mInputJump;

    #endregion

    #region Events

    /// <summary>
    /// Gets invoked just before the player dies.
    /// </summary>
    public event EventHandler OnPlayerDie;

    /// <summary>
    /// Gets invoked when the health of the player changed. It does not get invoked when the player dies.
    /// </summary>
    public event EventHandler<PlayerHealthChangedEventArgs> OnPlayerHealthChanged;

    /// <summary>
    /// Gets invoked when the energy of the player changed.
    /// </summary>
    public event EventHandler<PlayerEnergyChangedEventArgs> OnPlayerEnergyChanged;

    #endregion

    /// <summary>
    /// Init of this code.
    /// </summary>
    public void Start()
    {
        //this.Health = this.MaxHealth;
	}
	
	/// <summary>
    /// Frame loop
    /// </summary>
	public void Update()
    {
        this.UpdateInputs();
    }

    /// <summary>
    /// Fixed game loop.
    /// </summary>
    public void FixedUpdate()
    {
        // The player just died / was just spawned in. Set the player variables to their defaults.
        if (this.Health == 0)
        {
            this.Health = this.MaxHealth;
            this.Energy = 100F;
        }
        // Regen energy
        this.Energy += this.EnergyRegen;
        this.UpdateMovement();
    }

    /// <summary>
    /// Updates the input members.
    /// </summary>
    private void UpdateInputs()
    {
        this.mInputXMovement = Input.GetAxisRaw("Horizontal");
        this.PlayerAnimator.SetFloat("PlayerSpeed", Mathf.Abs(this.mInputXMovement));
        if (Input.GetButtonDown("Jump") && this.Energy >= this.PlayerJumpEnergyLoss)
        {
            this.mInputJump = true;
            if (this.CharacterController.m_Grounded)
                this.Energy -= this.PlayerJumpEnergyLoss;
        }
        if (Input.GetButtonDown("Fire1")) this.Health -= 5;
    }

    /// <summary>
    /// Updates the movement of the player. This function has to be run out of the physics loop (FixedUpdate loop)
    /// </summary>
    private void UpdateMovement()
    {
        float xMovement = this.mInputXMovement;
        xMovement *= this.MovementSpeed;
        xMovement *= this.MOVE_SPEED_COEFF;
        xMovement *= Time.fixedDeltaTime;
        // Call the character controller to move the player.
        this.CharacterController.Move(xMovement, false, this.mInputJump);
        // Reset the jump request bool.
        this.mInputJump = false;
        // Check for falling death
        if (this.transform.position.y < this.FallDeathLevel) this.Die();
        // Update the animation
        this.PlayerAnimator.SetBool("IsJumping", !this.CharacterController.m_Grounded);
    }

    #region Player Methods

    /// <summary>
    /// Kills the player.
    /// </summary>
    public void Die()
    {
        this.Health = 0;
    }

    ///// <summary>
    ///// Gets invoked when the player lands on ground.
    ///// </summary>
    //public void OnPlayerLanding()
    //{
    //    this.PlayerAnimator.SetBool("IsJumping", false);
    //}

    #endregion
}

public class PlayerHealthChangedEventArgs : EventArgs
{
    /// <summary>
    /// The new health of the player.
    /// </summary>
    public int NewHealth
    {
        get; private set;
    }

    /// <summary>
    /// Creates a new instance of this class.
    /// </summary>
    /// <param name="newHealth">The new health of the palyer</param>
    public PlayerHealthChangedEventArgs(int newHealth)
    {
        this.NewHealth = newHealth;
    }
}

public class PlayerEnergyChangedEventArgs : EventArgs
{
    /// <summary>
    /// The new energy level of the player.
    /// </summary>
    public float NewEnergy
    {
        get; private set;
    }

    /// <summary>
    /// Creates a new instance of this class.
    /// </summary>
    /// <param name="newEnergy">The new energy level of the player</param>
    public PlayerEnergyChangedEventArgs(float newEnergy)
    {
        this.NewEnergy = newEnergy;
    }
}