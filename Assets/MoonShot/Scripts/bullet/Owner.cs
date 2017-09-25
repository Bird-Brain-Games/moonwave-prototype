using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Owner : MonoBehaviour {

    // Use this for initialization
    GameObject owner;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public GameObject getOwner() { return owner; }
    public void setOwner(GameObject s_owner) { owner = s_owner; }
}
