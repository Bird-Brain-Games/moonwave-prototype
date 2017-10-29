using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{

    bool m_isON;
    StateManager stateManager;
    // Use this for initialization
    public State(GameObject a_accesser)
    {
        Debug.Log("base created");
        m_isON = false;
        stateManager = a_accesser.GetComponent<StateManager>();
    }


    public virtual void StateUpdate()
    {
        Debug.Log("base class updater");
    }
    public virtual void SetIsOn(bool a_paused)
    {
        m_isON = a_paused;
    }
    public bool GetIsOn()
    {
        return m_isON;
    }
    public virtual StateManager GetManager()
    {
        return stateManager;
    }

    public void SetOtherStateIsOn(string a_name, bool a_pause)
    {
        State temp = stateManager.GetStates(a_name);
        if (temp != null)
        {
            Debug.Log(a_name + " Has been set to " + a_pause);
            temp.SetIsOn(a_pause);
        }
        else
        {
            Debug.Log("State does not exist");
        }
    }

}