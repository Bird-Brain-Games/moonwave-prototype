using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGravity : MonoBehaviour {

    public PlanetManager m_PlanetManager;
    public Vector3 m_ClosestPlanetDir;
    public Vector3 m_ClosestPlanetGravity;
    Rigidbody m_RigidBody;
    bool m_IsGrounded;

    Collider m_Collider;    // To be optimized
    float m_DistanceToGround;

    // Use this for initialization
    void Start () {
        m_RigidBody = GetComponent<Rigidbody>();
        m_Collider = GetComponent<Collider>();
        m_DistanceToGround = m_Collider.bounds.extents.y;
    }
	
	// Update is called once per physics update
	void FixedUpdate () {
        Vector3 gravity = m_PlanetManager.GetGravity(transform.position, m_RigidBody.mass, out m_ClosestPlanetDir, out m_ClosestPlanetGravity);
        //Vector3 gravity = m_PlanetManager.getConstantGravity(transform.position, m_RigidBody.mass);

        // If it's grounded, only apply the gravity of the planet
        m_IsGrounded = FindIfGrounded();

        //if (IsGrounded())
        //{
        //    m_RigidBody.AddForce(m_ClosestPlanetGravity);
        //    Debug.Log(m_ClosestPlanetGravity);
        //}
        //else
        //{
        //    m_RigidBody.AddForce(gravity);
        //    //Debug.Log(gravity);
        //}

        m_RigidBody.AddForce(gravity);
        m_RigidBody.transform.up = -gravity.normalized;
        
        //Debug.Log(gravity);
        //Debug.Log(m_ClosestPlanetDir);


	}
    bool FindIfGrounded()
    {
        // Inspiration from http://answers.unity3d.com/questions/196381/how-do-i-check-if-my-rigidbody-player-is-grounded.html
        return Physics.Raycast(transform.position, -transform.up, m_DistanceToGround + 0.1f);
    }

    public bool IsGrounded()
    {
        return m_IsGrounded;
    }
}
