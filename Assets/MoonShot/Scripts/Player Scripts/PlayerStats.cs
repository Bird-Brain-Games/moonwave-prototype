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
	// Use this for initialization
	void Start () {
        m_PlayerState = PlayerState.Drifting;
	}

    public bool GetBoostState()
    {
        return m_boostState;
    }
    
    public void SetBoostState(bool p_state)
    {
        m_boostState = p_state;
    }
}
