using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Primary coder campbell.
public class PlayerBoost : MonoBehaviour
{

    #region Variables

    //The amount of force a players action causes
    public float m_JumpForce;
    public float m_BoostForce;
    public float m_inertiaForce;
    public float m_boostForceChargePerSecond;
    float m_boostForceReset;
    public float m_MoveForce;

    //the amount of charges boost has (redudant)
    public int m_boostCharges;
    //the time it takes to recharge
    public float m_boostRecharge;

    //how long it can knockback for (rework)
    public float m_boostKnockbackDuration;

    //All of the componenets we need access to
    Rigidbody m_RigidBody;
    StickToPlanet m_Gravity;
    PlayerStats m_PlayerStats;
    Renderer m_Rend;
    Controls m_controls;
    boostCollision m_boostCollision;

    //the direction of analogue movement
    Vector2 m_move;
    //Whether the player has inputed the commands
    bool m_boost;
    bool m_charging;
    bool m_jump;

    //advancded direction variable
    Vector3 m_Direction;
    //The number of boosts the player has (semi redudant)
    int m_boostAmount;

    //cooldown variables for boosting
    float m_cooldownDuration;
    float m_cooldownStart;
    //how long the player has been holding boost for
    float m_boostChargedTime;

    // Logging  l_ is used to indicate a variable is for logging
    public int l_boosts;

    public Controls Controls
    {
        get
        {
            return m_controls;
        }

        set
        {
            m_controls = value;
        }
    }

    #endregion

    // Use this for initialization
    void Start()
    {
        //set all of the needed scripts
        m_RigidBody = GetComponent<Rigidbody>();
        m_Gravity = GetComponent<StickToPlanet>();
        m_PlayerStats = GetComponent<PlayerStats>();
        m_Rend = GetComponent<Renderer>();
        m_controls = GetComponent<Controls>();
        m_boostCollision = GetComponent<boostCollision>();
        //The number of boosts you have
        m_boostAmount = 1;
        l_boosts = 0;
        //Set player colour for boost reset.
        m_PlayerStats.colour = m_Rend.material.color;

        //So we can reset the boost default force.
        m_boostForceReset = m_BoostForce;
    }

    // Update is called once per frame
    void Update()
    {
        //Get input values
        m_move = m_controls.GetMove();
        m_boost = m_controls.GetBoost(BUTTON_DETECTION.GET_BUTTON_DOWN);
        m_jump = m_controls.GetJump(BUTTON_DETECTION.GET_BUTTON_DOWN);

        //set the players direction based off of the analog stick
        m_Direction = new Vector3(m_move.x, m_move.y, 0.0f);


        //If you are on a planet and you press jump
        if (m_jump && m_Gravity.IsGrounded())
        {
            //jump
            m_RigidBody.AddForce(m_JumpForce * transform.up, ForceMode.Impulse);
            m_PlayerStats.m_PlayerState = PlayerState.Drifting;
            //Debug.Log("Jump!");
        }

        // if we have no boosts available, start recharging
        if (m_boostAmount == 0)
        {
            //if the cooldown has just started
            if (m_cooldownDuration == 0)
            {
                m_Rend.material.color = m_PlayerStats.colourdull;
                m_cooldownDuration = Time.time;
                m_cooldownStart = Time.time;
                m_PlayerStats.SetBoostState(true);
            }

            //If the player boost knockback druation has ended.
            else if ((m_boostKnockbackDuration + m_boostChargedTime) < (m_cooldownDuration - m_cooldownStart) && m_PlayerStats.GetBoostState() == true)
            {

                m_PlayerStats.SetBoostState(false);
                m_cooldownDuration = Time.time;
                m_boostChargedTime = 0;
            }

            //If the boost has recharged.
            else if (m_boostRecharge < m_cooldownDuration - m_cooldownStart)
            {
                m_Rend.material.color = m_PlayerStats.colour;
                m_cooldownDuration = 0;
                m_cooldownStart = 0;
                m_boostAmount++;
            }
            //update timer
            else
            {
                m_cooldownDuration = Time.time;
            }
        }

        //if we have boost's available and we have pressed jump
        else if (m_boostAmount > 0)
        {
            //if we have pressed boost.
            if (m_boost)
            {
                Debug.Log("Boost pressed");
                m_charging = true;

            }

            //When we have released the boost
            else if (m_controls.GetBoost(BUTTON_DETECTION.GET_BUTTON_UP))
            {

                //set boost direction
                if (m_move.x == 0 && m_move.y == 0)
                {
                    //used if joystick isnt being pressed
                    m_Direction = new Vector3(m_RigidBody.velocity.x, m_RigidBody.velocity.y, 0.0f);
                    m_Direction.Normalize();
                }
                else
                    m_Direction = new Vector3(m_move.x, m_move.y, 0.0f);

                //Adding boost velocity.
                m_RigidBody.AddForce(m_BoostForce * m_Direction, ForceMode.Impulse);

                //reset and modify variables
                m_charging = false;
                m_boostAmount -= 1;
                m_BoostForce = m_boostForceReset;
                m_boostCollision.SetBoostDuration(m_boostChargedTime);

                Debug.Log("Boost fired!");

                //Log Boosts
                l_boosts++;
            }

            //When we are holding down the boost button.
            else if (m_charging)
            {
                Debug.Log("Boost charging");

                //increase boost force
                m_BoostForce += m_boostForceChargePerSecond * Time.deltaTime;
                //increase the time that the boost has charged for
                m_boostChargedTime += Time.deltaTime;

                //Setup Inertia canceling.
                Vector3 l_Inertia = (-1 * m_RigidBody.velocity);
                l_Inertia.Normalize();
                l_Inertia = l_Inertia *= m_inertiaForce;
                m_RigidBody.AddForce(l_Inertia, ForceMode.Impulse);

                //Intertia canceling
                if (m_move.x == 0 && m_move.y == 0)
                {
                    //used if joystick isnt being pressed
                    m_Direction = new Vector3(m_RigidBody.velocity.x, m_RigidBody.velocity.y, 0.0f);
                    m_Direction.Normalize();
                }
                else
                    m_Direction = new Vector3(m_move.x, m_move.y, 0.0f);

            }

        }



        //player basic movement
        if (!m_Gravity.IsGrounded())
        {
            //used to rotate player based on movement direciton
            //messes up gravity because gravity uses the players up vector.
            //float temp = Mathf.Atan2(m_move.y, m_move.x);
            //m_RigidBody.transform.localEulerAngles = new Vector3(0.0f, 0.0f, Mathf.Rad2Deg * temp);


            m_RigidBody.AddForce(m_MoveForce * m_Direction, ForceMode.Impulse);
        }

    }
}