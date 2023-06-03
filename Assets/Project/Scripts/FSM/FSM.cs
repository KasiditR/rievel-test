using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM
{
    private State currentState;
    private Dictionary<string, State> states = new Dictionary<string, State>();
    
    public void AddState(string name, State state)
    {
        states[name] = state;
    }
    
    public void SetState(string name)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }
        
        if (states.TryGetValue(name, out State newState))
        {
            currentState = newState;
            currentState.Enter();
        }
        else
        {
            Debug.LogWarning("State " + name + " does not exist in the FSM.");
        }
    }
    public Dictionary<string, State> GetStates()
    {
        return states;
    }
    
    public void Update()
    {
        if (currentState != null)
        {
            currentState.Update();
        }
    }
}
