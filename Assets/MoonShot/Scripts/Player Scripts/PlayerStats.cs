using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Drifting = 1,
    Grounded = 2,
    Stunned = 4,
    Orbiting = 8
}

public class PlayerStats : MonoBehaviour {


    public PlayerState m_PlayerState;

    public Color colourdull;
    public Color colour;

    bool m_boostState;
    public bool m_shieldState;

    public float m_CriticalMultiplyer;

	void Start () {
        m_PlayerState = PlayerState.Drifting;
        m_shieldState = true;

    }

    //Getters and Setters
    public bool GetBoostState()
    {
        return m_boostState;
    }
    
    public void SetBoostState(bool p_state)
    {
        m_boostState = p_state;
    }

    public bool GetShieldState()
    {
        return m_shieldState;
    }

    public void SetShieldState(bool p_state)
    {
        m_shieldState = p_state;
    }

    public float GetCriticalMultiplyer()
    {
        //if shield is active return 1 as multiplyer
        
        if (m_shieldState == true)
        {
            Debug.Log("Critical fail");
            return 1;
        }
        //if shield is deactivated return critical multiplayer.
        else
        {
            Debug.Log("Critical hit");
            return m_CriticalMultiplyer;
        }
    }
}
