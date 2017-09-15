using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveOnPlanet : MonoBehaviour {

    public float m_WalkSpeed;
    Rigidbody m_RigidBody;
    ObjectGravity m_Gravity;
    float m_hAxis;

    // Use this for initialization
    void Start () {
        m_RigidBody = GetComponent<Rigidbody>();
        m_Gravity = GetComponent<ObjectGravity>();
    }
	
	// Update is called once per frame
	void Update () {
        m_hAxis = Input.GetAxis("Horizontal");
	}

    // Update called once per physics update
    void FixedUpdate()
    {
        if (m_hAxis != 0.0f && m_Gravity.IsGrounded())
        {
            m_RigidBody.AddForce(transform.forward * m_WalkSpeed * m_hAxis, ForceMode.VelocityChange);
        }
    }
}
