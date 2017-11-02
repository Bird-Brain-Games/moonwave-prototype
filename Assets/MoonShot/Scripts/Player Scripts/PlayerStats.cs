using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

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

    //The force a boost impacts
    public float boostBaseImpact;
    //The force that is added for every second of charge
    public float boostAddedCharge;


	void Awake () {
        m_HitLastBy = null;
        m_Score = 0;
        m_shieldState = true;
        l_killedBy = new int[4];

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
            Debug.Log("Critical fail");
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
