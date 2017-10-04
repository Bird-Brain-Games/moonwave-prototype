using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    // Use this for initialization

    public Rigidbody bullet;

    Vector2 m_aim;
    bool m_shoot;
    Controls controls;

    public float m_bulletDelay;
    float m_timer;
    float m_startTime;
    bool m_shootStatus;

    // Logging
    public int l_bullets; // using l_ to indicate this data is for logging

    void Start () {
        controls = GetComponent<Controls>();
        l_bullets = 0;
    }
	
	// Update is called once per frame
	void Update () {

        m_shoot = controls.GetShoot();
        m_aim = controls.GetAim();

        if (m_shoot && m_shootStatus)
        {
            m_shootStatus = false;
            m_timer = Time.time;
            m_startTime = Time.time;

            Vector3 forward = new Vector3(m_aim.x, m_aim.y);
            forward.Normalize();

            Rigidbody clone;
            clone = Instantiate(bullet, transform.position + (forward*2.5f), Quaternion.identity);

            forward *= 50.0f;
            //Debug.Log(forward.ToString());

            clone.velocity = forward;
            //Debug.Log(clone.velocity.ToString());
            clone.GetComponent<Owner>().setOwner(gameObject);
            clone.GetComponent<Owner>().setVelocity(clone.velocity);

            // Log total shots fired
            l_bullets++; // Take a note of how many player shots
        }
        else if (m_bulletDelay > m_timer - m_startTime && !m_shootStatus)
        {
            m_timer = Time.time;
            //Debug.Log("start Time: " + m_startTime);
            //Debug.Log("Time: " + m_timer);
        }
        else if (m_bulletDelay < m_timer - m_startTime && !m_shootStatus)
        {
            //Debug.Log("shoot recharged");
            m_shootStatus = true;
            m_timer = 0;
            m_startTime = 0;
        }
	}
}