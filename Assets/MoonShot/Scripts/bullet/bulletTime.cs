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
    public void BulletOutOfBounds()
    {
            Debug.Log("bullet out of bounds");
            Destroy(gameObject, 0.0f); 
    }
}

