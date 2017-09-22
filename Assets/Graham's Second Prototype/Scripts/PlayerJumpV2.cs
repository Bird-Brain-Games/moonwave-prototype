using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpV2 : MonoBehaviour {

    public float JumpForce;
    Rigidbody m_RigidBody;
    StickToPlanet m_Gravity;
    PlayerStats m_PlayerStats;

    // Use this for initialization
    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody>();
        m_Gravity = GetComponent<StickToPlanet>();
        m_PlayerStats = GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && m_PlayerStats.m_PlayerState == PlayerState.Grounded)
        {
            m_RigidBody.AddForce(JumpForce * transform.up, ForceMode.Impulse);
            m_PlayerStats.m_PlayerState = PlayerState.Drifting;
            Debug.Log("Jump!");
        }
    }
}
