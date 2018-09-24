using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthDisplay : MonoBehaviour
{
    /// <summary>
    /// The player of which the health should be displayed.
    /// </summary>
    public PlayerBehavior Player;

    /// <summary>
    /// The text UI component where the health should be displayed.
    /// </summary>
    public Text HealthDisplayText;

	/// <summary>
    /// Init of this code.
    /// </summary>
	public void Start()
    {
        this.Player.OnPlayerHealthChanged += this.OnPlayerHealthChanged;
	}
	
    /// <summary>
    /// Gets invoked when the health of the player attached changed.
    /// </summary>
    /// <param name="sender">The sender of the event</param>
    /// <param name="e">The event args</param>
	private void OnPlayerHealthChanged(object sender, PlayerHealthChangedEventArgs e)
    {
        this.HealthDisplayText.text = string.Format("{0} / {1}", e.NewHealth, Player.MaxHealth);
    }
}
