using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    /// <summary>
    /// The label the text should be displayed to.
    /// </summary>
    public Text FPSCounterLabel;

    /// <summary>
    /// Enable bool for enabling the display of the text.
    /// </summary>
    public bool Enabled;

    /// <summary>
    /// Init of this code.
    /// </summary>
    public void Start()
    {
        this.StartCoroutine("UpdateFPSCounter");
	}

    /// <summary>
    /// Frame loop.
    /// Here the frame loop is used for inputs.
    /// </summary>
    public void Update()
    {
        if (Input.GetButtonDown("FPS Toggle"))
        {
            this.Enabled = !this.Enabled;
        }
    }

    /// <summary>
    /// Coroutine for displaying the fps on screen
    /// </summary>
    /// <returns></returns>
    public IEnumerator UpdateFPSCounter()
    {
        while (true)
        {
            if (Enabled)
                this.FPSCounterLabel.text = string.Format("{0:0.0} FPS", 1 / Time.deltaTime);
            else
                this.FPSCounterLabel.text = string.Empty;
            yield return new WaitForSeconds(0.5F);
        }
    }
}
