using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Owner : MonoBehaviour {

    // Use this for initialization
    GameObject owner;
    Vector3 velocity;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public GameObject getOwner() { return owner; }
    public void setOwner(GameObject s_owner) { owner = s_owner; }

    public void setVelocity(Vector3 s_velocity) { velocity = s_velocity; }
    public Vector3 getVelocity() { return velocity; }
}
