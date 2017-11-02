using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onCollision : MonoBehaviour
{
    //layermasks to detect what we hit
    public LayerMask m_planet;
    public LayerMask m_player;

    //how strong bullet knockback is.
 
    int m_layer;

    Rigidbody m_rigidBody;
    Shield m_shield;
    Owner m_owner;
    void Start()
    {
        //setting up layers.
        m_planet = 8;
        m_player = 9;

        //get the bullets rigidbody
        m_rigidBody = GetComponent<Rigidbody>();


        //Debug.Log("bullet V: " + m_rigidBody.velocity);
        //set the bullets owner.
        m_owner = GetComponent<Owner>();
    }

    private void OnCollisionEnter(Collision collision)
    {

        //sets what layer we have collided with
        m_layer = collision.gameObject.layer;
        //if (m_layer == planet)
        //{
        //    m_owner.setVelocity(m_rigidBody.velocity);
        //}

        // Robbie changed this
        if (collision.gameObject.tag == "Planet") // If the object is a planet
        {
            Destroy(gameObject, 0);  // Destorys bullets when they hit a planet
        }
        //if we have collided with a player.
        else if (m_layer == m_player && collision.gameObject != m_owner.getOwner())
        {

            //needs to shift this to a function call so all the variables are private instead of public.
            m_shield = collision.rigidbody.GetComponentInChildren<Shield>();

            // Stores who got the hit [Jack]
            collision.rigidbody.GetComponent<PlayerStats>().m_HitLastBy = m_owner.getOwner().GetComponent<PlayerStats>();

            var force = m_owner.getVelocity();
            force.Normalize();

            if (m_shield.m_shieldHealth == 0)
            {
                Debug.Log("bullet criticaly hit player");
                Vector3 temp = force * m_owner.GetImpact() * m_owner.GetCriticalMultiplyer();
                Debug.Log(force + " applied to enemy");
                collision.rigidbody.AddForce(temp, ForceMode.Impulse);
            }
            else
            {
                Debug.Log("bullet hit player");
                Vector3 temp = force * m_owner.GetImpact();
                collision.rigidbody.AddForce(temp, ForceMode.Impulse);
            }


           
            m_shield.ShieldHit(Shield.BULLET_TYPE.plasma);

            Destroy(gameObject, 0);


            //var force = collision.transform.position - transform.position;
            //
            //force.Normalize();
            //
            //collision.rigidbody.AddForce(force * impact);
            //
            //Destroy(gameObject, 0);
        }

    }
}
