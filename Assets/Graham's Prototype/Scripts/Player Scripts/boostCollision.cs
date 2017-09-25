using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boostCollision : MonoBehaviour
{

    public float boostImpact;
    LayerMask player;
    int m_layer;
    ControlStrings controls;
    Rigidbody m_rigidbody;
    // Use this for initialization
    void Start()
    {
        player = 9;
        controls = gameObject.GetComponent<ControlStrings>();
        m_rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {

        m_layer = collision.gameObject.layer;
        if (m_layer == player && controls.get_boost())
        {

            var force = collision.transform.position - transform.position;

            force.Normalize();

            collision.rigidbody.AddForce(force * boostImpact);
            m_rigidbody.AddForce(-force * boostImpact);

        }

    }

}
