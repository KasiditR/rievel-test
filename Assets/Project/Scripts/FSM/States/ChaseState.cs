using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    public override void Enter()
    {
        textState.text = "ChaseState";
        Debug.Log("Entering Chase State");
    }
    
    public override void Update()
    {
        // Logic for chase state
        
        agent.SetDestination(player.position);
        //If the player is out of range, transition back to idle state
        if (Vector3.Distance(agent.transform.position, player.position) < 2)
        {
            fsm.SetState("Attack");
        }
    }
    
    public override void Exit()
    {
        Debug.Log("Exiting Chase State");
        // animator.SetBool("IsMoving", false); // Set the animator parameter for idle animation
        agent.ResetPath();
    }
}
