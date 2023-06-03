using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    public override void Enter()
    {
        Debug.Log("Entering Chase State");
        // animator.SetBool("IsMoving", true); // Set the animator parameter for moving animation
        agent.SetDestination(player.position);
    }
    
    public override void Update()
    {
        // Logic for chase state
        
        // Example: If the player is out of range, transition back to idle state
        agent.SetDestination(player.position);
        if (Vector3.Distance(agent.transform.position, player.position) > 10)
        {
            fsm.SetState("Idle");
        }
    }
    
    public override void Exit()
    {
        Debug.Log("Exiting Chase State");
        // animator.SetBool("IsMoving", false); // Set the animator parameter for idle animation
        agent.ResetPath();
    }
}
