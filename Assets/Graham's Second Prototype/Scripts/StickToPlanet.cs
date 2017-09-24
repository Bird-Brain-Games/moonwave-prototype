﻿using System.Collections;
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
    PlayerStats m_PlayerStats;

    Vector3 m_CurrentGravityDirection;
    PlanetGravityField m_CurrentPlanet;
    List<Collider> m_PlanetsAffecting;
    Quaternion m_PreviousRotation;

    Collider m_PreviousPlanet;  // For use when multiple gravities are in effect (bad name, I know)
    float m_PreviousGravityFieldStrength;

    float linkDistance = 100.0f;
    bool m_IsGrounded;
    float m_DistanceToGround;

    [Tooltip("How large the turning angle of the player can be")]  
    public float m_TurningSpeed = 5.0f;

    // Use this for initialization
    void Start () {
        m_RigidBody = GetComponent<Rigidbody>();
        m_Collider = GetComponent<Collider>();
        m_PlayerStats = GetComponent<PlayerStats>();
        m_CurrentPlanet = null;
        m_PreviousPlanet = null;
        m_PlanetsAffecting = new List<Collider>();
        m_DistanceToGround = m_Collider.bounds.extents.y;
    }

    void ApplyGravity(RaycastHit hit1, float gravityForce)
    {
        // Rotate the player
        Vector3 rotationDirection = hit1.normal;
        if (Vector3.Angle(transform.up, hit1.normal) > 5.0f)
        {
            // Probably don't need the if check, the turning speed does it for us
            rotationDirection = Vector3.RotateTowards(transform.up, hit1.normal, m_TurningSpeed * Time.fixedDeltaTime, 1.0f);
        }
        m_PreviousRotation = Quaternion.LookRotation(transform.forward, rotationDirection);
        transform.rotation = m_PreviousRotation;

        // Apply Gravity
        if (m_PlayerStats.m_PlayerState == PlayerState.Drifting)
        {
            float distSquared = (hit1.transform.position - transform.position).sqrMagnitude;
            //gravityForce = m_CurrentPlanet.CalculateGravitationalForce(
            //m_RigidBody.mass, distSquared);
            m_RigidBody.AddForce(gravityForce * -rotationDirection);
        }
    }

    void FixedUpdate()
    {

        // Find if the player is grounded
        //if (m_PlayerStats.m_PlayerState == PlayerState.Drifting)
        //{
        //    m_IsGrounded = FindIfGrounded();
        //}
        
        //if (IsGrounded())
            //m_PlayerStats.m_PlayerState = PlayerState.Grounded;
        //else if (!IsGrounded() && m_PlayerStats.m_PlayerState == PlayerState.Grounded)
        //    m_PlayerStats.m_PlayerState = PlayerState.Drifting;

        Debug.Log(m_PlayerStats.m_PlayerState);

        if (PlanetInRange() && IsGrounded())
        {
            RaycastHit hit1;
            bool hitPlanet = Physics.Raycast(transform.position, -transform.up,
                out hit1, linkDistance, LayerMask.GetMask("Default"), QueryTriggerInteraction.Ignore);

            // Stick to planet if it finds a planet directly below the player
            if (hitPlanet)
            {
                if (hit1.distance > m_DistanceToGround)
                {
                    transform.position = transform.position + (-transform.up * (hit1.distance - m_DistanceToGround));
                }
                transform.rotation = Quaternion.LookRotation(transform.forward, hit1.normal);
                m_RigidBody.AddForce(m_CurrentPlanet.m_GravityStrength * -transform.up);
            }
            // Shoot ray directed towards the center of the planet
            else if (Physics.Raycast(transform.position, (m_CurrentPlanet.transform.position - transform.position).normalized,
                out hit1, linkDistance, LayerMask.GetMask("Default"), QueryTriggerInteraction.Ignore))
            {
                if (hit1.distance > m_DistanceToGround)
                {
                    transform.position = transform.position + (-transform.up * (hit1.distance - m_DistanceToGround));
                    transform.rotation = Quaternion.LookRotation(transform.forward, hit1.normal);
                    m_RigidBody.AddForce(m_CurrentPlanet.m_GravityStrength * -transform.up);
                }
            }
        }
        else if (PlanetInRange() && OnlyOnePlanetInRange())
        {
            // Try to hit directly below the player
            RaycastHit hit1;
            bool hitPlanet = Physics.Raycast(transform.position, -transform.up, 
                out hit1, linkDistance, LayerMask.GetMask("Default"), QueryTriggerInteraction.Ignore);
            
            // Apply gravity if it finds a planet directly below the player
            if (hitPlanet)
            {
                ApplyGravity(hit1, m_CurrentPlanet.m_GravityStrength);
                Debug.DrawLine(transform.position, hit1.point);
            }

            // Shoot ray directed towards the center of the planet
            else if (Physics.Raycast(transform.position, (m_CurrentPlanet.transform.position - transform.position).normalized, 
                out hit1, linkDistance, LayerMask.GetMask("Default"), QueryTriggerInteraction.Ignore))
            {
                ApplyGravity(hit1, m_CurrentPlanet.m_GravityStrength);
                Debug.DrawLine(transform.position, hit1.point);
                Debug.Log("Backup Gravity");
            }
        }
        else if (PlanetInRange())
        {
            RaycastHit hit;
            Vector3 gravityForce = Vector3.zero;

            foreach (Collider planet in m_PlanetsAffecting)
            {
                Vector3 diff = (planet.transform.position - transform.position);
                float distance = diff.sqrMagnitude;
                Vector3 direction = diff.normalized;
                Debug.DrawLine(transform.position, transform.position + direction * 2.0f);
                if (Physics.Raycast(transform.position, direction, out hit, 
                    linkDistance, LayerMask.GetMask("Default"), QueryTriggerInteraction.Ignore) &&
                    hit.transform.tag == "Planet")
                {
                    gravityForce += direction * planet.GetComponent<PlanetGravityField>().
                        CalculateGravitationalForce(m_RigidBody.mass, distance);
                }
            }

            // Rotate player towards the gravity
            Vector3 rotationDirection = Vector3.RotateTowards(transform.up, gravityForce.normalized, 100, 1.0f);
            Debug.DrawLine(transform.position, transform.position + gravityForce.normalized * 2.0f, Color.green);
            m_PreviousRotation = Quaternion.LookRotation(transform.forward, -gravityForce.normalized);
            transform.rotation = m_PreviousRotation;

            // Apply the gravity
            m_RigidBody.AddForce(gravityForce);
        }


    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Planet")
        {
            if (!m_PlanetsAffecting.Contains(other))
            {
                m_PlanetsAffecting.Add(other);
                m_CurrentPlanet = other.GetComponent<PlanetGravityField>();
                Debug.Log("Entering " + m_CurrentPlanet.name);

                // Want to smoothly rotate towards new planet, this will do for now
                //Vector3 planetDir = (transform.position - m_CurrentPlanet.transform.position).normalized;
                //Quaternion lookAtPlanet = Quaternion.LookRotation(transform.forward, planetDir);
                //transform.rotation = Quaternion.RotateTowards(transform.rotation, lookAtPlanet, 180.0f);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {      
        if (other.tag == "Planet")
        {
            if (m_PlanetsAffecting.Contains(other))
            {
                Debug.Log("Leaving " + other.name);
                m_PlanetsAffecting.Remove(other);
            }
        }

        if (!PlanetInRange())
        {
            m_CurrentPlanet = null;
            Debug.Log("Drifting");
        }
    }

    bool PlanetInRange()
    {
        return (m_PlanetsAffecting.Count > 0);
    }

    bool OnlyOnePlanetInRange()
    {
        return (m_PlanetsAffecting.Count == 1);
    }

    public bool IsGrounded()
    {
        return m_PlayerStats.m_PlayerState == PlayerState.Grounded;
    }

    bool FindIfGrounded()
    {
        // Inspiration from http://answers.unity3d.com/questions/196381/how-do-i-check-if-my-rigidbody-player-is-grounded.html
        RaycastHit hit;
        if (!Physics.Raycast(transform.position, -transform.up, out hit, m_DistanceToGround + 0.1f,
            LayerMask.GetMask("Default"), QueryTriggerInteraction.Ignore))
            return false;

        return (hit.transform.tag == "Planet");
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Planet")
        {
            if (m_PlayerStats.m_PlayerState == PlayerState.Drifting)
            {
                m_PlayerStats.m_PlayerState = PlayerState.Grounded;
            }
        }
    }

}
