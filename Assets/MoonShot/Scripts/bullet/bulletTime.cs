using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletTime : MonoBehaviour
{
    
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position.x < -60
            || transform.position.x > 60
            || transform.position.y > 34
            || transform.position.y < -34)
        {
            Debug.Log("bullet out of bounds");
            Debug.Log("bullet V: " + gameObject.GetComponent<Rigidbody>().velocity);
            Destroy(gameObject, 0.0f);
        }

    }
}

