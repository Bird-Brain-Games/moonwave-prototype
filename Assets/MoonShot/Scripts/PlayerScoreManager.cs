using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScoreManager : MonoBehaviour {

    PlayerStats[] m_PlayerStats;
	// Use this for initialization
	void Start () {
        m_PlayerStats = GetComponentsInChildren<PlayerStats>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
