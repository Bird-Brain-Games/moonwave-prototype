using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlStrings : MonoBehaviour
{

    public string append;

    string m_name;
    string m_jump;
    string m_shoot;
    string m_joystickH;
    string m_joystickV;
    string m_aimH;
    string m_aimV;

    // Use this for initialization
    void Start()
    {
        m_name = append +"name";
        m_jump = append +"jump";
        m_shoot = append +"shoot";
        m_joystickH = append +"joystickH";
        m_joystickV = append +"joystickV";
        m_aimH = append +"aimH";
        m_aimV = append + "aimV";
    }

    // Update is called once per frame
    void Update()
    {

    }
}
