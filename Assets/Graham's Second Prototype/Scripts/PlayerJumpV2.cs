using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpV2 : MonoBehaviour {

    public float JumpForce;
    Rigidbody m_RigidBody;
    StickToPlanet m_Gravity;

    // Use this for initialization
    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody>();
        m_Gravity = GetComponent<StickToPlanet>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && m_Gravity.IsGrounded())
        {
            m_RigidBody.AddForce(JumpForce * transform.up, ForceMode.Impulse);
            Debug.Log("Jump!");
        }
    }
}
