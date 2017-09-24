using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    // Use this for initialization

    public Rigidbody bullet;

    float m_aimH;
    float m_aimV;
    bool m_shoot;
    ControlStrings controls;

    float m_bulletDelay;
    float m_timer;
    float m_startTime;

	void Start () {
        controls = GetComponent<ControlStrings>();

    }
	
	// Update is called once per frame
	void Update () {

        m_shoot = Input.GetButton(controls.get_shoot());

        m_aimH = Input.GetAxis(controls.get_aimH());
        m_aimV = Input.GetAxis(controls.get_aimV());
        //Debug.Log("Horizontal axis "+ m_aimH.ToString());
        //Debug.Log("Vertical axis " + m_aimV.ToString());
        if (m_shoot && m_timer == 0)
        {
            m_timer = Time.time;
            m_startTime = Time.time;

            Vector3 forward = new Vector3(m_aimH, m_aimV, 0.0f);
            forward.Normalize();

            Rigidbody clone;
            clone = Instantiate(bullet, transform.position + (forward*1.5f), Quaternion.identity);

            Debug.Log(forward.ToString());
            clone.velocity = forward * 15.0f; 
        }
        else if (m_bulletDelay > m_timer - m_startTime)
        {

        }
	}
}