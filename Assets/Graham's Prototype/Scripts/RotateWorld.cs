using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWorld : MonoBehaviour {

    public Transform m_Transform;

	// Use this for initialization
	void Start () {
	}
	
	void LateUpdate()
    {
        transform.LookAt(m_Transform);
    }
}
