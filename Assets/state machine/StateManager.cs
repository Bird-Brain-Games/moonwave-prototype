using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{

    //Holds all of our items in it.

    State currentState;
    State defaultState;
    TestState testState;
    MainState mainState;
    // Use this for initialization
    void Awake()
    {
        testState = gameObject.AddComponent<TestState>();
        mainState = gameObject.AddComponent<MainState>();
        //testState = new TestState(gameObject);
        //mainState = new MainState(gameObject);
        
    }

    void Start()
    {
        currentState = testState;
        defaultState = testState;
    }

    // Make sure to only change state in the late update of the state
    // So that all the updates can call first [G, C]
    public void ChangeState(State a_State)
    {
        currentState.Exit();
        currentState = a_State;
        currentState.Enter();
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
