using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGravityField : MonoBehaviour {

    /*
     * The attributes and functions for a basic planet
     */

    public float m_GravityStrength;
    static float m_GravitationalConstant = 15;  // Used for the gravitational force equation

    float m_OrbitDistance;
    Collider m_GravityTrigger;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// Uses the gravitational force equation to calculate accurate gravity between planets
    /// </summary>
    /// <param name="objectMass"></param>
    /// <param name="distSquared"></param>
    /// <returns></returns>
    public float CalculateGravitationalForce(float objectMass, float distSquared)
    {
        return m_GravitationalConstant * ((m_GravityStrength * objectMass) / distSquared);
    }
}
