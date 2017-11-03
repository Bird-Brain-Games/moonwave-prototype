using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	Rigidbody m_Rigidbody;

	// Layer masks for collision
	int planetLayer;
	int playerLayer;

	public int playerNum {get; set;}
	public float force {get; set;}
	public Vector3 initialVelocity;
	public PlayerStats m_PlayerStats {get; set;}

	// Use this for initialization
	void Start () {
		m_Rigidbody = GetComponent<Rigidbody>();
		planetLayer = 8;//LayerMask.GetMask("Planet");
		playerLayer = 9;//LayerMask.GetMask("Player");
	}

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
        if (layer == planetLayer) // If the object is a planet
        {
            Destroy(gameObject, 0);  // Destroys bullets when they hit a planet
        }

        //if we have collided with a player.
        else if (layer == playerLayer)
        {
			// If the player owns the bullet, don't collide
			int otherPlayerNum = collision.gameObject.GetComponent<Controls>().playerNumber;
			if (playerNum != otherPlayerNum)
            	collideWithPlayer(collision);
        }

    }

	void collideWithPlayer(Collision collision)
	{
		Shield m_shield = collision.gameObject.GetComponentInChildren<Shield>();
		Vector3 direction = initialVelocity.normalized;
		//Debug.Log(m_Rigidbody.velocity);
		Vector3 addForce;


		collision.rigidbody.GetComponent<PlayerStats>().m_HitLastBy = m_PlayerStats;

		// If the shield has health, change how much force 
		if (m_shield.m_shieldHealth == 0)
		{
			addForce = direction * force * 
				collision.gameObject.GetComponent<PlayerStats>().m_CriticalMultipier;
			//Debug.Log("bullet criticaly hit player");
		}
		else
		{
			addForce = direction * force;
			//Debug.Log("bullet hit player ");
			
		}

		// Add the force [Graham]
		collision.rigidbody.AddForce(addForce, ForceMode.Impulse);
		//Debug.Log(addForce + " applied to player");

		// Tell the shield to be hit
		m_shield.ShieldHit(Shield.BULLET_TYPE.plasma);

		Destroy(gameObject, 0);
	}
}
