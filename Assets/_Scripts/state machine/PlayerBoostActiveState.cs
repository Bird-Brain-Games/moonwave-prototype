﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoostActiveState : State
{

    Rigidbody m_RigidBody;
    PlayerStats m_PlayerStats;
    Controls m_Controls;
    PlayerBoost m_Boost;

    // Use this for initialization
    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody>();
        m_PlayerStats = GetComponent<PlayerStats>();
        m_Controls = GetComponent<Controls>();
        m_Boost = GetComponent<PlayerBoost>();
    }

    override public void StateEnter()
    {
        //setup variables.
        //m_Boost.EntryBoost();
    }

    public override void StateExit()
    {
        //resets the 
        m_Boost.BoostDurationEnd();
    }
    override public void StateUpdate()
    {
        
        if (m_Boost.BoostDuration())
            ChangeState(m_PlayerStats.PlayerDriftStateString);

    }

    override public void StateOnCollisionEnter(Collision collision)
    {
        // Collide with a player
        if (collision.gameObject.CompareTag("Player"))
        {
            //sets the direction of the force
            var direction = collision.transform.position - transform.position;
            direction.Normalize();

            //Adds the force to the player we collided with
            collision.rigidbody.AddForce(direction
                * m_Boost.GetBoostForce()
                * m_PlayerStats.GetCriticalMultiplier());

            //adds inverse force to us to signifiy knockback.
            //m_rigidbody.AddForce(-force * boostImpact);
        }
    }
}
