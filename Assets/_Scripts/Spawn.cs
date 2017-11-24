using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {

    Spawner_Available[] spawners;
    Vector3[] spawnPoints;
	// Use this for initialization
	void Start () {
        spawners = GetComponentsInChildren<Spawner_Available>();
        spawnPoints = new Vector3[spawners.Length];
        for (int i = 0; i < spawners.Length; i++)
        {
            spawnPoints[i] = spawners[i].GetComponent<Transform>().position;
        }
    }

    public Vector3 getSpawnPoint()
    {
        int leastCollisions = 0;
        int spot = 0;
        for (int i = 0; i < spawners.Length; i++)
        {
            int tempColliders = spawners[i].GetNumColliders();
            if (tempColliders == 0)
            {
                return spawnPoints[i];
            }
            else if (tempColliders < leastCollisions)
            {
                spot = i;
                leastCollisions = tempColliders;
            }
        }
        return spawnPoints[spot];
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
