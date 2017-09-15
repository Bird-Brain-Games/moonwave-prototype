using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGravity : MonoBehaviour {

    public PlanetManager m_PlanetManager;
    public Vector3 m_ClosestPlanetDir;
    Rigidbody m_RigidBody;

	// Use this for initialization
	void Start () {
        m_RigidBody = GetComponent<Rigidbody>();
        
	}
	
	// Update is called once per physics update
	void FixedUpdate () {
        Vector3 gravity = m_PlanetManager.GetGravity(transform.position, m_RigidBody.mass, out m_ClosestPlanetDir);
        //Vector3 gravity = m_PlanetManager.getConstantGravity(transform.position, m_RigidBody.mass);
        m_RigidBody.AddForce(gravity);
        m_RigidBody.transform.up = -gravity.normalized;
        //Debug.Log(gravity);
        //Debug.Log(m_ClosestPlanetDir);


	}
}
