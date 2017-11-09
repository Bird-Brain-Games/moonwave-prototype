using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDriftState : State {

	Vector3 m_Direction;
	bool m_CollidedWithPlanet;

	Rigidbody m_RigidBody;
	PlayerStats m_PlayerStats;
	Controls m_Controls;
	Shoot m_Shoot;
	StickToPlanet m_Gravity;

	// Use this for initialization
	void Start () {
		m_RigidBody = GetComponent<Rigidbody>();
		m_PlayerStats = GetComponent<PlayerStats>();
		m_Controls = GetComponent<Controls>();
		m_Shoot = GetComponent<Shoot>();
		m_Gravity = GetComponent<StickToPlanet>();
	}

	override public void Enter()
	{
		m_CollidedWithPlanet = false;
	}

	override public void StateFixedUpdate()
	{
		m_CollidedWithPlanet = false;
	}

	override public void StateUpdate()
	{
        //set the players direction based off of the analog stick
        m_Direction = new Vector3(m_Controls.GetMove().x, m_Controls.GetMove().y, 0.0f);

		// If the stick is being moved, add the force [G, C]
		if (m_Direction.sqrMagnitude > 0.0f)
			m_RigidBody.AddForce(m_PlayerStats.driftMoveForce * m_Direction, ForceMode.Impulse);
		
		// If the shoot button is pressed, FIRE THE LASER
		if (m_Controls.GetShootLaser())
		{
			m_Shoot.ShootLaser();
		}

		// If the "fire shotgun" button is pressed, shoot it. [Graham]
		if (m_Controls.GetShootShotgun())
		{
			m_Shoot.ShootShotgun();
		}

		// Update the gravity [G, C]
		m_RigidBody.AddForce(m_Gravity.DriftingUpdate()* m_PlayerStats.fallGravMultiplier);
		if(m_CollidedWithPlanet)
		{
			// Change the state to the "Moving on planet" state
			ChangeState(m_PlayerStats.PlayerOnPlanetStateString);
		}

		// Check if boosting
		if (m_Controls.GetBoost(BUTTON_DETECTION.GET_BUTTON) && m_PlayerStats.CanBoost == true)
		{
			// Change the state to the "Boost Charge" state
			ChangeState(m_PlayerStats.PlayerBoostChargeString);
		}
	}

	void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Planet")
        {
            m_CollidedWithPlanet = true;
        }
    }

	void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Planet")
        {
            m_CollidedWithPlanet = true;
        }
    }
}
