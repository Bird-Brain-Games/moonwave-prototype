using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour {

	public ShotgunShot shotgunShot;
    Controls controls;
	PlayerStats m_PlayerStats;
	int m_PlayerNum;
	Vector3 aimDir;
	float currentCooldown;	// Cooldown until you can fire the shotgun again [Graham]
	float maxCooldown;
	float duration;	// How long the shotgun shot will exist [Graham]
	float force;
	
	// Use this for initialization
	void Start () {
		controls = GetComponent<Controls>();
		m_PlayerStats = GetComponent<PlayerStats>();

		maxCooldown = m_PlayerStats.m_Shoot.shotgunCooldown;
		duration = m_PlayerStats.m_Shoot.shotgunDuration;
		force = m_PlayerStats.m_Shoot.shotgunForce;
		m_PlayerNum = m_PlayerStats.m_PlayerID;
	}
	
	// Update is called once per frame
	void Update () {
		// Update the cooldown timer [Graham]
		if (currentCooldown > 0f)
		{
			currentCooldown -= Time.deltaTime;
			if (currentCooldown < 0f)
				currentCooldown = 0f;
		}
	}

	public void Shoot()
	{
		if (currentCooldown != 0f) return;	// Don't allow shoot if in cooldown [Graham]

		aimDir = controls.GetAim();

		ShotgunShot clone = Instantiate(shotgunShot, transform.position, Quaternion.Euler(aimDir));
		clone.gameObject.transform.Rotate(new Vector3(0f, 90f, -180f));

		clone.duration = duration;
		clone.playerNum = m_PlayerNum;
		clone.m_PlayerStats = m_PlayerStats;
		clone.direction = clone.gameObject.transform.rotation.eulerAngles;	// Not sure
		clone.force = force;

		// Reset the cooldown
		currentCooldown = maxCooldown;
	}
}
