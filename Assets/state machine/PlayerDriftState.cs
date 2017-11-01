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

	override public void StateUpdate()
	{
        //set the players direction based off of the analog stick
        m_Direction = new Vector3(m_Controls.GetMove().x, m_Controls.GetMove().y, 0.0f);

		// If the stick is being moved, add the force [G, C]
		if (m_Direction.sqrMagnitude > 0.0f)
			m_RigidBody.AddForce(m_PlayerStats.driftMoveForce * m_Direction * Time.deltaTime, ForceMode.Impulse);
		
		// If the shoot button is pressed, FIRE THE LASER
		if (m_Controls.GetShoot(BUTTON_DETECTION.GET_BUTTON))
		{
			m_Shoot.ShootLaser();
		}

		// Update the gravity [G, C]
		m_CollidedWithPlanet = m_Gravity.DriftingUpdate();
		if(m_CollidedWithPlanet)
		{
			// Change the state to the "Moving on planet" state
			ChangeState(m_PlayerStats.PlayerOnPlanetStateString);
		}

		// Check if boosting
		if (m_Controls.GetBoost(BUTTON_DETECTION.GET_BUTTON))
		{
			// Change the state to the "Boost Charge" state
			ChangeState(m_PlayerStats.PlayerBoostChargeString);
		}
	}
}
