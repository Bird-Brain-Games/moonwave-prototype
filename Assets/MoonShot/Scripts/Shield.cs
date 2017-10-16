using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{

    public enum BULLET_TYPE
    {
        plasma = 0,
        shotgun = 1
    }

    public int m_shieldHealth;
    public int mm_axShieldHealth;   // CLARIFY

    public float m_rechargeDelay;
    public int m_rechargeRatePerSecond;

    //[HideInInspector]
    public float m_timeSinceLastHit;
    //[HideInInspector]
    public float m_hitUpdate;

    float m_timeSinceLastRecharge;
    float m_rechargeUpdate;

    //triggers the recharge rate counter to start. set to false when the player is hit
    public bool m_canRecharge;

    // Use this for initialization
    void Start()
    {
        m_canRecharge = false;
        m_shieldHealth = mm_axShieldHealth;
    }

    //this is called whenever a player is hit and basically resets the shield recharge variables.
    public void ShieldHit(BULLET_TYPE bullet)
    {
        if (m_shieldHealth > 0)
        {
            switch (bullet)
            {
                case BULLET_TYPE.plasma:
                    m_shieldHealth -= 10;
                    break;
                case BULLET_TYPE.shotgun:
                    m_shieldHealth -= 5;
                    break;

            }


        }
        if (m_shieldHealth <= 0)
        {
            GetComponent<MeshRenderer>().enabled = false;
        }
        m_timeSinceLastHit = Time.time;
        m_hitUpdate = Time.time;
        m_canRecharge = false;
    }

    // Update is called once per frame
    void Update()
    {

        //a blanket if statement to reduce the times that this code will run. aka only if they are injured.
        if (mm_axShieldHealth > m_shieldHealth)
        {
            Debug.Log("shield updating");
            //controls whether enough time has passed for the shield to start recharging
            if (m_canRecharge == false)
            {
                //controls the timer updates and whether enough time has passed or not.
                if (m_hitUpdate - m_timeSinceLastHit < m_rechargeDelay)
                {
                    Debug.Log("hit update");
                    m_hitUpdate = Time.time;
                }
                else
                {
                    Debug.Log("shield can recharge");
                    m_canRecharge = true;
                    m_timeSinceLastRecharge = Time.time;
                }
            }
            //If the recharge delay has passed then the shield will start recharging.
            else
            {
                if (1.0 > m_rechargeUpdate - m_timeSinceLastRecharge)
                {
                    m_rechargeUpdate = Time.time;
                }
                else
                {
                    Debug.Log("shield recharge");
                    m_shieldHealth += m_rechargeRatePerSecond;
                    GetComponent<MeshRenderer>().enabled = true;
                    m_timeSinceLastRecharge = Time.time;
                }
            }
        }
    }
}
