﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountIn : StateMachineBehaviour {

    public Timer timer;
    Text countText;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        countText = animator.GetComponent<Text>();
        timer = animator.GetComponentInChildren<Timer>();
        countText.enabled = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // The normalized Time tells how many times the state has looped
        // We use this to determine the number it should show in the text
        // [Graham]
        if (stateInfo.normalizedTime < 3.0f)
        {
            int count = 4 - Mathf.CeilToInt(stateInfo.normalizedTime);
            countText.text = count.ToString();
        }
        else
        {
            // Show the start text
            countText.text = "Start!";
            animator.SetTrigger("Start Match");
        }            
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Start the timer and trigger the next animation [Graham]
        countText.text = "Time!";
        countText.enabled = false;
        animator.ResetTrigger("Start Match");
        timer.Show();
    }
}
