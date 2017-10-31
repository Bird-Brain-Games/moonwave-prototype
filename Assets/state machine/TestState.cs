using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestState : State {

    // Use this for initialization
    public TestState(GameObject a_accessor) : base(a_accessor){
        Debug.Log("created test state");
    }

    override
    public void StateUpdate()
    {
        Debug.Log("Testing update state");
        if (Input.GetKey(KeyCode.Space))
        {
            SetIsOn(false);
        }
    }
}
