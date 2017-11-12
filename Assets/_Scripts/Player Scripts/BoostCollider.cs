using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostCollider : MonoBehaviour
{

    // Use this for initialization
    PLayerBigHitState m_state;
    PLayerBigHitState m_tempState;
    PlayerStats m_stats;
    Transform m_parentTransform;
    BoxCollider m_BoxCollider;
    MeshRenderer m_MeshRender;

    public Vector3 Offset { get; set; }
    public float setOffset;
    public Quaternion Rotation { get; set; }
    void Start()
    {

        m_BoxCollider = GetComponent<BoxCollider>();
        m_MeshRender = GetComponent<MeshRenderer>();

    }
    public void PlayerLink(PlayerStats p_player)
    {
        m_stats = p_player;
        m_state = p_player.GetComponent<PLayerBigHitState>();
        m_parentTransform = p_player.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        Debug.Log("Boost fixed update");
        transform.position = m_parentTransform.position;
        transform.rotation = Quaternion.identity;
        transform.Translate(Offset * setOffset);
        transform.rotation = Rotation;
        transform.Rotate(new Vector3(0.0f, 0.0f, 90.0f));
    }

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("boost collider");
        if (collider.gameObject.tag == "Player")
        {
            Debug.Log("boost collider with player");

            if (collider.transform != m_parentTransform)
            {
                //basically switch to the playerBigHitState;
                Debug.Log("boost collider with player other than themselves");
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
}