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
    PlayerBoostChargeState boostChargeState;
    PlayerBoostActiveState boostActiveState;

    // Use this for initialization
    void Awake()
    {
        driftState = gameObject.AddComponent<PlayerDriftState>();
        onPlanetState = gameObject.AddComponent<PlayerOnPlanetState>();
        boostChargeState = gameObject.AddComponent<PlayerBoostChargeState>();
        boostActiveState = gameObject.AddComponent<PlayerBoostActiveState>();
        
        playerStats = GetComponent<PlayerStats>();

        
    }

    void Start()
    {
        currentState = driftState;
        defaultState = onPlanetState;

        states = new Dictionary<string, State>();
        states.Add(playerStats.PlayerDriftStateString, driftState);
        states.Add(playerStats.PlayerOnPlanetStateString, onPlanetState);
        states.Add(playerStats.PlayerBoostActiveString, boostActiveState);
        states.Add(playerStats.PlayerBoostChargeString, boostChargeState);

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
