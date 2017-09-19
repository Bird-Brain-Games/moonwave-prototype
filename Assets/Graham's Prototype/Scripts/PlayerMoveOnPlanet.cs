using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveOnPlanet : MonoBehaviour {

    public float m_WalkSpeed;
    public float m_MaxWalkSpeed;
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
            m_RigidBody.AddForce(transform.right * m_WalkSpeed * m_hAxis, ForceMode.VelocityChange);
            m_RigidBody.velocity = Vector3.ClampMagnitude(m_RigidBody.velocity, m_MaxWalkSpeed);
            
        }
        else if (m_hAxis == 0.0f && m_Gravity.IsGrounded() && m_RigidBody.velocity != Vector3.zero)
        {
            m_RigidBody.velocity += -m_RigidBody.velocity * 0.2f;
            if (m_RigidBody.velocity.magnitude < 0.1f)
            {
                m_RigidBody.velocity.Set(0, 0, 0);
            }
        }
        Debug.Log(m_RigidBody.velocity);
    }
}
