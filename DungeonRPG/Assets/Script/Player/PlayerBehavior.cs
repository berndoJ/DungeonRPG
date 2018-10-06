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
    /// The regenerated energy per gameloop tick. Default: 0.07F
    /// </summary>
    public float EnergyRegen = 0.07F;

    /// <summary>
    /// The amount of energy the player looses if he moves. Default: 0.15F
    /// </summary>
    public float PlayerMoveEnergyLoss = 0.15F;

    /// <summary>
    /// The loss of energy (in percent, 0F to 100F) the player gets when jumping. Default: 10F
    /// </summary>
    public float PlayerJumpEnergyLoss = 10F;

    /// <summary>
    /// Determines the speed of the player if his energy-level is too low. Default: 0.15F
    /// </summary>
    public float PlayerDeenergisedMovementReductionCoeff = 0.15F;

    /// <summary>
    /// Determines the threshold beyond which the player moves slowly because of deenergisement. Default: 10F
    /// </summary>
    public float PlayerDeenergisedMovementReductionThreshold = 10F;

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
            bool damageFlag = (value < this.mHealth);
            int damage = this.mHealth - value;
            this.mHealth = value;
            if (value > 0 && this.OnPlayerHealthChanged != null)
                this.OnPlayerHealthChanged(this, new PlayerHealthChangedEventArgs(this.mHealth));
            if (value > 0 && damageFlag && this.OnPlayerTakeDamage != null)
                this.OnPlayerTakeDamage(this, new PlayerTakeDamageEventArgs(damage, this.mHealth));
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
            //if (this.OnPlayerEnergyChanged != null)
            //    this.OnPlayerEnergyChanged(this, new PlayerEnergyChangedEventArgs(this.mEnergy));
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
    //public event EventHandler<PlayerEnergyChangedEventArgs2> OnPlayerEnergyChanged;

    /// <summary>
    /// Gets invoked when the player takes damage.
    /// </summary>
    public event EventHandler<PlayerTakeDamageEventArgs> OnPlayerTakeDamage;

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
        // Movement and energy handling.
        if (this.Energy < this.PlayerDeenergisedMovementReductionThreshold)
            xMovement *= this.PlayerDeenergisedMovementReductionCoeff;
        this.Energy -= Mathf.Abs(this.mInputXMovement) * this.PlayerMoveEnergyLoss;
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

public class PlayerEnergyChangedEventArgs2 : EventArgs
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
    public PlayerEnergyChangedEventArgs2(float newEnergy)
    {
        this.NewEnergy = newEnergy;
    }
}

public class PlayerTakeDamageEventArgs : PlayerHealthChangedEventArgs
{
    /// <summary>
    /// The amount of damage dealt to the player.
    /// </summary>
    public int Damage
    {
        get; private set;
    }

    /// <summary>
    /// Creates a new instance of this class.
    /// </summary>
    /// <param name="damage">The damage dealt to the player.</param>
    /// <param name="newHealth">The new health of the player.</param>
    public PlayerTakeDamageEventArgs(int damage, int newHealth) : base (newHealth)
    {
        this.Damage = damage;
    }
}