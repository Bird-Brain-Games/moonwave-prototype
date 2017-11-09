using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayerBigHitState : State
{

    // Use this for initialization
    PlayerStats m_playerStats;
    Rigidbody m_rigidBody;
    public float duration;

    float timer;

    //Whether this is the player that is the target of the big hit or not
    public bool isTarget { get; set; }
    public Vector3 Force { get; set; }
    public Vector3 Direction { get; set; }
    void Start()
    {
        duration = 1.0f;
        timer = duration;
        m_playerStats = GetComponent<PlayerStats>();
        m_rigidBody = GetComponent<Rigidbody>();
        isTarget = true;
    }

    public override void StateEnter()
    {
        timer = duration;
        Debug.Log("enter playerbig hit");
        m_rigidBody.velocity *= 0.1f;
        m_rigidBody.AddForce(Direction / (1.0f / (25 * duration)));
    }
    
    // Update is called once per frame
    public override void StateUpdate()
    {
        Debug.Log("big hit state update");

        if (timer > 0.0f)
        {
            timer -= Time.deltaTime;
            Debug.Log("big hit time update");
        }
        else
        {
            Debug.Log("change to drifting state");
            ChangeState(m_playerStats.PlayerDriftStateString);
        }
    }

    public override void StateExit()
    {
        if (isTarget == true)
        {
            //apply big knockback
            Debug.Log("Hit target");
            m_rigidBody.AddForce(Force);
        }
        isTarget = true;
        //GetComponent<PlayerBoost>().ResetBoostCollider();
        GetComponent<PlayerBoost>().BoostDurationEnd();
        //Apply the collision information.
    }

}
