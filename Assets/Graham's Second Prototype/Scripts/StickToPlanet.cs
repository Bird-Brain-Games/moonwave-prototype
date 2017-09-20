using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickToPlanet : MonoBehaviour {
    /// <summary>
    /// Adapted from
    /// https://github.com/nlra/Galaxy/blob/master/Assets/Game/Scripts/StickToPlanet.cs
    /// http://www.asteroidbase.com/devlog/7-learning-how-to-walk/
    /// https://gamedev.stackexchange.com/questions/89693/how-could-i-constrain-player-movement-to-the-surface-of-a-3d-object-using-unity
    /// https://mikeloscocco.wordpress.com/2015/10/13/mario-galaxy-physics-in-unity/
    /// </summary>

    Rigidbody m_RigidBody;
    Collider m_Collider;

    Vector3 m_CurrentGravityDirection;
    PlanetGravityField m_CurrentPlanet;
    List<Collider> m_PlanetsAffecting;
    Quaternion m_PreviousRotation;

    float linkDistance = 100.0f;
    bool m_IsGrounded;
    float m_DistanceToGround;

    [Tooltip("How large the turning angle of the player can be")]  
    public float m_TurningSpeed = 5.0f;


    // Use this for initialization
    void Start () {
        m_RigidBody = GetComponent<Rigidbody>();
        m_Collider = GetComponent<Collider>();
        m_CurrentPlanet = null;
        m_PlanetsAffecting = new List<Collider>();
        m_DistanceToGround = m_Collider.bounds.extents.y;
    }

    void ApplyGravity(RaycastHit hit1)
    {
        // Rotate the player
        // TODO: Make it only able to move a certain amount
        Vector3 rotationDirection = hit1.normal;
        if (Vector3.Angle(transform.up, hit1.normal) > 5.0f)
        {
            rotationDirection = Vector3.RotateTowards(transform.up, hit1.normal, m_TurningSpeed * Time.fixedDeltaTime, 1.0f);
        }


        m_PreviousRotation = Quaternion.LookRotation(transform.forward, rotationDirection);
        transform.rotation = m_PreviousRotation;

        // Apply Gravity
        float distSquared = (hit1.transform.position - transform.position).sqrMagnitude;
        float gravityForce = m_CurrentPlanet.CalculateGravitationalForce(
            m_RigidBody.mass, distSquared);

        m_RigidBody.AddForce(gravityForce * -rotationDirection);

        //Debug.Log(gravityForce * -rotationDirection);
        Debug.Log(distSquared);
    }

    void FixedUpdate()
    {
        if (InPlanetRange())
        {
            RaycastHit hit1;
            bool hitPlanet = Physics.Raycast(transform.position, -transform.up, 
                out hit1, linkDistance, LayerMask.GetMask("Default"), QueryTriggerInteraction.Ignore);
            
            // Apply gravity if it finds a planet
            if (hitPlanet)
            {
                ApplyGravity(hit1);
                Debug.DrawLine(transform.position, hit1.point);
            }

            // Shoot ray directed towards the center of the planet
            else if (Physics.Raycast(transform.position, (m_CurrentPlanet.transform.position - transform.position).normalized, 
                out hit1, linkDistance, LayerMask.GetMask("Default"), QueryTriggerInteraction.Ignore))
            {
                ApplyGravity(hit1);
                Debug.DrawLine(transform.position, hit1.point);
                Debug.Log("Backup Gravity");
            }
        }

        m_IsGrounded = FindIfGrounded();

        //Debug.Log(m_IsGrounded);
        //Debug.Log(m_RigidBody.velocity);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Planet")
        {
            if (!m_PlanetsAffecting.Contains(other))
            {
                m_PlanetsAffecting.Add(other);
                m_CurrentPlanet = other.GetComponent<PlanetGravityField>();
                Debug.Log(m_CurrentPlanet.name);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {      
        if (other.tag == "Planet")
        {
            if (m_PlanetsAffecting.Contains(other))
            {
                //Debug.Log("Left " + other.name);
                m_PlanetsAffecting.Remove(other);
            }
        }

        if (!InPlanetRange())
        {
            m_CurrentPlanet = null;
            Debug.Log("Drifting");
        }
    }

    bool InPlanetRange()
    {
        return (m_PlanetsAffecting.Count > 0);
    }

    public bool IsGrounded()
    {
        return m_IsGrounded;
    }

    bool FindIfGrounded()
    {
        // Inspiration from http://answers.unity3d.com/questions/196381/how-do-i-check-if-my-rigidbody-player-is-grounded.html
        return Physics.Raycast(transform.position, -transform.up, m_DistanceToGround + 0.1f);
    }

}
