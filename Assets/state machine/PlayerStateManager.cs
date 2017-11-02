using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour {

	StateManager movementStates;
	PlayerStats playerStats;

#region movementStates
    PlayerDriftState driftState;
    PlayerOnPlanetState onPlanetState;
    PlayerBoostChargeState boostChargeState;
    PlayerBoostActiveState boostActiveState;
#endregion

	void Awake()
	{
		movementStates = gameObject.AddComponent<StateManager>();
		driftState = gameObject.AddComponent<PlayerDriftState>();
        onPlanetState = gameObject.AddComponent<PlayerOnPlanetState>();
        boostChargeState = gameObject.AddComponent<PlayerBoostChargeState>();
        boostActiveState = gameObject.AddComponent<PlayerBoostActiveState>();
	}

	// Use this for initialization
	void Start () {
        playerStats = GetComponent<PlayerStats>();
        movementStates.attachDefaultState(playerStats.PlayerDriftStateString, driftState);
        movementStates.attachState(playerStats.PlayerOnPlanetStateString, onPlanetState);
        movementStates.attachState(playerStats.PlayerBoostActiveString, boostActiveState);
        movementStates.attachState(playerStats.PlayerBoostChargeString, boostChargeState);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
