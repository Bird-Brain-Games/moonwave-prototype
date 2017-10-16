using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutOfTime : MonoBehaviour {

    /*
     * Used for the large "Out of time" text
     */

    public Text timerText;  // Reference to the UI element of the timer
    Text m_Text;    // The text component of the game object
    Timer timer;  // Reference to the Timer component of the timer

	// Use this for initialization
	void Start () {
        timer = timerText.GetComponent<Timer>();
        m_Text = GetComponent<Text>();
        m_Text.enabled = false;     // Disable the ending text when the game is playing
	}
	
	// Update is called once per frame
	void Update () {
        // If the timer is out of time, trigger the end of game text
        if (timer.OutOfTime() && !m_Text.enabled)
        {
            m_Text.enabled = true;

        }
    }
}
