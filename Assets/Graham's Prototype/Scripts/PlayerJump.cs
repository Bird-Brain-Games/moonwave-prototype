using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour {

    public float JumpForce;
    Rigidbody m_RigidBody;
    Collider m_Collider;
    //bool m_isGrounded;
    float m_DistanceToGround;

    // Use this for initialization
    void Start () {
        //m_isGrounded = false;
        m_RigidBody = GetComponent<Rigidbody>();
        m_Collider = GetComponent<Collider>();
        m_DistanceToGround = m_Collider.bounds.extents.y;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            m_RigidBody.AddForce(JumpForce * transform.up, ForceMode.Impulse);
            //m_isGrounded = false;
            Debug.Log("Jump!");
        }
	}

    bool IsGrounded()
    {
        // Inspiration from http://answers.unity3d.com/questions/196381/how-do-i-check-if-my-rigidbody-player-is-grounded.html
        return Physics.Raycast(transform.position, -transform.up, m_DistanceToGround + 0.1f);
    }
}
