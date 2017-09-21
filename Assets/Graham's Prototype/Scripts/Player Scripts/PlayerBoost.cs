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
    public int m_type;

    Rigidbody m_RigidBody;
    ObjectGravity m_Gravity;
    Renderer m_Rend;

    float m_hAxis;
    float m_vAxis;
    Vector3 m_Direction;
    int m_boost;

    float m_cooldownDuration;
    float m_cooldownStart;


    
    // Use this for initialization
    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody>();
        m_Gravity = GetComponent<ObjectGravity>();
        m_Rend = GetComponent<Renderer>();
        m_boost = 0;

    }

    // Update is called once per frame
    void Update()
    {
        m_hAxis = Input.GetAxis("Horizontal");
        m_vAxis = Input.GetAxis("Vertical");
        m_Direction = new Vector3(m_hAxis, m_vAxis, 0.0f);

        // Gain your boosts back after touching ground. variable number of boosts.
        if (m_type == 0)
        {
            if (m_Gravity.IsGrounded())
            {
                m_boost = boostCharges;
            }
            else if (Input.GetButtonDown("Boost") && m_boost > 0)
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
            if (Input.GetButtonDown("Boost") && m_Gravity.IsGrounded())
            {
                m_RigidBody.AddForce(JumpForce * transform.up, ForceMode.Impulse);
                Debug.Log("Jump!");
            }
            else if (m_boost == 0)
            {
                if (m_cooldownDuration == 0)
                {
                    m_Rend.material.color = new Color(1, 0, 0);
                    m_cooldownDuration = Time.time;
                    m_cooldownStart = Time.time;
                   // Debug.Log("Initial Cooldown: " +( m_cooldownDuration - m_cooldownStart));
                }
                else if (boostRecharge < m_cooldownDuration - m_cooldownStart)
                {
                    m_Rend.material.color = new Color(0, 1, 0);
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
            else if (Input.GetButtonDown("Boost") && m_boost > 0)
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