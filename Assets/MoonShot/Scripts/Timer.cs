using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    /*
     * Used for the timer in the corner
     */

    Text m_Text;            // The text for the timer
    bool m_OutOfTime;       // If the timer is out of time
    public float m_MaxTime; // The starting time on the timer
    float m_Time;           // The current timer value

	// Use this for initialization
	void Start () {
        m_Text = GetComponent<Text>();
        m_OutOfTime = false;
        m_Time = m_MaxTime;
	}
	
	// Update is called once per frame
	void Update () {
        // If the timer is still running, decrease the timer
        // If it is out of time, tell the program the timer is done.
        if (!OutOfTime())
        {
            m_Time -= Time.deltaTime;
            if (m_Time < 1.0f)
            {
                m_Time = 0.0f;
                m_OutOfTime = true;
            }
        }

        // Update the timer text
        if (((int)m_Time) % 60 < 10)
            m_Text.text = (((int)m_Time) / 60) + ":0" + (((int)m_Time) % 60);
        else
            m_Text.text = (((int)m_Time) / 60) + ":" + (((int)m_Time) % 60);
    }

    public bool OutOfTime()
    {
        return m_OutOfTime;
    }
}
