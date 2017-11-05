using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public Rigidbody m_Rigidbody;
	public PlayerStats m_PlayerStats;

	// Collision Layers [Graham]
	protected int m_PlanetLayer;
	protected int m_PlayerLayer;
	protected int m_ProjectileLayer;

	// member variables [Graham]
	protected Vector2 m_Direction;
	protected float m_Force;

	// Use this for initialization
	void Start () {
		m_Rigidbody = GetComponent<Rigidbody>();
		m_PlanetLayer = 8;//LayerMask.GetMask("Planet");
		m_PlayerLayer = 9;//LayerMask.GetMask("Player");
		m_ProjectileLayer = 10;//LayerMask.GetMask("Player");
	}

	// Used to set the values of the projectile [Graham]
	public void Init(Vector2 a_direction, float a_force, PlayerStats a_PlayerStats)
	{
		m_Direction = a_direction.normalized;
		m_Force = a_force;
		m_PlayerStats = a_PlayerStats;
	}
}
