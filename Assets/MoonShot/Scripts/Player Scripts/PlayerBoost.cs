using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Primary coder campbell.
public class PlayerBoost : MonoBehaviour
{

    #region Variables

    //The amount of force a players action causes

    float m_BoostForce;
    float m_BoostDuration;
    float m_CooldownDuration;
    bool m_startCooldown;

    public float m_inertiaForce;


    //All of the componenets we need access to
    Rigidbody m_RigidBody;
    StickToPlanet m_Gravity;
    PlayerStats m_PlayerStats;
    Renderer m_Rend;
    Controls m_controls;

    //the direction of analogue movement
    Vector2 m_move;

    //advancded direction variable
    Vector3 m_Direction;



    // Logging  l_ is used to indicate a variable is for logging
    public int l_boosts;

    public Controls Controls
    {
        get
        {
            return m_controls;
        }

        set
        {
            m_controls = value;
        }
    }

    #endregion

    // Use this for initialization
    void Start()
    {
        //set all of the needed scripts
        m_RigidBody = GetComponent<Rigidbody>();
        m_Gravity = GetComponent<StickToPlanet>();
        m_PlayerStats = GetComponent<PlayerStats>();
        m_Rend = GetComponent<Renderer>();
        m_controls = GetComponent<Controls>();

        l_boosts = 0;
        //Set player colour for boost reset.
        m_PlayerStats.colour = m_Rend.material.color;
        m_startCooldown = false;

    }

    //This is called when we enter the charge boost state.
    public void EntryBoost()
    {
        m_BoostForce = m_PlayerStats.m_boost.BaseForce;
        m_BoostDuration = m_PlayerStats.m_boost.BaseDuration;
    }

    public void ChargeBoost()
    {
        //Debug.Log("Boost charging");

        //This unreadable mess is what allows our boost to charge

        //This increases the force of the boost
        if (m_BoostForce <= m_PlayerStats.m_boost.MaxForce)
            m_BoostForce += m_PlayerStats.m_boost.AddedForcePerSecond * Time.deltaTime;
        else
            m_BoostForce = m_PlayerStats.m_boost.MaxForce;

        //This increases the duration of the boost
        if (m_BoostDuration <= m_PlayerStats.m_boost.MaxDuration)
            m_BoostDuration += m_PlayerStats.m_boost.AddedDurationPerSecond * Time.deltaTime;
        else
            m_BoostDuration = m_PlayerStats.m_boost.MaxDuration;

        //Setup Inertia canceling.
        Vector3 l_Inertia = (-1 * m_RigidBody.velocity);
        l_Inertia.Normalize();
        l_Inertia = l_Inertia *= m_inertiaForce;
        m_RigidBody.AddForce(l_Inertia, ForceMode.Impulse);


    }

    public void FireBoost()
    {
        //set boost direction
        m_move = m_controls.GetMove();
        if (m_move.x == 0 && m_move.y == 0)
        {
            Debug.Log("movement direction");
            m_Direction = new Vector3(m_RigidBody.velocity.x, m_RigidBody.velocity.y, 0.0f);
            m_Direction.Normalize();
        }
        else
            m_Direction = new Vector3(m_move.x, m_move.y, 0.0f);

        //Adding boost velocity.
        m_RigidBody.AddForce(m_BoostForce * m_Direction, ForceMode.Impulse);

        //reset and modify variables
        m_PlayerStats.CanBoost = false;

        Debug.Log("Boost fired!");

        m_Rend.material.color = m_PlayerStats.colourdull;

        //Log Boosts
        l_boosts++;
    }

    public bool BoostDuration()
    {

        //If the player boost knockback duration has ended.
        if (m_BoostDuration > 0) //&& m_PlayerStats.GetBoostState() == true)
        {
            m_BoostDuration -= Time.deltaTime;
            return false;
        }
        //If the boost has recharged.
        m_startCooldown = true;
        m_Rend.material.color = m_PlayerStats.colour;
        m_CooldownDuration = m_PlayerStats.m_boost.Cooldown;
        return true;
    }

    public void Update()
    {
        if (m_startCooldown == true)
        {
            if (m_CooldownDuration > 0)
            {
                m_CooldownDuration -= Time.deltaTime;
            }
            else
            {
                m_startCooldown = false;
                m_PlayerStats.CanBoost = true;
            }
        }
    }

    public float GetBoostForce()
    {
        return m_BoostForce;
    }
}