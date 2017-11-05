using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunShot : MonoBehaviour {

	public float duration {get; set;}
	public int playerNum {get; set;}
	public PlayerStats m_PlayerStats {get; set;}
	public Vector2 direction {get; set;}
	public float force {get; set;}
	float currentDuration {get; set;}

	// Use this for initialization
	void Start () {
		currentDuration = duration;
	}
	
	// Update is called once per frame
	void Update () {
		currentDuration -= Time.deltaTime;
		if (currentDuration <= 0f)
			Destroy(gameObject);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			// If the player owns the bullet, don't collide
			int otherPlayerNum = other.gameObject.GetComponent<Controls>().playerNumber;
			if (playerNum != otherPlayerNum)
            	collideWithPlayer(other);
		}
	}

	void collideWithPlayer(Collider other)
	{
		Shield m_shield = other.gameObject.GetComponentInChildren<Shield>();
		Vector3 addForce;


		other.gameObject.GetComponent<PlayerStats>().m_HitLastBy = m_PlayerStats;

		// If the shield has health, change how much force 
		if (m_shield.m_shieldHealth == 0)
		{
			addForce = direction * force * 
				other.gameObject.GetComponent<PlayerStats>().m_CriticalMultipier;
			//Debug.Log("shot criticaly hit player");
		}
		else
		{
			addForce = direction * force;
			//Debug.Log("shot hit player ");
			
		}

		// Add the force [Graham]
		other.GetComponent<Rigidbody>().AddForce(addForce, ForceMode.Impulse);
		//Debug.Log(addForce + " applied to player");

		// Tell the shield to be hit
		m_shield.ShieldHit(Shield.BULLET_TYPE.shotgun);
	}
}
