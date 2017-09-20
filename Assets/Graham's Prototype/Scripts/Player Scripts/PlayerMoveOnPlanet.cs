using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveOnPlanet : MonoBehaviour
{


    public float m_WalkSpeed;
    public float m_MaxWalkSpeed;
    Rigidbody m_RigidBody;
    ObjectGravity m_Gravity;
    float m_hAxis;
    float m_vAxis;
    int m_MovementType = 1;
    float dotProduct;

    Vector3 joyStick;

    // Use this for initialization
    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody>();
        m_Gravity = GetComponent<ObjectGravity>();
    }

    // Update is called once per frame
    void Update()
    {
        m_hAxis = Input.GetAxis("Horizontal");
        m_vAxis = Input.GetAxis("Vertical");
    }

    // Update called once per physics update
    void FixedUpdate()
    {

        if (m_MovementType == 0)
        {
            if (m_hAxis != 0.0f && m_Gravity.IsGrounded())
            {
                m_RigidBody.AddForce(transform.right * m_WalkSpeed * m_hAxis, ForceMode.VelocityChange);
                m_RigidBody.velocity = Vector3.ClampMagnitude(m_RigidBody.velocity, m_MaxWalkSpeed);

            }
            else if (m_hAxis == 0.0f && m_Gravity.IsGrounded() && m_RigidBody.velocity != Vector3.zero)
            {
                m_RigidBody.velocity += -m_RigidBody.velocity * 0.2f;
                if (m_RigidBody.velocity.magnitude < 0.1f)
                {
                    m_RigidBody.velocity.Set(0, 0, 0);
                }
            }
        }
        else if (m_MovementType == 1)
        {

           
            // if the joystick is pressed
            if ((m_hAxis != 0.0f || m_vAxis != 0.0f) && m_Gravity.IsGrounded())
            {
                //create joystick vector and normalize it
                joyStick = new Vector3(m_hAxis, m_vAxis, 0.0f);
                joyStick.Normalize();

                //dot the joystick and player up vector
                dotProduct = Vector3.Dot(joyStick, transform.up);
                Debug.Log("Dot product " + Vector3.Dot(joyStick, transform.up));
                if (dotProduct < 0.95f || dotProduct > 1.05f)
                {
                    if (joyStick.y * transform.up.x > joyStick.x * transform.up.y)
                    {
                        Debug.Log("Move counter clockwise");
                        m_RigidBody.AddForce(transform.right * m_WalkSpeed *  -1, ForceMode.VelocityChange);
                        m_RigidBody.velocity = Vector3.ClampMagnitude(m_RigidBody.velocity, m_MaxWalkSpeed);
                    }
                    else
                    {
                        Debug.Log("Move clockwise");
                        m_RigidBody.AddForce(transform.right * m_WalkSpeed * 1, ForceMode.VelocityChange);
                        m_RigidBody.velocity = Vector3.ClampMagnitude(m_RigidBody.velocity, m_MaxWalkSpeed);
                    }
                }
                else
                {
                    m_RigidBody.velocity += -m_RigidBody.velocity * 0.2f;
                    if (m_RigidBody.velocity.magnitude < 0.1f)
                    {
                        m_RigidBody.velocity.Set(0, 0, 0);
                    }
                }

            }
            //IF the player isnt moving apply friction
            else if (m_hAxis == 0.0f && m_Gravity.IsGrounded() && m_RigidBody.velocity != Vector3.zero)
            {
                m_RigidBody.velocity += -m_RigidBody.velocity * 0.2f;
                if (m_RigidBody.velocity.magnitude < 0.1f)
                {
                    m_RigidBody.velocity.Set(0, 0, 0);
                }
            }
        }
        else
        {

        }
        Debug.Log(m_RigidBody.velocity);
        Debug.Log(m_hAxis);
    }
}
