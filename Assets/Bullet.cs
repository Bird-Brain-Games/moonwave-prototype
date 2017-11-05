using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile {
	public Vector3 m_InitialVelocity;

	public void BulletOutOfBounds()
    {
            //Debug.Log("bullet out of bounds");
            Destroy(gameObject, 0.0f); 
    }

	void OnCollisionEnter(Collision collision)
    {
        //sets what layer we have collided with
        int layer = collision.gameObject.layer;

        // If the bullet collides with a planet, destroy it [Robbie]
        if (layer == m_PlanetLayer) // If the object is a planet
        {
            Destroy(gameObject, 0);  // Destroys bullets when they hit a planet
        }

        //if we have collided with a player.
        else if (layer == m_PlayerLayer)
        {
            collideWithPlayer(collision);
			Destroy(gameObject, 0);
        }
    }

	void collideWithPlayer(Collision other)
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
		other.gameObject.GetComponent<Rigidbody>().AddForce(addForce, ForceMode.Impulse);

		// Tell the shield to be hit
		m_shield.ShieldHit(Shield.BULLET_TYPE.plasma);
	}
}
