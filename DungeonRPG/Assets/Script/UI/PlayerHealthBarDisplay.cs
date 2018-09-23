using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBarDisplay : MonoBehaviour
{

    /// <summary>
    /// The player of which the health should be displayed.
    /// </summary>
    public PlayerBehavior Player;

    /// <summary>
    /// The image to fill according to the health of the player.
    /// </summary>
    public Image HealthBarImage;

    /// <summary>
    /// Init of this code.
    /// </summary>
    void Start ()
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
        this.HealthBarImage.fillAmount = (float)e.NewHealth / this.Player.MaxHealth;
    }

}
