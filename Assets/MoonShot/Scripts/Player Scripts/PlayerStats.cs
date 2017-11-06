using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    [System.Serializable]
    public struct Boost
    {
        //The default boost force
        public float BaseForce;
        //The force that is added for every second of charge
        public float AddedForcePerSecond;
        //The max force a boost can have
        public float MaxForce;
        //The default boost duration
        public float BaseDuration;
        //the added time a boost lasts for, for every second of charge
        public float AddedDurationPerSecond;
        //The max duration a boost can last for
        public float MaxDuration;
        //The time it takes for a boost to be ready agian.
        public float Cooldown;
    }

    [System.Serializable]
    public struct Shoot
    {
        // Shotgun based variables
        [Tooltip("The time (s) it takes to reach maximum size and disappear")]
        public float shotgunDuration;
        [Tooltip("The length of time (s) between shotgun shots")]
        public float shotgunCooldown;
        [Tooltip("The force of the shotgun bullets on the player")]
        public float shotgunForce;
        [Tooltip("How far the shotgun shot travels")]
        public float shotgunDistance;
        [Tooltip("The size of the cone of effect")]
        public float shotgunRadius;
        public float shotgunCriticalMultiplier;
    }

#region  StateStrings
    public string PlayerOnPlanetStateString {get; set;}   
    public string PlayerDriftStateString {get; set;}
    public string PlayerBoostChargeString {get; set;}
    public string PlayerBoostActiveString {get; set;}
#endregion

    public Color colourdull;
    public Color colour;

    // Score Calculations
    public int m_PlayerID;
    public PlayerStats m_HitLastBy;
    public int m_Score;

    // Killed by (Logging) [Jack]
    public int[] l_killedBy;

    public bool m_shieldState;

    public float m_CriticalMultipier;

    // Drift based variables
    public float driftMoveForce;
    public float walkMoveForce;

    // Grounded based variables
    public float jumpForce;
	public float maxStunTime;

    public Shoot m_Shoot;
    public Boost m_boost;

    //Determines whether we can boost or not.
    public bool CanBoost { get; set; }

    // Stunned variables [Graham]
    public bool stunTrigger {get; set;}



	void Awake () {
        m_HitLastBy = null;
        m_Score = 0;
        m_shieldState = true;
        CanBoost = true;
        l_killedBy = new int[4];
        stunTrigger = false;

        // Making them small strings, easier to compare (probably change to ints) [Graham]
        PlayerOnPlanetStateString = "onPlanet";
        PlayerDriftStateString = "drift";
        PlayerBoostActiveString = "boostActive";
        PlayerBoostChargeString = "boostCharge";
    }

    //Getters and Setters

    public int getScore()
    {
        return m_Score;
    }
    
    public bool GetShieldState()
    {
        return m_shieldState;
    }

    public void SetShieldState(bool p_state)
    {
        m_shieldState = p_state;
    }

    public float GetCriticalMultiplier()
    {
        //if shield is active return 1 as multiplier
        
        if (m_shieldState == true)
        {
            //Debug.Log("Critical fail");
            return 1;
        }
        //if shield is deactivated return critical multiplier.
        else
        {
            Debug.Log("Critical hit");
            return m_CriticalMultipier;
        }
    }
}
