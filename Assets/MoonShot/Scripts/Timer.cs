using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    Text m_Text;
    public float m_Time;

	// Use this for initialization
	void Start () {
        m_Text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        // Reduce the timer
        m_Time -= Time.deltaTime;

        // Update the timer text
        if (((int)m_Time) % 60 < 10)
            m_Text.text = (((int)m_Time) / 60) + ":0" + (((int)m_Time) % 60);
        else
            m_Text.text = (((int)m_Time) / 60) + ":" + (((int)m_Time) % 60);
    }
}
