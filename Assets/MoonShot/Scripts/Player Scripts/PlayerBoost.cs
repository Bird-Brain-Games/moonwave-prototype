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
    Controls controls;

    Vector2 m_move;
    
    bool m_boost;
    bool m_jump;
    Vector3 m_Direction;
    int m_boostAmount;

    float m_cooldownDuration;
    float m_cooldownStart;

    // Logging  l_ is used to indicate a variable is for logging
    public int l_boosts;


    
    // Use this for initialization
    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody>();
        m_Gravity = GetComponent<StickToPlanet>();
        m_PlayerStats = GetComponent<PlayerStats>();
        m_Rend = GetComponent<Renderer>();
        controls = GetComponent<Controls>();
        m_boostAmount = 1;
        
        l_boosts = 0;

    }

    // Update is called once per frame
    void Update()
    {
        m_move = controls.GetMove();
        m_boost = controls.GetBoost();
        m_jump = controls.GetJump();
        m_Direction = new Vector3(m_move.x, m_move.y, 0.0f);

        // Gain your boosts back after touching ground. variable number of boosts.
        if (m_type == 0)
        {
            if (m_Gravity.IsGrounded())
            {
                m_boostAmount = boostCharges;
            }
            else if (m_boost && m_boostAmount > 0)
            {

                if (m_move.x != 0.0f || m_move.y != 0.0f)
                {
                    m_RigidBody.AddForce(JumpForce * m_Direction, ForceMode.Impulse);
                    m_boostAmount -= 1;
                    Debug.Log("boost!");

                    // Log Boosts
                    l_boosts++;
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
            if (m_boostAmount == 0)
            {
                if (m_cooldownDuration == 0)
                {
                    //transform.localScale *= 1.5f;
                    m_Rend.material.color = m_PlayerStats.colourdull;
                    m_cooldownDuration = Time.time;
                    m_cooldownStart = Time.time;
                    // Debug.Log("Initial Cooldown: " +( m_cooldownDuration - m_cooldownStart));
                    m_PlayerStats.SetBoostState(true);
                }
                else if (boostKnockbackDuration < m_cooldownDuration - m_cooldownStart && m_PlayerStats.GetBoostState() == true)
                {
                    m_PlayerStats.SetBoostState(false);
                    //transform.localScale /= 1.5f;
                    m_cooldownDuration = Time.time;
                }
                else if (boostRecharge < m_cooldownDuration - m_cooldownStart)
                {
                    m_Rend.material.color = m_PlayerStats.colour;
                    //Debug.Log("Cooldown reset: " + (m_cooldownDuration - m_cooldownStart));
                    m_cooldownDuration = 0;
                    m_cooldownStart = 0;
                    m_boostAmount++;
                }
                else
                {
                    m_cooldownDuration = Time.time;
                    //Debug.Log("Cooldown pass: " + (m_cooldownDuration - m_cooldownStart));
                }
            }
            else if (m_boost && m_boostAmount > 0)
            {

                if (m_move.x != 0.0f || m_move.y != 0.0f)
                {
                    m_Direction = new Vector3(m_move.x, m_move.y, 0.0f);
                    m_RigidBody.AddForce(BoostForce * m_Direction, ForceMode.Impulse);
                    m_boostAmount -= 1;
                    Debug.Log("boost!");
                    // Log Boosts
                    l_boosts++;
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