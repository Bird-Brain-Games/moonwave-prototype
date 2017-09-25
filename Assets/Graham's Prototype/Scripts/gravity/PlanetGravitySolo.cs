using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGravitySolo : MonoBehaviour {

    public float m_GravityStrength;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay(Collider other)
    {
        // From https://forum.unity.com/threads/moving-around-a-sphere.131610/
        // Get the normalized direction vector
        Vector3 direction = transform.position - other.transform.position;
        Vector3 force = direction.normalized * m_GravityStrength;

        // Apply the force to the other object
        if (other.attachedRigidbody != null)
        {
            other.attachedRigidbody.AddForce(force, ForceMode.Acceleration);
        }
    }
}
