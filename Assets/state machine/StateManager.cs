using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{

    //Holds all of our items in it.
    PlayerStats playerStats;

    State currentState;
    State defaultState;

    Dictionary<string, State> states;
    PlayerDriftState driftState;
    PlayerOnPlanetState onPlanetState;

    // Use this for initialization
    void Awake()
    {
        driftState = gameObject.AddComponent<PlayerDriftState>();
        onPlanetState = gameObject.AddComponent<PlayerOnPlanetState>();
        playerStats = GetComponent<PlayerStats>();
    }

    void Start()
    {
        currentState = driftState;
        defaultState = onPlanetState;
        

        //states.Add(playerStats.PlayerDriftStateString, driftState);
        //states.Add(playerStats.PlayerMovementOnPlanetString, onPlanetState);

    }

    // Make sure to only change state in the late update of the state
    // So that all the updates can call first [G, C]
    public void ChangeState(string a_State)
    {
        currentState.Exit();
        //currentState = states[a_State];
        currentState = defaultState;
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
