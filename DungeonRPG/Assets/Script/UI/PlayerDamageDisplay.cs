using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamageDisplay : MonoBehaviour
{
    /// <summary>
    /// The player object reference.
    /// </summary>
    public PlayerBehavior Player;

    /// <summary>
    /// The text object to display the text on.
    /// </summary>
    public Text DamageDisplayText;

    /// <summary>
    /// Integer containing the damage amount to be displayed on screen.
    /// </summary>
    private int mDamageToDisplay
    {
        get { return this.m_mDamageToDisplay; }
        set
        {
            this.m_mDamageToDisplay = value;
            if (!this.DamageDisplayText.enabled)
            {
                this.StartCoroutine("DisplayDamageCoroutine");
            }
        }
    }
    private int m_mDamageToDisplay;

    /// <summary>
    /// Init of this code.
    /// </summary>
	public void Start ()
    {
        this.Player.OnPlayerTakeDamage += this.OnPlayerTakeDamage;
        this.DamageDisplayText.enabled = false;
	}

    /// <summary>
    /// Gets invoked when the player takes damage.
    /// </summary>
    /// <param name="e">The event arguments</param>
    public void OnPlayerTakeDamage(object sender, PlayerTakeDamageEventArgs e)
    {
        this.mDamageToDisplay += e.Damage;
    }

    /// <summary>
    /// Coroutine that handles the display and stacking of the damage values.
    /// </summary>
    /// <returns>-</returns>
    public IEnumerator DisplayDamageCoroutine()
    {
        this.DamageDisplayText.enabled = true;
        int damageDisplayed = this.mDamageToDisplay;
        this.DisplayDamage(damageDisplayed);
        int counter = 0;
        while (true)
        {
            if (counter == 0)
            {
                this.DamageDisplayText.CrossFadeAlpha(0F, 0.5F, false);
            }
            counter++;
            if (this.mDamageToDisplay != damageDisplayed)
            {
                counter = 0;
                damageDisplayed = this.mDamageToDisplay;
                this.DisplayDamage(damageDisplayed);
            }
            if (counter >= 5)
            {
                break;
            }
            yield return new WaitForSeconds(0.1F);
        }
        this.mDamageToDisplay = 0;
        this.DamageDisplayText.enabled = false;
    }

    /// <summary>
    /// Displays the damage given on screen (text)
    /// </summary>
    /// <param name="damage">The damage amount (positive) to display.</param>
    public void DisplayDamage(int damage)
    {
        this.DamageDisplayText.CrossFadeAlpha(1F, 0F, false);
        this.DamageDisplayText.text = string.Format("-{0}", damage);
    }
}
