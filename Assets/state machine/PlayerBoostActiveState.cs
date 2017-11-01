using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoostActiveState : State {

	Vector3 m_Direction;
	float m_ChargeForce;
	float m_timeRemaining;

	Rigidbody m_RigidBody;
	PlayerStats m_PlayerStats;
	Controls m_Controls;
    PlayerBoost m_Boost;

	// Use this for initialization
	void Start () {
		m_RigidBody = GetComponent<Rigidbody>();
		m_PlayerStats = GetComponent<PlayerStats>();
		m_Controls = GetComponent<Controls>();
        m_Boost = GetComponent<PlayerBoost>();
	}

	override public void Enter()
	{
		m_ChargeForce = m_Boost.getChargeForce();
		// Get the total "cooldown" until you can move again
		m_timeRemaining = 0f;
	}

	override public void StateUpdate()
	{
		// Move the "I can't move during this time" cooldown into this function,
		// And when the cooldown is done, change back to drifting state

		ChangeState(m_PlayerStats.PlayerDriftStateString);
	}

	void OnCollisionEnter(Collision collision)
    {
        // Collide with a player
        if (collision.gameObject.CompareTag("Player"))
        {
            //sets the direction of the force
            var force = collision.transform.position - transform.position;
            force.Normalize();

            //Adds the force to the player we collided with
            collision.rigidbody.AddForce(force 
                * (m_PlayerStats.boostBaseImpact + 
				(m_PlayerStats.boostAddedCharge * m_ChargeForce)) 
                * m_PlayerStats.GetCriticalMultiplier());

            //adds inverse force to us to signifiy knockback.
            //m_rigidbody.AddForce(-force * boostImpact);
        }
    }
}
