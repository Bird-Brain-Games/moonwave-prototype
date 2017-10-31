using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{

    //Holds all of our items in it.
    
    public Dictionary<string, State> states;
    TestState testState;
    MainState mainState;
    // Use this for initialization
    void Start()
    {
        states = new Dictionary<string, State>();
        testState = new TestState(gameObject);
        mainState = new MainState(gameObject);

        states.Add("main state", mainState);
        states.Add("test state", testState);
        
    }

    public State GetStates(string name)
    {
        if (states.ContainsKey(name))
        {
            return states[name];
        }
        else
            return null;
    }


    // Update is called once per frame
    void Update()
    {
        foreach (State element in states.Values)
        {
            if (element.GetIsOn())
                element.StateUpdate();
        }
    }
}
