using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainState : State {


	// Use this for initialization
	public MainState(GameObject a_accessor) : base(a_accessor){
        SetIsOn(true);
        Debug.Log("created main state");
    }

    override
    public void StateUpdate()
    {
        Debug.Log("Main update state");
        if (Input.GetKey(KeyCode.KeypadEnter))
        {
            SetIsOn(false);
        }
    }
}
