using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunShot : Projectile {

	public float duration {get; set;}
	float currentDuration {get; set;}

	// Use this for initialization
	void Start()
	{
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
			addForce = m_Direction * m_Force * 
				other.gameObject.GetComponent<PlayerStats>().m_CriticalMultipier;
		}
		else
		{
			addForce = m_Direction * m_Force;
		}

		// Add the force [Graham]
		other.GetComponent<Rigidbody>().AddForce(addForce, ForceMode.Impulse);

		// Tell the shield to be hit
		m_shield.ShieldHit(Shield.BULLET_TYPE.shotgun);
	}
}
