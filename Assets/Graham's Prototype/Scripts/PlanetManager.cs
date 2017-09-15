using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetManager : MonoBehaviour {

    public float m_GravitationalConstant;
    GameObject[] m_PlanetList;
    PlanetGravity[] m_GravityList;

	// Use this for initialization
	void Start () {
        m_PlanetList = GameObject.FindGameObjectsWithTag("Planet");

        if (m_PlanetList.Length == 0)   // If no planets were found
        {
            Debug.Log("Error: No planets exist in scene");
            return;
        }

        m_GravityList = new PlanetGravity[m_PlanetList.Length];
        for (int i = 0; i < m_PlanetList.Length; i++)
        {
            m_GravityList[i] = m_PlanetList[i].GetComponent<PlanetGravity>();
            if (m_GravityList[i] == null)
            {
                Debug.Log("Error: No gravity attached to planet " + m_PlanetList[i].name);
            }
        }
	}

    float CalculateGravitationalForce(float mPlanet, float mObject, float distSquared)
    {
        return m_GravitationalConstant * ((mPlanet * mObject) / distSquared);
    }

    public Vector3 GetGravity(Vector3 objectPos, float objectMass, out Vector3 closestPlanetDir, out Vector3 closestPlanetGravity)
    {
        Vector3 result, distance, direction, gravity;
        float distSquared, closestDistance;
        int numActingForces = 0;
        result = new Vector3();
        closestDistance = Mathf.Infinity;
        closestPlanetDir = new Vector3();
        closestPlanetGravity = new Vector3();
        gravity = new Vector3();

        // Find if the position is within the radius of a planet,
        // And calculate the gravity force based on all the planets it is close to.
        for (int i = 0; i < m_PlanetList.Length; i++)
        {
            distance = m_PlanetList[i].transform.position - objectPos;
            distSquared = distance.sqrMagnitude;
            
            if (distSquared <= m_GravityList[i].gravityRadius * m_GravityList[i].gravityRadius)
            {
                direction = distance.normalized;
                gravity = direction * CalculateGravitationalForce(
                    m_GravityList[i].gravityStrength, objectMass, distSquared); // Acting as if gravity strength is the mass
                result += gravity;
                numActingForces++;
                //Debug.Log(result);

                // Set the closest planet position
                if (closestDistance > distSquared)
                {
                    closestPlanetDir = direction;
                    closestPlanetGravity = gravity;
                    closestDistance = distSquared;
                }
            }
        }

        if (numActingForces > 0) result /= numActingForces;
        return result;
    }

    public Vector3 getConstantGravity(Vector3 objectPos, float objectMass)
    {
        Vector3 result, distance, direction;
        float distSquared;
        int numActingForces = 0;
        result = new Vector3();

        // Find if the position is within the radius of a planet,
        // And calculate the gravity force based on all the planets it is close to.
        for (int i = 0; i < m_PlanetList.Length; i++)
        {
            distance = m_PlanetList[i].transform.position - objectPos;
            distSquared = distance.sqrMagnitude;

            if (distSquared <= m_GravityList[i].gravityRadius * m_GravityList[i].gravityRadius)
            {
                direction = distance.normalized;
                result += direction * m_GravityList[i].gravityStrength;
                numActingForces++;
            }
        }

        if (numActingForces > 0) result /= numActingForces;
        return result;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
