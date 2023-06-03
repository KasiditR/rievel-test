using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    private enum AttackType
    {
        AOE,
        Melee,
        ShootSkill
    }
    
    private AttackType currentAttack;
    
    public override void Enter()
    {
        Debug.Log("Entering Attack State");
        animator.SetBool("IsMoving", true);
        agent.SetDestination(player.position);
        // Choose a random attack type
        currentAttack = (AttackType)Random.Range(0, 3);
        
        // Trigger the corresponding animation based on the attack type
        switch (currentAttack)
        {
            case AttackType.AOE:
                //AOE
                // animator.SetTrigger("AttackAOE");
                fsm.SetState("AOE");
                break;
            case AttackType.Melee:
                //Melee
                // animator.SetTrigger("AttackMelee");
                fsm.SetState("Melee");
                break;
            case AttackType.ShootSkill:
                //ShootSkill
                // animator.SetTrigger("AttackShootSkill");
                fsm.SetState("ShootSkill");
                break;
        }
    }
    
    public override void Update()
    {
        // Logic for attack state
        
        // Example: If the player moves out of range, transition back to idle state
        // if (Vector3.Distance(agent.transform.position, player.position) > 15f)
        // {
        //     fsm.SetState("Idle");
        // }
    }
    
    public override void Exit()
    {
        animator.SetBool("IsMoving", false);
        Debug.Log("Exiting Attack State");
    }
}
