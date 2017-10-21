using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutOfTime : MonoBehaviour {

    public GameObject timerObject;
    Text m_Text;
    Timer timer;
    Animator m_Animator;

	// Use this for initialization
	void Start () {
        timer = timerObject.GetComponent<Timer>();
        m_Animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        // Update the animator time control (for transitions) [Graham]
        m_Animator.SetFloat("time", timer.GetTime());

        // If the match is finished, tell the animator [Graham]
        if (timer.OutOfTime())
        {
            timer.Hide();
            m_Animator.SetTrigger("End Match"); 
        }
    }
}
