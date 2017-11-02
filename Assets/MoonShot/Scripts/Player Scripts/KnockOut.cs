using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockOut : MonoBehaviour {

 
    Rigidbody m_rigidBody;

    // Logging    l_ is used to indicate a variable is a logging variable
    public int l_deaths;
	// Use this for initialization
	void Start () {
        m_rigidBody = gameObject.GetComponent<Rigidbody>();
        l_deaths = 0;
    }
	
	// Update is called once per frame
	public void PlayerKnockedOut ()
    {
        // The player who hit them out gets 2 points [Jack]
        if (GetComponent<PlayerStats>().m_HitLastBy != null)
        {
            GetComponent<PlayerStats>().m_HitLastBy.m_Score += 2;

            // Log who player was killed by [Jack]
            GetComponent<PlayerStats>().l_killedBy[GetComponent<PlayerStats>().m_HitLastBy.m_PlayerID]++;
        }
        else
        {
            // Log that the player killed themself [Jack]
            GetComponent<PlayerStats>().l_killedBy[GetComponent<PlayerStats>().m_PlayerID]++;
        }

        // The player who died loses a point [Jack]
        GetComponent<PlayerStats>().m_Score--;

        // Reset m_HitLastBy for respawning [Jack]
        GetComponent<PlayerStats>().m_HitLastBy = null;

        Debug.Log("KNOCKOUT!!!!1!!!!!");
        transform.position = new Vector3(9, 11, 0);
        m_rigidBody.velocity = new Vector3(0f, 0f, 0f);

        // Logging
        l_deaths++;
    }
}
