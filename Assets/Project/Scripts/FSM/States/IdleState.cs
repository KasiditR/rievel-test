using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    private MonsterCharacter monsterCharacter;
    public override void Enter()
    {
        textState.text = "IdleState";
        Debug.Log("Entering Idle State");
        monsterCharacter = agent.GetComponent<MonsterCharacter>();
        // animator.SetBool("IsMoving", false); // Set the animator parameter for idle animation
    }
    
    public override void Update()
    {
        // Logic for idle state
        if (monsterCharacter.health <= 20)
        {
            fsm.SetState("Attack");
        }
        // Example: If the player is within a certain range, transition to chase state
        if (Vector3.Distance(agent.transform.position, player.position) < 4)
        {
            // fsm.SetState("Attack");
            fsm.SetState("Chase");
        }
    }
    
    public override void Exit()
    {
        Debug.Log("Exiting Idle State");
    }
}
