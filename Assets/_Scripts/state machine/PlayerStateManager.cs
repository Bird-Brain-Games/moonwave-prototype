using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{

    StateManager movementStates;
    PlayerStats playerStats;
    StickToPlanet m_Gravity;
    Rigidbody m_RigidBody;

    float stunTimer;
    bool isCountingDown;

    #region movementStates
    PlayerDriftState driftState;
    PlayerJumpState jumpState;
    PlayerOnPlanetState onPlanetState;
    PlayerBoostChargeState boostChargeState;
    PlayerBoostActiveState boostActiveState;
    PLayerBigHitState bigHitState;
    RespawnState respawnState;
    #endregion

    void Awake()
    {
        movementStates = gameObject.AddComponent<StateManager>();
        driftState = gameObject.AddComponent<PlayerDriftState>();
        jumpState = gameObject.AddComponent<PlayerJumpState>();
        onPlanetState = gameObject.AddComponent<PlayerOnPlanetState>();
        boostChargeState = gameObject.AddComponent<PlayerBoostChargeState>();
        boostActiveState = gameObject.AddComponent<PlayerBoostActiveState>();
        bigHitState = gameObject.AddComponent<PLayerBigHitState>();
        respawnState = gameObject.AddComponent<RespawnState>();
    }

    // Use this for initialization
    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        m_Gravity = GetComponent<StickToPlanet>();
        m_RigidBody = GetComponent<Rigidbody>();

        movementStates.AttachDefaultState(playerStats.PlayerDriftStateString, driftState);
        movementStates.AttachDefaultState(playerStats.PlayerJumpStateString, jumpState);
        movementStates.AttachState(playerStats.PlayerOnPlanetStateString, onPlanetState);
        movementStates.AttachState(playerStats.PlayerBoostActiveString, boostActiveState);
        movementStates.AttachState(playerStats.PlayerBoostChargeString, boostChargeState);
        movementStates.AttachState(playerStats.PlayerBigHitState, bigHitState);
        movementStates.AttachState(playerStats.PlayerRespawnState, respawnState);

        stunTimer = 3f;
        movementStates.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // If the trigger is sent to stun the player
        if (playerStats.stunTrigger)
        {
            StunPlayer();
        }

        // If the player is stunned
        if (stunTimer > 0f)
        {
            // Update the stun timer [Graham]
            stunTimer -= Time.deltaTime;
            if (stunTimer <= 0f)
            {
                UnStunPlayer();
            }

            // Cause the stunned player to be affected by gravity[Graham]
            //m_RigidBody.AddForce(m_Gravity.DriftingUpdate() * 0.5f);
        }
    }

    public void ResetPlayer()
    {
        UnStunPlayer();
        movementStates.ChangeState(playerStats.PlayerRespawnState);
    }

    void StunPlayer()
    {
        playerStats.stunTrigger = false;
        stunTimer = playerStats.StunTimer;
        movementStates.enabled = false;
        GetComponent<Collider>().material.bounciness = 1.0f;
        GetComponent<Collider>().material.bounceCombine = PhysicMaterialCombine.Maximum;
    }

    void UnStunPlayer()
    {
        movementStates.enabled = true;
        movementStates.ResetToDefaultState();
        stunTimer = 0f;
        GetComponent<Collider>().material.bounciness = 0.0f;
        GetComponent<Collider>().material.bounceCombine = PhysicMaterialCombine.Average;
    }

    public void ChangeState(string a_State)
    {
        movementStates.ChangeState(a_State);
    }
}
