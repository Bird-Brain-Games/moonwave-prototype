using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boostCollision : MonoBehaviour
{

    public float boostImpact;
    LayerMask player;
    int m_layer;
    PlayerStats m_PlayerStats;
    Rigidbody m_rigidbody;
    // Use this for initialization
    void Start()
    {
        player = 9;
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
        if (m_layer == player && m_PlayerStats.GetBoostState())
        {

            Debug.Log("collide");
            //sets the direction of the force
            var force = collision.transform.position - transform.position;
            force.Normalize();

            //Adds the force to the player we collided with
            
            collision.rigidbody.AddForce(force * boostImpact * collision.transform.GetComponent<PlayerStats>().GetCriticalMultiplyer());

            //adds inverse force to us to signifiy knockback.
            //m_rigidbody.AddForce(-force * boostImpact);

        }

    }

}
