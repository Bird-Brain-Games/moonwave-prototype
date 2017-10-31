using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boostCollision : MonoBehaviour
{
    //The force a boost impacts
    public float m_boostBaseImpact;
    //The force that is added for every second of charge
    public float m_boostAddedCharge;
    //The duration we charged boost for
    float m_boostDuration;

    //used to detect if we collided with a player
    LayerMask m_player;

    //The layer of the object we collide with
    int m_layer;

    //Access to several object components
    PlayerStats m_PlayerStats;
    Rigidbody m_rigidbody;
    // Use this for initialization
    void Start()
    {
        //initialization.
        m_player = 9;
        m_PlayerStats = gameObject.GetComponent<PlayerStats>();
        m_rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {

        m_layer = collision.gameObject.layer;

        //detects collision between players and boosting state
        if (m_layer == m_player && m_PlayerStats.GetBoostState())
        {

            Debug.Log("collide");

            //sets the direction of the force
            var force = collision.transform.position - transform.position;
            force.Normalize();

            //Adds the force to the player we collided with
            collision.rigidbody.AddForce(force 
                * (m_boostBaseImpact + (m_boostAddedCharge * m_boostDuration) ) 
                * m_PlayerStats.GetCriticalMultiplier());

            //adds inverse force to us to signifiy knockback.
            //m_rigidbody.AddForce(-force * boostImpact);

        }

    }

    public void SetBoostDuration(float a_boostDuration)
    {
        m_boostDuration = a_boostDuration;
    }
}
