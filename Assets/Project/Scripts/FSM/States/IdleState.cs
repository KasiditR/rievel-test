using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public override void Enter()
    {
        Debug.Log("Entering Idle State");
        // animator.SetBool("IsMoving", false); // Set the animator parameter for idle animation
    }
    
    public override void Update()
    {
        // Logic for idle state
        
        // Example: If the player is within a certain range, transition to chase state
        if (Vector3.Distance(agent.transform.position, player.position) < 10f)
        {
            fsm.SetState("Attack");
        }
    }
    
    public override void Exit()
    {
        Debug.Log("Exiting Idle State");
    }
}
