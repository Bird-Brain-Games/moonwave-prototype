using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onCollision : MonoBehaviour
{

    public LayerMask planet;
    public LayerMask player;
    public float impact;
    int m_layer;

    Rigidbody m_rigidBody;
    Shield m_shield;
    Owner owner;
    void Start()
    {
        planet = 8;
        player = 9;
        m_rigidBody = GetComponent<Rigidbody>();
        Debug.Log("bullet V: " + m_rigidBody.velocity);
    }

    // Update is called once per frame
    void Update()
    {
        owner = GetComponent<Owner>();
    }

    private void OnCollisionEnter(Collision collision)
    {

        m_layer = collision.gameObject.layer;
        if (m_layer == planet)
        {
            owner.setVelocity(m_rigidBody.velocity);
        }
        else if (m_layer == player && collision.gameObject != owner.getOwner())
        {

            //needs to shift this to a function call so all the variables are private instead of public.
            m_shield = collision.rigidbody.GetComponentInChildren<Shield>();

            var force = owner.getVelocity();
            Debug.Log("bullet V: " + force);
            force.Normalize();

            Debug.Log("collision V: " + collision.rigidbody.velocity);

            if (m_shield.m_shieldHealth == 0)
                collision.rigidbody.AddForce(force * impact * 10);
            else
                collision.rigidbody.AddForce(force);

            Debug.Log("collision V after: " + collision.rigidbody.velocity);

           
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
