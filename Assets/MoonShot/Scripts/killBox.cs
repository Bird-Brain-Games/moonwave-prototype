using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killBox : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        // When player leaves kill box, call player's knockout funciton
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<KnockOut>().PlayerKnockedOut();
        }

        // When the bullet leaves the kill box, call the bullet OutOfBounds function
        else if (other.gameObject.CompareTag("Bullet"))
        {
            other.gameObject.GetComponent<bulletTime>().BulletOutOfBounds();
        }

    }

}
