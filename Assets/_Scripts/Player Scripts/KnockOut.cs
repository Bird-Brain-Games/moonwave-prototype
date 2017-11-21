using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockOut : MonoBehaviour {

 
    Rigidbody m_rigidBody;
    PlayerStats m_PlayerStats;
    PlayerStateManager m_StateManager;
    Shield m_shield;

    // Logging    l_ is used to indicate a variable is a logging variable
    public int l_deaths;
	// Use this for initialization
	void Start () {
        m_rigidBody = GetComponent<Rigidbody>();
        m_PlayerStats = GetComponent<PlayerStats>();
        m_StateManager = GetComponent<PlayerStateManager>();
        m_shield = GetComponentInChildren<Shield>();
        l_deaths = 0;
    }
	

	public void PlayerKnockedOut ()
    {
        // SFX
        FindObjectOfType<AudioManager>().Play("Death");
        
        // The player who hit them out gets 2 points [Jack]
        if (m_PlayerStats.m_HitLastBy != null)
        {
            m_PlayerStats.m_HitLastBy.m_Score += 2;

            // Log who player was killed by [Jack]
            m_PlayerStats.l_killedBy[m_PlayerStats.m_HitLastBy.m_PlayerID]++;
        }
        else
        {
            // Log that the player killed themself [Jack]
            m_PlayerStats.l_killedBy[m_PlayerStats.m_PlayerID]++;
        }

        // The player who died loses a point [Jack]
        m_PlayerStats.m_Score--;

        // Reset m_HitLastBy for respawning [Jack]
        m_PlayerStats.m_HitLastBy = null;

        ResetPlayer();

        // Logging
        l_deaths++;
    }

    void ResetPlayer()
    {
        m_rigidBody.ResetInertiaTensor();
        transform.position = new Vector3(12, 11, 0);
        m_rigidBody.velocity = new Vector3(0f, 0f, 0f);
        m_StateManager.ResetPlayer();
        m_shield.ResetShield();//
    }
}
