using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnPlanetState : State {

	Vector3 m_Direction;

	Rigidbody m_RigidBody;
	PlayerStats m_PlayerStats;
	Controls m_Controls;
	Shoot m_Shoot;
	StickToPlanet m_Gravity;
	PlayerMoveOnPlanet m_Move;

	// Use this for initialization
	void Start () {
		m_RigidBody = GetComponent<Rigidbody>();
		m_PlayerStats = GetComponent<PlayerStats>();
		m_Controls = GetComponent<Controls>();
		m_Shoot = GetComponent<Shoot>();
		m_Gravity = GetComponent<StickToPlanet>();
		m_Move = GetComponent<PlayerMoveOnPlanet>();
	}

	override public void StateUpdate()
	{
		// Get the player input
        //set the players direction based off of the analog stick
        m_Direction = new Vector3(m_Controls.GetMove().x, m_Controls.GetMove().y, 0.0f);

		// Update the gravity [G, C]
		m_Gravity.GroundedUpdate();

		// If the stick is being moved, add the force [G, C]
		if (m_Direction.sqrMagnitude > 0.0f)
			m_Move.MoveOnPlanet();
		
		// If the shoot button is pressed, FIRE THE LASER
		if (m_Controls.GetShoot(BUTTON_DETECTION.GET_BUTTON))
		{
			m_Shoot.ShootLaser();
		}

		// If the jump button is pressed, cause the player to jump
		if (m_Controls.GetJump(BUTTON_DETECTION.GET_BUTTON_DOWN))
		{
			m_RigidBody.AddForce(m_PlayerStats.jumpForce * transform.up, ForceMode.Impulse);
			ChangeState(m_PlayerStats.PlayerDriftStateString);
		}
	}
}
