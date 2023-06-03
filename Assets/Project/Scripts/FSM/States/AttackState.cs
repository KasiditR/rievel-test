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
        textState.text = "AttackState";
        Debug.Log("Entering Attack State");
        agent.SetDestination(player.position);
        // Choose a random attack type
        currentAttack = (AttackType)Random.Range(0, 3);
        
        // Trigger the corresponding animation based on the attack type
        switch (currentAttack)
        {
            case AttackType.AOE:
                //AOE
                textState.text += " => AOE";
                fsm.SetState("AOE");
                break;
            case AttackType.Melee:
                //Melee
                textState.text += " => Melee";
                fsm.SetState("Melee");
                break;
            case AttackType.ShootSkill:
                //ShootSkill
                textState.text += " => ShootSkill";
                fsm.SetState("ShootSkill");
                break;
        }
    }
    
    public override void Update()
    {
        
    }
    
    public override void Exit()
    {
        Debug.Log("Exiting Attack State");
    }
}
