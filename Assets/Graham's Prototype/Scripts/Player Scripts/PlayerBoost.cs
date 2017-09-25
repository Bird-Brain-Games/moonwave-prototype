using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoost : MonoBehaviour
{

    public float JumpForce;
    public float BoostForce;
    public float MoveForce;
    public int boostCharges;
    public float boostRecharge;
    public float boostKnockbackDuration;
    public int m_type;

    Rigidbody m_RigidBody;
    StickToPlanet m_Gravity;
    PlayerStats m_PlayerStats;
    Renderer m_Rend;
    ControlStrings controls;

    float m_hAxis;
    float m_vAxis;
    bool m_jump;
    Vector3 m_Direction;
    int m_boost;

    float m_cooldownDuration;
    float m_cooldownStart;


    
    // Use this for initialization
    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody>();
        m_Gravity = GetComponent<StickToPlanet>();
        m_PlayerStats = GetComponent<PlayerStats>();
        m_Rend = GetComponent<Renderer>();
        controls = GetComponent<ControlStrings>();
        m_boost = 1;

    }

    // Update is called once per frame
    void Update()
    {
        m_hAxis = Input.GetAxis(controls.get_joystickH());
        m_vAxis = Input.GetAxis(controls.get_joystickV());
        m_jump = Input.GetButtonDown(controls.get_jump());
        m_Direction = new Vector3(m_hAxis, m_vAxis, 0.0f);

        // Gain your boosts back after touching ground. variable number of boosts.
        if (m_type == 0)
        {
            if (m_Gravity.IsGrounded())
            {
                m_boost = boostCharges;
            }
            else if (m_jump && m_boost > 0)
            {

                if (m_hAxis != 0.0f || m_vAxis != 0.0f)
                {
                    m_RigidBody.AddForce(JumpForce * m_Direction, ForceMode.Impulse);
                    m_boost -= 1;
                    Debug.Log("boost!");
                }
            }
        }

        //Your boost is on a timer.
        else if (m_type == 1)
        {
            if (m_jump && m_Gravity.IsGrounded())
            {
                m_RigidBody.AddForce(JumpForce * transform.up, ForceMode.Impulse);
                m_PlayerStats.m_PlayerState = PlayerState.Drifting;
                //Debug.Log("Jump!");
            }
            else if (m_boost == 0)
            {
                if (m_cooldownDuration == 0)
                {
                    //transform.localScale *= 1.5f;
                    m_Rend.material.color = controls.colourdull;
                    m_cooldownDuration = Time.time;
                    m_cooldownStart = Time.time;
                    // Debug.Log("Initial Cooldown: " +( m_cooldownDuration - m_cooldownStart));
                    controls.set_boost(true);
                }
                else if (boostKnockbackDuration < m_cooldownDuration - m_cooldownStart && controls.get_boost() == true)
                {
                    controls.set_boost(false);
                    //transform.localScale /= 1.5f;
                    m_cooldownDuration = Time.time;
                }
                else if (boostRecharge < m_cooldownDuration - m_cooldownStart)
                {
                    m_Rend.material.color = controls.colour;
                    //Debug.Log("Cooldown reset: " + (m_cooldownDuration - m_cooldownStart));
                    m_cooldownDuration = 0;
                    m_cooldownStart = 0;
                    m_boost++;
                }
                else
                {
                    m_cooldownDuration = Time.time;
                    //Debug.Log("Cooldown pass: " + (m_cooldownDuration - m_cooldownStart));
                }
            }
            else if (m_jump && m_boost > 0)
            {

                if (m_hAxis != 0.0f || m_vAxis != 0.0f)
                {
                    m_Direction = new Vector3(m_hAxis, m_vAxis, 0.0f);
                    m_RigidBody.AddForce(BoostForce * m_Direction, ForceMode.Impulse);
                    m_boost -= 1;
                    Debug.Log("boost!");
                }
            }
        }

        //player basic movement
        if (!m_Gravity.IsGrounded())
        {
            m_RigidBody.AddForce(MoveForce * m_Direction, ForceMode.Impulse);
        }

    }
}