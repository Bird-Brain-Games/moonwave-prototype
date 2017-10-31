using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    // Keeping track of the next state to change to
    bool changingState;
    string nextState;

    StateManager stateManager;
    // Use this for initialization
    public State()
    {
        Debug.Log("Base State created");
        stateManager = GetComponent<StateManager>();
    }

#region Virtual States
    
    public virtual void StateUpdate()
    {

    }

    public virtual void StateFixedUpdate()
    {

    }

    public virtual void StateLateUpdate()
    {

    }

    public virtual void Enter()
    {

    }

    public virtual void Exit()
    {

    }

    public virtual StateManager GetManager()
    {
        return stateManager;
    }
#endregion

    public void ChangeStateUpdate()
    {
        if (changingState)
        {
            stateManager.ChangeState(nextState);
            changingState = false;
        }
        
    }
    
    public void ChangeState(string a_State)
    {
        nextState = a_State;
        changingState = true;
    }

}