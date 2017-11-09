using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostCollider : MonoBehaviour {

    // Use this for initialization
    PLayerBigHitState m_state;
    PLayerBigHitState m_tempState;
    PlayerStats m_stats;
    BoxCollider m_BoxCollider;
    MeshRenderer m_MeshRender;
	void Start () {
		        m_state = GetComponentInParent<PLayerBigHitState>();
        m_stats = GetComponentInParent<PlayerStats>();
	}
	
	// Update is called once per frame
	void Update () {
        m_BoxCollider = GetComponent<BoxCollider>();
        m_MeshRender = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("boost collider");
        if (collider.gameObject.tag == "Player")
        {
            Debug.Log("boost collider with player");
            //basically switch to the playerBigHitState;

            //Set hitlastby
            collider.gameObject.GetComponent<PlayerStats>().m_HitLastBy = m_stats;

            if (collider.transform.GetComponent<PlayerStats>().GetShieldState() == false)
            {
                Debug.Log("boost collider without shield");

                

                //get the colliders BigHitState
                m_tempState = collider.transform.GetComponentInParent<PLayerBigHitState>();

                //calculate the force acting on the hit player
                var Force = collider.transform.position - m_stats.transform.position;
                m_state.Direction = Force;
                m_tempState.Direction = -Force;
                Force.Normalize();

                //Adds the force to the player we collided with
                Force = (Force
                    * m_stats.m_boost.MaxForce
                    * m_stats.GetCriticalMultiplier()
                    * 50);

                collider.GetComponent<StateManager>().ChangeState(m_stats.PlayerBigHitState);


                m_tempState.Force = Force;

                m_state.GetComponent<StateManager>().ChangeState(m_stats.PlayerBigHitState);
                m_state.isTarget = false;

                m_BoxCollider.enabled = true;
                m_MeshRender.enabled = true;
            }
        }
    }
}
