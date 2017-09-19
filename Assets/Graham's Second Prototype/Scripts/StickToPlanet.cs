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
    bool isGrounded;

    [Tooltip("How large the turning angle of the player can be")]  
    public float m_TurningSpeed = 5.0f;


    // Use this for initialization
    void Start () {
        m_RigidBody = GetComponent<Rigidbody>();
        m_Collider = GetComponent<Collider>();
        m_CurrentPlanet = null;
        m_PlanetsAffecting = new List<Collider>();
	}
	
	// Update is called once per frame
	void Update () {
        // If it's inside a trigger, apply the stick to planet
        // That way you can jump and still stick to the planet
        // If it's outside of a trigger, then it should not apply gravity (Needs to be implemented)

        if (InPlanetRange())
        {
            // If there is more than one planet, shoot a ray towards each planet
            // And figure out which one is closer and orient towards that one?
            // That could be a lot of rays
            // For now, assume that there is only one planet
            // Why don't we assume that if it enters a trigger, then it sets that as the new planet?

            // Shoot a ray out of the foot of the character, 
            // Orient the player according to the normal they just casted to,
            // apply gravity to the down vector

            //RaycastHit hit1;
            //if (Physics.Raycast(transform.position, -transform.up, out hit1, linkDistance))
            //{
            //    // Rotate the player
            //    transform.rotation = Quaternion.FromToRotation(transform.up, hit1.normal) * transform.rotation;
            //}

            // Apply Gravy
            
            


            
        }
	}

    void ApplyGravity(RaycastHit hit1)
    {
        // Rotate the player
        // TODO: Make it only able to move a certain amount
        Vector3 rotationDirection = hit1.normal;
        if (Vector3.Angle(transform.up, hit1.normal) > 5.0f)
        {
            // Needs to be refined, doesn't do what it should yet
            rotationDirection = Vector3.RotateTowards(transform.up, hit1.normal, m_TurningSpeed * Time.fixedDeltaTime, 1.0f);

        }


        m_PreviousRotation = Quaternion.LookRotation(transform.forward, rotationDirection);
        transform.rotation = m_PreviousRotation;



        // If the distance is small enough, consider grounded (TBI)

        // Apply Gravy
        m_RigidBody.AddForce(-transform.up * m_CurrentPlanet.m_GravityStrength, ForceMode.Force);
        //m_RigidBody.tor
    }

    void FixedUpdate()
    {
        if (InPlanetRange())
        {
            RaycastHit hit1;
            bool hitPlanet = Physics.Raycast(transform.position, -transform.up, out hit1, linkDistance, LayerMask.GetMask("Default"), QueryTriggerInteraction.Ignore);
            

            if (hitPlanet)
            {
                ApplyGravity(hit1);
                Debug.DrawLine(transform.position, hit1.point);
            }
            // Shoot ray directed towards the center of the planet
            else if (Physics.Raycast(transform.position, (m_CurrentPlanet.transform.position - transform.position).normalized, out hit1, linkDistance, LayerMask.GetMask("Default"), QueryTriggerInteraction.Ignore))
            {
                ApplyGravity(hit1);
                Debug.DrawLine(transform.position, hit1.point);
                Debug.Log("Backup Gravity");

                // Shoot out two other rays, at slight angles from the target. If either of these hit, do the calculations
                // Put the calculations in a function

                // 
                //m_RigidBody.AddForce(
                //    Vector3.Normalize(m_CurrentPlanet.transform.position - transform.position)
                //    * m_CurrentPlanet.m_GravityStrength, ForceMode.Force);
            }
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
            //Debug.Log("Drifting");
        }
    }

    bool InPlanetRange()
    {
        return (m_PlanetsAffecting.Count > 0);
    }
}
