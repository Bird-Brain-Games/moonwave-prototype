using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletTime : MonoBehaviour
{
    
    // Use this for initialization
    void Start()
    {

    }

    /// <summary>
    /// Destroys the bullet and logs
    /// </summary>
    public void BulletOutOfBounds()
    {
            Debug.Log("bullet out of bounds");
            Debug.Log("bullet V: " + gameObject.GetComponent<Rigidbody>().velocity);
            Destroy(gameObject, 0.0f); 
    }
}

