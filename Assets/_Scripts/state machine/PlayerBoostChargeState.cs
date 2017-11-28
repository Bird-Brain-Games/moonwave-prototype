using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoostChargeState : State
{

    Vector3 m_Direction;

    PlayerStats m_PlayerStats;
    Controls m_Controls;
    PlayerBoost m_Boost;
    Animator m_Animator;
    BulletParticles m_chargeParticles;
    Vector3 bulletRand;
    Rigidbody m_rigidbody;

    float rand = 0.75f;
    // Use this for initialization
    void Start()
    {
        m_PlayerStats = GetComponent<PlayerStats>();
        m_Controls = GetComponent<Controls>();
        m_Boost = GetComponent<PlayerBoost>();
        m_Animator = GetComponentInChildren<Animator>();
        m_rigidbody = GetComponent<Rigidbody>();
    }

    public override void StateEnter()
    {
        m_Boost.EntryBoost();
        m_Animator.SetTrigger("Boost Started");
        Debug.Log("Pre instantiant");
        m_chargeParticles = Instantiate(m_PlayerStats.m_Particles);
        m_chargeParticles.transform.position = m_PlayerStats.transform.position;
        m_chargeParticles.velocity = -m_Boost.m_Direction * 25;
        //Vector3 direction = -m_Boost.m_Direction.normalized;
        //bulletRand = direction + m_rigidbody.transform.right;
        //m_chargeParticles.random = bulletRand * rand;
        //m_chargeParticles.SetRand(bulletRand * rand);
    }

    public override void StateExit()
    {
        m_chargeParticles.Alive = false;
        Debug.Log("state exit");
    }

    override public void StateUpdate()
    {
        //set the players direction based off of the analog stick
        m_Direction = new Vector3(m_Controls.GetMove().x, m_Controls.GetMove().y, 0.0f);

        // Charge the boost [Graham] (pull the lever graham, The other lever)
        m_Boost.ChargeBoost();
        //m_chargeParticles.velocity = -m_Boost.m_Direction * 25;
        //m_chargeParticles.transform.position = m_PlayerStats.transform.position;
       //
       // Vector3 direction = -m_Boost.m_Direction.normalized;
       // bulletRand = direction + m_rigidbody.transform.right;
       // m_chargeParticles.SetRand(bulletRand.normalized * rand);

        if (m_Controls.GetBoost(BUTTON_DETECTION.GET_BUTTON_UP))
        {
            // If the boost is released, fire the boost [Graham]
            m_Boost.FireBoost();
            ChangeState(m_PlayerStats.PlayerBoostActiveString);
        }
    }
}
