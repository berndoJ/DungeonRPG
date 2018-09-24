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

    private readonly int PLAYER_FALLING_Y_DEATH = -10;

    #endregion

    #region General Variables

    /// <summary>
    /// The movement speed of the player.
    /// </summary>
    public int MovementSpeed = 10;

    /// <summary>
    /// The maximum health that a player can have.
    /// </summary>
    public int MaxHealth = 5;

    /// <summary>
    /// The character controller script.
    /// </summary>
    public CharacterController2D CharacterController;

    #endregion

    #region Members

    /// <summary>
    /// The current health of the player.
    /// </summary>
    private int Health
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
            this.mHealth = value;
            if (value > 0 && this.OnPlayerHealthChanged != null)
                this.OnPlayerHealthChanged(this, new PlayerHealthChangedEventArgs(this.mHealth));
        }
    }
    private int mHealth;

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
        if (this.Health == 0) this.Health = this.MaxHealth;
        this.UpdateMovement();
    }

    /// <summary>
    /// Updates the input members.
    /// </summary>
    private void UpdateInputs()
    {
        this.mInputXMovement = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump")) this.mInputJump = true;
        if (Input.GetButtonDown("Fire1")) this.Health -= 5;
    }

    /// <summary>
    /// Updates the movement of the player.
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
        if (this.transform.position.y < this.PLAYER_FALLING_Y_DEATH) this.Die();
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