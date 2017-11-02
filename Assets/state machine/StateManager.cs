using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{

    //Holds all of our items in it.
    State currentState;
    State defaultState;

    Dictionary<string, State> states;

    void Awake()
    {
        states = new Dictionary<string, State>();
    }

    // Make sure to only change state in the late update of the state
    // So that all the updates can call first [G, C]
    public void ChangeState(string a_State)
    {
        currentState.Exit();
        currentState = states[a_State];
        currentState.Enter();

        Debug.Log("Changing to " + a_State);
    }

    public void attachState(string key, State s)
    {
        if (states.ContainsKey(key))    return; // If it's already in the list, don't add it [Graham]

        states.Add(key, s);
        if (currentState == null)
        {
            currentState = states[key];
        }
    }

    public void attachDefaultState(string key, State s)
    {
        attachState(key, s);
        defaultState = states[key];
    }

    // Update is called once per frame
    void Update()
    {
        currentState.StateUpdate();
    }

    // Update called each physics update
    void FixedUpdate()
    {
        currentState.StateFixedUpdate();
    }

    // Update called after other updates
    void LateUpdate()
    {
        currentState.StateLateUpdate();
        currentState.ChangeStateUpdate();
    }
}
