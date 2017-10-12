using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutOfTime : MonoBehaviour {

    public Text timerText;
    Text m_Text;
    Timer m_Timer;

	// Use this for initialization
	void Start () {
        m_Timer = timerText.GetComponent<Timer>();
        m_Text = GetComponent<Text>();
        m_Text.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (m_Timer.OutOfTime() && !m_Text.enabled)
        {
            m_Text.enabled = true;

        }
    }
}
