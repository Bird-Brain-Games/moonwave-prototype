using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Owner : MonoBehaviour {

    // Use this for initialization
    GameObject m_owner;
    PlayerStats m_playerStats;
    Vector3 velocity;
    float m_impact;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public float GetCriticalMultiplyer()
    {
        return m_playerStats.GetCriticalMultiplyer();
    }

    public GameObject getOwner() { return m_owner; }
    public void setOwner(GameObject s_owner) { m_owner = s_owner; }

    public PlayerStats GetPlayerStats() { return m_playerStats; }
    public void SetPlayerStats(PlayerStats s_playerStates) { m_playerStats = s_playerStates; }

    public void setVelocity(Vector3 s_velocity) { velocity = s_velocity; }
    public Vector3 getVelocity() { return velocity; }

    public float GetImpact() { return m_impact; }
    public void SetImpact(float s_impact) { m_impact = s_impact; }

}
