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
	PlayerJumpState jumpState;
    PlayerOnPlanetState onPlanetState;
    PlayerBoostChargeState boostChargeState;
    PlayerBoostActiveState boostActiveState;
#endregion

	void Awake()
	{
		movementStates = gameObject.AddComponent<StateManager>();
		driftState = gameObject.AddComponent<PlayerDriftState>();
		jumpState = gameObject.AddComponent<PlayerJumpState>();
        onPlanetState = gameObject.AddComponent<PlayerOnPlanetState>();
        boostChargeState = gameObject.AddComponent<PlayerBoostChargeState>();
        boostActiveState = gameObject.AddComponent<PlayerBoostActiveState>();
	}

	// Use this for initialization
	void Start () {
        playerStats = GetComponent<PlayerStats>();
		m_Gravity = GetComponent<StickToPlanet>();
		
        movementStates.AttachDefaultState(playerStats.PlayerDriftStateString, driftState);
		movementStates.AttachDefaultState(playerStats.PlayerJumpStateString, jumpState);
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

	public void ResetPlayer()
	{
		movementStates.enabled = true;
		movementStates.ResetToDefaultState();
		playerStats.stunTrigger = false;
		stunTimer = 0f;
		GetComponentInChildren<Shield>().ResetShield();		// TEMP [Graham]
	}
}
