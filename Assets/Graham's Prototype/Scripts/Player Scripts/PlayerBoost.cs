using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoost : MonoBehaviour
{

    public float JumpForce;
    Rigidbody m_RigidBody;
    ObjectGravity m_Gravity;
    float m_hAxis;
    float m_vAxis;
    Vector3 m_Direction;
    bool m_boost;
    // Use this for initialization
    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody>();
        m_Gravity = GetComponent<ObjectGravity>();
        m_boost = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Gravity.IsGrounded())
        {
            m_boost = true;
        }
        else if (Input.GetButtonDown("Jump") && m_boost == true)
        {
            m_hAxis = Input.GetAxis("Horizontal");
            m_vAxis = Input.GetAxis("Vertical");
            if (m_hAxis != 0.0f || m_vAxis != 0.0f)
            {
                m_Direction = new Vector3(m_hAxis, m_vAxis, 0.0f);
                m_RigidBody.AddForce(JumpForce * m_Direction, ForceMode.Impulse);
                m_boost = false;
                Debug.Log("boost!");
            }
        }
    }
}
