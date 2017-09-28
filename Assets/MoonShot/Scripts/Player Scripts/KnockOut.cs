﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockOut : MonoBehaviour {

 
    Rigidbody m_rigidBody;

    // Logging    l_ is used to indicate a variable is a logging variable
    int l_deaths;
	// Use this for initialization
	void Start () {
        m_rigidBody = gameObject.GetComponent<Rigidbody>();
        l_deaths = 0;
    }
	
	// Update is called once per frame
	void Update () {
        
        if (transform.position.x < -60
            || transform.position.x >  60
            || transform.position.y >  34
            || transform.position.y < -34)
        {
            Debug.Log("knockout");
            Debug.Log("bullet V: " + m_rigidBody.velocity);
            transform.position = new Vector3(9, 11, 0);
            m_rigidBody.velocity = new Vector3();

            // Logging
            l_deaths++;
        }

	}
}
