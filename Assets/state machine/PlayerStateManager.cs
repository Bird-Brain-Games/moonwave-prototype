using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour {

	StateManager movementStates;
	PlayerStats playerStats;
	StickToPlanet m_Gravity;

	float stunTimer;
	bool isCountingDown;

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
		m_Gravity = GetComponent<StickToPlanet>();
        movementStates.AttachDefaultState(playerStats.PlayerDriftStateString, driftState);
        movementStates.AttachState(playerStats.PlayerOnPlanetStateString, onPlanetState);
        movementStates.AttachState(playerStats.PlayerBoostActiveString, boostActiveState);
        movementStates.AttachState(playerStats.PlayerBoostChargeString, boostChargeState);
		
	}
	
	// Update is called once per frame
	void Update () {
		// If the trigger is sent to stun the player
		if (playerStats.stunTrigger)
		{
			playerStats.stunTrigger = false;
			stunTimer = playerStats.maxStunTime;
			movementStates.enabled = false;
		}

		// If the player is stunned
		if (stunTimer > 0f)
		{
			// Update the stun timer [Graham]
			stunTimer -= Time.deltaTime;
			if (stunTimer <= 0f)
			{
				stunTimer = 0f;
				movementStates.enabled = true;
				movementStates.ResetToDefaultState();
			}

			// Cause the stunned player to be affected by gravity[Graham]
			m_Gravity.DriftingUpdate();
		}
	}
}
