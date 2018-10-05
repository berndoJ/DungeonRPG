using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class F2EasterEgg : MonoBehaviour
{
    /// <summary>
    /// Text of the easter egg.
    /// </summary>
    public Text F2EasterEggText;
	
    /// <summary>
    /// Init of this code.
    /// </summary>
    public void Start()
    {
        this.F2EasterEggText.enabled = false;
    }

	/// <summary>
    /// Frame loop.
    /// </summary>
	void Update ()
    {
		if (Input.GetButtonDown("Whats here"))
        {
            this.F2EasterEggText.enabled = !this.F2EasterEggText.enabled;
        }
	}
}
