﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveOnPlanet : MonoBehaviour
{

    public float m_WalkSpeed;
    Rigidbody m_RigidBody;
    StickToPlanet m_Gravity;
    float m_hAxis;
    float m_vAxis;
    public int m_MovementType = 1;
    float dotProduct;

    Vector3 m_JoyStick;
    Vector3 m_PlanetToPlayer;
    ControlStrings controls;

    // Use this for initialization
    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody>();
        m_Gravity = GetComponent<StickToPlanet>();
        controls = GetComponent<ControlStrings>();
    }

    // Update is called once per frame
    void Update()
    {
        m_hAxis = Input.GetAxis(controls.get_joystickH());
        m_vAxis = Input.GetAxis(controls.get_joystickV());
    }

    // Update called once per physics update
    void FixedUpdate()
    {

        if (m_MovementType == 0)
        {
            if (m_hAxis != 0.0 && m_Gravity.IsGrounded())
            {
                m_RigidBody.velocity = transform.right * m_hAxis * m_WalkSpeed;
            }
        }
        else if (m_MovementType == 1)
        {

           
            // if the joystick is pressed
            if ((m_hAxis != 0.0f || m_vAxis != 0.0f) && m_Gravity.IsGrounded())
            {
                //create joystick vector and normalize it
                m_JoyStick = new Vector3(m_hAxis, m_vAxis, 0.0f);
                m_JoyStick.Normalize();

                //dot the joystick and player up vector

                m_PlanetToPlayer = -m_Gravity.GetDirectionOfCurrentPlanet();//new Vector3 (transform.position - )
                dotProduct = Vector3.Dot(m_JoyStick, m_PlanetToPlayer);
                //Debug.Log("Dot product " + Vector3.Dot(m_JoyStick, transform.up));
                if (dotProduct < 0.95f || dotProduct > 1.05f)
                {
                    if (m_JoyStick.y * m_PlanetToPlayer.x > m_JoyStick.x * m_PlanetToPlayer.y)
                    {
                        //Debug.Log("Move counter clockwise");
                        m_RigidBody.velocity = transform.right * m_hAxis * m_WalkSpeed * -1.0f;
                    }
                    else
                    {
                        //Debug.Log("Move clockwise");
                        m_RigidBody.velocity = transform.right * m_hAxis * m_WalkSpeed * 1.0f;
                    }
                }

            }
        }
       // Debug.Log(m_RigidBody.velocity);
       // Debug.Log(m_hAxis);
    }
}
